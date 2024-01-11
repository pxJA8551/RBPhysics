using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _parent; } }
        public abstract RBColliderDetailType DetailType { get; }

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

        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, Vector3 scale);

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
                        .Select(item => item.magnitude * (1f / Vector3.Dot(penetrationDir.normalized, item.normalized)))
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
        public static float GetNearestDist(RBCollider collider_a, RBCollider collider_b, Vector3 cg_a, Vector3 cg_b, Vector3 penetration, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;

            //OBB-OBB
            if (collider_a.DetailType == RBColliderDetailType.OBB && collider_b.DetailType == RBColliderDetailType.OBB)
            {
                RBColliderOBB obb_a = collider_a.CalcOBB();
                RBColliderOBB obb_b = collider_b.CalcOBB();

                obb_a.pos += penetration; //àÍéûìIÇ…è’ìÀÇâè¡

                float nearestAB = GetNearestDist(obb_a, obb_a, cg_b, out Vector3 anAB, out Vector3 bnAB);
                float nearestBA = GetNearestDist(obb_b, obb_a, cg_a, out Vector3 bnBA, out Vector3 anBA);

                if (nearestAB < nearestBA)
                {
                    aNearest = anAB - penetration;
                    bNearest = bnAB;
                    return nearestAB;
                }
                else
                {
                    aNearest = anBA - penetration;
                    bNearest = bnBA;
                    return nearestBA;
                }
            }

            return 0;
        }

        //OBB-OBBç≈ãﬂì_îªíË
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist(RBColliderOBB obb_a, RBColliderOBB obb_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;

            Vector3 normal_a_x = obb_a.GetAxisRight();
            Vector3 normal_a_y = obb_a.GetAxisUp();
            Vector3 normal_a_z = obb_a.GetAxisForward();

            Vector3[] obb_a_verts = obb_a.GetVertices();

            (float dist, Vector3 aNearest, Vector3 bNearest)[] nearests = new (float dist, Vector3 aNearest, Vector3 bNearest)[3];

            Vector3 d = obb_b.Center - obb_a.Center;

            if (Vector3.Dot(d, normal_a_x) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[5], obb_a_verts[7], obb_a_verts[3], obb_a_verts[1] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y, obb_a.size.z / 2f));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, obb_a.size.z));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_x, d, cg, obb_b, out Vector3 an, out Vector3 bn);
                nearests[0] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -normal_a_x) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[4], obb_a_verts[6], obb_a_verts[2], obb_a_verts[0] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(0, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y, obb_a.size.z / 2f));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, obb_a.size.z));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_x, d, cg, obb_b, out Vector3 an, out Vector3 bn);

                nearests[0] = (dist, an, bn);
            }

            if (Vector3.Dot(d, normal_a_y) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[2], obb_a_verts[6], obb_a_verts[7], obb_a_verts[3] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, obb_a.size.z));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y, obb_a.size.z / 2f));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_y, d, cg, obb_b, out Vector3 an, out Vector3 bn);

                nearests[1] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -normal_a_y) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[0], obb_a_verts[4], obb_a_verts[5], obb_a_verts[1] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, obb_a.size.z));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, 0, obb_a.size.z / 2f), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, 0, obb_a.size.z / 2f));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_y, d, cg, obb_b, out Vector3 an, out Vector3 bn);

                nearests[1] = (dist, an, bn);
            }
            
            if (Vector3.Dot(d, normal_a_z) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[0], obb_a_verts[2], obb_a_verts[3], obb_a_verts[1] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, 0));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, 0), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, 0));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal_a_z, d, cg, obb_b, out Vector3 an, out Vector3 bn);

                nearests[2] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -normal_a_z) > 0)
            {
                Vector3[] rectPointsClockwise = new Vector3[4] { obb_a_verts[4], obb_a_verts[6], obb_a_verts[7], obb_a_verts[5] };
                (Vector3 begin, Vector3 end) edgeLX = (obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, 0, obb_a.size.z), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x / 2f, obb_a.size.y, obb_a.size.z));
                (Vector3 begin, Vector3 end) edgeLY = (obb_a.pos + obb_a.rot * new Vector3(0, obb_a.size.y / 2f, obb_a.size.z), obb_a.pos + obb_a.rot * new Vector3(obb_a.size.x, obb_a.size.y / 2f, obb_a.size.z));
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, -normal_a_z, d, cg, obb_b, out Vector3 an, out Vector3 bn);

                nearests[2] = (dist, an, bn);
            }

            (float dist, Vector3 aNearest, Vector3 bNearest) nearest = (-1, Vector3.zero, Vector3.zero);
            foreach (var p in nearests)
            {
                if (p.dist != -1 && (nearest.dist == -1 || p.dist < nearest.dist))
                {
                    nearest = p;
                }
            }

            aNearest = nearest.aNearest;
            bNearest = nearest.bNearest;

            return nearest.dist;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist(Vector3[] rectPointsClockwise, (Vector3 begin, Vector3 end) edgeLX, (Vector3 begin, Vector3 end) edgeLY, Vector3 normal, Vector3 d, Vector3 cg, RBColliderOBB obb, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;

            Vector3[] b_verts = obb.GetVertices();

            (float dist, Vector3 aNearest, Vector3 bNearest)[] nearests = new (float dist, Vector3 aNearest, Vector3 bNearest)[3];

            Vector3 bRightN = obb.GetAxisRight();
            Vector3 bUpN = obb.GetAxisUp();
            Vector3 bForwardN = obb.GetAxisForward();

            if (Vector3.Dot(d, bRightN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[5], b_verts[7]), (b_verts[7], b_verts[3]), (b_verts[3], b_verts[1]), (b_verts[1], b_verts[5]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, bRightN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[0] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -bRightN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[4], b_verts[6]), (b_verts[6], b_verts[2]), (b_verts[2], b_verts[0]), (b_verts[0], b_verts[4]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, -bRightN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[0] = (dist, an, bn);
            }

            if (Vector3.Dot(d, bUpN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[2], b_verts[0]), (b_verts[0], b_verts[7]), (b_verts[7], b_verts[3]), (b_verts[3], b_verts[2]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, bUpN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[1] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -bUpN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[0], b_verts[4]), (b_verts[4], b_verts[5]), (b_verts[5], b_verts[1]), (b_verts[1], b_verts[0]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, -bUpN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[1] = (dist, an, bn);
            }

            if (Vector3.Dot(d, bForwardN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[0], b_verts[2]), (b_verts[2], b_verts[3]), (b_verts[3], b_verts[1]), (b_verts[1], b_verts[0]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, bForwardN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[2] = (dist, an, bn);
            }

            if (Vector3.Dot(d, -bForwardN) > 0)
            {
                (Vector3 begin, Vector3 end)[] b_edges = new (Vector3, Vector3)[4] { (b_verts[4], b_verts[6]), (b_verts[6], b_verts[7]), (b_verts[7], b_verts[5]), (b_verts[5], b_verts[4]) };
                float dist = GetNearestDist(rectPointsClockwise, edgeLX, edgeLY, normal, b_edges, -bForwardN, cg, out Vector3 an, out Vector3 bn, out bool parallel);
                nearests[2] = (dist, an, bn);
            }

            (float dist, Vector3 aNearest, Vector3 bNearest) nearest = (-1, Vector3.zero, Vector3.zero);
            foreach (var p in nearests)
            {
                if (p.dist != -1 && (nearest.dist == -1 || p.dist < nearest.dist))
                {
                    nearest = p;
                }
            }

            aNearest = nearest.aNearest;
            bNearest = nearest.bNearest;

            return nearest.dist;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetNearestDist(Vector3[] rectPointsClockwise, (Vector3 begin, Vector3 end) edgeLX, (Vector3 begin, Vector3 end) edgeLY, Vector3 normal_a, (Vector3 begin, Vector3 end)[] edges_b, Vector3 normal_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest, out bool parallel)
        {
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;
            parallel = false;

            Vector3 center_a = rectPointsClockwise[0];
            Vector3 center_b = edges_b[0].begin;

            (Vector3 begin, Vector3 end)[] edges_a = new (Vector3 begin, Vector3 end)[4];
            edges_a[0] = (rectPointsClockwise[0], rectPointsClockwise[1]);
            edges_a[1] = (rectPointsClockwise[1], rectPointsClockwise[2]);
            edges_a[2] = (rectPointsClockwise[2], rectPointsClockwise[3]);
            edges_a[3] = (rectPointsClockwise[3], rectPointsClockwise[0]);

            (Vector3 begin, Vector3 end) tangentEdge;
            Vector3 dir = Vector3.Cross(normal_a, normal_b);

            if (dir == Vector3.zero)
            {
                parallel = true;
            }

            Vector3 rP = (ProjectPointOnPlane(center_b, normal_a, center_a) - center_b);
            Vector3 rPC_dirN = Vector3.Cross(rP, dir).normalized;
            Vector3 rPC = rPC_dirN * (rP.magnitude / Mathf.Sqrt(1 - Mathf.Pow(Vector3.Dot(normal_a, -normal_b), 2)));

            Vector3 center = center_b + rPC;
            (Vector3 begin, Vector3 end) tangentEdgeLx = ProjectEdgeOnLine(edgeLX, (dir, center));
            (Vector3 begin, Vector3 end) tangentEdgeLy = ProjectEdgeOnLine(edgeLY, (dir, center));
            tangentEdge = GetDuplicatedEdge(tangentEdgeLx, tangentEdgeLy);

            (Vector3 begin, Vector3 end)[] edges_b_prjOnA = edges_b.Select(item => ProjectEdgeOnPlane(item, normal_a, center_a)).ToArray();

            (Vector3 nearest, float dist, bool edgeParallel)[] nearests = new (Vector3 nearest, float dist, bool edgeParallel)[8];

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
                    aNearest = ProjectPointOnRect(np.nearest, rectPointsClockwise, normal_a);
                    bNearest = np.nearest;
                    return Vector3.Distance(aNearest, bNearest);
                }
                else if (parallelCount == 2)
                {
                    parallel = true;
                    Vector3 nearest = ProjectPointOnEdge(cg, edges_b_prjOnA[np.index]);
                    aNearest = ProjectPointOnRect(nearest, rectPointsClockwise, normal_a);
                    bNearest = nearest;
                    return Vector3.Distance(aNearest, bNearest);
                }
            }

            return 0;
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
                return Vector3.Distance(edge.begin, tangentLine.center);
            }

            (Vector3 normal, Vector3 center) prjPlane = (projectionNormal, tangentLine.center);
            (Vector3 begin, Vector3 end) edgePrj = (ProjectPointOnPlane(edge.begin, prjPlane.normal, prjPlane.center), ProjectPointOnPlane(edge.end, prjPlane.normal, prjPlane.center));
            (Vector3 dir, Vector3 center) edgePrjLined = (edgePrj.end - edgePrj.begin, (edgePrj.begin + edgePrj.end) / 2f);

            Vector3 contact = (CalcContactPointOnSameNormal(tangentLine, edgePrjLined, prjPlane.normal));

            Vector3 prjCenter = ProjectPointOnPlane(edgePrjLined.center, prjPlane.normal, prjPlane.center);
            Vector3 prjNearest = prjCenter + Vector3.ClampMagnitude(contact - prjCenter, Vector3.Distance(edgePrj.begin, edgePrj.end) / 2f);

            Vector3 bToPrjB = ProjectPointOnPlane(edge.begin, prjPlane.normal, prjPlane.center) - edge.begin;

            nearest = prjNearest - bToPrjB;

            return Vector3.Distance(nearest, ProjectPointOnEdge(prjNearest, tangentLine));
        }
    }
}