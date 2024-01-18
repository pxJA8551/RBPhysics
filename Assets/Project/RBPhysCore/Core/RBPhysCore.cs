using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static RBPhys.RBColliderCollision;

namespace RBPhys
{
    public static class RBPhysCore
    {
        public const int COLLIDER_SOLVER_ITERATION = 6;
        public const int DEFAULT_SOLVER_ITERATION = 6;

        static List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        static List<RBCollider> _colliders = new List<RBCollider>();

        static RBTrajectory[] _activeTrajectories = new RBTrajectory[0];
        static RBTrajectory[] _staticTrajectories = new RBTrajectory[0];

        static RBTrajectory[] _trajectories_orderByXMin = new RBTrajectory[0];

        static IEnumerable<RBCollision > _collisions = new List<RBCollision>();
        static List<RBCollision> _collisionsInFrame = new List<RBCollision>();

        static List<Task<(bool collide, RBCollision[] cols, Vector3 velAcc_a, Vector3 angVelAcc_a, Vector3 velAcc_b, Vector3 angVelAcc_b)>> _solveCollisionTasks = new List<Task<(bool collide, RBCollision[] cols, Vector3 velAcc_a, Vector3 angVelAcc_a, Vector3 velAcc_b, Vector3 angVelAcc_b)>>();

        public static void AddRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Add(rb);
        }

