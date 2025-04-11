using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, true)]
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBOBB
        {
            enum PenetrationType
            {
                None = -1,
                ARight = 0,
                AUp = 1,
                AFwd = 2,
                BRight = 3,
                BUp = 4,
                BFwd = 5,
                Cross_ARight_BRight = 6,
                Cross_AUp_BRight = 7,
                Cross_AFwd_BRight = 8,
                Cross_ARight_BUp = 9,
                Cross_AUp_BUp = 10,
                Cross_AFwd_BUp = 11,
                Cross_ARight_BFwd = 12,
                Cross_AUp_BFwd = 13,
                Cross_AFwd_BFwd = 14,
            }

            enum AxisType
            {
                X,
                Y,
                Z
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Vector3 PickVtxWs(RBColliderOBB obb, Vector3 wsSepHplnN)
            {
                var lsSepHplnN = Quaternion.Inverse(obb.rot) * wsSepHplnN;

                Vector3 vLs = Vector3.zero;
                if (lsSepHplnN.x >= 0) vLs.x = obb.size.x;
                if (lsSepHplnN.y >= 0) vLs.y = obb.size.y;
                if (lsSepHplnN.z >= 0) vLs.z = obb.size.z;

                return obb.pos + obb.rot * vLs;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static (Vector3 v0, Vector3 v1) PickEdgeWs(RBColliderOBB obb, Vector3 wsSepHplnN, AxisType lsSepHplnAxis)
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

            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderOBB obb_b)
            {
                Vector3 penetration;
                float pSqrMag;

                PenetrationType penetrationType = PenetrationType.None;

                Vector3 d = obb_b.Center - obb_a.Center;

                Vector3 aFwdN = obb_a.rot * Vector3.forward;
                Vector3 aRightN = obb_a.rot * Vector3.right;
                Vector3 aUpN = obb_a.rot * Vector3.up;

                Vector3 bFwdN = obb_b.rot * Vector3.forward;
                Vector3 bRightN = obb_b.rot * Vector3.right;
                Vector3 bUpN = obb_b.rot * Vector3.up;

                //Separating Axis 1: aFwd
                {
                    float dd = Vector3.Dot(d, aFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.z);
                    float rB = obb_b.GetAxisSize(aFwdN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                    pSqrMag = p.sqrMagnitude;
                    penetrationType = PenetrationType.AFwd;
                }

                //Separating Axis 2: aRight
                {
                    float dd = Vector3.Dot(d, aRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.x);
                    float rB = obb_b.GetAxisSize(aRightN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;

                    penetrationType = pMin ? PenetrationType.ARight : penetrationType;
                }

                //Separating Axis 3: aUp
                {
                    float dd = Vector3.Dot(d, aUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.y);
                    float rB = obb_b.GetAxisSize(aUpN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.AUp : penetrationType;
                }

                //Separating Axis 4: bFwd
                {
                    float dd = Vector3.Dot(d, bFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bFwdN);
                    float rB = Mathf.Abs(obb_b.size.z);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = bFwdN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.BFwd : penetrationType;
                }

                //Separating Axis 5: bRight
                {
                    float dd = Vector3.Dot(d, bRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bRightN);
                    float rB = Mathf.Abs(obb_b.size.x);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = bRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.BRight : penetrationType;
                }

                //Separating Axis 6: bUp
                {
                    float dd = Vector3.Dot(d, bUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bUpN);
                    float rB = Mathf.Abs(obb_b.size.y);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0) return default;

                    Vector3 p = bUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    penetrationType = pMin ? PenetrationType.BUp : penetrationType;
                }

                //Separating Axis 7: aFwd x bFwd
                {
                    Vector3 c = Vector3.Cross(aFwdN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AFwd_BFwd : penetrationType;
                    }
                }

                //Separating Axis 8: aFwd x bRight
                {
                    Vector3 c = Vector3.Cross(aFwdN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AFwd_BRight : penetrationType;
                    }
                }

                //Separating Axis 9: aFwd x bUp
                {
                    Vector3 c = Vector3.Cross(aFwdN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AFwd_BUp : penetrationType;
                    }
                }

                //Separating Axis 10: aRight x bFwd
                {
                    Vector3 c = Vector3.Cross(aRightN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_ARight_BFwd : penetrationType;
                    }
                }

                //Separating Axis 11: aRight x bRight
                {
                    Vector3 c = Vector3.Cross(aRightN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_ARight_BRight : penetrationType;
                    }
                }

                //Separating Axis 12: aRight x bUp
                {
                    Vector3 c = Vector3.Cross(aRightN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_ARight_BUp : penetrationType;
                    }
                }

                //Separating Axis 13: aUp x bFwd
                {
                    Vector3 c = Vector3.Cross(aUpN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AUp_BFwd : penetrationType;
                    }
                }

                //Separating Axis 14: aUp x bRight
                {
                    Vector3 c = Vector3.Cross(aUpN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AUp_BRight : penetrationType;
                    }
                }

                //Separating Axis 15: aUp x bUp
                {
                    Vector3 c = Vector3.Cross(aUpN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0) return default;

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        penetrationType = pMin ? PenetrationType.Cross_AUp_BUp : penetrationType;
                    }
                }

                if (pSqrMag != -1)
                {
                    if (penetrationType == PenetrationType.None) throw new System.Exception();

                    Vector3 pDirN = -penetration.normalized;

                    if ((int)penetrationType <= 5)
                    {
                        if ((int)penetrationType <= 2)
                        {
                            //F(A)-V(B)

                            var vBA = -pDirN;

                            var contactWs_b = PickVtxWs(obb_b, vBA);
                            var centerWs_a = PickVtxWs(obb_a, -vBA);
                            var contactWs_a = ProjectPointToPlane(contactWs_b, -vBA, centerWs_a);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else
                        {
                            //F(B)-V(A)

                            var vAB = pDirN;

                            var contactWs_a = PickVtxWs(obb_a, vAB);
                            var centerWs_b = PickVtxWs(obb_b, -vAB);
                            var contactWs_b = ProjectPointToPlane(contactWs_a, -vAB, centerWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                    }
                    else
                    {
                        //E-E

                        if (penetrationType == PenetrationType.Cross_ARight_BRight)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.X);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.X);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AUp_BRight)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Y);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.X);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AFwd_BRight)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Z);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.X);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_ARight_BUp)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.X);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Y);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AUp_BUp)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Y);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Y);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AFwd_BUp)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Z);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Y);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_ARight_BFwd)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.X);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Z);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AUp_BFwd)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Y);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Z);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                        else if (penetrationType == PenetrationType.Cross_AFwd_BFwd)
                        {
                            var vBA = -pDirN;

                            (var ws0_a, var ws1_a) = PickEdgeWs(obb_a, -vBA, AxisType.Z);
                            (var ws0_b, var ws1_b) = PickEdgeWs(obb_b, vBA, AxisType.Z);

                            CalcNearest(ws0_a, ws1_a, ws0_b, ws1_b, out var contactWs_a, out var contactWs_b);

                            return new Penetration(penetration, contactWs_a, contactWs_b);
                        }
                    }
                }

                return default;
            }
        }
    }
}