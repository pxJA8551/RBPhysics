using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
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
                return new Penetration(r.p, r.pA, r.pB, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
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

                bool radius_d;

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

                    radius_d = (dp > -capsule_b.radius * 2);
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
                    radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
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
                    radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
                }

                //Separating Axis 4: capsuleDir
                {
                    float dd = Vector3.Dot(d, capsuleDirN);
                    float prjL = Mathf.Abs(dd);
                    float rA = obb_a.GetAxisSize(capsuleDirN);
                    float rB = capsule_b.height + capsule_b.radius * 2;

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
                    radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
                }

                //Separating Axis 5: aFwd x capsuleDir
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
                        radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
                    }
                }

                //Separating Axis 6: aFwd x capsuleDir
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
                        radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
                    }
                }

                //Separating Axis 7: aFwd x capsuleDir
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
                        radius_d = pMin ? (dp > -capsule_b.radius * 2) : radius_d;
                    }
                }

                if (pSqrMag != -1)
                {
                    Vector3 pDirN = -penetration.normalized;
                    float eps = FACE_PARALLEL_DOT_EPSILON;

                    float dAFwd = F32Sign101Epsilon(Vector3.Dot(aFwdN, pDirN), eps);
                    float dARight = F32Sign101Epsilon(Vector3.Dot(aRightN, pDirN), eps);
                    float dAUp = F32Sign101Epsilon(Vector3.Dot(aUpN, pDirN), eps);

                    float dCapsuleDir = F32Sign101Epsilon(Vector3.Dot(capsuleDirN, -pDirN), eps);

                    Vector3 fA1 = Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fA2 = Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 ofAFwd = aFwd * dAFwd / 2;
                    Vector3 ofARight = aRight * dARight / 2;
                    Vector3 ofAUp = aUp * dAUp / 2;

                    Vector3 ofCapsuleDir = capsuleLine * dCapsuleDir / 2;

                    Vector3 nA = ofAFwd + ofARight + ofAUp;
                    Vector3 nB = ofCapsuleDir;

                    aDp = obb_a.Center + nA;
                    bDp = capsule_b.pos + nB;

                    nA.Normalize();
                    nB.Normalize();

                    fA1 /= 2f;
                    fA2 /= 2f;

                    aDp = RBVectorUtil.ProjectPointToRect(bDp, aDp + fA1 - fA2, aDp + fA1 + fA2, aDp - fA1 + fA2, aDp - fA1 - fA2, aDp, pDirN);
                    if (radius_d) bDp = RBVectorUtil.ProjectPointToEdge(aDp, capsuleEdge.begin, capsuleEdge.end);

                    var v = aDp - bDp;

                    if (radius_d && v.magnitude > capsule_b.radius)
                    {
                        return default;
                    }

                    bDp -= (v * Vector3.Dot(v, pDirN)).normalized * capsule_b.radius;

                    return (bDp - aDp, aDp, bDp);
                }

                return default;
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision2(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                var capsuleEdge = capsule_b.GetEdge();
                var capsuleDirN = capsule_b.GetHeightAxisN();

                {
                    Vector3 cp = capsuleEdge.begin;
                    Vector3 d = (cp - obb_a.Center);

                    Vector3 aFwdN = obb_a.GetAxisForwardN();
                    Vector3 aRightN = obb_a.GetAxisRightN();
                    Vector3 aUpN = obb_a.GetAxisUpN();

                    float dpx = Mathf.Clamp(Vector3.Dot(aRightN, d), -obb_a.size.x / 2, obb_a.size.x / 2);
                    float dpy = Mathf.Clamp(Vector3.Dot(aUpN, d), -obb_a.size.y / 2, obb_a.size.y / 2);
                    float dpz = Mathf.Clamp(Vector3.Dot(aFwdN, d), -obb_a.size.z / 2, obb_a.size.z / 2);

                    Vector3 pA = obb_a.Center + obb_a.rot * new Vector3(dpx, dpy, dpz);
                    Vector3 pd = pA - cp;
                    float pdL = pd.magnitude;

                    if (pdL == 0)
                    {
                        return (Vector3.zero, pA, cp);
                    }

                    Vector3 pdN = pd / pdL;
                    Vector3 pB = cp + pdN * capsule_b.radius;
                    float sub = capsule_b.radius - pdL;

                    if (sub > 0 && Vector3.Dot(pdN, capsuleDirN) > 0)
                    {
                        return (pdN * sub, pA, pB);
                    }
                }

                {
                    Vector3 cp = capsuleEdge.end;
                    Vector3 d = (cp - obb_a.Center);

                    Vector3 aFwdN = obb_a.GetAxisForwardN();
                    Vector3 aRightN = obb_a.GetAxisRightN();
                    Vector3 aUpN = obb_a.GetAxisUpN();

                    float dpx = Mathf.Clamp(Vector3.Dot(aRightN, d), -obb_a.size.x / 2, obb_a.size.x / 2);
                    float dpy = Mathf.Clamp(Vector3.Dot(aUpN, d), -obb_a.size.y / 2, obb_a.size.y / 2);
                    float dpz = Mathf.Clamp(Vector3.Dot(aFwdN, d), -obb_a.size.z / 2, obb_a.size.z / 2);

                    Vector3 pA = obb_a.Center + obb_a.rot * new Vector3(dpx, dpy, dpz);
                    Vector3 pd = pA - cp;
                    float pdL = pd.magnitude;

                    if (pdL == 0)
                    {
                        return (Vector3.zero, pA, cp);
                    }

                    Vector3 pdN = pd / pdL;
                    Vector3 pB = cp + pdN * capsule_b.radius;
                    float sub = capsule_b.radius - pdL;

                    if (sub > 0 && Vector3.Dot(pdN, -capsuleDirN) > 0)
                    {
                        return (pdN * sub, pA, pB);
                    }
                }

                {
                    Vector3 aFwdN = obb_a.rot * Vector3.forward;
                    Vector3 aRightN = obb_a.rot * Vector3.right;
                    Vector3 aUpN = obb_a.rot * Vector3.up;

                    Vector3 aFwd = aFwdN * obb_a.size.z;
                    Vector3 aRight = aRightN * obb_a.size.x;
                    Vector3 aUp = aUpN * obb_a.size.y;

                    Vector3 d = capsule_b.pos - obb_a.Center;

                    Vector3 aDp = Vector3.zero;
                    Vector3 bDp = Vector3.zero;

                    Vector3 penetration;
                    float pSqrMag;

                    //Separating Axis 1: aFwd
                    {
                        float dd = Vector3.Dot(d, aFwdN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.z);
                        float rB = capsule_b.GetCylinderAxisN(aFwdN);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }
                    }

                    //Separating Axis 2: aRight
                    {
                        float dd = Vector3.Dot(d, aRightN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.x);
                        float rB = capsule_b.GetCylinderAxisN(aRightN);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }
                    }

                    //Separating Axis 3: aUp
                    {
                        float dd = Vector3.Dot(d, aUpN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.y);
                        float rB = capsule_b.GetCylinderAxisN(aUpN);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            penetration = Vector3.zero;
                            return (penetration, aDp, bDp);
                        }
                    }

                    // Project Closest Edge of OBB On Plane: n = bUp
                    {
                        Vector3 vxyz = obb_a.pos;
                        Vector3 vxyZ = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 0, 0, 1);
                        Vector3 vxYz = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 0, 1, 0);
                        Vector3 vxYZ = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 0, 1, 1);
                        Vector3 vXyz = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 1, 0, 0);
                        Vector3 vXyZ = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 1, 0, 1);
                        Vector3 vXYz = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 1, 1, 0);
                        Vector3 vXYZ = obb_a.pos + obb_a.rot * V3Multiply(obb_a.size, 1, 1, 1);

                        Quaternion toLsRot = Quaternion.Inverse(capsule_b.rot);

                        Vector3 pxyz = V3YZero(toLsRot * (vxyz - capsule_b.pos));
                        Vector3 pxyZ = V3YZero(toLsRot * (vxyZ - capsule_b.pos));
                        Vector3 pxYz = V3YZero(toLsRot * (vxYz - capsule_b.pos));
                        Vector3 pxYZ = V3YZero(toLsRot * (vxYZ - capsule_b.pos));
                        Vector3 pXyz = V3YZero(toLsRot * (vXyz - capsule_b.pos));
                        Vector3 pXyZ = V3YZero(toLsRot * (vXyZ - capsule_b.pos));
                        Vector3 pXYz = V3YZero(toLsRot * (vXYz - capsule_b.pos));
                        Vector3 pXYZ = V3YZero(toLsRot * (vXYZ - capsule_b.pos));

                        float dSqr = -1;

                        float gR = capsule_b.radius;

                        Vector3 pA = Vector3.zero;
                        Vector3 pB = ProjectPointToEdge(pA, capsuleEdge.begin, capsuleEdge.end);

                        {
                            CalcClosestInXZRectPZero(pxyz, pXyz, pXyZ, pxyZ, vxyz, vXyz, vXyZ, vxyZ, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0)
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        {
                            CalcClosestInXZRectPZero(pXyz, pXyZ, pXYZ, pXYz, vXyz, vXyZ, vXYZ, vXYz, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0 && (pSqr < dSqr || dSqr < 0))
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        {
                            CalcClosestInXZRectPZero(pxyz, pxyZ, pxYZ, pxYz, vxyz, vxyZ, vxYZ, vxYz, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0 && (pSqr < dSqr || dSqr < 0))
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        {
                            CalcClosestInXZRectPZero(pxyz, pXyz, pXYz, pxYz, vxyz, vXyz, vXYz, vxYz, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0 && (pSqr < dSqr || dSqr < 0))
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        {
                            CalcClosestInXZRectPZero(pxyZ, pXyZ, pXYZ, pxYZ, vxyZ, vXyZ, vXYZ, vxYZ, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0 && (pSqr < dSqr || dSqr < 0))
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        {
                            CalcClosestInXZRectPZero(pxYz, pXYz, pXYZ, pxYZ, vxYz, vXYz, vXYZ, vxYZ, out Vector3 p, out Vector3 pp);

                            float pSqr = p.sqrMagnitude;

                            if (pSqr != 0 && (pSqr < dSqr || dSqr < 0))
                            {
                                dSqr = pSqr;
                                pA = pp;
                            }
                        }

                        float dp = capsule_b.radius - Mathf.Sqrt(dSqr);

                        if (dp > 0)
                        {
                            pB = ProjectPointToEdge(pA, capsuleEdge.begin, capsuleEdge.end);

                            Vector3 pd = Vector3.ProjectOnPlane(pA - pB, capsuleDirN);
                            pB = pA + pd * dp;
                            pd = pA - pB;
                            pd *= -1;
                            return (pd, pA, pB);
                        }
                        else
                        {
                            return default;
                        }

                        [MethodImpl(MethodImplOptions.AggressiveInlining)]
                        void CalcClosestInXZRectPZero(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 va, Vector3 vb, Vector3 vc, Vector3 vd, out Vector3 p, out Vector3 pp)
                        {
                            p = Vector3.zero;
                            float cab = Vector3.Cross(b - a, a - p).y;
                            float cbc = Vector3.Cross(c - b, b - p).y;
                            float ccd = Vector3.Cross(d - c, c - p).y;
                            float cda = Vector3.Cross(a - d, d - p).y;

                            float s = cab + cbc + ccd;
                            pp = va * (cab / s) + vb * (cbc / s) + vc * (ccd / s);

                            if (cab < 0)
                            {
                                p = ProjectPointToEdgePZero(a, b, out float tp);
                                pp = va * (1 - tp) + vb * tp;
                                return;
                            }

                            if (cbc < 0)
                            {
                                p = ProjectPointToEdgePZero(b, c, out float tp);
                                pp = vb * (1 - tp) + vc * tp;
                                return;
                            }

                            if (ccd < 0)
                            {
                                p = ProjectPointToEdgePZero(c, d, out float tp);
                                pp = vc * (1 - tp) + vd * tp;
                                return;
                            }

                            if (cda < 0)
                            {
                                p = ProjectPointToEdgePZero(d, a, out float tp);
                                pp = vd * (1 - tp) + va * tp;
                                return;
                            }

                            return;
                        }
                    }

                    return default;
                }
            }
        }
    }
}