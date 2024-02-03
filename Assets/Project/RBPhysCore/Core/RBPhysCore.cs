using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEditor;

namespace RBPhys
{
    public static class RBPhysCore
    {
        public const int CPU_COLLISION_SOLVER_MAX_ITERATION = 5;
        public const int CPU_COLLISION_SOLVER_INTERNAL_MAX_ITERATION = 3;
        public const float CPU_SOLVER_ABORT_VELADD_SQRT = 0.01f * 0.01f;
        public const float CPU_SOLVER_ABORT_ANGVELADD_SQRT = 0.1f * 0.1f;

        public const int DEFAULT_SOLVER_ITERATION = 6;

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

        static List<Task<(Vector3, Vector3, Vector3, RBDetailCollision.DetailCollisionInfo)>> _detailCollisionTasks = new List<Task<(Vector3, Vector3, Vector3, RBDetailCollision.DetailCollisionInfo)>>();

        static List<Task> _solveCollisionTasks = new List<Task>();
        static List<Task> _updateTrajectorieTasks = new List<Task>();

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

        public static void OpenPhysicsFrameWindow(float dt)
        {
            UpdateTransforms();
            UpdateExtTrajectories(dt);
            SortTrajectories();
            SolveColliders(dt);

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ExpVelocity += new Vector3(0, -9.81f, 0) * dt;
            }

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

        static void UpdateTransforms()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbody");
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform();
            }
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

