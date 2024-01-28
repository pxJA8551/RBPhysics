#undef COLLISION_NARROW_PHASE_HW_ACCELERATION
#undef COLLISION_SOLVER_HW_ACCELERATION

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using System.Threading;
using UnityEditor;

namespace RBPhys
{
    public static class RBPhysCore
    {
        public const int CPU_COLLISION_SOLVER_MAX_ITERATION = 15;
        public const float CPU_SOLVER_ABORT_VELADD_SQRT = 0.01f * 0.01f;
        public const float CPU_SOLVER_ABORT_ANGVELADD_SQRT = 0.15f * 0.15f;

        public const float CPU_COLLISION_FACE_PARALLEL_EPSILON = 0.005f;

        public const int DEFAULT_SOLVER_ITERATION = 6;

        static List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        static List<RBCollider> _colliders = new List<RBCollider>();

        static RBTrajectory[] _trajectories_orderByXMin = new RBTrajectory[0];
        static float[] _trajectories_xMin = new float[0];

        static List<RBRigidbody> _rbAddQueue = new List<RBRigidbody>();
        static List<RBRigidbody> _rbRemoveQueue = new List<RBRigidbody>();

        static List<RBCollider> _colAddQueue = new List<RBCollider>();
        static List<RBCollider> _colRemoveQueue = new List<RBCollider>();

        static List<RBCollision > _collisions = new List<RBCollision>();
        static List<RBCollision> _collisionsInSolver = new List<RBCollision>();

#if !COLLISION_NARROW_PHASE_HW_ACCELERATION
        static List<Task<(Vector3, Vector3, Vector3)>> _detailCollisionTasks = new List<Task<(Vector3, Vector3, Vector3)>>();
#endif

        static List<Task> _solveCollisionTasks = new List<Task>();

#if COLLISION_NARROW_PHASE_HW_ACCELERATION
        static HWAcceleration.DetailCollision.HWA_DetailCollisionOBBOBB _hwa_obb_obb_detail = new HWAcceleration.DetailCollision.HWA_DetailCollisionOBBOBB(8);
#endif

#if COLLISION_SOLVER_HW_ACCELERATION
        static HWAcceleration.HWA_SolveCollision _hwa_solveCollision = new HWAcceleration.HWA_SolveCollision(4, 8);
#endif

        public static void AddRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Add(rb);
            _rbAddQueue.Add(rb);
        }

