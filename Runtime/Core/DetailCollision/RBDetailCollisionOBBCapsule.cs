using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBCapsule
        {
            const float FACE_PARALLEL_DOT_EPSILON = 0.000001f;

            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                var r = CalcDetailCollision(obb_a, capsule_b);
                return new Penetration(r.p, r.pA, r.pB);
            }

            enum AxisType
            {
                X,
                Y,
                Z
            }

            enum PenetrationType
            {
                None = -1,
                Right = 0,
                Up = 1,
                Fwd = 2,
                Cross_Right_CapsuleAxis = 3,
                Cross_Up_CapsuleAxis = 4,
                Cross_Fwd_CapsuleAxis = 5
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Vector3 ExtractCapsuleRadius(Vector3 vCapsule, Vector3 vContactObb, Vector3 wsSepHplnN, float radius)
            {
                var rN = (vCapsule - vContactObb).normalized;
                float sign = Vector3.Dot(wsSepHplnN, rN) >= 0 ? 1 : -1;
                return vCapsule + rN * -sign * radius;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Vector3 PickCapsuleVtxWs(Vector3 v0, Vector3 v1, Vector3 wsSepHplnN)
            {
                if (Vector3.Dot(v1 - v0, wsSepHplnN) >= 0) return v1;
                else return v0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Vector3 ProjectPointToOBBFace(RBColliderOBB obb_a, Vector3 v, Vector3 wsSepHplnN, AxisType hplnAxis)
            {
                Vector3 lsSepHplnN = Quaternion.Inverse(obb_a.rot) * wsSepHplnN;
                Vector3 lsV = Quaternion.Inverse(obb_a.rot) * (v - obb_a.pos);

                if (hplnAxis == AxisType.X)
                {
                    Vector3 lsOrg = Vector3.zero;
                    if (lsSepHplnN.x >= 0) lsOrg.x = obb_a.size.x;

                    Vector3 lsProjV = lsOrg + Vector3.ProjectOnPlane(lsV - lsOrg, Vector3.right);
                    lsProjV.y = Mathf.Clamp(lsProjV.y, 0, obb_a.size.y);
                    lsProjV.z = Mathf.Clamp(lsProjV.z, 0, obb_a.size.z);

                    Vector3 wsProjV = obb_a.pos + obb_a.rot * lsProjV;
                    return wsProjV;
                }
                else if (hplnAxis == AxisType.Y)
                {
                    Vector3 lsOrg = Vector3.zero;
                    if (lsSepHplnN.y >= 0) lsOrg.y = obb_a.size.y;

                    Vector3 lsProjV = lsOrg + Vector3.ProjectOnPlane(lsV - lsOrg, Vector3.up);
                    lsProjV.x = Mathf.Clamp(lsProjV.x, 0, obb_a.size.x);
                    lsProjV.z = Mathf.Clamp(lsProjV.z, 0, obb_a.size.z);

                    Vector3 wsProjV = obb_a.pos + obb_a.rot * lsProjV;
                    return wsProjV;
                }
                else if (hplnAxis == AxisType.Z)
                {
                    Vector3 lsOrg = Vector3.zero;
                    if (lsSepHplnN.z >= 0) lsOrg.z = obb_a.size.z;

                    Vector3 lsProjV = lsOrg + Vector3.ProjectOnPlane(lsV - lsOrg, Vector3.forward);
                    lsProjV.x = Mathf.Clamp(lsProjV.x, 0, obb_a.size.x);
                    lsProjV.y = Mathf.Clamp(lsProjV.y, 0, obb_a.size.y);

                    Vector3 wsProjV = obb_a.pos + obb_a.rot * lsProjV;
                    return wsProjV;
                }

                return default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static (Vector3 v0, Vector3 v1) PickOBBEdgeWs(RBColliderOBB obb, Vector3 wsSepHplnN, AxisType lsSepHplnAxis)
            {
                var lsSepHplnN = Quaternion.Inverse(obb.rot) * wsSepHplnN;

                if (lsSepHplnAxis == AxisType.X)
                {
                    Vector3 vLs = Vector3.zero;

                    if (lsSepHplnN.y >= 0) vLs.y = obb.size.y;
                    if (lsSepHplnN.z >= 0) vLs.z = obb.size.z;

                    Vector3 v0 = vLs;
                    Vector3 v1 = vLs + new Vector3(obb.size.x, 0, 0);

                    v0 = obb.pos + obb.rot * v0;
                    v1 = obb.pos + obb.rot * v1;

                    return (v0, v1);
                }
                else if (lsSepHplnAxis == AxisType.Y)
                {
                    Vector3 vLs = Vector3.zero;

                    if (lsSepHplnN.x >= 0) vLs.x = obb.size.x;
                    if (lsSepHplnN.z >= 0) vLs.z = obb.size.z;

                    Vector3 v0 = vLs;
                    Vector3 v1 = vLs + new Vector3(0, obb.size.y, 0);

                    v0 = obb.pos + obb.rot * v0;
                    v1 = obb.pos + obb.rot * v1;

                    return (v0, v1);
                }
                else if (lsSepHplnAxis == AxisType.Z)
                {
                    Vector3 vLs = Vector3.zero;

                    if (lsSepHplnN.x >= 0) vLs.x = obb.size.x;
                    if (lsSepHplnN.y >= 0) vLs.y = obb.size.y;

                    Vector3 v0 = vLs;
                    Vector3 v1 = vLs + new Vector3(0, 0, obb.size.z);

                    v0 = obb.pos + obb.rot * v0;
                    v1 = obb.pos + obb.rot * v1;

                    return (v0, v1);
                }
                else throw new NotImplementedException();
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                PenetrationType penetrationType = PenetrationType.None;

                var capsuleEdge = capsule_b.GetEdge();
                var capsuleDirN = capsule_b.GetHeightAxisN();

                Vector3 d = (capsule_b.pos - obb_a.Center);

                Vector3 aFwd = obb_a.rot * new Vector3(0, 0, obb_a.size.z);
                Vector3 aRight = obb_a.rot * new Vector3(obb_a.size.x, 0, 0);
                Vector3 aUp = obb_a.rot * new Vector3(0, obb_a.size.y, 0);

                Vector3 capsuleLine = capsule_b.rot * new Vector3(0, capsule_b.height, 0);

                Vector3 aFwdN = obb_a.GetAxisForwardN();
                Vector3 aRightN = obb_a.GetAxisRightN();
                Vector3 aUpN = obb_a.GetAxisUpN();

                Vector3 penetration;
                float pSqrMag;

                Vector3 aDp = Vector3.zero;
                Vector3 bDp = Vector3.zero;

                Vector3 de = capsuleEdge.end - capsuleEdge.begin;

                //Separating Axis 1: aFwd
                {
                    float dd = Vector3.Dot(d, aFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.z);
                    float rB = Mathf.Abs(Vector3.Dot(aFwdN, de)) + capsule_b.radius * 2;

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                    pSqrMag = p.sqrMagnitude;
                    penetrationType = PenetrationType.Fwd;
                }

                //Separating Axis 2: aRight
                {
                    float dd = Vector3.Dot(d, aRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.x);
                    float rB = Mathf.Abs(Vector3.Dot(aRightN, de)) + capsule_b.radius * 2;

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.Right : penetrationType;
                }

                //Separating Axis 3: aUp
                {
                    float dd = Vector3.Dot(d, aUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.y);
                    float rB = Mathf.Abs(Vector3.Dot(aUpN, de)) + capsule_b.radius * 2;

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.Up : penetrationType;
                }

                //Separating Axis 4: aFwd x capsuleDir
                {
                    Vector3 c = Vector3.Cross(aFwdN, capsuleDirN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = Mathf.Abs(Vector3.Dot(c, de)) + capsule_b.radius * 2;

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_Fwd_CapsuleAxis : penetrationType;
                    }
                }

                //Separating Axis 5: aRight x capsuleDir
                {
                    Vector3 c = Vector3.Cross(aRightN, capsuleDirN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = Mathf.Abs(Vector3.Dot(c, de)) + capsule_b.radius * 2;

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_Right_CapsuleAxis : penetrationType;
                    }
                }

                //Separating Axis 6: aUp x capsuleDir
                {
                    Vector3 c = Vector3.Cross(aUpN, capsuleDirN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = Mathf.Abs(Vector3.Dot(c, de)) + capsule_b.radius * 2;

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_Up_CapsuleAxis : penetrationType;
                    }
                }

                if (pSqrMag != -1)
                {
                    //vAB: OBB --> Capsule
                    var vAB = -penetration;

                    if ((int)penetrationType <= 2)
                    {
                        //F-S

                        AxisType hplnAxis = AxisType.X;
                        if (penetrationType == PenetrationType.Up) hplnAxis = AxisType.Y;
                        else if (penetrationType == PenetrationType.Fwd) hplnAxis = AxisType.Z;

                        var cpWs = PickCapsuleVtxWs(capsuleEdge.begin, capsuleEdge.end, -vAB);
                        var contactWs_obb = ProjectPointToOBBFace(obb_a, cpWs, vAB, hplnAxis);
                        var contactWs_capsule = ExtractCapsuleRadius(cpWs, contactWs_obb, vAB, capsule_b.radius);

                        return (contactWs_capsule - contactWs_obb, contactWs_obb, contactWs_capsule);
                    }
                    else
                    {
                        //E-S

                        (var ws0_capsule, var ws1_capsule) = (capsuleEdge.begin, capsuleEdge.end);

                        (var ws0_obb, var ws1_obb) = (Vector3.zero, Vector3.zero);

                        if (penetrationType == PenetrationType.Cross_Right_CapsuleAxis) (ws0_obb, ws1_obb) = PickOBBEdgeWs(obb_a, vAB, AxisType.X);
                        if (penetrationType == PenetrationType.Cross_Up_CapsuleAxis) (ws0_obb, ws1_obb) = PickOBBEdgeWs(obb_a, vAB, AxisType.Y);
                        if (penetrationType == PenetrationType.Cross_Fwd_CapsuleAxis) (ws0_obb, ws1_obb) = PickOBBEdgeWs(obb_a, vAB, AxisType.Z);

                        CalcNearest(ws0_obb, ws1_obb, ws0_capsule, ws1_capsule, out var contactWs_obb, out var cpWs);
                        var contactWs_capsule = ExtractCapsuleRadius(cpWs, contactWs_obb, vAB, capsule_b.radius);

                        return (contactWs_capsule - contactWs_obb, contactWs_obb, contactWs_capsule);
                    }
                }

                return default;
            }
        }
    }
}