        static void UpdateColliderExtTrajectories(float dt)
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbody");
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateColliderExpTrajectory(dt);
            }
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

        static List<(RBCollider col_a, RBCollider col_b)> _obb_obb_cols = new List<(RBCollider, RBCollider)>();
        static List<(RBCollider col_a, RBCollider col_b)> _obb_sphere_cols = new List<(RBCollider, RBCollider)>();
        static List<(RBCollider col_a, RBCollider col_b)> _sphere_sphere_cols = new List<(RBCollider, RBCollider)>();
        static List<(RBCollider col_a, RBCollider col_b)> _obb_capsule_cols = new List<(RBCollider, RBCollider)>();
        static List<(RBCollider col_a, RBCollider col_b)> _sphere_capsule_cols = new List<(RBCollider, RBCollider)>();
        static List<(RBCollider col_a, RBCollider col_b)> _capsule_capsule_cols = new List<(RBCollider, RBCollider)>();

        static List<(Vector3 p, Vector3 pA, Vector3 pB, RBDetailCollision.DetailCollisionInfo info)> _cols_res = new List<(Vector3 p, Vector3 pA, Vector3 pB, RBDetailCollision.DetailCollisionInfo info)>();

        public static void SolveColliders(float dt)
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

                            if (!activeTraj.IsStaticOrSleeping || !targetTraj.IsStaticOrSleeping) //罪
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
            Profiler.EndSample();

            //�Փˌ��m�i�i���[�t�F�[�Y�j�Ɖ��

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareDetailTest");

            _cols_res.Clear();
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

            _detailCollisionTasks.Clear();
            foreach (var colPair in _obb_obb_cols)
            {
                var t = RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpOBB());
                _detailCollisionTasks.Add(t);
            }

            foreach (var colPair in _obb_sphere_cols)
            {
                var t =  RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpSphere());
                _detailCollisionTasks.Add(t);
            }

            foreach (var colPair in _sphere_sphere_cols)
            {
                var t = RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollisionInfo(colPair.col_a.CalcExpSphere(), colPair.col_b.CalcExpSphere());
                _detailCollisionTasks.Add(t);
            }

            foreach (var colPair in _obb_capsule_cols)
            {
                var t = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpOBB(), colPair.col_b.CalcExpCapsule());
                _detailCollisionTasks.Add(t);
            }

            foreach (var colPair in _sphere_capsule_cols)
            {
                var t = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpSphere(), colPair.col_b.CalcExpCapsule());
                _detailCollisionTasks.Add(t);
            }

            foreach (var colPair in _capsule_capsule_cols)
            {
                var t = RBDetailCollision.DetailCollisionCapsuleCapsule.CalcDetailCollisionInfo(colPair.col_a.CalcExpCapsule(), colPair.col_b.CalcExpCapsule());
                _detailCollisionTasks.Add(t);
            }

            Task.WhenAll(_detailCollisionTasks).Wait();

            foreach (var t in _detailCollisionTasks)
            {
                var r = t.Result;
                _cols_res.Add((r.Item1, r.Item2, r.Item3, r.Item4));
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareSolveCollisions");

            for (int i = 0; i < _obb_obb_cols.Count; i++)
            {
                if (_cols_res[i].p != Vector3.zero)
                {
                    var pair = _obb_obb_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    var p = pair.col_b.ExpToCurrentVector(_cols_res[i].p);
                    var pA = pair.col_a.ExpToCurrent(_cols_res[i].pA);
                    var pB = pair.col_b.ExpToCurrent(_cols_res[i].pB);

                    rbc.Update(p, pA, pB);
                    rbc.InitVelocityConstraint(dt);
                    rbc.info = _cols_res[i].info;

                    _collisionsInSolver.Add(rbc);
                }
            }
            int offset = _obb_obb_cols.Count;

            for (int i = 0; i < _obb_sphere_cols.Count; i++)
            {
                int p = offset + i;

                if (_cols_res[p].p != Vector3.zero)
                {
                    var pair = _obb_sphere_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    rbc.Update(_cols_res[p].p, _cols_res[p].pA, _cols_res[p].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }
            offset += _obb_sphere_cols.Count;

            for (int i = 0; i < _sphere_sphere_cols.Count; i++)
            {
                int p = offset + i;

                if (_cols_res[p].p != Vector3.zero)
                {
                    var pair = _sphere_sphere_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    rbc.Update(_cols_res[p].p, _cols_res[p].pA, _cols_res[p].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }
            offset += _sphere_sphere_cols.Count;

            for (int i = 0; i < _obb_capsule_cols.Count; i++)
            {
                int p = offset + i;

                if (_cols_res[p].p != Vector3.zero)
                {
                    var pair = _obb_capsule_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    rbc.Update(_cols_res[p].p, _cols_res[p].pA, _cols_res[p].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }
            offset += _obb_capsule_cols.Count;

            for (int i = 0; i < _sphere_capsule_cols.Count; i++)
            {
                int p = offset + i;

                if (_cols_res[p].p != Vector3.zero)
                {
                    var pair = _sphere_capsule_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    rbc.Update(_cols_res[p].p, _cols_res[p].pA, _cols_res[p].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }
            offset += _sphere_capsule_cols.Count;

            for (int i = 0; i < _capsule_capsule_cols.Count; i++)
            {
                int p = offset + i;

                if (_cols_res[p].p != Vector3.zero)
                {
                    var pair = _capsule_capsule_cols[i];
                    var rbc = FindCollision(pair.Item1, pair.Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(pair.Item1, pair.Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(pair.Item1, pair.Item2, _cols_res[i].p);
                    }

                    rbc.Update(_cols_res[p].p, _cols_res[p].pA, _cols_res[p].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }
            offset += _capsule_capsule_cols.Count;
            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-SolveCollisions");

            for (int iter = 0; iter < CPU_COLLISION_SOLVER_MAX_ITERATION; iter++)
            {
                for (int i = 0; i < CPU_COLLISION_SOLVER_INTERNAL_MAX_ITERATION; i++)
                {
                    Profiler.BeginSample(name: String.Format("SolveCollisions({0}-{1}/{2})", iter, i, CPU_COLLISION_SOLVER_INTERNAL_MAX_ITERATION * CPU_COLLISION_SOLVER_MAX_ITERATION));

                    _solveCollisionTasks.Clear();

                    foreach (var col in _collisionsInSolver)
                    {
                        if (!col.skipInSolver && col.penetration != Vector3.zero)
                        {
                            var t = SolveCollisionPair(col, dt);

                            _solveCollisionTasks.Add(t);
                        }
                    }

                    Task.WhenAll(_solveCollisionTasks).Wait();

                    Profiler.EndSample();
                }

                if (iter != CPU_COLLISION_SOLVER_MAX_ITERATION - 1)
                {
                    Profiler.BeginSample(name: String.Format("UpdateTrajectories({0}/{1})", iter, CPU_COLLISION_SOLVER_MAX_ITERATION));

                    _updateTrajectorieTasks.Clear();

                    UpdateColliderExtTrajectories(dt);

                    foreach (var col in _collisionsInSolver)
                    {
                        var t = UpdateTrajectoryPair(col, dt);
                        _updateTrajectorieTasks.Add(t);
                    }

                    Task.WhenAll(_updateTrajectorieTasks).Wait();

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

        static async Task SolveCollisionPair(RBCollision col, float dt)
        {
            (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) = SolveCollision(col, dt);

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

            if (velAdd_a.sqrMagnitude < CPU_SOLVER_ABORT_VELADD_SQRT && angVelAdd_a.sqrMagnitude < CPU_SOLVER_ABORT_ANGVELADD_SQRT && velAdd_b.sqrMagnitude < CPU_SOLVER_ABORT_VELADD_SQRT && angVelAdd_b.sqrMagnitude < CPU_SOLVER_ABORT_ANGVELADD_SQRT)
            {
                col.skipInSolver = true;
            }
        }

        static async Task UpdateTrajectoryPair(RBCollision col, float dt)
        {
            bool collide = RecalculateCollision(col.collider_a, col.collider_b, col.info, out Vector3 p, out Vector3 pA, out Vector3 pB);
            p = col.collider_a.ExpToCurrentVector(p);
            pA = col.collider_a.ExpToCurrent(pA);
            pB = col.collider_b.ExpToCurrent(pB);

            if (p != Vector3.zero)
            {
                //Debug.Log((p, pA, pB, col.info.obb_obb_penetrationIndex));
                col.Update(p, pA, pB);
                col.InitVelocityConstraint(dt, false);
            }
            else
            {
                col.skipInSolver = true;
            }
        }

        static void DetectCollisions(RBTrajectory traj_a, RBTrajectory traj_b, ref List<(RBCollider, RBCollider)> obb_obb_cols, ref List<(RBCollider, RBCollider)> obb_sphere_cols, ref List<(RBCollider, RBCollider)> sphere_sphere_cols, ref List<(RBCollider, RBCollider)> obb_capsule_cols, ref List<(RBCollider, RBCollider)> _sphere_capsule_cols, ref List<(RBCollider, RBCollider)> capsule_capsule_cols)
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

            //AABB��x�ŏ��l�ŃR���C�_������\�[�g
            trajAABB_a = trajAABB_a.OrderBy(item => item.aabb.MinX).ToArray();
            trajAABB_b = trajAABB_b.OrderBy(item => item.aabb.MinX).ToArray();

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

                        if (b_x_max < a_x_min)
                        {
                            continue;
                        }

                        if (a_x_max < b_x_min)
                        {
                            break;
                        }

                        if (collider_b.collider.isActiveAndEnabled)
                        {
                            Vector3 cg = traj_a.IsStatic ? traj_b.IsStatic ? Vector3.zero : traj_b.Rigidbody.CenterOfGravityWorld : traj_a.Rigidbody.CenterOfGravityWorld;

                            bool aabbCollide = collider_a.aabb.OverlapAABB(collider_b.aabb);

                            if (aabbCollide)
                            {
                                if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //OBB-OBB衝突
                                    obb_obb_cols.Add((collider_a.collider, collider_b.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-OBB衝突
                                    obb_sphere_cols.Add((collider_a.collider, collider_b.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //Sphere-OBB衝突（逆転）
                                    obb_sphere_cols.Add((collider_b.collider, collider_a.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-Sphere衝突
                                    sphere_sphere_cols.Add((collider_b.collider, collider_a.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //OBB-Capsule衝突
                                    obb_capsule_cols.Add((collider_a.collider, collider_b.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.OBB)
                                {
                                    //OBB-Capsule衝突（逆転）
                                    obb_capsule_cols.Add((collider_b.collider, collider_a.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //Sphere-Capsule衝突
                                    _sphere_capsule_cols.Add((collider_a.collider, collider_b.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                                {
                                    //Sphere-Capsule衝突（逆転）
                                    _sphere_capsule_cols.Add((collider_b.collider, collider_a.collider));
                                }
                                else if (collider_a.collider.GeometryType == RBGeometryType.Capsule && collider_b.collider.GeometryType == RBGeometryType.Capsule)
                                {
                                    //Capsule-Capsule衝突
                                    capsule_capsule_cols.Add((collider_a.collider, collider_b.collider));
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

        static (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) SolveCollision(RBCollision col, float dt)
        {
            col.SolveVelocityConstraints(out Vector3 velocityAdd_a, out Vector3 angularVelocityAdd_a, out Vector3 velocityAdd_b, out Vector3 angularVelocityAdd_b);
            return (velocityAdd_a, angularVelocityAdd_a, velocityAdd_b, angularVelocityAdd_b);
        }

        static void VerifyVelocity(RBRigidbody rb, bool enableStaticCollision = false)
        {

        }

        public static void Dispose()
        {
        }
    }

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

            _jN.Resolve(this, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
            _jT.Resolve(this, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
            _jB.Resolve(this, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
        }

        const float COLLISION_ERROR_SLOP = 0.005f;

        struct Jacobian
        {
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

                if (initBias)
                {
                    _totalLambda = 0;
                }
            }

            public void Resolve(RBCollision col, ref Vector3 vAdd_a, ref Vector3 avAdd_a, ref Vector3 vAdd_b, ref Vector3 avAdd_b)
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
                vAdd_b += col.InverseMass_b * _vb * lambda;
                avAdd_a += Vector3.Scale(col.InverseInertiaWs_a, _wa) * lambda;
                avAdd_b += Vector3.Scale(col.InverseInertiaWs_b, _wb) * lambda;
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

        bool _isValidTrajectory;
        RBRigidbody _rigidbody;
        bool _isStatic;
        RBCollider _collider;
        RBCollider[] _colliders;

        public RBTrajectory()
        {
            _isValidTrajectory = false;
        }

        public RBTrajectory(RBRigidbody rigidbody, float dt)
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
        }

        public RBTrajectory(RBCollider collider, float dt)
        {
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            _colliders = new RBCollider[] { collider };
        }

        public void Update(RBRigidbody rigidbody, float dt)
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
        }

        public void Update(RBCollider collider)
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