        public static void RemoveRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Remove(rb);
        }

        public static void AddCollider(RBCollider c)
        {
            _colliders.Add(c);
        }

        public static void RemoveCollider(RBCollider c)
        {
            _colliders.Remove(c);
        }

        public static void OpenPhysicsFrameWindow(float dt)
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform();
            }

            foreach (RBCollider c in _colliders)
            {
                c.UpdateTransform();
            }

            _colliders.ForEach(item => item.UpdateTransform());

            // ====== 物理フレームウインドウ ここから ======

            //動的・静的軌道計算

            if (_activeTrajectories.Length != _rigidbodies.Count)
            {
                _activeTrajectories = new RBTrajectory[_rigidbodies.Count];
            }

            if (_staticTrajectories.Length != _colliders.Count)
            {
                _staticTrajectories = new RBTrajectory[_colliders.Count];
            }

            for (int i = 0; i < _rigidbodies.Count; i++)
            {
                _activeTrajectories[i] = new RBTrajectory(_rigidbodies[i]);
            }

            for (int i = 0; i < _colliders.Count; i++)
            {
                if (_colliders[i].GetParentRigidbody() == null)
                {
                    _staticTrajectories[i] = new RBTrajectory(_colliders[i]);
                }
                else
                {
                    _staticTrajectories[i] = new RBTrajectory();
                }
            }

            SolveColliders(dt);

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ExpVelocity += new Vector3(0, -9.81f, 0) * dt;
            }

            //OnClosePhysicsFrameへ
        }

        public static void ClosePhysicsFrameWindow(float dt)
        {
            //FixedUpdate終了時に実行

            // ====== 物理フレームウインドウ ここまで ======

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplyTransform(dt);
            }
        }

        public static async void SolveColliders(float dt)
        {
            //衝突検知（ブロードフェーズ）

            List<(RBTrajectory, RBTrajectory)> collideInNextFrame = new List<(RBTrajectory, RBTrajectory)>();

            {
                //AABBのx最小値で昇順ソート
                _trajectories_orderByXMin = _activeTrajectories.Concat(_staticTrajectories).ToArray();
                _trajectories_orderByXMin = _trajectories_orderByXMin.OrderBy(item => item.trajectoryAABB.GetMin().x).ToArray();

                List<(Task<bool> task, RBTrajectory traj_a, RBTrajectory traj_b)> aabbTasks = new List<(Task<bool> task, RBTrajectory traj_a, RBTrajectory traj_b)>();

                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    RBTrajectory activeTraj = _trajectories_orderByXMin[i];

                    if (activeTraj.isValidTrajectory)
                    {
                        float x_min = activeTraj.trajectoryAABB.GetMin().x;
                        float x_max = activeTraj.trajectoryAABB.GetMax().x;

                        for (int j = i + 1; j < _trajectories_orderByXMin.Length; j++)
                        {
                            RBTrajectory targetTraj = _trajectories_orderByXMin[j];

                            if (!activeTraj.isStatic || !targetTraj.isStatic)
                            {
                                if (targetTraj.isValidTrajectory)
                                {
                                    float x_min_target = targetTraj.trajectoryAABB.GetMin().x;
                                    float x_max_target = targetTraj.trajectoryAABB.GetMax().x;

                                    if (x_max < x_min_target)
                                    {
                                        break;
                                    }

                                    aabbTasks.Add((Task.Run(() => RBPhysUtil.RangeOverlap(x_min, x_max, x_min_target, x_max_target)), activeTraj, targetTraj));
                                }
                            }
                        }
                    }
                }

                Task.WhenAll(aabbTasks.Select(item => item.task)); //同期処理

                foreach (var t in aabbTasks)
                {
                    collideInNextFrame.Add((t.traj_a, t.traj_b));
                }

                for (int i = 0; i < collideInNextFrame.Count; i++)
                {
                    (RBTrajectory, RBTrajectory) trajPair = collideInNextFrame[i];

                    if (!trajPair.Item1.trajectoryAABB.OverlapAABB(trajPair.Item2.trajectoryAABB))
                    {
                        collideInNextFrame.RemoveAt(i);
                        i--;
                    }
                }
            }

            //衝突検知（ナローフェーズ）と解消

            for (int i = 0; i < COLLIDER_SOLVER_ITERATION; i++)
            {
                _solveCollisionTasks.Clear();

                foreach (var trajPair in collideInNextFrame)
                {
                    //２オブジェクト間の侵入を解消（非同期処理）
                    _solveCollisionTasks.Add(SolveCollisions(trajPair.Item1, trajPair.Item2, dt));
                }

                Task.WhenAll(_solveCollisionTasks); // 同期処理

                foreach (var t in _solveCollisionTasks)
                {
                    var r = t.Result;

                    if (r.collide & r.cols.Any())
                    {
                        RBCollision collision = r.cols[0];
                        Vector3 velocityAcc_a = r.velAcc_a;
                        Vector3 angularVelocityAcc_a = r.angVelAcc_a;
                        Vector3 velocityAcc_b = r.velAcc_b;
                        Vector3 angularVelocityAcc_b = r.angVelAcc_b;

                        //Debug.Log(velocityAcc_a);
                        //Debug.Log(angularVelocityAcc_a);
                        //Debug.Log(velocityAcc_b);
                        //Debug.Log(angularVelocityAcc_b);
                        //Debug.Log((velocityAcc_a, angularVelocityAcc_a, velocityAcc_b, angularVelocityAcc_b));

                        if (collision.rigidbody_a != null)
                        {
                            collision.rigidbody_a.ExpVelocity += velocityAcc_a;
                            collision.rigidbody_a.ExpAngularVelocity += angularVelocityAcc_a;
                        }

                        if (collision.rigidbody_b != null)
                        {
                            collision.rigidbody_b.ExpVelocity += velocityAcc_b;
                            collision.rigidbody_b.ExpAngularVelocity += angularVelocityAcc_b;
                        }

                        _collisionsInFrame.AddRange(r.cols);
                    }
                }
                _collisions = _collisionsInFrame.ToArray();
                _collisionsInFrame.Clear();
            }
        }

        static async Task<(bool collide, RBCollision[] cols, Vector3 vel_a, Vector3 angVel_a, Vector3 vel_b, Vector3 angVel_b)> SolveCollisions(RBTrajectory traj_a, RBTrajectory traj_b, float dt)
        {
            List<RBCollision> collisions;

            collisions = await DetectCollisions(traj_a, traj_b).ConfigureAwait(false);

            List<Task<(bool collide, RBCollision col, Vector3 vel_a, Vector3 angVel_a, Vector3 vel_b, Vector3 angVel_b)>> solveCollisionTasks = new List<Task<(bool collide, RBCollision col, Vector3 vel_a, Vector3 angVel_a, Vector3 vel_b, Vector3 angVel_b)>>();

            foreach (var rbc in collisions)
            {
                if (rbc != null)
                {
                    var task = Task.Run(() =>
                    {
                        if (rbc.penetration != Vector3.zero)
                        {
                            var p = GetNearestDistAsync(rbc.collider_a, rbc.collider_b, rbc.cg_a, rbc.cg_b, rbc.penetration).Result;

                            float d = p.dist;
                            Vector3 aNearest = p.aNearest;
                            Vector3 bNearest = p.bNearest;

                            rbc.Update(rbc.penetration, -p.pDir, aNearest, bNearest);
                            rbc.InitVelocityConstraint(dt);

                            if (0 <= d)
                            {
                                var v = SolveCollision(rbc, dt);
                                return (true, rbc, v.velAdd_a, v.angVelAdd_a, v.velAdd_b, v.angVelAdd_b);
                            }
                        }

                        return (false, null, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);
                    });

                    solveCollisionTasks.Add(task);
                }
            }

            await Task.WhenAll(solveCollisionTasks).ConfigureAwait(false);

            List<RBCollision> colsNew = new List<RBCollision>();
            Vector3 vAdd_a = Vector3.zero;
            Vector3 avAdd_a = Vector3.zero;
            Vector3 vAdd_b = Vector3.zero;
            Vector3 avAdd_b = Vector3.zero;

            foreach (var t in solveCollisionTasks)
            {
                var p = t.Result;
                if (p.collide)
                {
                    colsNew.Add(p.col);
                    vAdd_a += p.vel_a;
                    avAdd_a += p.angVel_a;
                    vAdd_b += p.vel_b;
                    avAdd_b += p.angVel_b;
                }
            }

            return (colsNew.Any(), colsNew.ToArray(), vAdd_a, avAdd_a, vAdd_b, avAdd_b);
        }

        static async Task<List<RBCollision>> DetectCollisions(RBTrajectory traj_a, RBTrajectory traj_b)
        {
            List<RBCollision> ret = new List<RBCollision>();

            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_a;
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_b;

            if (traj_a.isStatic)
            {
                trajAABB_a = new (RBCollider, RBColliderAABB)[] { (traj_a.collider, traj_a.collider.CalcAABB()) };
            }
            else
            {
                trajAABB_a = traj_a.rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
            }

            if (traj_b.isStatic)
            {
                trajAABB_b = new (RBCollider, RBColliderAABB)[] { (traj_b.collider, traj_b.collider.CalcAABB()) };
            }
            else
            {
                trajAABB_b = traj_b.rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
            }

            //AABBのx最小値でコライダを昇順ソート
            trajAABB_a = trajAABB_a.OrderBy(item => item.aabb.GetMin().x).ToArray();
            trajAABB_b = trajAABB_b.OrderBy(item => item.aabb.GetMin().x).ToArray();

            List<Task<RBCollision>> tasks = new List<Task<RBCollision>>();

            //コライダ毎に接触を判定
            for (int i = 0; i < trajAABB_a.Length; i++)
            {
                var collider_a = trajAABB_a[i];

                float a_x_min = collider_a.aabb.GetMin().x;
                float a_x_max = collider_a.aabb.GetMax().x;

                for (int j = 0; j < trajAABB_b.Length; j++)
                {
                    var collider_b = trajAABB_b[j];

                    float b_x_min = collider_b.aabb.GetMin().x;
                    float b_x_max = collider_b.aabb.GetMax().x;

                    if (b_x_max < a_x_min)
                    {
                        continue;
                    }

                    if (a_x_max < b_x_min)
                    {
                        break;
                    }

                    Vector3 penetrationDir = Vector3.zero;

                    RBCollision rbc = FindCollision(traj_a, traj_b, collider_a.collider, collider_b.collider);
                    if (rbc != null)
                    {
                        penetrationDir = rbc.ContactNormal;
                    }

                    var t = Task.Run(() =>
                    {
                        bool aabbCollide = collider_a.aabb.OverlapAABB(collider_b.aabb);

                        Vector3 penetration = Vector3.zero;

                        if (aabbCollide)
                        {
                            bool detailCollide = false;

                            if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.OBB)
                            {
                                //OBB-OBB衝突
                                detailCollide = RBColliderCollision.DetectCollision(collider_a.collider.CalcOBB(), collider_b.collider.CalcOBB(), penetrationDir, out Vector3 p);
                                penetration = p;
                            }
                            else if (collider_a.collider.GeometryType == RBGeometryType.OBB && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                            {
                                //Sphere-OBB衝突
                                detailCollide = RBColliderCollision.DetectCollision(collider_a.collider.CalcOBB(), collider_b.collider.CalcSphere(), out Vector3 p);
                                penetration = p;
                            }
                            else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.OBB)
                            {
                                //Sphere-OBB衝突（逆転）
                                detailCollide = RBColliderCollision.DetectCollision(collider_b.collider.CalcOBB(), collider_a.collider.CalcSphere(), out Vector3 p);
                                p = -p;
                                penetration = p;
                            }
                            else if (collider_a.collider.GeometryType == RBGeometryType.Sphere && collider_b.collider.GeometryType == RBGeometryType.Sphere)
                            {
                                //Sphere-Sphere衝突
                                detailCollide = RBColliderCollision.DetectCollision(collider_a.collider.CalcSphere(), collider_b.collider.CalcSphere(), out Vector3 p);
                                penetration = p;
                            }

                            if (rbc == null && penetration != Vector3.zero)
                            {
                                rbc = new RBCollision(traj_a, collider_a.collider, traj_b, collider_b.collider, penetration);
                            }

                            if (rbc != null)
                            {
                                rbc.penetration = penetration;
                            }

                            return rbc;
                        }

                        return null;
                    });

                    tasks.Add(t);
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            foreach (var t in tasks)
            {
                ret.Add(t.Result);
            }

            return ret;
        }

        static RBCollision FindCollision(RBTrajectory traj_a, RBTrajectory traj_b, RBCollider col_a, RBCollider col_b)
        {
            if (!traj_a.isStatic && !traj_b.isStatic)
            {
                var ab = (traj_a.rigidbody, traj_b.rigidbody);
                var ba = (traj_b.rigidbody, traj_a.rigidbody);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.rigidbody_a, r.rigidbody_b) == ab || (r.rigidbody_a, r.rigidbody_b) == ba)
                    {
                        return r;
                    }
                }
            }
            else if (traj_a.isStatic && !traj_b.isStatic)
            {
                var ab = (col_a, traj_b.rigidbody);
                var ba = (traj_b.rigidbody, col_a);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.collider_a, r.rigidbody_b) == ab || (r.rigidbody_a, r.collider_b) == ba)
                    {
                        return r;
                    }
                }
            }
            else if (!traj_a.isStatic && traj_b.isStatic)
            {
                var ab = (traj_a.rigidbody, col_b);
                var ba = (col_b, traj_a.rigidbody);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.rigidbody_a, r.collider_b) == ab || (r.collider_a, r.rigidbody_b) == ba)
                    {
                        return r;
                    }
                }
            }

            return null;
        }

        static RBCollision FindCollision(RBTrajectory traj_a, RBTrajectory traj_b)
        {
            if (!traj_a.isStatic && !traj_b.isStatic)
            {
                var ab = (traj_a.rigidbody, traj_b.rigidbody);
                var ba = (traj_b.rigidbody, traj_a.rigidbody);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.rigidbody_a, r.rigidbody_b) == ab || (r.rigidbody_a, r.rigidbody_b) == ba)
                    {
                        return r;
                    }
                }
            }
            else if (traj_a.isStatic && !traj_b.isStatic)
            {
                var ab = (traj_a.collider, traj_b.rigidbody);
                var ba = (traj_b.rigidbody, traj_a.collider);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.collider_a, r.rigidbody_b) == ab || (r.rigidbody_a, r.collider_b) == ba)
                    {
                        return r;
                    }
                }
            }
            else if (!traj_a.isStatic && traj_b.isStatic)
            {
                var ab = (traj_a.rigidbody, traj_a.collider);
                var ba = (traj_a.collider, traj_a.rigidbody);

                foreach (RBCollision r in _collisions)
                {
                    if ((r.rigidbody_a, r.collider_b) == ab || (r.collider_a, r.rigidbody_b) == ba)
                    {
                        return r;
                    }
                }
            }

            return null;
        }

        static (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) SolveCollision(RBCollision col, float dt)
        {
            col.SolveVelocityConstraints(dt, out Vector3 velocityAdd_a, out Vector3 angularVelocityAdd_a, out Vector3 velocityAdd_b, out Vector3 angularVelocityAdd_b);
            return (velocityAdd_a, angularVelocityAdd_a, velocityAdd_b, angularVelocityAdd_b);
        }

        static void VerifyVelocity(RBRigidbody rb, bool enableStaticCollision = false)
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
        public Vector3 ContactNormal { get { return _contactNormal;  } set { _contactNormal = value.normalized; } }
        public Vector3 rA;
        public Vector3 rB;

        Jacobian _jN; //Normal
        Jacobian _jT; //Tangent
        Jacobian _jB; //Bi-Tangent

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

        Vector3 _contactNormal;

        public RBCollision(RBTrajectory traj_a, RBCollider col_a, RBTrajectory traj_b, RBCollider col_b, Vector3 penetration)
        {
            collider_a = col_a;
            rigidbody_a = traj_a.rigidbody;
            collider_b = col_b;
            rigidbody_b = traj_b.rigidbody;

            cg_a = traj_a.isStatic ? col_a.GetColliderCenter() : traj_a.rigidbody.CenterOfGravityWorld;
            cg_b = traj_b.isStatic ? col_b.GetColliderCenter() : traj_b.rigidbody.CenterOfGravityWorld;

            this.penetration = penetration;
            ContactNormal = (traj_b.isStatic ? Vector3.zero : traj_b.rigidbody.Velocity) - (traj_a.isStatic ? Vector3.zero : traj_a.rigidbody.Velocity);
        }

        public void Update(Vector3 penetration, Vector3 contactNormal, Vector3 aNearest, Vector3 bNearest)
        {
            this.aNearest = aNearest;
            this.bNearest = bNearest;

            this.penetration = penetration;
            ContactNormal = contactNormal;

            cg_a = rigidbody_a?.CenterOfGravityWorld ?? collider_a.GetColliderCenter();
            cg_b = rigidbody_b?.CenterOfGravityWorld ?? collider_b.GetColliderCenter();

            rA = aNearest - cg_a;
            rB = bNearest - cg_b;
        }

        public void InitVelocityConstraint(float dt)
        {
            _jN = new Jacobian(Jacobian.Type.Normal);
            _jT = new Jacobian(Jacobian.Type.Tangent);
            _jB = new Jacobian(Jacobian.Type.Tangent);

            Vector3 contactNormal = ContactNormal;
            Vector3 tangent = Vector3.zero;
            Vector3 bitangent = Vector3.zero;

            Vector3.OrthoNormalize(ref contactNormal, ref tangent, ref bitangent);

            _jN.Init(this, contactNormal, dt);
            _jT.Init(this, tangent, dt);
            _jB.Init(this, bitangent, dt);
        }

        public void SolveVelocityConstraints(float dt, out Vector3 vAdd_a, out Vector3 avAdd_a, out Vector3 vAdd_b, out Vector3 avAdd_b)
        {
            vAdd_a = Vector3.zero;
            avAdd_a = Vector3.zero;
            vAdd_b = Vector3.zero;
            avAdd_b = Vector3.zero;

            _jN.Resolve(this, dt, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
            _jT.Resolve(this, dt, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
            _jB.Resolve(this, dt, ref vAdd_a, ref avAdd_a, ref vAdd_b, ref avAdd_b);
        }

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

            public void Init(RBCollision col, Vector3 dir, float dt)
            {
                Vector3 pDirN = dir;

                _va = pDirN;
                _wa = Vector3.Cross(col.rA, pDirN);
                _vb = -pDirN;
                _wb = Vector3.Cross(col.rB, -pDirN);

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

                    float closingVelocity = Vector3.Dot(relVel, pDirN);
                    _bias = -(beta / dt) * col.penetration.magnitude + restitution * closingVelocity;
                }

                float k = 0;
                k += col.InverseMass_a;
                k += Vector3.Dot(_wa, Vector3.Scale(col.InverseInertiaWs_a, _wa));
                k += col.InverseMass_b;
                k += Vector3.Dot(_wb, Vector3.Scale(col.InverseInertiaWs_b, _wb));

                _effectiveMass = 1 / k;
                _totalLambda = 0;
            }

            public void Resolve(RBCollision col, float dt, ref Vector3 vAdd_a, ref Vector3 avAdd_a, ref Vector3 vAdd_b, ref Vector3 avAdd_b)
            {
                float jv = 0;
                jv += Vector3.Dot(_va, col.ExpVelocity_a + vAdd_a);
                jv += Vector3.Dot(_wa, col.ExpAngularVelocity_a + avAdd_a);
                jv += Vector3.Dot(_vb, col.ExpVelocity_b + vAdd_b);
                jv += Vector3.Dot(_wb, col.ExpAngularVelocity_b + avAdd_b);

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

        public RBColliderAABB(Vector3 center, Vector3 size)
        {
            isValidAABB = true;
            this.Center = center;
            this.Size = RBPhysUtil.V3Abs(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetMin()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Min(p, q);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetMax()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Max(p, q);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(Vector3 point)
        {
            if (isValidAABB)
            {
                if (!ContainsPoint(point))
                {
                    Vector3 res_min = Vector3.Min(GetMin(), point);
                    Vector3 res_max = Vector3.Max(GetMax(), point);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;
                }
            }
            else
            {
                Center = point;
                Size = Vector3.zero;
                isValidAABB = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(RBColliderAABB aabb)
        {
            if (aabb.isValidAABB)
            {
                if (isValidAABB)
                {
                    Vector3 res_min = Vector3.Min(GetMin(), aabb.GetMin());
                    Vector3 res_max = Vector3.Max(GetMax(), aabb.GetMax());

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;
                }
                else
                {
                    Center = aabb.Center;
                    Size = aabb.Size;
                    isValidAABB = true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsPoint(Vector3 point)
        {
            return isValidAABB && RBPhysUtil.IsV3Less(GetMin(), point) && RBPhysUtil.IsV3Less(point, GetMax());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool OverlapAABB(RBColliderAABB ext)
        {
            if (isValidAABB && ext.isValidAABB)
            {
                Vector3 min = GetMin();
                Vector3 max = GetMax();
                Vector3 extMin = ext.GetMin();
                Vector3 extMax = ext.GetMax();

                if (!RBPhysUtil.RangeOverlap(min.x, max.x, extMin.x, extMax.x)) return false;
                if (!RBPhysUtil.RangeOverlap(min.y, max.y, extMin.y, extMax.y)) return false;
                if (!RBPhysUtil.RangeOverlap(min.z, max.z, extMin.z, extMax.z)) return false;

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

        public Vector3 Center { get { return pos + rot * size / 2f; } }

        public RBColliderOBB(Vector3 pos, Quaternion rot, Vector3 size)
        {
            this.pos = pos;
            this.rot = rot;
            this.size = size;
            isValidOBB = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetAxisSize(Vector3 axis)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, size.z), axis));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(size.x, 0, 0), axis));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, size.y, 0), axis));
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3[] GetVertices()
        {
            Vector3 xyz = pos + rot * new Vector3(0, 0, 0);
            Vector3 Xyz = pos + rot * new Vector3(size.x, 0, 0);
            Vector3 xYz = pos + rot * new Vector3(0, size.y, 0);
            Vector3 XYz = pos + rot * new Vector3(size.x, size.y, 0);
            Vector3 xyZ = pos + rot * new Vector3(0, 0, size.z);
            Vector3 XyZ = pos + rot * new Vector3(size.x, 0, size.z);
            Vector3 xYZ = pos + rot * new Vector3(0, size.y, size.z);
            Vector3 XYZ = pos + rot * new Vector3(size.x, size.y, size.z);

            return new Vector3[] { xyz, Xyz, xYz, XYz, xyZ, XyZ, xYZ, XYZ };
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

    public struct RBTrajectory
    {
        public RBColliderAABB trajectoryAABB;

        public bool isValidTrajectory;

        public readonly RBRigidbody rigidbody;
        public readonly bool isStatic;

        public readonly RBCollider collider;

        public readonly RBCollider[] colliders;

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
            this.rigidbody = rigidbody;
            this.collider = null;
            isStatic = false;
            isValidTrajectory = true;

            colliders = rigidbody.GetColliders();
        }

        public RBTrajectory(RBCollider collider)
        {
            trajectoryAABB = collider.CalcAABB(collider.GameObjectPos, collider.GameObjectRot, collider.GameObjectLossyScale);
            this.rigidbody = null;
            this.collider = collider;
            isStatic = true;
            isValidTrajectory = true;

            colliders = new RBCollider[] { collider };
        }
    }
}