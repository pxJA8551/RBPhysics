using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEditor;
using UnityEngine;

using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBOBB
        {
            const float FACE_PARALLEL_DOT_EPSILON = 0.00001f;

            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderOBB obb_b)
            {
                Vector3 penetration;
                Vector3 aDp = Vector3.zero;
                Vector3 bDp = Vector3.zero;
                float pSqrMag;

                DetailCollisionInfo info = new DetailCollisionInfo();
                info.obb_obb_penetrationIndex = -1;

                Vector3 d = obb_b.Center - obb_a.Center;

                Vector3 aFwd = obb_a.rot * new Vector3(0, 0, obb_a.size.z);
                Vector3 aRight = obb_a.rot * new Vector3(obb_a.size.x, 0, 0);
                Vector3 aUp = obb_a.rot * new Vector3(0, obb_a.size.y, 0);

                Vector3 bFwd = obb_b.rot * new Vector3(0, 0, obb_b.size.z);
                Vector3 bRight = obb_b.rot * new Vector3(obb_b.size.x, 0, 0);
                Vector3 bUp = obb_b.rot * new Vector3(0, obb_b.size.y, 0);

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

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }

                    Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                    pSqrMag = p.sqrMagnitude;
                    info.obb_obb_penetrationIndex = 0;
                }

                //Separating Axis 2: aRight
                {
                    float dd = Vector3.Dot(d, aRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.x);
                    float rB = obb_b.GetAxisSize(aRightN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }
                    Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;

                    info.obb_obb_penetrationIndex = pMin ? (sbyte)1 : info.obb_obb_penetrationIndex;
                }

                //Separating Axis 3: aUp
                {
                    float dd = Vector3.Dot(d, aUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.y);
                    float rB = obb_b.GetAxisSize(aUpN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }

                    Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    info.obb_obb_penetrationIndex = pMin ? (sbyte)2 : info.obb_obb_penetrationIndex;
                }

                //Separating Axis 4: bFwd
                {
                    float dd = Vector3.Dot(d, bFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bFwdN);
                    float rB = Mathf.Abs(obb_b.size.z);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }

                    Vector3 p = bFwdN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    info.obb_obb_penetrationIndex = pMin ? (sbyte)3 : info.obb_obb_penetrationIndex;
                }

                //Separating Axis 5: bRight
                {
                    float dd = Vector3.Dot(d, bRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bRightN);
                    float rB = Mathf.Abs(obb_b.size.x);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }

                    Vector3 p = bRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    info.obb_obb_penetrationIndex = pMin ? (sbyte)4 : info.obb_obb_penetrationIndex;
                }

                //Separating Axis 6: bUp
                {
                    float dd = Vector3.Dot(d, bUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bUpN);
                    float rB = Mathf.Abs(obb_b.size.y);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return new Penetration(penetration, aDp, bDp, default);
                    }

                    Vector3 p = bUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    info.obb_obb_penetrationIndex = pMin ? (sbyte)5 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)6 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)7 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)8 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)9 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)10 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)11 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)12 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)13 : info.obb_obb_penetrationIndex;
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

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return new Penetration(penetration, aDp, bDp, default);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                        info.obb_obb_penetrationIndex = pMin ? (sbyte)14 : info.obb_obb_penetrationIndex;
                    }
                }

                if (pSqrMag != -1)
                {
                    Vector3 pDirN = -penetration.normalized;

                    float eps = FACE_PARALLEL_DOT_EPSILON;

                    float dAFwd = F32Sign101Epsilon(Vector3.Dot(aFwdN, pDirN), eps);
                    float dARight = F32Sign101Epsilon(Vector3.Dot(aRightN, pDirN), eps);
                    float dAUp = F32Sign101Epsilon(Vector3.Dot(aUpN, pDirN), eps);
                    int aPd = (dAFwd == 0 ? 1 : 0) + (dARight == 0 ? 1 : 0) + (dAUp == 0 ? 1 : 0);

                    float dBFwd = F32Sign101Epsilon(Vector3.Dot(bFwdN, -pDirN), eps);
                    float dBRight = F32Sign101Epsilon(Vector3.Dot(bRightN, -pDirN), eps);
                    float dBUp = F32Sign101Epsilon(Vector3.Dot(bUpN, -pDirN), eps);
                    int bPd = (dBFwd == 0 ? 1 : 0) + (dBRight == 0 ? 1 : 0) + (dBUp == 0 ? 1 : 0);

                    Vector3 ofAFwd = aFwd * dAFwd / 2;
                    Vector3 ofARight = aRight * dARight / 2;
                    Vector3 ofAUp = aUp * dAUp / 2;

                    Vector3 ofBFwd = bFwd * dBFwd / 2;
                    Vector3 ofBRight = bRight * dBRight / 2;
                    Vector3 ofBUp = bUp * dBUp / 2;

                    Vector3 nA = ofAFwd + ofARight + ofAUp;
                    Vector3 nB = ofBFwd + ofBRight + ofBUp;

                    aDp = obb_a.Center + nA;
                    bDp = obb_b.Center + nB;

                    nA.Normalize();
                    nB.Normalize();

                    Vector3 fA1 = Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fA2 = Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fB1 = Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bFwd * (dBFwd == 0 ? 1 : 0) : Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bRight * (dBRight == 0 ? 1 : 0) : Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bUp * (dBUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fB2 = Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bUp * (dBUp == 0 ? 1 : 0) : Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bRight * (dBRight == 0 ? 1 : 0) : Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bFwd * (dBFwd == 0 ? 1 : 0) : Vector3.zero;

                    fA1 /= 4f;
                    fA2 /= 4f;
                    fB1 /= 4f;
                    fB2 /= 4f;

                    if (aPd == 0 || bPd == 0)
                    {
                        if (aPd == 1)
                        {
                            aDp = ProjectPointToEdge(bDp, aDp + (fA1 + fA2), aDp - (fA1 + fA2));
                        }
                        else if (aPd == 2)
                        {
                            aDp = ProjectPointToPlane(bDp, nA, aDp);
                        }
                        else if (bPd == 1)
                        {
                            bDp = ProjectPointToEdge(aDp, bDp + (fB1 + fB2), bDp - (fB1 + fB2));
                        }
                        else if (bPd == 2)
                        {
                            bDp = ProjectPointToPlane(aDp, nB, bDp);
                        }

                        return new Penetration(penetration, aDp, bDp, info);
                    }
                    else if (aPd == 1 && bPd == 1)
                    {
                        CalcNearest(aDp + (fA1 + fA2), aDp - (fA1 + fA2), bDp + (fB1 + fB2), bDp - (fB1 + fB2), out aDp, out bDp, out _);
                        return new Penetration(penetration, aDp, bDp, info);
                    }
                    else if (aPd == 1)
                    {
                        fA1 *= 2f;
                        fA2 *= 2f;

                        Vector3 te_begin = aDp + (fA1 + fA2);
                        Vector3 te_end = aDp - (fA1 + fA2);

                        ReverseProjectLineToLine(ref te_begin, ref te_end, bDp + fB1, bDp - fB1);
                        ReverseProjectLineToLine(ref te_begin, ref te_end, bDp + fB2, bDp - fB2);

                        aDp = (te_begin + te_end) / 2;
                        bDp = ProjectPointToPlane(aDp, nB, bDp);

                        return new Penetration(penetration, aDp, bDp, info);
                    }
                    else if (bPd == 1)
                    {
                        fB1 *= 2f;
                        fB2 *= 2f;

                        Vector3 te_begin = bDp + (fB1 + fB2);
                        Vector3 te_end = bDp - (fB1 + fB2);

                        ReverseProjectLineToLine(ref te_begin, ref te_end, aDp + fA1, aDp - fA1);
                        ReverseProjectLineToLine(ref te_begin, ref te_end, aDp + fA2, aDp - fA2);

                        bDp = (te_begin + te_end) / 2;
                        aDp = ProjectPointToPlane(bDp, nA, aDp);

                        return new Penetration(penetration, aDp, bDp, info);
                    }
                    else
                    {
                        Vector3[] verts = new Vector3[8];

                        fA1 *= 2f;
                        fA2 *= 2f;
                        fB1 *= 2f;
                        fB2 *= 2f;

                        for (int i = 0; i < aPd * 2; i++)
                        {
                            if (aPd == 2)
                            {
                                verts[i] = aDp + fA1 * (((i + (i / 2)) % 2 == 0) ? 1 : -1) + fA2 * ((i / 2 == 0) ? 1 : -1);
                            }
                        }

                        for (int i = 0; i < bPd * 2; i++)
                        {
                            if (bPd == 2)
                            {
                                verts[4 + i] = bDp + fB1 * (((i + (i / 2)) % 2 == 0) ? 1 : -1) + fB2 * ((i / 2 == 0) ? 1 : -1);
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            verts[4 + i] = ProjectPointToPlane(verts[4 + i], nA, aDp);
                        }

                        Vector3[] contacts = new Vector3[16];
                        bool[] valid = new bool[16];

                        for (int ie = 0; ie < 4; ie++)
                        {
                            for (int je = 0; je < 4; je++)
                            {
                                contacts[ie * 4 + je] = CalcNearest(verts[ie], verts[(ie + 1) % 4], verts[4 + je], verts[4 + (je + 1) % 4], out bool invalid);
                                valid[ie * 4 + je] = !invalid;
                            }
                        }

                        Vector3 sum = Vector3.zero;
                        int count = 0;

                        for (int i = 0; i < 4; i++)
                        {
                            Vector3 c = verts[i];

                            if (c != Vector3.zero && IsInRect(c, verts[4], verts[5], verts[6], verts[7], nB))
                            {
                                sum += c;
                                count++;
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            Vector3 c = verts[4 + i];

                            if (c != Vector3.zero && IsInRect(c, verts[0], verts[1], verts[2], verts[3], nA))
                            {
                                sum += c;
                                count++;
                            }
                        }

                        for (int i = 0; i < 16; i++)
                        {
                            Vector3 c = contacts[i];
                            if (valid[i])
                            {
                                sum += c;
                                count++;
                            }
                        }

                        aDp = ProjectPointToPlane(sum / count, nA, aDp);
                        bDp = ProjectPointToPlane(sum / count, nB, bDp);
                        return new Penetration(penetration, aDp, bDp, info);
                    }
                }

                return new Penetration(Vector3.zero, Vector3.zero, Vector3.zero, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollisionLighter(RBColliderOBB obb_a, RBColliderOBB obb_b, DetailCollisionInfo info)
            {
                Vector3 penetration = Vector3.zero;
                Vector3 aDp = Vector3.zero;
                Vector3 bDp = Vector3.zero;

                Vector3 d = obb_b.Center - obb_a.Center;

                Vector3 aFwd = obb_a.rot * new Vector3(0, 0, obb_a.size.z);
                Vector3 aRight = obb_a.rot * new Vector3(obb_a.size.x, 0, 0);
                Vector3 aUp = obb_a.rot * new Vector3(0, obb_a.size.y, 0);

                Vector3 bFwd = obb_b.rot * new Vector3(0, 0, obb_b.size.z);
                Vector3 bRight = obb_b.rot * new Vector3(obb_b.size.x, 0, 0);
                Vector3 bUp = obb_b.rot * new Vector3(0, obb_b.size.y, 0);

                Vector3 aFwdN = obb_a.rot * Vector3.forward;
                Vector3 aRightN = obb_a.rot * Vector3.right;
                Vector3 aUpN = obb_a.rot * Vector3.up;

                Vector3 bFwdN = obb_b.rot * Vector3.forward;
                Vector3 bRightN = obb_b.rot * Vector3.right;
                Vector3 bUpN = obb_b.rot * Vector3.up;

                //Separating Axis 1: aFwd
                if (info.obb_obb_penetrationIndex == 0)
                {
                    float dd = Vector3.Dot(d, aFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.z);
                    float rB = obb_b.GetAxisSize(aFwdN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 2: aRight
                else if (info.obb_obb_penetrationIndex == 1)
                {
                    float dd = Vector3.Dot(d, aRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.x);
                    float rB = obb_b.GetAxisSize(aRightN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 3: aUp
                else if (info.obb_obb_penetrationIndex == 2)
                {
                    float dd = Vector3.Dot(d, aUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.y);
                    float rB = obb_b.GetAxisSize(aUpN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 4: bFwd
                else if (info.obb_obb_penetrationIndex == 3)
                {
                    float dd = Vector3.Dot(d, bFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bFwdN);
                    float rB = Mathf.Abs(obb_b.size.z);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = bFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 5: bRight
                else if (info.obb_obb_penetrationIndex == 4)
                {
                    float dd = Vector3.Dot(d, bRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bRightN);
                    float rB = Mathf.Abs(obb_b.size.x);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = bRightN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 6: bUp
                else if (info.obb_obb_penetrationIndex == 5)
                {
                    float dd = Vector3.Dot(d, bUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(bUpN);
                    float rB = Mathf.Abs(obb_b.size.y);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        penetration = Vector3.zero;
                        return (penetration, aDp, bDp);
                    }

                    Vector3 p = bUpN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                }

                //Separating Axis 7: aFwd x bFwd
                else if (info.obb_obb_penetrationIndex == 6)
                {
                    Vector3 c = Vector3.Cross(aFwdN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 8: aFwd x bRight
                else if (info.obb_obb_penetrationIndex == 7)
                {
                    Vector3 c = Vector3.Cross(aFwdN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 9: aFwd x bUp
                else if (info.obb_obb_penetrationIndex == 8)
                {
                    Vector3 c = Vector3.Cross(aFwdN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 10: aRight x bFwd
                else if (info.obb_obb_penetrationIndex == 9)
                {
                    Vector3 c = Vector3.Cross(aRightN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 11: aRight x bRight
                else if (info.obb_obb_penetrationIndex == 10)
                {
                    Vector3 c = Vector3.Cross(aRightN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 12: aRight x bUp
                else if (info.obb_obb_penetrationIndex == 11)
                {
                    Vector3 c = Vector3.Cross(aRightN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 13: aUp x bFwd
                else if (info.obb_obb_penetrationIndex == 12)
                {
                    Vector3 c = Vector3.Cross(aUpN, bFwdN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 14: aUp x bRight
                else if (info.obb_obb_penetrationIndex == 13)
                {
                    Vector3 c = Vector3.Cross(aUpN, bRightN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                //Separating Axis 15: aUp x bUp
                else if (info.obb_obb_penetrationIndex == 14)
                {
                    Vector3 c = Vector3.Cross(aUpN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = obb_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        penetration = p;
                    }
                }

                if (penetration != Vector3.zero)
                {
                    Vector3 pDirN = -penetration.normalized;

                    float eps = FACE_PARALLEL_DOT_EPSILON;

                    float dAFwd = F32Sign101Epsilon(Vector3.Dot(aFwdN, pDirN), eps);
                    float dARight = F32Sign101Epsilon(Vector3.Dot(aRightN, pDirN), eps);
                    float dAUp = F32Sign101Epsilon(Vector3.Dot(aUpN, pDirN), eps);
                    int aPd = (dAFwd == 0 ? 1 : 0) + (dARight == 0 ? 1 : 0) + (dAUp == 0 ? 1 : 0);

                    float dBFwd = F32Sign101Epsilon(Vector3.Dot(bFwdN, -pDirN), eps);
                    float dBRight = F32Sign101Epsilon(Vector3.Dot(bRightN, -pDirN), eps);
                    float dBUp = F32Sign101Epsilon(Vector3.Dot(bUpN, -pDirN), eps);
                    int bPd = (dBFwd == 0 ? 1 : 0) + (dBRight == 0 ? 1 : 0) + (dBUp == 0 ? 1 : 0);

                    Vector3 ofAFwd = aFwd * dAFwd / 2;
                    Vector3 ofARight = aRight * dARight / 2;
                    Vector3 ofAUp = aUp * dAUp / 2;

                    Vector3 ofBFwd = bFwd * dBFwd / 2;
                    Vector3 ofBRight = bRight * dBRight / 2;
                    Vector3 ofBUp = bUp * dBUp / 2;

                    Vector3 nA = ofAFwd + ofARight + ofAUp;
                    Vector3 nB = ofBFwd + ofBRight + ofBUp;

                    aDp = obb_a.Center + nA;
                    bDp = obb_b.Center + nB;

                    nA.Normalize();
                    nB.Normalize();

                    Vector3 fA1 = Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fA2 = Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fB1 = Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bFwd * (dBFwd == 0 ? 1 : 0) : Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bRight * (dBRight == 0 ? 1 : 0) : Vector3.zero;
                    fB1 += (fB1 == Vector3.zero) ? bUp * (dBUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fB2 = Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bUp * (dBUp == 0 ? 1 : 0) : Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bRight * (dBRight == 0 ? 1 : 0) : Vector3.zero;
                    fB2 += (fB2 == Vector3.zero) ? bFwd * (dBFwd == 0 ? 1 : 0) : Vector3.zero;

                    fA1 /= 4f;
                    fA2 /= 4f;
                    fB1 /= 4f;
                    fB2 /= 4f;

                    if (aPd == 0 || bPd == 0)
                    {
                        if (aPd == 1)
                        {
                            aDp = ProjectPointToEdge(bDp, aDp + (fA1 + fA2), aDp - (fA1 + fA2));
                        }
                        else if (aPd == 2)
                        {
                            aDp = ProjectPointToPlane(bDp, nA, aDp);
                        }
                        else if (bPd == 1)
                        {
                            bDp = ProjectPointToEdge(aDp, bDp + (fB1 + fB2), bDp - (fB1 + fB2));
                        }
                        else if (bPd == 2)
                        {
                            bDp = ProjectPointToPlane(aDp, nB, bDp);
                        }

                        return (penetration, aDp, bDp);
                    }
                    else if (aPd == 1 && bPd == 1)
                    {
                        CalcNearest(aDp + (fA1 + fA2), aDp - (fA1 + fA2), bDp + (fB1 + fB2), bDp - (fB1 + fB2), out aDp, out bDp, out _);
                        return (penetration, aDp, bDp);
                    }
                    else if (aPd == 1)
                    {
                        fA1 *= 2f;
                        fA2 *= 2f;

                        Vector3 te_begin = aDp + (fA1 + fA2);
                        Vector3 te_end = aDp - (fA1 + fA2);

                        ReverseProjectLineToLine(ref te_begin, ref te_end, bDp + fB1, bDp - fB1);
                        ReverseProjectLineToLine(ref te_begin, ref te_end, bDp + fB2, bDp - fB2);

                        aDp = (te_begin + te_end) / 2;
                        bDp = ProjectPointToPlane(aDp, nB, bDp);

                        return (penetration, aDp, bDp);
                    }
                    else if (bPd == 1)
                    {
                        fB1 *= 2f;
                        fB2 *= 2f;

                        Vector3 te_begin = bDp + (fB1 + fB2);
                        Vector3 te_end = bDp - (fB1 + fB2);

                        ReverseProjectLineToLine(ref te_begin, ref te_end, aDp + fA1, aDp - fA1);
                        ReverseProjectLineToLine(ref te_begin, ref te_end, aDp + fA2, aDp - fA2);

                        bDp = (te_begin + te_end) / 2;
                        aDp = ProjectPointToPlane(bDp, nA, aDp);

                        return (penetration, aDp, bDp);
                    }
                    else
                    {
                        Vector3[] verts = new Vector3[8];

                        fA1 *= 2f;
                        fA2 *= 2f;
                        fB1 *= 2f;
                        fB2 *= 2f;

                        for (int i = 0; i < aPd * 2; i++)
                        {
                            if (aPd == 2)
                            {
                                verts[i] = aDp + fA1 * (((i + (i / 2)) % 2 == 0) ? 1 : -1) + fA2 * ((i / 2 == 0) ? 1 : -1);
                            }
                        }

                        for (int i = 0; i < bPd * 2; i++)
                        {
                            if (bPd == 2)
                            {
                                verts[4 + i] = bDp + fB1 * (((i + (i / 2)) % 2 == 0) ? 1 : -1) + fB2 * ((i / 2 == 0) ? 1 : -1);
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            verts[4 + i] = ProjectPointToPlane(verts[4 + i], nA, aDp);
                        }

                        Vector3[] contacts = new Vector3[16];
                        bool[] valid = new bool[16];

                        for (int ie = 0; ie < 4; ie++)
                        {
                            for (int je = 0; je < 4; je++)
                            {
                                contacts[ie * 4 + je] = CalcNearest(verts[ie], verts[(ie + 1) % 4], verts[4 + je], verts[4 + (je + 1) % 4], out bool invalid);
                                valid[ie * 4 + je] = !invalid;
                            }
                        }

                        Vector3 sum = Vector3.zero;
                        int count = 0;

                        for (int i = 0; i < 4; i++)
                        {
                            Vector3 c = verts[i];

                            if (c != Vector3.zero && IsInRect(c, verts[4], verts[5], verts[6], verts[7], nB))
                            {
                                sum += c;
                                count++;
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            Vector3 c = verts[4 + i];

                            if (c != Vector3.zero && IsInRect(c, verts[0], verts[1], verts[2], verts[3], nA))
                            {
                                sum += c;
                                count++;
                            }
                        }

                        for (int i = 0; i < 16; i++)
                        {
                            Vector3 c = contacts[i];
                            if (valid[i])
                            {
                                sum += c;
                                count++;
                            }
                        }

                        aDp = ProjectPointToPlane(sum / count, nA, aDp);
                        bDp = ProjectPointToPlane(sum / count, nB, bDp);
                        return (penetration, aDp, bDp);
                    }
                }

                return (Vector3.zero, Vector3.zero, Vector3.zero);
            }
        }
    }
}