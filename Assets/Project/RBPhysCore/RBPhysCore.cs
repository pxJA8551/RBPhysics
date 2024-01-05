using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysCore
    {
        static List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        static List<RBCollider> _colliders = new List<RBCollider>();

        static RBTrajectory[] _activeTrajectories = new RBTrajectory[0];
        static RBTrajectory[] _staticTrajectories = new RBTrajectory[0];

        static RBTrajectory[] _trajectories_orderByXMin = new RBTrajectory[0];

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

        public static async void SimulateFixedStep(float dt)
        {
            // ====== 物理フレームウインドウ ここまで ======

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplyTransform(dt);
            }

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
                _activeTrajectories[i] = new RBTrajectory(_rigidbodies[i], dt);
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

            //衝突検知（ブロードフェーズ）

            List<(RBTrajectory, RBTrajectory)> collideInNextFrame = new List<(RBTrajectory, RBTrajectory)>();

            {
                //AABBのx最小値で昇順ソート
                _trajectories_orderByXMin = _activeTrajectories.Concat(_staticTrajectories).ToArray();
                _trajectories_orderByXMin = _trajectories_orderByXMin.OrderBy(item => item.trajectoryAABB.GetMin().x).ToArray();

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

                            if (targetTraj.isValidTrajectory)
                            {
                                float x_min_target = targetTraj.trajectoryAABB.GetMin().x;
                                float x_max_target = targetTraj.trajectoryAABB.GetMax().x;

                                if (x_max < x_min_target)
                                {
                                    break;
                                }

                                if (RBPhysUtil.RangeOverlap(x_min, x_max, x_min_target, x_max_target))
                                {
                                    collideInNextFrame.Add((activeTraj, targetTraj));
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < collideInNextFrame.Count; i++)
                {
                    (RBTrajectory, RBTrajectory) trajPair = collideInNextFrame[i];

                    if (!trajPair.Item1.trajectoryAABB.OverlapAABB(trajPair.Item2.trajectoryAABB))
                    {
                        collideInNextFrame.RemoveAt(i);
                    }
                }
            }

            //衝突検知（ナローフェーズ）と解消
            {
                List<Task<Vector3>> penetrationTasks = new List<Task<Vector3>>();

                foreach (var trajPair in collideInNextFrame)
                {
                    //２オブジェクト間の侵入量を推定（非同期処理）
                    penetrationTasks.Add(DetectCollisions(trajPair.Item1, trajPair.Item2));
                }

                await Task.WhenAll(penetrationTasks);

                List<Task<(Vector3 velocityAdd, Vector3 angularVelocityAdd)>> velocityAddTasks = new List<Task<(Vector3, Vector3)>>();

                Vector3 p = Vector3.zero;

                int count = 0;
                foreach (var t in penetrationTasks)
                {
                    var trajPair = collideInNextFrame[count++];
                    Vector3 penetration = t.Result;

                    //推定した２オブジェクト間の侵入量から、侵入を解消するために必要な速度、角速度の加算量を解析する。（非同期処理）

                    //velocityAddTasks.Add(SolveCollisions());
                    p += penetration;
                }
            }

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ExpVelocity += new Vector3(0, -9.81f, 0) * dt;
            }
        }

        static async Task<Vector3> DetectCollisions(RBTrajectory traj_a, RBTrajectory traj_b)
        {
            List<(RBCollider, RBCollider)> collidingCollisionPair = new List<(RBCollider, RBCollider)>();
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_a;
            (RBCollider collider, RBColliderAABB aabb)[] trajAABB_b;

            Vector3 penetrationDir = Vector3.zero;

            if (traj_a.isStatic)
            {
                trajAABB_a = new (RBCollider, RBColliderAABB)[] { (traj_a.collider, traj_a.collider.CalcAABB()) };
            }
            else
            {
                trajAABB_a = traj_a.rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
                penetrationDir = traj_a.rigidbody.Velocity;
            }

            if (traj_b.isStatic)
            {
                trajAABB_b = new (RBCollider, RBColliderAABB)[] { (traj_b.collider, traj_b.collider.CalcAABB()) };
            }
            else
            {
                trajAABB_b = traj_b.rigidbody.GetColliders().Select(item => (item, item.CalcAABB())).ToArray();
                penetrationDir = traj_b.rigidbody.Velocity;
            }

            if (penetrationDir != Vector3.zero) 
            {
                //AABBのx最小値でコライダを昇順ソート
                trajAABB_a = trajAABB_a.OrderBy(item => item.aabb.GetMin().x).ToArray();
                trajAABB_b = trajAABB_b.OrderBy(item => item.aabb.GetMin().x).ToArray();

                List<Task<Vector3>> tasks = new List<Task<Vector3>>();

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

                        var t = Task.Run(() =>
                        {
                            bool aabbCollide = collider_a.aabb.OverlapAABB(collider_b.aabb);

                            if (aabbCollide)
                            {
                                bool detailCollide = false;
                                Vector3 penetration = Vector3.zero;

                                if (collider_a.collider.DetailType == RBColliderDetailType.OBB && collider_b.collider.DetailType == RBColliderDetailType.OBB)
                                {
                                    //OBB-OBB衝突
                                    detailCollide = DetectCollision(collider_a.collider.CalcOBB(), collider_b.collider.CalcOBB(), penetrationDir, out penetration);
                                }
                                else if (collider_a.collider.DetailType == RBColliderDetailType.OBB && collider_b.collider.DetailType == RBColliderDetailType.Sphere)
                                {
                                    //Sphere-OBB衝突
                                    detailCollide = DetectCollision(collider_a.collider.CalcOBB(), collider_b.collider.CalcSphere(), out penetration);
                                }
                                else if (collider_a.collider.DetailType == RBColliderDetailType.Sphere && collider_b.collider.DetailType == RBColliderDetailType.OBB)
                                {
                                    //Sphere-OBB衝突（逆転）
                                    detailCollide = DetectCollision(collider_b.collider.CalcOBB(), collider_a.collider.CalcSphere(), out penetration);
                                    penetration = -penetration;
                                }
                                else if (collider_a.collider.DetailType == RBColliderDetailType.Sphere && collider_b.collider.DetailType == RBColliderDetailType.Sphere)
                                {
                                    //Sphere-Sphere衝突
                                    detailCollide = DetectCollision(collider_a.collider.CalcSphere(), collider_b.collider.CalcSphere(), out penetration);
                                }

                                return penetration;
                            }

                            return Vector3.zero;

                        });

                        tasks.Add(t);
                    }
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);

                Vector3 pMin = Vector3.zero;
                foreach (var t in tasks)
                {
                    if (t.Result != Vector3.zero && (pMin.magnitude < t.Result.magnitude || pMin == Vector3.zero))
                    {
                        pMin = t.Result;
                    }
                }

                return pMin;
            }

            return Vector3.zero;
        }

        //OBB-OBB衝突判定
        static bool DetectCollision(RBColliderOBB obb_a, RBColliderOBB obb_b, Vector3 penetrationDir, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (obb_a.isValidOBB && obb_b.isValidOBB) 
            {
                Vector3 d = obb_a.Center - obb_b.Center;
                Vector3[] penetrations = new Vector3[6];

                Vector3 sDir_a = obb_a.rot * obb_a.size;
                Vector3 sDir_b = obb_b.rot * obb_b.size;

                //http://marupeke296.com/COL_3D_No13_OBBvsOBB.html
                {
                    Vector3 aFwdN = obb_a.GetAxisForward();
                    Vector3 aRightN = obb_a.GetAxisRight();
                    Vector3 aUpN = obb_a.GetAxisUp();
                    Vector3 bFwdN = obb_b.GetAxisForward();
                    Vector3 bRightN = obb_b.GetAxisRight();
                    Vector3 bUpN = obb_b.GetAxisUp();

                    //分離軸１: aFwd
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aFwdN));
                        float rA = Mathf.Abs(obb_a.size.z);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, aFwdN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[0] = aFwdN * dp / 2f;
                    }

                    //分離軸２: aRight
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aRightN));
                        float rA = Mathf.Abs(obb_a.size.x);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, aRightN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[1] = aRightN * dp / 2f;
                    }

                    //分離軸３: aUp
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aUpN));
                        float rA = Mathf.Abs(obb_a.size.y);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, aUpN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }


                        penetrations[2] = aUpN * dp / 2f;
                    }

                    //分離軸４: bFwd
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bFwdN));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, bFwdN);
                        float rB = Mathf.Abs(obb_b.size.z);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[3] = bFwdN * dp / 2f;
                    }

                    //分離軸５: bRight
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bRightN));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, bRightN);
                        float rB = Mathf.Abs(obb_b.size.x);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[4] = bRightN * dp / 2f;
                    }

                    //分離軸６: bUp
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bUpN));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, bUpN);
                        float rB = Mathf.Abs(obb_b.size.y);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[5] = bUpN * dp / 2f;
                    }

                    //分離軸７: aFwd x bFwd
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸８: aFwd x bRight
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸９: aFwd x bUp
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１０: aRight x bFwd
                    {
                        Vector3 p = Vector3.Cross(aRightN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１１: aRight x bRight
                    {
                        Vector3 p = Vector3.Cross(aRightN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１２: aRight x bUp
                    {
                        Vector3 p = Vector3.Cross(aRightN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１３: aUp x bFwd
                    {
                        Vector3 p = Vector3.Cross(aUpN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１４: aUp x bRight
                    {
                        Vector3 p = Vector3.Cross(aUpN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //分離軸１５: aUp x bUp
                    {
                        Vector3 p = Vector3.Cross(aUpN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = RBPhysUtil.CalcOBBAxisSize(obb_a.size, obb_a.rot, p);
                        float rB = RBPhysUtil.CalcOBBAxisSize(obb_b.size, obb_b.rot, p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    penetration = penetrationDir * penetrations.Select(item => item.magnitude * (1f / Vector3.Dot(penetrationDir.normalized, item.normalized))).Min();

                    return true;
                }
            }

            return false;
        }

        //OBB-Sphere衝突判定
        static bool DetectCollision(RBColliderOBB obb_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (obb_a.isValidOBB && sphere_b.isValidSphere)
            {

            }

            return false;
        }

        //Sphere-Sphere衝突判定
        static bool DetectCollision(RBColliderSphere sphere_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (sphere_a.isValidSphere && sphere_b.isValidSphere)
            {

            }

            return false;
        }

        static Task<(Vector3 velocityAcc, Vector3 angularVelocityAcc)> SolveCollisions()
        {
            return null;
        }

        static void VerifyVelocity(RBRigidbody rb)
        {

        }
    }

    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _parent; } }
        public abstract RBColliderDetailType DetailType { get; }

        public Vector3 Pos { get; private set; }
        public Quaternion Rot { get; private set; }
        public Vector3 LossyScale { get; private set; }

        void Awake()
        {
            RBPhysCore.AddCollider(this);
        }

        void OnDestroy()
        {
            RBPhysCore.RemoveCollider(this);
        }

        public void SetParentRigidbody(RBRigidbody r)
        {
            if (r != null)
            {
                _parent = r;
            }
        }

        public void ClearParentRigidbody()
        {
            _parent = null;
        }

        public void UpdateTransform()
        {
            Pos = gameObject.transform.position;
            Rot = gameObject.transform.rotation; 
            LossyScale = gameObject.transform.lossyScale; 
        }

        public RBRigidbody GetParentRigidbody()
        {
            return _parent;
        }

        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot);

        public virtual RBColliderSphere CalcSphere()
        {
            return CalcSphere(Pos, Rot);
        }

        public virtual RBColliderAABB CalcAABB()
        {
            return CalcAABB(Pos, Rot);
        }

        public virtual RBColliderOBB CalcOBB()
        {
            return CalcOBB(Pos, Rot);
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

        public Vector3 GetMin()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Min(p, q);
        }

        public Vector3 GetMax()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Max(p, q);
        }

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

        public bool ContainsPoint(Vector3 point)
        {
            return isValidAABB && RBPhysUtil.IsV3Less(GetMin(), point) && RBPhysUtil.IsV3Less(point, GetMax());
        }

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

        public Vector3 GetAxisForward()
        {
            return rot * Vector3.forward;
        }

        public Vector3 GetAxisRight()
        {
            return rot * Vector3.right;
        }

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

    public enum RBColliderDetailType
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

        public RBTrajectory(RBRigidbody rigidbody, float dt)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            Vector3 pos = rigidbody.Position;
            Quaternion rot = rigidbody.Rotation;

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
        }

        public RBTrajectory(RBCollider collider)
        {
            trajectoryAABB = collider.CalcAABB(collider.transform.position, collider.transform.rotation);
            this.rigidbody = null;
            this.collider = collider;
            isStatic = true;
            isValidTrajectory = true;
        }
    }
}