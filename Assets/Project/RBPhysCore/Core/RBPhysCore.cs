using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEditor;
using Unity.IL2CPP.CompilerServices;

namespace RBPhys
{
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public static partial class RBPhysCore
    {
        public const int CPU_STD_SOLVER_MAX_ITERATION = 3;
        public const int CPU_STD_SOLVER_INTERNAL_SYNC_PER_ITERATION = 2;
        public const float CPU_SOLVER_ABORT_VELADD_SQRT = 0.01f * 0.01f;
        public const float CPU_SOLVER_ABORT_ANGVELADD_SQRT = 0.05f * 0.05f;

        public static int cpu_std_solver_max_iter = CPU_STD_SOLVER_MAX_ITERATION;
        public static int cpu_std_solver_internal_sync_per_iteration = CPU_STD_SOLVER_INTERNAL_SYNC_PER_ITERATION;
        public static float cpu_solver_abort_veladd_sqrt = CPU_SOLVER_ABORT_VELADD_SQRT;
        public static float cpu_solver_abort_angveladd_sqrt = CPU_SOLVER_ABORT_ANGVELADD_SQRT;

        static List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        static List<RBCollider> _colliders = new List<RBCollider>();

        static RBTrajectory[] _trajectories_orderByXMin = new RBTrajectory[0];
        static float[] _trajectories_xMin = new float[0];

        static List<RBRigidbody> _rbAddQueue = new List<RBRigidbody>();
        static List<RBRigidbody> _rbRemoveQueue = new List<RBRigidbody>();
        static List<RBCollider> _colAddQueue = new List<RBCollider>();
        static List<RBCollider> _colRemoveQueue = new List<RBCollider>();
        static List<RBCollision> _collisions = new List<RBCollision>();
        static List<RBCollision> _collisionsInSolver = new List<RBCollision>();

        static List<RBConstraints.IRBPhysObject> _physObjects = new List<RBConstraints.IRBPhysObject>();
        static List<RBConstraints.IStdSolver> _stdSolversAsync = new List<RBConstraints.IStdSolver>();

        static int[] _collisionIgnoreLayers = new int[32];

        public static void AddRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Add(rb);
            _rbAddQueue.Add(rb);
            _rbRemoveQueue.Remove(rb);
        }

