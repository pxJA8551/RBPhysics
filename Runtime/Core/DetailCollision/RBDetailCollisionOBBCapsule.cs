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

                    aDp = ProjectPointToRect(bDp, aDp + fA1 - fA2, aDp + fA1 + fA2, aDp - fA1 + fA2, aDp - fA1 - fA2, aDp, pDirN);
                    if (radius_d) bDp = ProjectPointToEdge(aDp, capsuleEdge.begin, capsuleEdge.end);

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
        }
    }
}