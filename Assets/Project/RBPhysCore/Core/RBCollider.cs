using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public float beta = 0.5f;
        public float restitution = 0.5f; //”½”­ŒW”
        public float friction = 0.5f; //–€ŽCŒW”

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
        internal const float V3_PARALLEL_DOT_EPSILON = 0.0000000001f;

        static Vector3[] _penetrations = new Vector3[15];

        //OBB-OBBÕ“Ë”»’è
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DetectCollision(RBColliderOBB obb_a, RBColliderOBB obb_b, Vector3 cg, out Vector3 penetration, out Vector3 aNearest, out Vector3 bNearest)
        {
            penetration = Vector3.zero;
            aNearest = Vector3.zero;
            bNearest = Vector3.zero;

            if (obb_a.isValidOBB && obb_b.isValidOBB)
            {
                Vector3 d = obb_b.Center - obb_a.Center;

                //http://marupeke296.com/COL_3D_No13_OBBvsOBB.html
                {
                    Vector3 aFwdN = obb_a.GetAxisForward();
                    Vector3 aRightN = obb_a.GetAxisRight();
                    Vector3 aUpN = obb_a.GetAxisUp();
                    Vector3 bFwdN = obb_b.GetAxisForward();
                    Vector3 bRightN = obb_b.GetAxisRight();
                    Vector3 bUpN = obb_b.GetAxisUp();

                    //•ª—£Ž²‚P: aFwd
                    {
                        float dd = Vector3.Dot(d, aFwdN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.z);
                        float rB = obb_b.GetAxisSize(aFwdN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[0] = aFwdN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚Q: aRight
                    {
                        float dd = Vector3.Dot(d, aRightN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.x);
                        float rB = obb_b.GetAxisSize(aRightN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[1] = aRightN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚R: aUp
                    {
                        float dd = Vector3.Dot(d, aUpN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.y);
                        float rB = obb_b.GetAxisSize(aUpN);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[2] = aUpN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚S: bFwd
                    {
                        float dd = Vector3.Dot(d, bFwdN);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(bFwdN);
                        float rB = Mathf.Abs(obb_b.size.z);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[3] = bFwdN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚T: bRight
                    {
                        float dd = Vector3.Dot(d, bRightN);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(bRightN);
                        float rB = Mathf.Abs(obb_b.size.x);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[4] = bRightN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚U: bUp
                    {
                        float dd = Vector3.Dot(d, bUpN);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(bUpN);
                        float rB = Mathf.Abs(obb_b.size.y);

                        float dp = prjL * 2f - (rA + rB);

                        if (dp > 0)
                        {
                            return false;
                        }

                        _penetrations[5] = bUpN * (dp / 2f) * F32Sign11(dd);
                    }

                    //•ª—£Ž²‚V: aFwd x bFwd
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bFwdN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[6] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[6] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚W: aFwd x bRight
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bRightN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[7] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[7] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚X: aFwd x bUp
                    {
                        Vector3 p = Vector3.Cross(aFwdN, bUpN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[8] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[8] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚O: aRight x bFwd
                    {
                        Vector3 p = Vector3.Cross(aRightN, bFwdN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[9] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[9] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚P: aRight x bRight
                    {
                        Vector3 p = Vector3.Cross(aRightN, bRightN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[10] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[10] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚Q: aRight x bUp
                    {
                        Vector3 p = Vector3.Cross(aRightN, bUpN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[11] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[11] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚R: aUp x bFwd
                    {
                        Vector3 p = Vector3.Cross(aUpN, bFwdN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[12] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[12] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚S: aUp x bRight
                    {
                        Vector3 p = Vector3.Cross(aUpN, bRightN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[13] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[13] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    //•ª—£Ž²‚P‚T: aUp x bUp
                    {
                        Vector3 p = Vector3.Cross(aUpN, bUpN).normalized;

                        if (p == Vector3.zero)
                        {
                            _penetrations[14] = Vector3.negativeInfinity;
                        }
                        else
                        {
                            float dd = Vector3.Dot(d, p);
                            float prjL = Mathf.Abs(dd);
                            float rA = obb_a.GetAxisSize(p);
                            float rB = obb_b.GetAxisSize(p);

                            float dp = prjL * 2f - (rA + rB);

                            if (dp > 0)
                            {
                                return false;
                            }

                            _penetrations[14] = p * (dp / 2f) * F32Sign11(dd);
                        }
                    }

                    if (_penetrations.Any()) 
                    {
                        float pMinSqrt = -1;
                        int index = -1;

                        for (int i = 0; i < _penetrations.Length; i++)
                        {
                            float f = _penetrations[i].sqrMagnitude;
                            if (pMinSqrt == -1 || f < pMinSqrt)
                            {
                                pMinSqrt = f;
                                index = i;
                            }
                        }

                        if (index != -1)
                        {
                            penetration = _penetrations[index];

                            Vector3 pDirN = penetration.normalized;

                            Vector3 dpA = obb_a.GetDirectional(-pDirN, out int axisInfo_a);
                            Vector3 dpB = obb_b.GetDirectional(pDirN, out int axisInfo_b);

                            bool point_a = axisInfo_a == 0;
                            bool point_b = axisInfo_b == 0;

                            bool face_a = IsInt32Pow2(axisInfo_a);
                            bool face_b = IsInt32Pow2(axisInfo_b);

                            bool edge_a = !point_a && !face_a;
                            bool edge_b = !point_b && !face_b;

                            if (point_a && point_b)
                            {
                                GetOBBNearest(dpA, dpB, out aNearest, out bNearest);
                                return true;
                            }

                            if (point_a && edge_b)
                            {
                                GetOBBNearest(dpA, obb_b.GetDirectionalEdge(axisInfo_b), out aNearest, out bNearest);
                                return true;
                            }

                            if (point_a && face_b)
                            {
                                GetOBBNearest(dpA, obb_b.GetDirectionalRect(axisInfo_b), out aNearest, out bNearest);
                                return true;
                            }

                            if (edge_a && point_b)
                            {
                                GetOBBNearest(dpB, obb_a.GetDirectionalEdge(axisInfo_a), out bNearest, out aNearest);
                                return true;
                            }

                            if (edge_a && edge_b)
                            {
                                GetOBBNearest(obb_a.GetDirectionalEdge(axisInfo_a), obb_b.GetDirectionalEdge(axisInfo_b), cg, out aNearest, out bNearest);
                                return true;
                            }

                            if (edge_a && face_b)
                            {
                                GetOBBNearest(obb_a.GetDirectionalEdge(axisInfo_a), obb_b.GetDirectionalRect(axisInfo_b), cg, out aNearest, out bNearest);
                                return true;
                            }

                            if (face_a && point_b)
                            {
                                GetOBBNearest(dpB, obb_a.GetDirectionalRect(axisInfo_a), out bNearest, out aNearest);
                                return true;
                            }

                            if (face_a && edge_b)
                            {
                                GetOBBNearest(obb_b.GetDirectionalEdge(axisInfo_b), obb_a.GetDirectionalRect(axisInfo_a), cg, out bNearest, out aNearest);
                                return true;
                            }

                            if (face_a && face_b)
                            {
                                GetOBBNearest(obb_a.GetDirectionalRect(axisInfo_a), obb_b.GetDirectionalRect(axisInfo_b), cg, out aNearest, out bNearest);
                                return true;
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest(Vector3 point_a, Vector3 point_b, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = point_a;
            bNearest = point_b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest(Vector3 point_a, (Vector3 begin, Vector3 end) edge_b, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = point_a;
            bNearest = ProjectPointOnEdge(aNearest, edge_b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest(Vector3 point_a, (Vector3[] vertsCW, Vector3 normal) rect_b, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = point_a;
            bNearest = ProjectPointOnRect(aNearest, rect_b.vertsCW, rect_b.normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest((Vector3 begin, Vector3 end) edge_a, (Vector3 begin, Vector3 end) edge_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest)
        {
            if (Vector3.Cross(edge_a.end - edge_a.begin, edge_b.end - edge_b.begin) == Vector3.zero) 
            {
                aNearest = ProjectPointOnEdge(cg, edge_a);
                bNearest = ProjectPointOnEdge(cg, edge_b);
                return;
            }

            CalcNearestEdgeLine(edge_a, edge_b, out aNearest, out bNearest);
            bNearest = ProjectPointOnEdge(aNearest, edge_b);
            aNearest = ProjectPointOnEdge(bNearest, edge_a);
            bNearest = ProjectPointOnEdge(aNearest, edge_b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest((Vector3 begin, Vector3 end) edge_a, (Vector3[] vertsCW, Vector3 normal) rect_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = ProjectPointOnEdge(cg, edge_a);
            bNearest = ProjectPointOnRect(aNearest, rect_b.vertsCW, rect_b.normal);
            aNearest = ProjectPointOnEdge(bNearest, edge_a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void GetOBBNearest((Vector3[] vertsCW, Vector3 normal) rect_a, (Vector3[] vertsCW, Vector3 normal) rect_b, Vector3 cg, out Vector3 aNearest, out Vector3 bNearest)
        {
            aNearest = ProjectPointOnRect(cg, rect_a.vertsCW, rect_a.normal);
            bNearest = ProjectPointOnRect(aNearest, rect_b.vertsCW, rect_b.normal);
            aNearest = ProjectPointOnRect(bNearest, rect_a.vertsCW, rect_a.normal);
        }

        //OBB-SphereÕ“Ë”»’è
        public static bool DetectCollision(RBColliderOBB obb_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (obb_a.isValidOBB && sphere_b.isValidSphere)
            {

            }

            return false;
        }

        //Sphere-SphereÕ“Ë”»’è
        public static bool DetectCollision(RBColliderSphere sphere_a, RBColliderSphere sphere_b, out Vector3 penetration)
        {
            penetration = Vector3.zero;

            if (sphere_a.isValidSphere && sphere_b.isValidSphere)
            {
                
            }

            return false;
        }
    }
}