        public static void RemoveRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Remove(rb);
            _rbAddQueue.Remove(rb);
            _rbRemoveQueue.Add(rb);
        }

        public static void AddCollider(RBCollider c)
        {
            _colliders.Add(c);
            _colAddQueue.Add(c);
            _colRemoveQueue.Remove(c);
        }

        public static void RemoveCollider(RBCollider c)
        {
            _colliders.Remove(c);
            _colAddQueue.Remove(c);
            _colRemoveQueue.Add(c);
        }

        public static void SwitchToCollider(RBCollider c)
        {
            _colAddQueue.Add(c);
            _colRemoveQueue.Remove(c);
        }

        public static void SwitchToRigidbody(RBCollider c)
        {
            _colAddQueue.Remove(c);
            _colRemoveQueue.Add(c);
        }

        public static void AddStdSolver(RBConstraints.IStdSolver solver)
        {
            if (!_stdSolversAsync.Contains(solver))
            {
                _stdSolversAsync.Add(solver);
            }
        }

        public static void RemoveStdSolver(RBConstraints.IStdSolver solver)
        {
            _stdSolversAsync.Remove(solver);
        }

        public static void AddPhysObject(RBConstraints.IRBPhysObject physObj, bool asyncIteration = true)
        {
            if (!_physObjects.Contains(physObj))
            {
                _physObjects.Add(physObj);
            }
        }

        public static void RemovePriorSolver(RBConstraints.IRBPhysObject physObj)
        {
            _physObjects.Remove(physObj);
        }

        public static void SetCollisionOption(int layer_a, int layer_b, RBCollisionOption option)
        {
            switch (option)
            {
                case RBCollisionOption.Ignore:
                    _collisionIgnoreLayers[layer_a] |= (1 << layer_b);
                    _collisionIgnoreLayers[layer_b] |= (1 << layer_a);
                    break;

                case RBCollisionOption.Both:
                    _collisionIgnoreLayers[layer_a] &= ~(1 << layer_b);
                    _collisionIgnoreLayers[layer_b] &= ~(1 << layer_a);
                    break;
            }
        }

        public static void OpenPhysicsFrameWindow(float dt)
        {
            UpdateTransforms();
            UpdateExtTrajectories(dt);
            SortTrajectories();

            foreach (var p in _physObjects)
            {
                p.BeforeSolver();
            }

            SolveConstraints(dt);

            foreach (var p in _physObjects)
            {
                p.AfterSolver();
            }

            foreach (RBRigidbody rb in _rigidbodies)
            {
                if (!rb.isSleeping)
                {
                    rb.ExpVelocity += new Vector3(0, -9.81f, 0) * dt;
                }
            }
            
            TrySleepRigidbodies();
            TryAwakeRigidbodies();

            //OnClosePhysicsFrame��
        }

        public static void ClosePhysicsFrameWindow(float dt)
        {
            //FixedUpdate�I�����Ɏ��s

            // ====== �����t���[���E�C���h�E �����܂� ======

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplyTransform(dt);
            }
        }

        static void TrySleepRigidbodies()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodySleepTest");
            Parallel.ForEach(_rigidbodies, rb =>
            {
                rb.UpdatePhysSleepGrace();
            });

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.TryPhysSleep();
            }
            Profiler.EndSample();
        }

        static void TryAwakeRigidbodies()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodyAwakeTest");
            foreach (RBRigidbody rb in _rigidbodies)
            {
                if (rb.isSleeping)
                {
                    for(int i = 0; i < rb.collidingCount; i++)
                    {
                        var c = rb.colliding[i];
                        if (c.ParentRigidbody != null && ((c.ParentRigidbody.isActiveAndEnabled && !c.ParentRigidbody.isSleeping) || !c.ParentRigidbody.isActiveAndEnabled))
                        {
                            rb.PhysAwake();
                            break;
                        }
                    }
                }
            }
            Profiler.EndSample();
        }

        static void UpdateTransforms()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbody");

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform();
            }

            ClearCollisions();

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateCollider");
            foreach (RBCollider c in _colliders)
            {
                if (c.ParentRigidbody == null)
                {
                    c.UpdateTransform();
                }
            }
            Profiler.EndSample();
        }

        static void ClearCollisions()
        {
            Parallel.ForEach(_rigidbodies, rb =>
            {
                if (!rb.isSleeping)
                {
                    ClearCollision(rb);
                }
            });
        }

        static void ClearCollision(RBRigidbody rb)
        {
            for (int i = 0; i < rb.collidingCount; i++)
            {
                rb.colliding[i] = null;
            }

            if (Mathf.Max(2, rb.collidingCount) != rb.colliding.Length)
            {
                Array.Resize(ref rb.colliding, rb.collidingCount);
            }
            rb.collidingCount = 0;
        }

        //こいつを並列化するとなんか遅くなるし物理挙動が乱れるので触らない
        static void UpdateColliderExtTrajectories(float dt)
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbodyTrajectory");
            Parallel.ForEach(_rigidbodies, rb =>
            {
                rb.UpdateColliderExpTrajectory(dt);
            });
            Profiler.EndSample();
        }

        static void UpdateExtTrajectories(float dt, bool updateColliders = true)
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateExpTrajectory(dt, updateColliders);
            }
        }

        static void SortTrajectories()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-Sort");

            int count = _trajectories_orderByXMin.Length + _rbAddQueue.Count + _colAddQueue.Count;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                _trajectories_xMin[i] = _trajectories_orderByXMin[i].trajectoryAABB.MinX;
            }

            if (_rbAddQueue.Any() || _colAddQueue.Any())
            {
                int offset = _trajectories_orderByXMin.Length;

                Array.Resize(ref _trajectories_orderByXMin, count);
                Array.Resize(ref _trajectories_xMin, count);

                for (int i = 0; i < _rbAddQueue.Count; i++)
                {
                    int p = offset + i;
                    _trajectories_xMin[p] = _rbAddQueue[i].ExpObjectTrajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _rbAddQueue[i].ExpObjectTrajectory;
                }

                for (int i = 0; i < _colAddQueue.Count; i++)
                {
                    int p = offset + _rbAddQueue.Count + i;
                    _trajectories_xMin[p] = _colAddQueue[i].ExpTrajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _colAddQueue[i].ExpTrajectory;
                }

                _rbAddQueue.Clear();
                _colAddQueue.Clear();
            }

            if (_rbRemoveQueue.Any() || _colRemoveQueue.Any())
            {
                int rmvOffset_min = 0;

                //削除（間をつめる）
                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    bool isStatic = _trajectories_orderByXMin[i].IsStatic;

                    if (rmvOffset_min > 0)
                    {
                        _trajectories_orderByXMin[i - rmvOffset_min] = _trajectories_orderByXMin[i];
                        _trajectories_xMin[i - rmvOffset_min] = _trajectories_xMin[i];
                    }

                    if ((isStatic && _colRemoveQueue.Contains(_trajectories_orderByXMin[i].Collider)) || (!isStatic && _rbRemoveQueue.Contains(_trajectories_orderByXMin[i].Rigidbody)))
                    {
                        rmvOffset_min++;
                    }
                }

                Array.Resize(ref _trajectories_orderByXMin, _trajectories_orderByXMin.Length - rmvOffset_min);
                Array.Resize(ref _trajectories_xMin, _trajectories_xMin.Length - rmvOffset_min);

                _rbRemoveQueue.Clear();
                _colRemoveQueue.Clear();
            }

            //挿入ソート
            for (int i = 1; i < _trajectories_orderByXMin.Length; i++)
            {
                float a = _trajectories_xMin[i];
                var aTraj = _trajectories_orderByXMin[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    float p = _trajectories_xMin[j];
                    if (a < p)
                    {
                        _trajectories_xMin[j + 1] = p;
                        _trajectories_orderByXMin[j + 1] = _trajectories_orderByXMin[j];
                    }
                    else
                    {
                        _trajectories_xMin[j + 1] = a;
                        _trajectories_orderByXMin[j + 1] = aTraj;
                        break;
                    }

                    _trajectories_xMin[j] = a;
                    _trajectories_orderByXMin[j] = aTraj;
                }
            }

            Profiler.EndSample();
        }

        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _obb_obb_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();
        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _obb_sphere_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();
        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _sphere_sphere_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();
        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _obb_capsule_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();
        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _sphere_capsule_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();
        static List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _capsule_capsule_cols = new List<(RBCollider, RBCollider, RBDetailCollision.Penetration p, RBCollision col)>();

        public static void SolveConstraints(float dt)
        {
            //�Փˌ��m�i�u���[�h�t�F�[�Y�j

            List<(RBTrajectory, RBTrajectory)> collidingTrajs = new List<(RBTrajectory, RBTrajectory)>();

            Profiler.BeginSample(name: "Physics-CollisionResolution-TrajectoryAABBTest");
            {
                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    RBTrajectory activeTraj = _trajectories_orderByXMin[i];

                    if (activeTraj.IsValidTrajectory)
                    {
                        float x_min = activeTraj.trajectoryAABB.MinX;
                        float x_max = activeTraj.trajectoryAABB.MaxX;

                        for (int j = i + 1; j < _trajectories_orderByXMin.Length; j++)
                        {
                            RBTrajectory targetTraj = _trajectories_orderByXMin[j];

                            if ((_collisionIgnoreLayers[activeTraj.Layer] & (1 << targetTraj.Layer)) == 0)
                            {
                                if (!activeTraj.IsStaticOrSleeping || !targetTraj.IsStaticOrSleeping)
                                {
                                    if (targetTraj.IsValidTrajectory)
                                    {
                                        float x_min_target = targetTraj.trajectoryAABB.MinX;
                                        float x_max_target = targetTraj.trajectoryAABB.MaxX;

                                        if (x_max < x_min_target)
                                        {
                                            break;
                                        }

                                        if (activeTraj.trajectoryAABB.OverlapAABB(targetTraj.trajectoryAABB))
                                        {
                                            collidingTrajs.Add((activeTraj, targetTraj));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Profiler.EndSample();

            //�Փˌ��m�i�i���[�t�F�[�Y�j�Ɖ��

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareDetailTest");

            _obb_obb_cols.Clear();
            _obb_sphere_cols.Clear();
            _sphere_sphere_cols.Clear();
            _obb_capsule_cols.Clear();
            _sphere_capsule_cols.Clear();
            _capsule_capsule_cols.Clear();

            foreach (var trajPair in collidingTrajs)
            {
                DetectCollisions(trajPair.Item1, trajPair.Item2, ref _obb_obb_cols, ref _obb_sphere_cols, ref _sphere_sphere_cols, ref _obb_capsule_cols, ref _sphere_capsule_cols, ref _capsule_capsule_cols);
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-DetailTest");

            Parallel.For(0, _obb_obb_cols.Count, i =>
            {
                var colPair = _obb_obb_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpOBB());
                _obb_obb_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Parallel.For(0, _obb_sphere_cols.Count, i =>
            {
                var colPair = _obb_sphere_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpSphere());
                _obb_sphere_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Parallel.For(0, _sphere_sphere_cols.Count, i =>
            {
                var colPair = _sphere_sphere_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollisionInfo(colPair.col_a.CalcExpSphere(), colPair.col_b.CalcExpSphere());
                _sphere_sphere_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Parallel.For(0, _obb_capsule_cols.Count, i =>
            {
                var colPair = _obb_capsule_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpCapsule());
                _obb_capsule_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Parallel.For(0, _sphere_capsule_cols.Count, i =>
            {
                var colPair = _sphere_capsule_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpSphere(), colPair.col_b.CalcExpCapsule());
                _sphere_capsule_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Parallel.For(0, _capsule_capsule_cols.Count, i =>
            {
                var colPair = _capsule_capsule_cols[i];
                RBDetailCollision.Penetration p = RBDetailCollision.DetailCollisionCapsuleCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpCapsule(), colPair.col_b.CalcExpCapsule());
                _capsule_capsule_cols[i] = (colPair.col_a, colPair.col_b, p, null);
            });

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareSolveCollisions");

            Parallel.For(0, _obb_obb_cols.Count, i =>
            {
                var pair = _obb_obb_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _obb_obb_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _obb_obb_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }

            Parallel.For(0, _obb_sphere_cols.Count, i =>
            {
                var pair = _obb_sphere_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _obb_sphere_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _obb_sphere_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }

            Parallel.For(0, _sphere_sphere_cols.Count, i =>
            {
                var pair = _sphere_sphere_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _sphere_sphere_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _sphere_sphere_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }

            Parallel.For(0, _obb_capsule_cols.Count, i =>
            {
                var pair = _obb_capsule_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _obb_capsule_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _obb_capsule_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }

            Parallel.For(0, _sphere_capsule_cols.Count, i =>
            {
                var pair = _sphere_capsule_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _sphere_capsule_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _sphere_capsule_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }

            Parallel.For(0, _capsule_capsule_cols.Count, i =>
            {
                var pair = _capsule_capsule_cols[i];

                if (pair.p.p != Vector3.zero)
                {
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, pair.p.p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(pair.p.p);
                    var pA = pair.col_a.ExpToCurrent(pair.p.pA);
                    var pB = pair.col_b.ExpToCurrent(pair.p.pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = pair.p.info;

                    _capsule_capsule_cols[i] = (default, default, default, rbc);
                }
            });

            foreach (var c in _capsule_capsule_cols)
            {
                if (c.col != null)
                {
                    _collisionsInSolver.Add(c.col);
                }
            }
            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodyPrepareSolve");

            Parallel.For(0, _stdSolversAsync.Count, j =>
            {
                _stdSolversAsync[j].StdSolverInit(dt, true);
            });

            foreach (RBCollision col in _collisionsInSolver)
            {
                if (col.rigidbody_a != null)
                {
                    if (col.rigidbody_a.isSleeping)
                    {
                        ClearCollision(col.rigidbody_a);
                    }
                    col.rigidbody_a.PhysAwake();
                    AddCollision(col.rigidbody_a, col.collider_b);
                }

                if (col.rigidbody_b != null)
                {
                    if (col.rigidbody_b.isSleeping)
                    {
                        ClearCollision(col.rigidbody_b);
                    }
                    col.rigidbody_b.PhysAwake();
                    AddCollision(col.rigidbody_b, col.collider_a);
                }
            }
            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-SolveCollisions/StdSolver");
            for (int iter = 0; iter < cpu_std_solver_max_iter; iter++)
            {
                for (int i = 0; i < cpu_std_solver_internal_sync_per_iteration; i++)
                {
                    Profiler.BeginSample(name: "SolveConstraints");
                    int p = _stdSolversAsync.Count;
                    Parallel.For(0, p + _collisionsInSolver.Count, i =>
                    {
                        if (i < p)
                        {
                            _stdSolversAsync[i].StdSolverIteration(iter);
                        }
                        else
                        {
                            SolveCollisionPair(_collisionsInSolver[i - p]);
                        }
                    });
                    Profiler.EndSample();
                }

                if (iter != cpu_std_solver_max_iter - 1)
                {
                    Profiler.BeginSample(name: "UpdateTrajectories");

                    UpdateColliderExtTrajectories(dt);

                    Parallel.For(0, _collisionsInSolver.Count, j =>
                    {
                        UpdateTrajectoryPair(_collisionsInSolver[j], dt);
                    });

                    Parallel.ForEach(_stdSolversAsync, s =>
                    {
                        s.StdSolverInit(dt, false);
                    });

                    Profiler.EndSample();
                }
            }

            Profiler.EndSample();

            _collisions.Clear();
            _collisions.AddRange(_collisionsInSolver);
            _collisionsInSolver.Clear();
        }

        static bool RecalculateCollision(RBCollider col_a, RBCollider col_b, RBDetailCollision.DetailCollisionInfo info, out Vector3 p, out Vector3 pA, out Vector3 pB)
        {
            bool aabbCollide = col_a.ExpTrajectory.trajectoryAABB.OverlapAABB(col_b.ExpTrajectory.trajectoryAABB);

            if (aabbCollide)
            {
                if (col_a.GeometryType == RBGeometryType.OBB && col_b.GeometryType == RBGeometryType.OBB)
                {
                    //OBB-OBB衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollisionLighter(col_a.CalcExpOBB(), col_b.CalcExpOBB(), info);
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.OBB && col_b.GeometryType == RBGeometryType.Sphere)
                {
                    //Sphere-OBB衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollision(col_a.CalcExpOBB(), col_b.CalcExpSphere());
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Sphere && col_b.GeometryType == RBGeometryType.OBB)
                {
                    //Sphere-OBB衝突（逆転）
                    (p, pB, pA) = RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollision(col_b.CalcExpOBB(), col_a.CalcExpSphere());
                    p = -p;
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Sphere && col_b.GeometryType == RBGeometryType.Sphere)
                {
                    //Sphere-Sphere衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollision(col_a.CalcExpSphere(), col_b.CalcExpSphere());
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.OBB && col_b.GeometryType == RBGeometryType.Capsule)
                {
                    //OBB-Capsule衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollision(col_a.CalcExpOBB(), col_b.CalcExpCapsule());
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Capsule && col_b.GeometryType == RBGeometryType.OBB)
                {
                    //OBB-Capsule衝突（逆転）
                    (p, pB, pA) = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollision(col_b.CalcExpOBB(), col_a.CalcExpCapsule());
                    p = -p;
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Sphere && col_b.GeometryType == RBGeometryType.Capsule)
                {
                    //Sphere-Capsule衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollision(col_a.CalcExpSphere(), col_b.CalcExpCapsule());
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Capsule && col_b.GeometryType == RBGeometryType.Sphere)
                {
                    //Sphere-Capsule衝突（逆転）
                    (p, pB, pA) = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollision(col_b.CalcExpSphere(), col_a.CalcExpCapsule());
                    p = -p;
                    return true;
                }
                else if (col_a.GeometryType == RBGeometryType.Capsule && col_b.GeometryType == RBGeometryType.Capsule)
                {
                    //Capsule-Capsule衝突
                    (p, pA, pB) = RBDetailCollision.DetailCollisionCapsuleCapsule.CalcDetailCollision(col_a.CalcExpCapsule(), col_b.CalcExpCapsule());
                    return true;
                }
            }

            p = Vector3.zero;
            pA = Vector3.zero;
            pB = Vector3.zero;

            return false;
        }

        static void AddCollision(RBRigidbody rb, RBCollider collider)
        {
            if (rb.colliding.Length <= rb.collidingCount)
            {
                Array.Resize(ref rb.colliding, rb.collidingCount + 1);
            }

            rb.colliding[rb.collidingCount] = collider;
            rb.collidingCount++;
        }

        static void SolveCollisionPair(RBCollision col)
        {
            if (!col.skipInSolver && col.penetration != Vector3.zero)
            {
                (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) = SolveCollision(col);

                if (col.rigidbody_a != null)
                {
                    col.rigidbody_a.ExpVelocity += velAdd_a;
                    col.rigidbody_a.ExpAngularVelocity += angVelAdd_a;
                }

                if (col.rigidbody_b != null)
                {
                    col.rigidbody_b.ExpVelocity += velAdd_b;
                    col.rigidbody_b.ExpAngularVelocity += angVelAdd_b;
                }

                if (velAdd_a.sqrMagnitude < cpu_solver_abort_veladd_sqrt && angVelAdd_a.sqrMagnitude < cpu_solver_abort_angveladd_sqrt && velAdd_b.sqrMagnitude < cpu_solver_abort_veladd_sqrt && angVelAdd_b.sqrMagnitude < cpu_solver_abort_angveladd_sqrt)
                {
                    col.skipInSolver = true;
                }
            }
        }

        static void UpdateTrajectoryPair(RBCollision col, float dt)
        {
            RecalculateCollision(col.collider_a, col.collider_b, col.info, out Vector3 p, out Vector3 pA, out Vector3 pB);

            if (p != Vector3.zero)
            {
                p = col.collider_a.ExpToCurrentVector(p);
                pA = col.collider_a.ExpToCurrent(pA);
                pB = col.collider_b.ExpToCurrent(pB);
                col.Update(p, pA, pB);
                col.InitVelocityConstraint(dt, false);
            }
            else
            {
                col.skipInSolver = true;
            }
        }

        static void DetectCollisions(RBTrajectory traj_a, RBTrajectory traj_b, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> obb_obb_cols, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> obb_sphere_cols, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> sphere_sphere_cols, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> obb_capsule_cols, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> _sphere_capsule_cols, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> capsule_capsule_cols)
        {
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_a;
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_b;

            if (traj_a.IsStatic)
            {
                trajAABB_a = new (RBCollider, RBColliderAABB)[] { (traj_a.Collider, traj_a.Collider.ExpTrajectory.trajectoryAABB) };
            }
            else
            {
                trajAABB_a = traj_a.Rigidbody.GetColliders().Select(item => (item, item.ExpTrajectory.trajectoryAABB)).ToArray();
            }

            if (traj_b.IsStatic)
            {
                trajAABB_b = new (RBCollider, RBColliderAABB)[] { (traj_b.Collider, traj_b.Collider.ExpTrajectory.trajectoryAABB) };
            }
            else
            {
                trajAABB_b = traj_b.Rigidbody.GetColliders().Select(item => (item, item.ExpTrajectory.trajectoryAABB)).ToArray();
            }

            //�R���C�_���ɐڐG�𔻒�
            for (int i = 0; i < trajAABB_a.Length; i++)
            {
                var collider_a = trajAABB_a[i];

                if (collider_a.collider.isActiveAndEnabled)
                {
                    float a_x_min = collider_a.aabb.MinX;
                    float a_x_max = collider_a.aabb.MaxX;

                    for (int j = 0; j < trajAABB_b.Length; j++)
                    {
                        var collider_b = trajAABB_b[j];
                        float b_x_min = collider_b.aabb.MinX;
                        float b_x_max = collider_b.aabb.MaxX;

                        if (collider_b.collider.isActiveAndEnabled)
                        {
                            Vector3 cg = traj_a.IsStatic ? traj_b.IsStatic ? Vector3.zero : traj_b.Rigidbody.CenterOfGravityWorld : traj_a.Rigidbody.CenterOfGravityWorld;

                            bool aabbCollide = collider_a.aabb.OverlapAABB(collider_b.aabb);

                            if (aabbCollide)
                            {
                                if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //OBB-OBB衝突
                                    obb_obb_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-OBB衝突
                                    obb_sphere_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //Sphere-OBB衝突（逆転）
                                    obb_sphere_cols.Add((collider_b.collider, collider_a.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-Sphere衝突
                                    sphere_sphere_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //OBB-Capsule衝突
                                    obb_capsule_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //OBB-Capsule衝突（逆転）
                                    obb_capsule_cols.Add((collider_b.collider, collider_a.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //Sphere-Capsule衝突
                                    _sphere_capsule_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-Capsule衝突（逆転）
                                    _sphere_capsule_cols.Add((collider_b.collider, collider_a.collider, default, null));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //Capsule-Capsule衝突
                                    capsule_capsule_cols.Add((collider_a.collider, collider_b.collider, default, null));
                                }
                            }
                        }
                    }
                }
            }
        }

        static RBCollision FindCollision(RBCollider col_a, RBCollider col_b, out bool isInverted)
        {
            isInverted = false;

            foreach (RBCollision r in _collisions.ToList())
            {
                if (r != null)
                {
                    if ((r.collider_a, r.collider_b) == (col_a, col_b))
                    {
                        return r;
                    }

                    if ((r.collider_a, r.collider_b) == (col_b, col_a))
                    {
                        isInverted = true;
                        return r;
                    }
                }
            }

            return null;
        }

        static (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) SolveCollision(RBCollision col)
        {
            col.SolveVelocityConstraints(out Vector3 velocityAdd_a, out Vector3 angularVelocityAdd_a, out Vector3 velocityAdd_b, out Vector3 angularVelocityAdd_b);
            return (velocityAdd_a, angularVelocityAdd_a, velocityAdd_b, angularVelocityAdd_b);
        }

        public static void Dispose()
        {
        }
    }

    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RBCollision
    {
        public bool isValidCollision = false;

        public RBCollider collider_a;
        public RBRigidbody rigidbody_a;
        public RBCollider collider_b;
        public RBRigidbody rigidbody_b;

        public Vector3 cg_a;
        public Vector3 cg_b;
        public Vector3 aNearest;
        public Vector3 bNearest;
        public Vector3 penetration;
        public Vector3 ContactNormal { get { return _contactNormal; } set { _contactNormal = value.normalized; } }
        public Vector3 rA;
        public Vector3 rB;
        public RBDetailCollision.DetailCollisionInfo info;

        public bool skipInSolver;

        Jacobian _jN = new Jacobian(Jacobian.Type.Normal); //Normal
        Jacobian _jT = new Jacobian(Jacobian.Type.Tangent); //Tangent
        Jacobian _jB = new Jacobian(Jacobian.Type.Tangent); //Bi-Tangent

        public Vector3 Velocity_a { get { return rigidbody_a?.Velocity ?? Vector3.zero; } }
        public Vector3 AngularVelocity_a { get { return rigidbody_a?.AngularVelocity ?? Vector3.zero; } }
        public Vector3 ExpVelocity_a { get { return rigidbody_a?.ExpVelocity ?? Vector3.zero; } }
        public Vector3 ExpAngularVelocity_a { get { return rigidbody_a?.ExpAngularVelocity ?? Vector3.zero; } }
        public float InverseMass_a { get { return rigidbody_a?.InverseMass ?? 0; } }
        public Vector3 InverseInertiaWs_a { get { return rigidbody_a?.InverseInertiaWs ?? Vector3.zero; } }

        public Vector3 Velocity_b { get { return rigidbody_b?.Velocity ?? Vector3.zero; } }
        public Vector3 AngularVelocity_b { get { return rigidbody_b?.AngularVelocity ?? Vector3.zero; } }
        public Vector3 ExpVelocity_b { get { return rigidbody_b?.ExpVelocity ?? Vector3.zero; } }
        public Vector3 ExpAngularVelocity_b { get { return rigidbody_b?.ExpAngularVelocity ?? Vector3.zero; } }
        public float InverseMass_b { get { return rigidbody_b?.InverseMass ?? 0; } }
        public Vector3 InverseInertiaWs_b { get { return rigidbody_b?.InverseInertiaWs ?? Vector3.zero; } }

        public bool IsSleeping_a { get { return rigidbody_a?.isSleeping ?? true; } }
        public bool IsSleeping_b { get { return rigidbody_b?.isSleeping ?? true; } }

        Vector3 _contactNormal;

        public RBCollision(RBTrajectory traj_a, RBCollider col_a, RBTrajectory traj_b, RBCollider col_b, Vector3 penetration)
        {
            collider_a = col_a;
            rigidbody_a = traj_a.Rigidbody;
            collider_b = col_b;
            rigidbody_b = traj_b.Rigidbody;

            cg_a = traj_a.IsStatic ? col_a.GetColliderCenter() : traj_a.Rigidbody.CenterOfGravityWorld;
            cg_b = traj_b.IsStatic ? col_b.GetColliderCenter() : traj_b.Rigidbody.CenterOfGravityWorld;

            this.penetration = penetration;
            _contactNormal = (traj_b.IsStatic ? Vector3.zero : traj_b.Rigidbody.Velocity) - (traj_a.IsStatic ? Vector3.zero : traj_a.Rigidbody.Velocity);
        }

        public RBCollision(RBCollider col_a, RBCollider col_b, Vector3 penetration)
        {
            collider_a = col_a;
            rigidbody_a = col_a.ParentRigidbody;
            collider_b = col_b;
            rigidbody_b = col_b.ParentRigidbody;

            cg_a = rigidbody_a?.CenterOfGravityWorld ?? col_a.GetColliderCenter();
            cg_b = rigidbody_b?.CenterOfGravityWorld ?? col_b.GetColliderCenter();

            this.penetration = penetration;
            _contactNormal = penetration.normalized;
        }

        public bool ObjectEquals(RBCollision col)
        {
            return ObjectEquals(col, false, out _);
        }

        public bool ObjectEquals(RBCollision col, bool containsInverted, out bool isInverted)
        {
            isInverted = false;

            if (col == null)
            {
                return true;
            }

            if (col != null)
            {
                bool b1 = collider_a = col.collider_a;
                bool b2 = collider_b = col.collider_b;
                bool b3 = rigidbody_a = col.rigidbody_a;
                bool b4 = rigidbody_b = col.rigidbody_b;

                bool bRet = (b1 && b2 && b3 && b4);

                if (!containsInverted && !bRet)
                {
                    bool bc1 = collider_a = col.collider_b;
                    bool bc2 = collider_b = col.collider_a;
                    bool bc3 = rigidbody_a = col.rigidbody_b;
                    bool bc4 = rigidbody_b = col.rigidbody_a;

                    bool bcRet = (bc1 && bc2 && bc3 && bc4);

                    isInverted = bcRet;
                    return bcRet;
                }
                else
                {
                    return bRet;
                }
            }

            return false;
        }

        public void SwapTo(RBCollider col_a, RBCollider col_b)
        {
            if ((collider_a, collider_b) == (col_b, col_a))
            {
                (collider_a, collider_b) = (col_a, col_b);
                (rigidbody_a, rigidbody_b) = (rigidbody_b, rigidbody_a);
                (cg_a, cg_b) = (cg_b, cg_a);
                (aNearest, bNearest) = (bNearest, aNearest);
                penetration = -penetration;
                _contactNormal = -_contactNormal;
                (rA, rB) = (rB, rA);
            }
        }

        public Vector3 CalcRelativeVelocityAtPoints(Vector3 pointOnA, Vector3 pointOnB)
        {
            return -Velocity_a - Vector3.Cross(AngularVelocity_a, pointOnA - cg_a) + Velocity_b + Vector3.Cross(AngularVelocity_b, pointOnB - cg_b);
        }

        public Vector3 CalcRelativeExpVelocityAtPoints(Vector3 pointOnA, Vector3 pointOnB)
        {
            return ExpVelocity_a + Vector3.Cross(ExpAngularVelocity_a, pointOnA - cg_a) - ExpVelocity_b - Vector3.Cross(ExpAngularVelocity_b, pointOnB - cg_b);
        }

        public void Update(Vector3 penetration, Vector3 aNearest, Vector3 bNearest)
        {
            this.aNearest = aNearest;
            this.bNearest = bNearest;

            this.penetration = penetration;
            _contactNormal = penetration.normalized;

            cg_a = rigidbody_a?.CenterOfGravityWorld ?? collider_a.GetColliderCenter();
            cg_b = rigidbody_b?.CenterOfGravityWorld ?? collider_b.GetColliderCenter();

            rA = aNearest - cg_a;
            rB = bNearest - cg_b;

            skipInSolver = false;
        }

        public void InitVelocityConstraint(float dt, bool initBias = true)
        {
            Vector3 contactNormal = ContactNormal;
            Vector3 tangent = Vector3.zero;
            Vector3 bitangent = Vector3.zero;

            Vector3.OrthoNormalize(ref contactNormal, ref tangent, ref bitangent);

            _jN.Init(this, contactNormal, dt, initBias);
            _jT.Init(this, tangent, dt, initBias);
            _jB.Init(this, bitangent, dt, initBias);
        }

        public void SolveVelocityConstraints(out Vector3 vAdd_a, out Vector3 avAdd_a, out Vector3 vAdd_b, out Vector3 avAdd_b)
        {
            vAdd_a = Vector3.zero;
            avAdd_a = Vector3.zero;
            vAdd_b = Vector3.zero;
            avAdd_b = Vector3.zero;

            (vAdd_a, avAdd_a, vAdd_b, avAdd_b) = _jN.Resolve(this, vAdd_a, avAdd_a, vAdd_b, avAdd_b);
            (vAdd_a, avAdd_a, vAdd_b, avAdd_b) = _jT.Resolve(this, vAdd_a, avAdd_a, vAdd_b, avAdd_b);
            (vAdd_a, avAdd_a, vAdd_b, avAdd_b) = _jB.Resolve(this, vAdd_a, avAdd_a, vAdd_b, avAdd_b);
        }

        const float COLLISION_ERROR_SLOP = 0.005f;

        struct Jacobian
        {
            // Jv + b >= 0

            Type _type;

            Vector3 _va;
            Vector3 _wa;
            Vector3 _vb;
            Vector3 _wb;

            float _bias;
            float _totalLambda;
            float _effectiveMass;

            public enum Type
            {
                Normal,
                Tangent
            }

            public Jacobian(Type type)
            {
                _type = type;

                _va = Vector3.zero;
                _wa = Vector3.zero;
                _vb = Vector3.zero;
                _wb = Vector3.zero;

                _bias = 0;
                _totalLambda = 0;
                _effectiveMass = 0;
            }

            public void Init(RBCollision col, Vector3 dir, float dt, bool initBias = true)
            {
                Vector3 dirN = dir;

                _va = dirN;
                _wa = Vector3.Cross(col.rA, dirN);
                _vb = -dirN;
                _wb = Vector3.Cross(col.rB, -dirN);

                if (initBias)
                {
                    _bias = 0;
                    _totalLambda = 0;
                }

                if (_type == Type.Normal)
                {
                    float beta = col.collider_a.beta * col.collider_b.beta;
                    float restitution = col.collider_a.restitution * col.collider_b.restitution;
                    Vector3 relVel = Vector3.zero;
                    relVel += col.ExpVelocity_a;
                    relVel += Vector3.Cross(col.ExpAngularVelocity_a, col.rA);
                    relVel -= col.ExpVelocity_b;
                    relVel -= Vector3.Cross(col.ExpAngularVelocity_b, col.rB);

                    if (initBias)
                    {
                        float closingVelocity = Vector3.Dot(relVel, dirN);
                        _bias = -(beta / dt) * Mathf.Max(0, col.penetration.magnitude - COLLISION_ERROR_SLOP) + restitution * closingVelocity;
                    }
                }

                float k = 0;
                k += col.InverseMass_a;
                k += Vector3.Dot(_wa, Vector3.Scale(col.InverseInertiaWs_a, _wa));
                k += col.InverseMass_b;
                k += Vector3.Dot(_wb, Vector3.Scale(col.InverseInertiaWs_b, _wb));
                _effectiveMass = 1 / k;
            }

            public (Vector3, Vector3, Vector3, Vector3) Resolve(RBCollision col, Vector3 vAdd_a, Vector3 avAdd_a, Vector3 vAdd_b, Vector3 avAdd_b)
            {
                float jv = 0;
                jv += Vector3.Dot(_va, col.ExpVelocity_a);
                jv += Vector3.Dot(_wa, col.ExpAngularVelocity_a);
                jv += Vector3.Dot(_vb, col.ExpVelocity_b);
                jv += Vector3.Dot(_wb, col.ExpAngularVelocity_b);

                float lambda = _effectiveMass * (-(jv + _bias));
                float oldTotalLambda = _totalLambda;

                if (_type == Type.Normal)
                {
                    _totalLambda = Mathf.Max(0.0f, _totalLambda + lambda);
                }
                else if (_type == Type.Tangent)
                {
                    float friction = col.collider_a.friction * col.collider_b.friction;
                    float maxFriction = friction * col._jN._totalLambda;
                    _totalLambda = Mathf.Clamp(_totalLambda + lambda, -maxFriction, maxFriction);
                }

                lambda = _totalLambda - oldTotalLambda;

                vAdd_a += col.InverseMass_a * _va * lambda;
                avAdd_a += Vector3.Scale(col.InverseInertiaWs_a, _wa) * lambda;
                vAdd_b += col.InverseMass_b * _vb * lambda;
                avAdd_b += Vector3.Scale(col.InverseInertiaWs_b, _wb) * lambda;

                return (vAdd_a, avAdd_a, vAdd_b, avAdd_b);
            }
        }
    }

    public struct RBColliderAABB
    {
        public bool isValidAABB;
        public Vector3 Center { get; private set; }
        public Vector3 Size { get; private set; }
        public Vector3 Extents { get { return Size / 2f; } }

        public float MinX { get { return _minX; } }
        public float MaxX { get { return _maxX; } }
        public Vector3 Min { get { return _min; } }
        public Vector3 Max { get { return _max; } }

        float _minX;
        float _maxX;
        Vector3 _min;
        Vector3 _max;

        public RBColliderAABB(Vector3 center, Vector3 size)
        {
            isValidAABB = true;
            this.Center = center;
            this.Size = RBPhysUtil.V3Abs(size);

            _min = Center - Size / 2;
            _max = Center + Size / 2;
            _minX = _min.x;
            _maxX = _max.x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(Vector3 point)
        {
            if (isValidAABB)
            {
                if (!ContainsPoint(point))
                {
                    Vector3 res_min = Vector3.Min(Min, point);
                    Vector3 res_max = Vector3.Max(Max, point);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
            }
            else
            {
                Center = point;
                Size = Vector3.zero;
                isValidAABB = true;

                _min = Center;
                _max = Center;
                _minX = _min.x;
                _maxX = _max.x;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(RBColliderAABB aabb)
        {
            if (aabb.isValidAABB)
            {
                if (isValidAABB)
                {
                    Vector3 res_min = Vector3.Min(Min, aabb.Min);
                    Vector3 res_max = Vector3.Max(Max, aabb.Max);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
                else
                {
                    Center = aabb.Center;
                    Size = aabb.Size;
                    isValidAABB = true;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsPoint(Vector3 point)
        {
            return isValidAABB && RBPhysUtil.IsV3Less(Min, point) && RBPhysUtil.IsV3Less(point, Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool OverlapAABB(RBColliderAABB ext)
        {
            if (isValidAABB && ext.isValidAABB)
            {
                if (!RBPhysUtil.RangeOverlap(Min.x, Max.x, ext.Min.x, ext.Max.x)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.y, Max.y, ext.Min.y, ext.Max.y)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.z, Max.z, ext.Min.z, ext.Max.z)) return false;

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool OverlapAABBEpsilon(RBColliderAABB ext, float epsilon)
        {
            float e = epsilon / 2f;

            if (isValidAABB && ext.isValidAABB)
            {
                if (!RBPhysUtil.RangeOverlap(Min.x, Max.x, ext.Min.x, ext.Max.x, e)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.y, Max.y, ext.Min.y, ext.Max.y, e)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.z, Max.z, ext.Min.z, ext.Max.z, e)) return false;

                return true;
            }

            return false;
        }
    }

    public struct RBColliderOBB
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 size;
        public bool isValidOBB;

        Vector3 _center;

        public Vector3 Center { get { return _center; } }

        public RBColliderOBB(Vector3 pos, Quaternion rot, Vector3 size)
        {
            this.pos = pos;
            this.rot = rot;
            this.size = size;
            isValidOBB = true;
            _center = pos + rot * size / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetAxisSize(Vector3 axisN)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, size.z), axisN));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(size.x, 0, 0), axisN));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, size.y, 0), axisN));
            return fwd + right + up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisForwardN()
        {
            return rot * Vector3.forward;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisRightN()
        {
            return rot * Vector3.right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisUpN()
        {
            return rot * Vector3.up;
        }
    }

    public struct RBColliderSphere
    {
        public Vector3 pos;
        public float radius;
        public bool isValidSphere;

        public RBColliderSphere(Vector3 pos, float radius)
        {
            this.pos = pos;
            this.radius = radius;
            isValidSphere = true;
        }
    }

    public struct RBColliderCapsule
    {
        public Vector3 pos;
        public Quaternion rot;
        public float radius;
        public float height;
        public bool isValidCapsule;

        public RBColliderCapsule(Vector3 pos, Quaternion rot, float radius, float height)
        {
            this.pos = pos;
            this.rot = rot;
            this.radius = radius;
            this.height = height;
            isValidCapsule = true;
        }

        public Vector3 GetHeightAxisN()
        {
            return rot * Vector3.up;
        }

        public float GetAxisSize(Vector3 axisN)
        {
            return Mathf.Abs(Vector3.Dot(rot * new Vector3(0, height, 0), axisN)) + radius * 2;
        }

        public (Vector3 begin, Vector3 end) GetEdge()
        {
            return (pos + rot * new Vector3(0, height / 2f, 0), pos - rot * new Vector3(0, height / 2f, 0));
        }
    }

    public enum RBGeometryType
    {
        OBB,
        Sphere,
        Capsule
    }

    public class RBTrajectory
    {
        public RBColliderAABB trajectoryAABB;

        public bool IsValidTrajectory { get { return _isValidTrajectory; } }
        public RBRigidbody Rigidbody { get { return _rigidbody; } }
        public bool IsStatic { get { return _isStatic; } }
        public RBCollider Collider { get { return _collider; } }
        public RBCollider[] Colliders { get { return _colliders; } }
        public bool IsStaticOrSleeping { get { return Rigidbody?.isSleeping ?? true || IsStatic; } }
        public int Layer { get { return _layer; } }

        bool _isValidTrajectory;
        RBRigidbody _rigidbody;
        bool _isStatic;
        RBCollider _collider;
        RBCollider[] _colliders;
        int _layer;

        public RBTrajectory()
        {
            _isValidTrajectory = false;
        }

        public RBTrajectory(RBRigidbody rigidbody, int layer)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.isActiveAndEnabled)
                {
                    aabb.Encapsulate(c.ExpTrajectory.trajectoryAABB);
                }
            }

            trajectoryAABB = aabb;
            _rigidbody = rigidbody;
            _collider = null;
            _isStatic = false;
            _isValidTrajectory = true;

            _colliders = rigidbody.GetColliders();
            _layer = layer;
        }

        public RBTrajectory(RBCollider collider, int layer)
        {
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            _colliders = new RBCollider[] { collider };
            _layer = layer;
        }

        public void Update(RBRigidbody rigidbody, int layer)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.isActiveAndEnabled)
                {
                    aabb.Encapsulate(c.ExpTrajectory.trajectoryAABB);
                }
            }

            trajectoryAABB = aabb;
            _rigidbody = rigidbody;
            _collider = null;
            _isStatic = false;
            _isValidTrajectory = true;

            _colliders = rigidbody.GetColliders();
            _layer = layer;
        }

        public void Update(RBCollider collider, int layer)
        {
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            if ((_colliders?.Length ?? -1) != 1)
            {
                _colliders = new RBCollider[] { collider };
            }
            else
            {
                _colliders[0] = collider;
            }

            _layer = layer;
        }

        public void Update(RBCollider collider, Vector3 pos, Quaternion rot)
        {
            trajectoryAABB = collider.CalcAABB(pos, rot);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            if ((_colliders?.Length ?? -1) != 1)
            {
                _colliders = new RBCollider[] { collider };
            }
            else
            {
                _colliders[0] = collider;
            }
        }

        public void TryPhysAwake()
        {
            if (Rigidbody != null)
            {
                Rigidbody.PhysAwake();
            }
        }
    }
}