        public static void RemoveRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Remove(rb);
            _rbRemoveQueue.Add(rb);
        }

        public static void AddCollider(RBCollider c)
        {
            _colliders.Add(c);
            _colAddQueue.Add(c);
        }

        public static void RemoveCollider(RBCollider c)
        {
            _colliders.Remove(c);
            _colRemoveQueue.Add(c);
        }

        public static void SwitchToCollider(RBCollider c)
        {
            _colAddQueue.Add(c);
        }

        public static void SwitchToRigidbody(RBCollider c)
        {
            _colRemoveQueue.Add(c);
            _colAddQueue.Remove(c);
        }

        public static void OpenPhysicsFrameWindow(float dt)
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbody");
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform(false);
            }
            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateCollider");
            foreach (RBCollider c in _colliders)
            {
                c.UpdateTransform();
            }
            Profiler.EndSample();

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
                    _trajectories_xMin[p] = _rbAddQueue[i].ObjectTrajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _rbAddQueue[i].ObjectTrajectory;
                }

                for (int i = 0; i < _colAddQueue.Count; i++)
                {
                    int p = offset + _rbAddQueue.Count + i;
                    _trajectories_xMin[p] = _colAddQueue[i].Trajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _colAddQueue[i].Trajectory;
                }

                _rbAddQueue.Clear();
                _colAddQueue.Clear();
            }

            if (_rbRemoveQueue.Any() || _colRemoveQueue.Any())
            {
                int rmvOffset = 0;

                //削除（間をつめる）
                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    if (i < _trajectories_orderByXMin.Length - rmvOffset)
                    {
                        bool isStatic = _trajectories_orderByXMin[i].IsStatic;

                        if (rmvOffset > 0)
                        {
                            _trajectories_orderByXMin[i - rmvOffset] = _trajectories_orderByXMin[i];
                            _trajectories_xMin[i - rmvOffset] = _trajectories_xMin[i];
                        }

                        if ((isStatic && _colRemoveQueue.Contains(_trajectories_orderByXMin[i].Collider)) || (!isStatic && _rbRemoveQueue.Contains(_trajectories_orderByXMin[i].Rigidbody)))
                        {
                            rmvOffset++;
                        }
                    }
                }

                int pc = _rbRemoveQueue.Count + _colRemoveQueue.Count;
                Array.Resize(ref _trajectories_orderByXMin, _trajectories_orderByXMin.Length - pc);
                Array.Resize(ref _trajectories_xMin, _trajectories_xMin.Length - pc);

                _rbRemoveQueue.Clear();
                _colRemoveQueue.Clear();
            }

            //_trajectories_orderByXMin = _trajectories_orderByXMin.OrderBy(item => item.trajectoryAABB.MinX).ToArray();

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

        static List<(RBCollider col_a, RBCollider col_b)> _obb_obb_cols = new List<(RBCollider, RBCollider)>();
        static List<(Vector3 p, Vector3 pA, Vector3 pB)> _obb_obb_cols_res = new List<(Vector3 p, Vector3 pA, Vector3 pB)>();

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

                                    if (RBPhysUtil.RangeOverlap(x_min, x_max, x_min_target, x_max_target) && activeTraj.trajectoryAABB.OverlapAABB(targetTraj.trajectoryAABB))
                                    {
                                        collidingTrajs.Add((activeTraj, targetTraj));
                                    }
                                }
                            }
                        }
                    }
                }

                Profiler.EndSample();
            }

            //�Փˌ��m�i�i���[�t�F�[�Y�j�Ɖ��

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareDetailTest");

            _obb_obb_cols.Clear();
            _obb_obb_cols_res.Clear();

            foreach (var trajPair in collidingTrajs)
            {
                DetectCollisions(trajPair.Item1, trajPair.Item2, ref _obb_obb_cols);
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-DetailTest");

#if COLLISION_NARROW_PHASE_HW_ACCELERATION
            _hwa_obb_obb_detail.HWA_ComputeDetailCollision(_obb_obb_cols);

            _hwa_obb_obb_detail.GetDatas_AfterDispatch(_obb_obb_cols.Count, ref _obb_obb_cols_res);
#else
            _detailCollisionTasks.Clear();
            foreach (var colPair in _obb_obb_cols)
            {
                var t = Task.Run(() => RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollision(colPair.col_a.CalcOBB(), colPair.col_b.CalcOBB()));
                _detailCollisionTasks.Add(t);
            }

            Task.WhenAll(_detailCollisionTasks).Wait();

            foreach (var t in _detailCollisionTasks)
            {
                var r = t.Result;
                _obb_obb_cols_res.Add((r.Item1, r.Item2, r.Item3));
            }
#endif

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-SolveCollisions");

            for (int i = 0; i < _obb_obb_cols.Count; i++)
            {
                if (_obb_obb_cols_res[i].p != Vector3.zero)
                {
                    var rbc = FindCollision(_obb_obb_cols[i].Item1, _obb_obb_cols[i].Item2, out bool isInverted);

                    if (isInverted && rbc != null)
                    {
                        rbc.SwapTo(_obb_obb_cols[i].Item1, _obb_obb_cols[i].Item2);
                    }

                    if (rbc == null)
                    {
                        rbc = new RBCollision(_obb_obb_cols[i].Item1, _obb_obb_cols[i].Item2, _obb_obb_cols_res[i].p);
                    }

                    if (rbc.collider_a.name.Contains("sfp") || rbc.collider_b.name.Contains("sfp"))
                    {
                        Debug.Log((rbc.collider_a.name, rbc.collider_b.name, _obb_obb_cols_res[i].p, _obb_obb_cols_res[i].p.normalized, _obb_obb_cols_res[i].pA, _obb_obb_cols_res[i].pB));
                    }

                    rbc.Update(_obb_obb_cols_res[i].p, _obb_obb_cols_res[i].pA, _obb_obb_cols_res[i].pB);
                    rbc.InitVelocityConstraint(dt);

                    _collisionsInSolver.Add(rbc);
                }
            }

#if COLLISION_SOLVER_HW_ACCELERATION
            _hwa_solveCollision.HWA_ComputeSolveCollision(_collisionsInSolver);
#else
            for (int i = 0; i < CPU_COLLISION_SOLVER_MAX_ITERATION; i++)
            {
                _solveCollisionTasks.Clear();

                Profiler.BeginSample(name: String.Format("SolveCollisions({0}/{1})", i, CPU_COLLISION_SOLVER_MAX_ITERATION));

                foreach (var col in _collisionsInSolver)
                {
                    if (!col.skipInSolver)
                    {
                        var t = Task.Run(() =>
                        {
                            (Vector3 velAdd_a, Vector3 angVel_add_a, Vector3 velAdd_b, Vector3 angVel_add_b) = SolveCollision(col, dt);

                            if (col.rigidbody_a != null)
                            {
                                col.rigidbody_a.ExpVelocity += velAdd_a;
                                col.rigidbody_a.ExpAngularVelocity += angVel_add_a;
                            }

                            if (col.rigidbody_b != null)
                            {
                                col.rigidbody_b.ExpVelocity += velAdd_b;
                                col.rigidbody_b.ExpAngularVelocity += angVel_add_b;
                            }

                            if (velAdd_a.sqrMagnitude < CPU_SOLVER_ABORT_VELADD_SQRT && angVel_add_a.sqrMagnitude < CPU_SOLVER_ABORT_ANGVELADD_SQRT && velAdd_b.sqrMagnitude < CPU_SOLVER_ABORT_VELADD_SQRT && angVel_add_b.sqrMagnitude < CPU_SOLVER_ABORT_ANGVELADD_SQRT)
                            {
                                col.skipInSolver = true;
                            }
                        });

                        _solveCollisionTasks.Add(t);
                    }
                }

                Task.WhenAll(_solveCollisionTasks).Wait();

                Profiler.EndSample();
            }
#endif

            Profiler.EndSample();

            _collisions.Clear();
            _collisions.AddRange(_collisionsInSolver);
            _collisionsInSolver.Clear();
        }

        static SemaphoreSlim _rbcEditSemaphore = new SemaphoreSlim(1, 1);

        static void DetectCollisions(RBTrajectory traj_a, RBTrajectory traj_b, ref List<(RBCollider, RBCollider)> obb_obb_cols)
        {
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_a;
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_b;

            if (traj_a.IsStatic)
            {
                trajAABB_a = new (RBCollider, RBColliderAABB)[] { (traj_a.Collider, traj_a.Collider.CalcAABB()) };
            }
            else
            {
                trajAABB_a = traj_a.Rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
            }

            if (traj_b.IsStatic)
            {
                trajAABB_b = new (RBCollider, RBColliderAABB)[] { (traj_b.Collider, traj_b.Collider.CalcAABB()) };
            }
            else
            {
                trajAABB_b = traj_b.Rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
            }

            //AABB��x�ŏ��l�ŃR���C�_������\�[�g
            trajAABB_a = trajAABB_a.OrderBy(item => item.aabb.MinX).ToArray();
            trajAABB_b = trajAABB_b.OrderBy(item => item.aabb.MinX).ToArray();

            //�R���C�_���ɐڐG�𔻒�
            for (int i = 0; i < trajAABB_a.Length; i++)
            {
                var collider_a = trajAABB_a[i];

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
                        }
                        else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.OBB)
                        {
                            //Sphere-OBB衝突（逆転）
                        }
                        else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                        {
                            //Sphere-Sphere衝突
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

#if !COLLISION_SOLVER_HW_ACCELERATION
        static (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) SolveCollision(RBCollision col, float dt)
        {
            col.SolveVelocityConstraints(out Vector3 velocityAdd_a, out Vector3 angularVelocityAdd_a, out Vector3 velocityAdd_b, out Vector3 angularVelocityAdd_b);
            return (velocityAdd_a, angularVelocityAdd_a, velocityAdd_b, angularVelocityAdd_b);
        }
#endif

        static void VerifyVelocity(RBRigidbody rb, bool enableStaticCollision = false)
        {

        }

        public static void Dispose()
        {

#if COLLISION_NARROW_PHASE_HW_ACCELERATION
            _hwa_obb_obb_detail?.Dispose()
#endif

#if COLLISION_NARROW_PHASE_HW_ACCELERATION
            _hwa_obb_obb_detail?.Dispose()
#endif

#if COLLISION_SOLVER_HW_ACCELERATION
            _hwa_solveCollision?.Dispose();
#endif
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
        public Vector3 ContactNormal { get { return _contactNormal;  } set { _contactNormal = value.normalized; } }
        public Vector3 rA;
        public Vector3 rB;

        public bool skipInSolver;

#if COLLISION_SOLVER_HW_ACCELERATION
        HWAcceleration.HWA_SolveCollision.RBCollisionHWA _hwaData;
#else
        Jacobian _jN = new Jacobian(Jacobian.Type.Normal); //Normal
        Jacobian _jT = new Jacobian(Jacobian.Type.Tangent); //Tangent
        Jacobian _jB = new Jacobian(Jacobian.Type.Tangent); //Bi-Tangent
#endif

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

#if COLLISION_SOLVER_HW_ACCELERATION
        public HWAcceleration.HWA_SolveCollision.RBCollisionHWA HWAData { get { return _hwaData; } set { _hwaData = value; } }
#endif

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

#if COLLISION_SOLVER_HW_ACCELERATION
            _hwaData = new HWAcceleration.HWA_SolveCollision.RBCollisionHWA(this);
#endif
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

#if COLLISION_SOLVER_HW_ACCELERATION
            _hwaData = new HWAcceleration.HWA_SolveCollision.RBCollisionHWA(this);
#endif
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

#if COLLISION_SOLVER_HW_ACCELERATION

        public void UpdateHWA()
        {
            _hwaData.Update(this);
        }
#endif

        public void InitVelocityConstraint(float dt)
        {

#if COLLISION_SOLVER_HW_ACCELERATION
            _hwaData.Init(this, dt);
#else
            Vector3 contactNormal = ContactNormal;
            Vector3 tangent = Vector3.zero;
            Vector3 bitangent = Vector3.zero;

            Vector3.OrthoNormalize(ref contactNormal, ref tangent, ref bitangent);

            _jN.Init(this, contactNormal, dt);
            _jT.Init(this, tangent, dt);
            _jB.Init(this, bitangent, dt);

        }
#endif

#if !COLLISION_SOLVER_HW_ACCELERATION

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

        const float COLLISION_ERROR_SLOP = -0.0001f;

        struct Jacobian
        {
            Type _type;

            Vector3 _va;
            Vector3 _wa;
            Vector3 _vb;
            Vector3 _wb;

            float _bias;
            float _totalLambda;
            float _totalLambdaInFrame;
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
                _totalLambdaInFrame = 0;
                _effectiveMass = 0;
            }

            public void Init(RBCollision col, Vector3 dir, float dt)
            {
                Vector3 dirN = dir;

                _va = dirN;
                _wa = Vector3.Cross(col.rA, dirN);
                _vb = -dirN;
                _wb = Vector3.Cross(col.rB, -dirN);

                _bias = 0;

                if (_type == Type.Normal)
                {
                    float beta = col.collider_a.beta * col.collider_b.beta;
                    float restitution = col.collider_a.restitution * col.collider_b.restitution;
                    Vector3 relVel = Vector3.zero;
                    relVel += col.ExpVelocity_a;
                    relVel += Vector3.Cross(col.ExpAngularVelocity_a, col.rA);
                    relVel -= col.ExpVelocity_b;
                    relVel -= Vector3.Cross(col.ExpAngularVelocity_b, col.rB);

                    float closingVelocity = Vector3.Dot(relVel, dirN);
                    _bias = -(beta / dt) * Mathf.Max(0, col.penetration.magnitude - COLLISION_ERROR_SLOP) + restitution * closingVelocity;
                }

                float k = 0;
                k += col.InverseMass_a;
                k += Vector3.Dot(_wa, Vector3.Scale(col.InverseInertiaWs_a, _wa));
                k += col.InverseMass_b;
                k += Vector3.Dot(_wb, Vector3.Scale(col.InverseInertiaWs_b, _wb));

                _effectiveMass = 1 / k;
                _totalLambdaInFrame = _totalLambda;
                _totalLambda = 0;
            }

            public void Resolve(RBCollision col, ref Vector3 vAdd_a, ref Vector3 avAdd_a, ref Vector3 vAdd_b, ref Vector3 avAdd_b)
            {
                float jv = 0;
                jv += Vector3.Dot(_va, col.ExpVelocity_a + vAdd_a);
                jv += Vector3.Dot(_wa, col.ExpAngularVelocity_a + avAdd_a);
                jv += Vector3.Dot(_vb, col.ExpVelocity_b + vAdd_b);
                jv += Vector3.Dot(_wb, col.ExpAngularVelocity_b + avAdd_b);

                float lambda = _effectiveMass * (-(jv + _bias)) + _totalLambdaInFrame;

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

                _totalLambdaInFrame = 0;
            }
        }
#endif
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
        public Vector3 GetAxisForward()
        {
            return rot * Vector3.forward;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisRight()
        {
            return rot * Vector3.right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisUp()
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

    public enum RBGeometryType
    {
        OBB,
        Sphere
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

        public RBTrajectory(RBRigidbody rigidbody)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.isActiveAndEnabled)
                {
                    aabb.Encapsulate(c.CalcAABB());
                }
            }

            trajectoryAABB = aabb;
            _rigidbody = rigidbody;
            _collider = null;
            _isStatic = false;
            _isValidTrajectory = true;

            _colliders = rigidbody.GetColliders();
        }

        public RBTrajectory(RBCollider collider)
        {
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot, collider.GameObjectLossyScale);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            _colliders = new RBCollider[] { collider };
        }

        public void Update(RBRigidbody rigidbody)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.isActiveAndEnabled)
                {
                    aabb.Encapsulate(c.CalcAABB());
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
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot, collider.GameObjectLossyScale);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            _colliders = new RBCollider[] { collider };
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