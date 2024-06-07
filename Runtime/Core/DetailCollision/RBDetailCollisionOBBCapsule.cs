using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBDetailCollision;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBCapsule
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                var r = CalcDetailCollision(obb_a, capsule_b);
                return new Penetration(r.p, r.pA, r.pB, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                Vector3 pDir = Vector3.zero;
                Vector3 pA = Vector3.zero;
                Vector3 pB = Vector3.zero;

                var edge = capsule_b.GetEdge();
                var org = edge.end;
                var capsuleDirN = capsule_b.GetHeightAxisN();
                float edgeLength = capsule_b.height;

                Quaternion toLsRot = Quaternion.Inverse(obb_a.rot);

                Vector3 lsPA;
                Vector3 lsPB;

                bool bA;
                bool bB;

                {
                    Vector3 lsDirN = toLsRot * capsuleDirN;
                    Vector3 lsOrg = toLsRot * (org - obb_a.pos);

                    float t_x_min = -lsOrg.x / lsDirN.x;
                    float t_x_max = (obb_a.size.x - lsOrg.x) / lsDirN.x;

                    float t_y_min = -lsOrg.y / lsDirN.y;
                    float t_y_max = (obb_a.size.y - lsOrg.y) / lsDirN.y;

                    float t_z_min = -lsOrg.z / lsDirN.z;
                    float t_z_max = (obb_a.size.z - lsOrg.z) / lsDirN.z;

                    bool rayHit = RBPhysUtil.RangeOverlap(t_x_min, t_x_max, t_y_min, t_y_max, t_z_min, t_z_max, out float t_min, out float t_max, out int i_min, out int i_max);

                    bA = (rayHit && t_min > 0 && t_min < edgeLength);
                    bB = (rayHit && t_max > 0 && t_max < edgeLength);

                    lsPA = bA ? lsOrg + lsDirN * t_min : lsOrg;
                    lsPB = bB ? lsOrg + lsDirN * t_max : lsOrg + lsDirN * edgeLength;
                }

                Vector3 wsPA = obb_a.pos + obb_a.rot * lsPA;
                Vector3 wsPB = obb_a.pos + obb_a.rot * lsPB;

                if (bA || bB)
                {
                    Vector3 d = (wsPB + wsPA) / 2f - obb_a.pos;
                    Vector3 wsD = wsPB - wsPA;
                    Vector3 dN = d.normalized;
                    float wsLength = d.magnitude;

                    Vector3 penetration;
                    float pSqr;

                    {
                        Vector3 aRightN = obb_a.GetAxisRightN();
                        if (d == Vector3.zero) { d = aRightN * .001f; }

                        float dd = Vector3.Dot(d, aRightN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.x);
                        float rB = Vector3.Dot(wsD, aRightN);

                        float dp = prjL * 2 - (rA + rB);

                        Vector3 p = aRightN * (dp / 2) * F32Sign11(dd);

                        pSqr = p.sqrMagnitude;
                        penetration = p;

                        bool pMin = p.sqrMagnitude < pSqr;
                        penetration = pMin ? p : penetration;
                        pSqr = pMin ? p.sqrMagnitude : pSqr;
                    }

                    {
                        Vector3 aUpN = obb_a.GetAxisUpN();
                        if (d == Vector3.zero) { d = aUpN * .001f; }

                        float dd = Vector3.Dot(d, aUpN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.y);
                        float rB = Vector3.Dot(wsD, aUpN);

                        float dp = prjL * 2 - (rA + rB);

                        Vector3 p = aUpN * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqr;
                        penetration = pMin ? p : penetration;
                        pSqr = pMin ? p.sqrMagnitude : pSqr;
                    }

                    {
                        Vector3 aFwdN = obb_a.GetAxisForwardN();
                        if (d == Vector3.zero) { d = aFwdN * .001f; }

                        float dd = Vector3.Dot(d, aFwdN);
                        float prjL = Mathf.Abs(dd);
                        float rA = Mathf.Abs(obb_a.size.z);
                        float rB = Vector3.Dot(wsD, aFwdN);

                        float dp = prjL * 2 - (rA + rB);

                        Vector3 p = aFwdN * (dp / 2) * F32Sign11(dd);

                        bool pMin = p.sqrMagnitude < pSqr;
                        penetration = pMin ? p : penetration;
                        pSqr = pMin ? p.sqrMagnitude : pSqr;
                    }

                    pA = wsPA;
                    pB = wsPB + (penetration.normalized) * capsule_b.radius;

                    return (penetration, pA, pB);
                }
                else
                {
                    {
                        var hs = obb_a.size / 2f;
                        var r = obb_a.rot;

                        Vector3 penetration = Vector3.zero;

                        float sqrP = -1;
                        {
                            Vector3 vr = r * V3Multiply(hs, 1, 0, 0);
                            var p = obb_a.Center + vr;
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 1, 1, 0), p + V3Multiply(hs, 1, -1, 0), p + V3Multiply(hs, -1, -1, 0), p + V3Multiply(hs, -1, 1, 0), vr.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if (Vector3.Dot(pp, vr.normalized) > 0)
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        {
                            var p = obb_a.Center + r * V3Multiply(hs, -1, 0, 0);
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 1, 1, 0), p + V3Multiply(hs, 1, -1, 0), p + V3Multiply(hs, -1, -1, 0), p + V3Multiply(hs, -1, 1, 0), capsuleDirN.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if ((sqrPP < sqrP || sqrP == -1))
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        {
                            var p = obb_a.Center + r * V3Multiply(hs, 0, 1, 0);
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 1, 0, 1), p + V3Multiply(hs, 1, 0, -1), p + V3Multiply(hs, -1, 0, -1), p + V3Multiply(hs, -1, 0, 1), capsuleDirN.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if ((sqrPP < sqrP || sqrP == -1))
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        {
                            var p = obb_a.Center + r * V3Multiply(hs, 0, -1, 0);
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 1, 0, 1), p + V3Multiply(hs, 1, 0, -1), p + V3Multiply(hs, -1, 0, -1), p + V3Multiply(hs, -1, 0, 1), capsuleDirN.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if ((sqrPP < sqrP || sqrP == -1))
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        {
                            var p = obb_a.Center + r * V3Multiply(hs, 0, 0, 1);
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 1, 0, 1), p + V3Multiply(hs, 1, 0, -1), p + V3Multiply(hs, -1, 0, -1), p + V3Multiply(hs, -1, 0, 1), capsuleDirN.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if ((sqrPP < sqrP || sqrP == -1))
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        {
                            var p = obb_a.Center + r * V3Multiply(hs, 0, 0, -1);
                            CalcNearestOnRectP(edge.begin, edge.end, p + V3Multiply(hs, 0, 1, 1), p + V3Multiply(hs, 0, 1, -1), p + V3Multiply(hs, 0, -1, -1), p + V3Multiply(hs, 0, -1, 1), capsuleDirN.normalized, p, out Vector3 ppA, out Vector3 ppB, out _);

                            Vector3 pp = ppB - ppA;

                            float sqrPP = pp.sqrMagnitude;
                            if ((sqrPP < sqrP || sqrP == -1))
                            {
                                sqrP = sqrPP;
                                penetration = pp;
                                pA = ppA;
                                pB = ppB;
                            }
                        }

                        if (penetration.magnitude < capsule_b.radius)
                        {
                            penetration = pA - pB;

                            float epsilon = .0001f * .0001f;

                            if ((pA - edge.begin).sqrMagnitude < epsilon)
                            {
                                penetration *= Vector3.Dot(penetration, -capsuleDirN) > 0 ? 1 : -1;
                            }
                            else if ((pA - edge.end).sqrMagnitude < epsilon)
                            {
                                penetration *= Vector3.Dot(penetration, capsuleDirN) > 0 ? 1 : -1;
                            }
                            else
                            {
                                penetration = Vector3.ProjectOnPlane(penetration, -capsuleDirN);
                            }

                            pB = pB + penetration.normalized * capsule_b.radius;
                            penetration = pA - pB;
                            return (penetration, pA, pB);
                        }

                        return default;
                    }
                }
            }
        }
    }
}