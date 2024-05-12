using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBLine
        {
            const float FACE_PARALLEL_DOT_EPSILON = 0.000001f;

            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderLine line_b)
            {
                var r = CalcDetailCollision(obb_a, line_b);
                return new Penetration(r.p, r.pA, r.pB, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderOBB obb_a, RBColliderLine line_b)
            {
                float pSqrMag;
                Vector3 penetration;
                Vector3 pA = Vector3.zero;
                Vector3 pB = Vector3.zero;

                Vector3 d = line_b.Center - obb_a.Center;

                Vector3 aFwdN = obb_a.GetAxisForwardN();
                Vector3 aRightN = obb_a.GetAxisRightN();
                Vector3 aUpN = obb_a.GetAxisUpN();

                Vector3 bUpN = line_b.Direction;

                Vector3 aFwd = aFwdN * obb_a.size.z;
                Vector3 aRight = aRightN * obb_a.size.x;
                Vector3 aUp = aUpN * obb_a.size.y;

                //•ª—£Ž²‚PFaFwd
                {
                    float dd = Vector3.Dot(d, aFwdN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.z);
                    float rB = line_b.GetAxisSize(aFwdN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        return (Vector3.zero, Vector3.zero, Vector3.zero);
                    }

                    Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                    penetration = p;
                    pSqrMag = p.sqrMagnitude;
                }

                //•ª—£Ž²‚QFaRight
                {
                    float dd = Vector3.Dot(d, aRightN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.x);
                    float rB = line_b.GetAxisSize(aRightN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        return (Vector3.zero, Vector3.zero, Vector3.zero);
                    }

                    Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                }

                //•ª—£Ž²‚RFaUp
                {
                    float dd = Vector3.Dot(d, aUpN);
                    float prjL = Mathf.Abs(dd);
                    float rA = Mathf.Abs(obb_a.size.y);
                    float rB = line_b.GetAxisSize(aUpN);

                    float dp = prjL * 2 - (rA + rB);

                    if (dp > 0)
                    {
                        return (Vector3.zero, Vector3.zero, Vector3.zero);
                    }

                    Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                    bool pMin = p.sqrMagnitude < pSqrMag;
                    penetration = pMin ? p : penetration;
                    pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                }

                //•ª—£Ž²‚SFaFwd x bUp
                {
                    Vector3 c = Vector3.Cross(aFwdN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = line_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            return (Vector3.zero, Vector3.zero, Vector3.zero);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    }
                }

                //•ª—£Ž²‚TFaRight x bUp
                {
                    Vector3 c = Vector3.Cross(aRightN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = line_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            return (Vector3.zero, Vector3.zero, Vector3.zero);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    }
                }

                //•ª—£Ž²‚UFaUp x bUp
                {
                    Vector3 c = Vector3.Cross(aUpN, bUpN).normalized;

                    if (c != Vector3.zero)
                    {
                        float dd = Vector3.Dot(d, c);
                        float prjL = Mathf.Abs(dd);
                        float rA = obb_a.GetAxisSize(c);
                        float rB = line_b.GetAxisSize(c);

                        float dp = prjL * 2 - (rA + rB);

                        if (dp > 0)
                        {
                            return (Vector3.zero, Vector3.zero, Vector3.zero);
                        }

                        Vector3 p = c * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqrMag;
                        penetration = pMin ? p : penetration;
                        pSqrMag = pMin ? p.sqrMagnitude : pSqrMag;
                    }
                }

                {
                    Vector3 pDirN = -penetration.normalized;

                    float eps = 0.0001f;

                    float dAFwd = F32Sign101Epsilon(Vector3.Dot(aFwdN, pDirN), eps);
                    float dARight = F32Sign101Epsilon(Vector3.Dot(aRightN, pDirN), eps);
                    float dAUp = F32Sign101Epsilon(Vector3.Dot(aUpN, pDirN), eps);
                    int aPd = (dAFwd == 0 ? 1 : 0) + (dARight == 0 ? 1 : 0) + (dAUp == 0 ? 1 : 0);

                    Vector3 ofAFwd = aFwd * dAFwd / 2;
                    Vector3 ofARight = aRight * dARight / 2;
                    Vector3 ofAUp = aUp * dAUp / 2;

                    Vector3 nA = ofAFwd + ofARight + ofAUp;

                    pA = obb_a.Center + nA;

                    nA.Normalize();

                    Vector3 fA1 = Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA1 += (fA1 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;

                    Vector3 fA2 = Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aUp * (dAUp == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aRight * (dARight == 0 ? 1 : 0) : Vector3.zero;
                    fA2 += (fA2 == Vector3.zero) ? aFwd * (dAFwd == 0 ? 1 : 0) : Vector3.zero;

                    fA1 /= 2f;
                    fA2 /= 2f;

                    if (aPd == 0)
                    {
                        (Vector3 begin, Vector3 end) edge = (line_b.pos_a, line_b.pos_b);
                        pB = ProjectPointToEdge(pA, edge.begin, edge.end);
                        return (penetration, pA, pB);
                    }
                    else if (aPd == 1)
                    {
                        (Vector3 begin, Vector3 end) edge = (line_b.pos_a, line_b.pos_b);
                        CalcNearest(edge.begin, edge.end, pA + (fA1 + fA2) / 2f, pA - (fA1 + fA2) / 2f, out pA, out pB, out _);
                        return (penetration, pA, pB);
                    }
                    else if (aPd == 2)
                    {
                        (Vector3 begin, Vector3 end) edge = (line_b.pos_a, line_b.pos_b);

                        Vector3 rv1 = pA + fA1 + fA2;
                        Vector3 rv2 = pA - fA1 + fA2;
                        Vector3 rv3 = pA - fA1 - fA2;
                        Vector3 rv4 = pA + fA1 - fA2;

                        if (Mathf.Abs(Vector3.Dot(bUpN, pDirN)) < FACE_PARALLEL_DOT_EPSILON)
                        {
                            Vector3 te_begin = ProjectPointToPlane(edge.begin, nA, pA);
                            Vector3 te_end = ProjectPointToPlane(edge.end, nA, pA);

                            ReverseProjectLineToLine(ref te_begin, ref te_end, rv1, rv2);
                            ReverseProjectLineToLine(ref te_begin, ref te_end, rv2, rv3);

                            pA = (te_begin + te_end) / 2;
                            pB = ProjectPointToEdge(pA, edge.begin, edge.end);
                        }
                        else
                        {
                            Vector3 prjB = CalcNearestOnPlane(edge.begin, edge.end, nA, pA);

                            pA = ProjectPointToRect(prjB, rv1, rv2, rv3, rv4, pA, nA);
                            pB = ProjectPointToEdge(pA, edge.begin, edge.end);
                        }

                        return (penetration, pA, pB);
                    }
                }

                return (penetration, pA, pB);
            }
        }
    }
}