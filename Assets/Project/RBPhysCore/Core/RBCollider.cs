using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _parent; } }
        public abstract RBGeometryType GeometryType { get; }

        public Vector3 GameObjectPos { get; private set; }
        public Quaternion GameObjectRot { get; private set; }
        public Vector3 GameObjectLossyScale { get; private set; }

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
            GameObjectPos = gameObject.transform.position;
            GameObjectRot = gameObject.transform.rotation;
            GameObjectLossyScale = gameObject.transform.lossyScale;
        }

        public RBRigidbody GetParentRigidbody()
        {
            return _parent;
        }

        public abstract float CalcVolume(Vector3 scale);
        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, Vector3 scale);

        public virtual float CalcVolume()
        {
            return CalcVolume(GameObjectLossyScale);
        }

        public virtual RBColliderSphere CalcSphere()
        {
            return CalcSphere(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public virtual RBColliderAABB CalcAABB()
        {
            return CalcAABB(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public virtual RBColliderOBB CalcOBB()
        {
            return CalcOBB(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public abstract Vector3 GetColliderCenter(Vector3 pos, Quaternion rot, Vector3 scale);

        public virtual Vector3 GetColliderCenter()
        {
            return GetColliderCenter(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }
    }

    public static class RBColliderCollision
    {
        //OBB-OBBè’ìÀîªíË
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DetectCollision(RBColliderOBB obb_a, RBColliderOBB obb_b, Vector3 penetrationDir, out Vector3 penetration)
        {
            penetrationDir = Vector3.Normalize(penetrationDir);

            Vector3[] penetrations = new Vector3[6];
            penetration = Vector3.zero;

            if (obb_a.isValidOBB && obb_b.isValidOBB)
            {
                Vector3 d = obb_a.Center - obb_b.Center;

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

                    //ï™ó£é≤ÇP: aFwd
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aFwdN));
                        float rA = Mathf.Abs(obb_a.size.z);
                        float rB = obb_b.GetAxisSize(aFwdN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[0] = aFwdN * dp / 2f;
                    }

                    //ï™ó£é≤ÇQ: aRight
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aRightN));
                        float rA = Mathf.Abs(obb_a.size.x);
                        float rB = obb_b.GetAxisSize(aRightN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[1] = aRightN * dp / 2f;
                    }

                    //ï™ó£é≤ÇR: aUp
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, aUpN));
                        float rA = Mathf.Abs(obb_a.size.y);
                        float rB = obb_b.GetAxisSize(aUpN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }


                        penetrations[2] = aUpN * dp / 2f;
                    }

                    //ï™ó£é≤ÇS: bFwd
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bFwdN));
                        float rA = obb_a.GetAxisSize(bFwdN);
                        float rB = Mathf.Abs(obb_b.size.z);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[3] = bFwdN * dp / 2f;
                    }

                    //ï™ó£é≤ÇT: bRight
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bRightN));
                        float rA = obb_a.GetAxisSize(bRightN);
                        float rB = Mathf.Abs(obb_b.size.x);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[4] = bRightN * dp / 2f;
                    }

                    //ï™ó£é≤ÇU: bUp
                    {
                        float prjL = Mathf.Abs(Vector3.Dot(d, bUpN));
                        float rA = obb_a.GetAxisSize(bUpN);
                        float rB = Mathf.Abs(obb_b.size.y);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        penetrations[5] = bUpN * dp / 2f;
                    }

                    //ï™ó£é≤ÇV: aFwd x bFwd
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇW: aFwd x bRight
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇX: aFwd x bUp
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇO: aRight x bFwd
                    {
                        Vector3 p = Vector3.Cross(aRightN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇP: aRight x bRight
                    {
                        Vector3 p = Vector3.Cross(aRightN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇQ: aRight x bUp
                    {
                        Vector3 p = Vector3.Cross(aRightN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇR: aUp x bFwd
                    {
                        Vector3 p = Vector3.Cross(aUpN, bFwdN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇS: aUp x bRight
                    {
                        Vector3 p = Vector3.Cross(aUpN, bRightN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    //ï™ó£é≤ÇPÇT: aUp x bUp
                    {
                        Vector3 p = Vector3.Cross(aUpN, bUpN);

                        float prjL = Mathf.Abs(Vector3.Dot(d, p));
                        float rA = obb_a.GetAxisSize(p);
                        float rB = obb_b.GetAxisSize(p);

                        if (prjL > rA + rB)
                        {
                            return false;
                        }
                    }

                    penetration = penetrationDir * penetrations
                        .Select(item => Mathf.Abs(item.magnitude * (1f / Vector3.Dot(penetrationDir, item.normalized))))
                        .Where(item => !float.IsNaN(item) && !float.IsInfinity(item))
                        .Min();

                    return true;
                }
            }

            return false;
        }

        //OBB-Sphereè’ìÀîªíË
        public static bool DetectCollision(RBColliderOBB obb_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (obb_a.isValidOBB && sphere_b.isValidSphere)
            {

            }

            return false;
        }

        //Sphere-Sphereè’ìÀîªíË
        public static bool DetectCollision(RBColliderSphere sphere_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (sphere_a.isValidSphere && sphere_b.isValidSphere)
            {
                
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<(float dist, Vector3 aNearest, Vector3 bNearest)> GetNearestDistAsync(RBCollider collider_a, RBCollider collider_b, Vector3 cg_a, Vector3 cg_b, Vector3 penetration)
        {
            //OBB-OBB
            if (collider_a.GeometryType == RBGeometryType.OBB && collider_b.GeometryType == RBGeometryType.OBB)
            {
                RBColliderOBB obb_a = collider_a.CalcOBB();
                RBColliderOBB obb_b = collider_b.CalcOBB();

                obb_a.pos -= penetration; //ìKìñÇ»ï˚å¸Ç…è’ìÀÇâè¡ÇµÇΩOBBÇ≈åvéZÇ∑ÇÈÇΩÇﬂÇ…OBBÇÃà íuÇïœçX

                var t_ab = GetNearestDistAsync(obb_a, obb_b, penetration, cg_b);
                var t_ba = GetNearestDistAsync(obb_b, obb_a, -penetration, cg_a);

                await Task.WhenAll(t_ab, t_ba).ConfigureAwait(false);

                var nearestAB = t_ab.Result;
                var nearestBA = t_ba.Result;

                if (nearestAB.dist > 0 && (nearestAB.dist < nearestBA.dist || nearestBA.dist <= 0))
                {
                    Vector3 aNearest = nearestAB.an + penetration;
                    Vector3 bNearest = nearestAB.bn;
                    return (Vector3.Distance(aNearest, bNearest), aNearest, bNearest);
                }
                else
                {
                    if (nearestBA.dist > 0)
                    {
                        Vector3 aNearest = nearestBA.an + penetration;
                        Vector3 bNearest = nearestBA.bn;
                        return (Vector3.Distance(aNearest, bNearest), aNearest, bNearest);
                    }
                }
            }

            return (0, Vector3.zero, Vector3.zero);
        }

        //OBB-OBBç≈ãﬂì_îªíË
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<(float dist, Vector3 an, Vector3 bn)> GetNearestDistAsync(RBColliderOBB obb_a, RBColliderOBB obb_b, Vector3 penetration, Vector3 cg)
        {
            Vector3 normal_a_x = obb_a.GetAxisRight();
            Vector3 normal_a_y = obb_a.GetAxisUp();
            Vector3 normal_a_z = obb_a.GetAxisForward();

            Vector3[] obb_a_verts = obb_a.GetVertices();

            List<Task<(float dist, Vector3 aNearest, Vector3 bNearest, bool faceParallel)>> nearests = new List<Task<(float dist, Vector3 aNearest, Vector3 bNearest, bool faceParallel)>>();

            if (Vector3.Dot(penetration, normal_a_x) < 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[5], obb_a_verts[7], obb_a_verts[3], obb_a_verts[1] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y, obb_a.size.z / 2f));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, obb_a.size.z));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_x, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            if (Vector3.Dot(penetration, -normal_a_x) < 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[0], obb_a_verts[2], obb_a_verts[6], obb_a_verts[4] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(0, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y, obb_a.size.z / 2f));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, obb_a.size.z));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_x, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            if (Vector3.Dot(penetration, normal_a_y) < 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[2], obb_a_verts[6], obb_a_verts[7], obb_a_verts[3] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, obb_a.size.z));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y, obb_a.size.z / 2f));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_y, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            if (Vector3.Dot(penetration, -normal_a_y) < 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[1], obb_a_verts[5], obb_a_verts[4], obb_a_verts[0] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, obb_a.size.z));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, 0, obb_a.size.z / 2f));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_y, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            if (Vector3.Dot(penetration, normal_a_z) > 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[0], obb_a_verts[2], obb_a_verts[3], obb_a_verts[1] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, 0));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, 0));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_z, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            if (Vector3.Dot(penetration, -normal_a_z) < 0)
            {
                var t = Task.Run(() =>
                {
                    Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[5], obb_a_verts[7], obb_a_verts[6], obb_a_verts[4] };
                    (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, obb_a.size.z), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, obb_a.size.z));
                    (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, obb_a.size.z), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, obb_a.size.z));
                    float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_z, cg, obb_b, out Vector3 an, out Vector3 bn, out bool faceParallel);

                    if (dist > 0)
                    {
                        return (dist, an, bn, faceParallel);
                    }

                    return (-1, Vector3.zero, Vector3.zero, false);
                });

                nearests.Add(t);
            }

            await Task.WhenAll(nearests).ConfigureAwait(false);

            (float dist, Vector3 aNearest, Vector3 bNearest) nearest = (-1, Vector3.zero, Vector3.zero);

            foreach (var t in nearests)
            {
                var p = t.Result;

                if (p.faceParallel)
                {
                    return (p.dist, p.aNearest, p.bNearest);
                }

                if (p.dist > 0 && (nearest.dist == -1 || p.dist < nearest.dist))
                {
                    nearest = (p.dist, p.aNearest, p.bNearest);
                }
            }

            return nearest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist(Vector3[] rectPointsClockwise, (Vector3 begin, Vector3 end) edgeLX, (Vector3 begin, Vector3 end) edgeLY, Vector3 normal, Vector3 cg, RBColliderOBB obb, out Vector3 aNearest, out Vector3 bNearest, out bool faceParallel)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;

            Vector3[] b_verts = obb.GetVertices();

            (float dist, Vector3 aNearest, Vector3 bNearest)[] nearests = new (float dist, Vector3 aNearest, Vector3 bNearest)[3];

            Vector3 bRightN = obb.GetAxisRight();
            Vector3 bUpN = obb.GetAxisUp();
            Vector3 bForwardN = obb.GetAxisForward();

            if (Vector3.Dot(normal, bRightN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] { b_verts[5], b_verts[7], b_verts[3], b_verts[1] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, bRightN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[0] = (dist, an, bn);
                }
            }

            if (Vector3.Dot(normal, -bRightN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] { b_verts[0], b_verts[2], b_verts[6], b_verts[4] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, -bRightN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[0] = (dist, an, bn);
                }
            }

            if (Vector3.Dot(normal, bUpN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] { b_verts[2], b_verts[6], b_verts[7], b_verts[3] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, bUpN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[1] = (dist, an, bn);
                }
            }

            if (Vector3.Dot(normal, -bUpN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] { b_verts[1], b_verts[5], b_verts[4], b_verts[0] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, -bUpN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[1] = (dist, an, bn);
                }
            }

            if (Vector3.Dot(normal, bForwardN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] { b_verts[0], b_verts[2], b_verts[3], b_verts[1] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, bForwardN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[2] = (dist, an, bn);
                }
            }

            if (Vector3.Dot(normal, -bForwardN) < 0)
            {
                Vector3[] rectPointsClockwise_b = new Vector3[4] {b_verts[5], b_verts[7], b_verts[6], b_verts[4] };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, rectPointsClockwise_b, -bForwardN, cg, out Vector3 an, out Vector3 bn, out faceParallel);
                if (dist > 0)
                {
                    if (faceParallel)
                    {
                        aNearest = an;
                        bNearest = bn;
                        return dist;
                    }

                    nearests[2] = (dist, an, bn);
                }
            }

            (float dist, Vector3 aNearest, Vector3 bNearest) nearest = (-1, Vector3.zero, Vector3.zero);
            foreach (var p in nearests)
            {
                if (p.dist > 0 && (nearest.dist == -1 || p.dist < nearest.dist))
                {
                    nearest = p;
                }
            }

            aNearest = nearest.aNearest;
            bNearest = nearest.bNearest;
            faceParallel = false;

            return nearest.dist;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist(Vector3[] rectPointsClockwise_a, (Vector3 begin, Vector3 end) edgeLX, (Vector3 begin, Vector3 end) edgeLY, Vector3 normal_a, Vector3[] rectPointsClockwise_b, Vector3 normal_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest, out bool faceParallel)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;
            faceParallel = false;

            Vector3 center_a = rectPointsClockwise_a[0];

            (Vector3 begin, Vector3 end)[] edges_a = new (Vector3 begin, Vector3 end)[4];
            edges_a[0] = (rectPointsClockwise_a[0], rectPointsClockwise_a[1]);
            edges_a[1] = (rectPointsClockwise_a[1], rectPointsClockwise_a[2]);
            edges_a[2] = (rectPointsClockwise_a[2], rectPointsClockwise_a[3]);
            edges_a[3] = (rectPointsClockwise_a[3], rectPointsClockwise_a[0]);

            (Vector3 begin, Vector3 end)[] edges_b = new (Vector3 begin, Vector3 end)[4];
            edges_b[0] = (rectPointsClockwise_b[0], rectPointsClockwise_b[1]);
            edges_b[1] = (rectPointsClockwise_b[1], rectPointsClockwise_b[2]);
            edges_b[2] = (rectPointsClockwise_b[2], rectPointsClockwise_b[3]);
            edges_b[3] = (rectPointsClockwise_b[3], rectPointsClockwise_b[0]);

            Vector3 center_b = edges_b[0].begin;

            (Vector3 begin, Vector3 end) tangentEdge;
            Vector3 dir = Vector3.Cross(normal_a, -normal_b).normalized;

            if (dir == Vector3.zero)
            {
                faceParallel = true;
                Vector3 nearest = ProjectPointOnRect(cg, rectPointsClockwise_b, normal_b);
                aNearest = ProjectPointOnRect(nearest, rectPointsClockwise_a, normal_a);
                bNearest = nearest;
                return Vector3.Distance(aNearest, bNearest);
            }

            float div = Mathf.Sqrt(1 - Mathf.Pow(Vector3.Dot(normal_a, -normal_b), 2));

            Vector3 center;
            if (div == 0)
            {
                center = ProjectPointOnPlane(center_b, normal_a, center_a);
            }
            else
            {
                Vector3 rP = (ProjectPointOnPlane(center_b, normal_a, center_a) - center_b);
                Vector3 rPC_dirN = -Vector3.Cross(-normal_b, dir).normalized;
                Vector3 rPC = rPC_dirN * (rP.magnitude / div);

                center = center_b + rPC;
            }

            (Vector3 begin, Vector3 end) tangentEdgeLx = ReverseProjectEdgeOnLine(edgeLX, (dir, center));
            (Vector3 begin, Vector3 end) tangentEdgeLy = ReverseProjectEdgeOnLine(edgeLY, (dir, center));

            tangentEdge = GetDuplicatedEdge(tangentEdgeLx, tangentEdgeLy);

            if (Vector3.Cross(dir, tangentEdgeLx.end - tangentEdgeLx.begin) == Vector3.zero && !IsV3EpsilonEqual(tangentEdgeLx.begin, tangentEdgeLx.end))
            {
                tangentEdge = tangentEdgeLx;
            }

            if (Vector3.Cross(dir, tangentEdgeLy.end - tangentEdgeLy.begin) == Vector3.zero && !IsV3EpsilonEqual(tangentEdgeLy.begin, tangentEdgeLy.end))
            {
                tangentEdge = tangentEdgeLy;
            }

            (Vector3 begin, Vector3 end)[] edges_b_prjOnA = edges_b.Select(item => ProjectEdgeOnPlane(item, normal_a, center_a)).ToArray();

            (Vector3 nearest, float dist, bool edgeParallel)[] nearests = new (Vector3 nearest, float dist, bool edgeParallel)[4];

            int count = 0;

            foreach (var edge in edges_b_prjOnA)
            {
                float d = GetNearestDist(edge, tangentEdge, out Vector3 nearest, out bool edgeParallel);
                nearests[count++] = (nearest, d, edgeParallel);
            }

            float dMin = -1;
            (Vector3 nearest, float dist, bool edgeParallel, int index) np = (Vector3.zero, 0, false, -1);
            
            for(int i = 0; i < nearests.Length; i++)
            {
                var n = nearests[i];
                if (dMin == -1 || n.dist < dMin)
                {
                    dMin = n.dist;
                    np = (n.nearest, n.dist, n.edgeParallel, i);
                }
            }

            int parallelCount = nearests.Count(item => item.edgeParallel);

            if (np.index != -1)
            {
                if (parallelCount == 0)
                {
                    aNearest = ProjectPointOnRect(np.nearest, rectPointsClockwise_a, normal_a);
                    bNearest = np.nearest;

                    return Vector3.Distance(aNearest, bNearest);
                }
                else if (parallelCount == 2)
                {
                    Vector3 nearest = ProjectPointOnEdge(cg, tangentEdge);
                    aNearest = ProjectPointOnRect(nearest, rectPointsClockwise_a, normal_a);
                    bNearest = nearest;
                    return Vector3.Distance(aNearest, bNearest);
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist((Vector3 begin, Vector3 end) edge, (Vector3 begin, Vector3 end) tangentEdge_inAnotherRect, out Vector3 nearest, out bool parallel)
        {
            nearest = Vector3.zero;
            parallel = false;

            (Vector3 dir, Vector3 center) tangentLine = (tangentEdge_inAnotherRect.end - tangentEdge_inAnotherRect.begin, tangentEdge_inAnotherRect.begin);

            Vector3 projectionNormal = Vector3.Cross(edge.end - edge.begin, tangentLine.dir).normalized;

            if (projectionNormal == Vector3.zero)
            {
                parallel = true;
                return Vector3.Distance(edge.begin, ProjectPointOnLine(edge.begin, tangentLine));
            }

            (Vector3 normal, Vector3 center) prjPlane = (projectionNormal, tangentLine.center);
            (Vector3 begin, Vector3 end) edgePrj = (ProjectPointOnPlane(edge.begin, prjPlane.normal, prjPlane.center), ProjectPointOnPlane(edge.end, prjPlane.normal, prjPlane.center));
            (Vector3 dir, Vector3 center) edgePrjLined = (edgePrj.end - edgePrj.begin, (edgePrj.begin + edgePrj.end) / 2f);

            Vector3 contact = (CalcContactPointOnSameNormal(tangentLine, edgePrjLined));

            Vector3 prjCenter = ProjectPointOnPlane(edgePrjLined.center, prjPlane.normal, prjPlane.center);
            Vector3 prjNearest = prjCenter + Vector3.ClampMagnitude(contact - prjCenter, Vector3.Distance(edgePrj.begin, edgePrj.end) / 2f);

            Vector3 bToPrjB = ProjectPointOnPlane(edge.begin, prjPlane.normal, prjPlane.center) - edge.begin;

            nearest = prjNearest - bToPrjB;

            return Vector3.Distance(nearest, ProjectPointOnEdge(prjNearest, tangentLine));
        }
    }
}