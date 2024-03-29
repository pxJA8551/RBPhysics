using RBPhys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBPhysCore;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public static partial class RBSphereCast
{
    public static class SphereCastOBB
    {
        public static RBColliderCastHitInfo CalcSphereCollision(RBColliderOBB obb, Vector3 org, Vector3 dirN, float length, float radius, bool allowNegativeDist)
        {
            Quaternion toLsRot = Quaternion.Inverse(obb.rot);
            Quaternion toWsRot = obb.rot;

            Vector3 ls_dirN = toLsRot * dirN;
            Vector3 ls_org = toLsRot * (org - obb.pos);

            Vector3 radius_size = Vector3.one * radius;

            float obbX = obb.size.x;
            float obbY = obb.size.y;
            float obbZ = obb.size.z;

            Vector3 size = obb.size;
            Vector3 extents = size / 2f;
            float eX = extents.x;
            float eY = extents.y;
            float eZ = extents.z;

            List<(float t, Vector3 pos, Vector3 normal)> hitPoints_t = new List<(float, Vector3, Vector3)>();

            //face6 xn
            {
                Vector3 f = new Vector3(-radius, eY, eZ);
                Vector3 xyzN = f - extents;
                AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t);
            }

            //face6 xp
            {
                Vector3 f = new Vector3(obbX + radius, eY, eZ);
                Vector3 xyzN = f - extents;
                if (AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t))
                {
                    if (hitPoints_t.Count == 2) goto L_RETURN;
                }
            }

            //face6 yn
            {
                Vector3 f = new Vector3(eX, -radius, eZ);
                Vector3 xyzN = f - extents;
                if (AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t))
                {
                    if (hitPoints_t.Count == 2) goto L_RETURN;
                }
            }

            //face6 yp
            {
                Vector3 f = new Vector3(eX, obbY + radius, eZ);
                Vector3 xyzN = f - extents;
                if (AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t))
                {
                    if (hitPoints_t.Count == 2) goto L_RETURN;
                }
            }

            //face6 zn
            {
                Vector3 f = new Vector3(eX, eY, -radius);
                Vector3 xyzN = f - extents;
                if (AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t))
                {
                    if (hitPoints_t.Count == 2) goto L_RETURN;
                }
            }

            //face6 zp
            {
                Vector3 f = new Vector3(eX, eY, obbZ + radius);
                Vector3 xyzN = f - extents;
                if (AABBPlaneRayOverlapXYZN(f, size, ls_org, ls_dirN, xyzN, ref hitPoints_t))
                {
                    if (hitPoints_t.Count == 2) goto L_RETURN;
                }
            }

            //vert8 xyz
            {
                Vector3 f = new Vector3(0, 0, 0);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 Xyz
            {
                Vector3 f = new Vector3(obbX, 0, 0);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(obb.pos + f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 xYz
            {
                Vector3 f = new Vector3(0, obbY, 0);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 XYz
            {
                Vector3 f = new Vector3(obbX, obbY, 0);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 xyZ
            {
                Vector3 f = new Vector3(0, 0, obbZ);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 XyZ
            {
                Vector3 f = new Vector3(obbX, 0, obbZ);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 xYZ
            {
                Vector3 f = new Vector3(0, obbY, obbZ);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //vert8 XYZ
            {
                Vector3 f = new Vector3(obbX, obbY, obbZ);
                Vector3 xyzN = f - extents;
                if (AABBOverlap(f, radius_size, ls_org, ls_dirN, xyzN))
                {
                    if (SphereRayOverlapXYZN(f, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 _21

            //edge12 xnN
            {
                Vector3 f = new Vector3(eX, 0, 0);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.x = obbX;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbX, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 xnP
            {
                Vector3 f = new Vector3(eX, obbY, 0);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.x = obbX;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbX, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 xpN
            {
                Vector3 f = new Vector3(eX, 0, obbZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.x = obbX;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbX, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 xpP
            {
                Vector3 f = new Vector3(eX, obbY, obbZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.x = obbX;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbX, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 ynN
            {
                Vector3 f = new Vector3(0, eY, 0);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.y = obbY;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbY, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 ynP
            {
                Vector3 f = new Vector3(0, eY, obbZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.y = obbY;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbY, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 ypN
            {
                Vector3 f = new Vector3(obbX, eY, 0);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.y = obbY;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbY, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 ypP
            {
                Vector3 f = new Vector3(obbX, eY, obbZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.y = obbY;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbY, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 znN
            {
                Vector3 f = new Vector3(0, 0, eZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.z = obbZ;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbZ, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 znP
            {
                Vector3 f = new Vector3(obbX, 0, eZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.z = obbZ;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbZ, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 zpN
            {
                Vector3 f = new Vector3(0, obbY, eZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.z = obbZ;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbZ, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            //edge12 zpP
            {
                Vector3 f = new Vector3(obbX, obbY, eZ);
                Vector3 xyzN = f - extents;
                Vector3 vp = radius_size;
                vp.z = obbZ;
                Vector3 vcp = f;
                if (AABBOverlap(vcp, vp, ls_org, ls_dirN, xyzN))
                {
                    if (CylinderRayOverlapXYZN(vcp, obbZ, radius, ls_org, ls_dirN, xyzN, ref hitPoints_t, allowNegativeDist))
                    {
                        if (hitPoints_t.Count == 2) goto L_RETURN;
                    }
                }
            }

            return default;

        // GOTO RETURN LABEL ========================

        L_RETURN:
            {
                if (hitPoints_t[1].t < hitPoints_t[0].t)
                {
                    (hitPoints_t[0], hitPoints_t[1]) = (hitPoints_t[1], hitPoints_t[0]);
                }

                bool pd1 = hitPoints_t[0].t > 0 || allowNegativeDist;
                bool pd2 = hitPoints_t[1].t > 0 || allowNegativeDist;

                if (pd1)
                {
                    Vector3 ls_pos = hitPoints_t[0].pos;
                    Vector3 ls_normal = hitPoints_t[0].normal;
                    ls_pos -= ls_normal * radius;

                    Vector3 ws_pos = obb.pos + toWsRot * ls_pos;
                    Vector3 ws_normal = toWsRot * ls_normal;

                    var info = new RBColliderCastHitInfo();
                    info.SetHit(ws_pos, ws_normal, hitPoints_t[0].t);
                    return info;
                }
                if (pd2)
                {
                    Vector3 ls_pos = hitPoints_t[1].pos;
                    Vector3 ls_normal = hitPoints_t[1].normal;
                    ls_pos -= ls_normal * radius;

                    Vector3 ws_pos = obb.pos + toWsRot * ls_pos;
                    Vector3 ws_normal = toWsRot * ls_normal;

                    var info = new RBColliderCastHitInfo();
                    info.SetHit(ws_pos, ws_normal, hitPoints_t[1].t);
                    return info;
                }

                return default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static (Vector3 v1, Vector3 v2) DecodeXYZN_Plane(Vector3 xyzN, Vector3 size)
        {
            Vector3 v1 = Vector3.zero;
            Vector3 v2 = Vector3.zero;

            size /= 2;

            if (xyzN.x == 0)
            {
                v1.x = -size.x;
                v2.x = size.x;
            }
            else
            {
                v1.x = 0;
                v2.x = 0;
            }

            if (xyzN.y == 0)
            {
                v1.y = -size.y;
                v2.y = size.y;
            }
            else
            {
                v1.y = 0;
                v2.y = 0;
            }

            if (xyzN.z == 0)
            {
                v1.z = -size.z;
                v2.z = size.z;
            }
            else
            {
                v1.z = 0;
                v2.z = 0;
            }

            return (v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool AABBOverlap(Vector3 aabb_pos, Vector3 aabb_size, Vector3 org, Vector3 dirN, Vector3 xyzN)
        {
            var v_xyzN = DecodeXYZN(xyzN, aabb_size);

            Vector3 pos_a = aabb_pos + v_xyzN.v1;
            Vector3 pos_b = aabb_pos + v_xyzN.v2;

            Vector3 pos_min = Vector3.Min(pos_a, pos_b);
            Vector3 pos_max = Vector3.Max(pos_a, pos_b);

            float t_x_min = (pos_min.x - org.x) / dirN.x;
            float t_x_max = (pos_max.x - org.x) / dirN.x;

            float t_y_min = (pos_min.y - org.y) / dirN.y;
            float t_y_max = (pos_max.y - org.y) / dirN.y;

            float t_z_min = (pos_min.z - org.z) / dirN.z;
            float t_z_max = (pos_max.z - org.z) / dirN.z;

            return RBPhysUtil.RangeOverlap(t_x_min, t_x_max, t_y_min, t_y_max, t_z_min, t_z_max, out float _, out float _, out int _, out int _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static (Vector3 v1, Vector3 v2) DecodeXYZN(Vector3 xyzN, Vector3 size)
        {
            Vector3 v1 = Vector3.zero;
            Vector3 v2 = Vector3.zero;

            if (xyzN.x > 0)
            {
                v2.x = size.x;
            }
            else if (xyzN.x < 0)
            {
                v2.x = -size.x;
            }
            else
            {
                v1.x = -size.x / 2f;
                v2.x = size.x / 2f;
            }

            if (xyzN.y > 0)
            {
                v2.y = size.y;
            }
            else if (xyzN.y < 0)
            {
                v2.y = -size.y;
            }
            else
            {
                v1.y = -size.y / 2f;
                v2.y = size.y / 2f;
            }

            if (xyzN.z > 0)
            {
                v2.z = size.z;
            }
            else if (xyzN.z < 0)
            {
                v2.z = -size.z;
            }
            else
            {
                v1.z = -size.z / 2f;
                v2.z = size.z / 2f;
            }

            return (v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool AABBPlaneRayOverlapXYZN(Vector3 aabb_pos, Vector3 aabb_size, Vector3 org, Vector3 dirN, Vector3 xyzN, ref List<(float t, Vector3 pos, Vector3 normal)> list)
        {
            var v_xyzN = DecodeXYZN_Plane(xyzN, aabb_size);

            Vector3 pos_a = aabb_pos + v_xyzN.v1;
            Vector3 pos_b = aabb_pos + v_xyzN.v2;

            Vector3 pos_min = Vector3.Min(pos_a, pos_b);
            Vector3 pos_max = Vector3.Max(pos_a, pos_b);

            float t_x_min = (pos_min.x - org.x) / dirN.x;
            float t_x_max = (pos_max.x - org.x) / dirN.x;

            float t_y_min = (pos_min.y - org.y) / dirN.y;
            float t_y_max = (pos_max.y - org.y) / dirN.y;

            float t_z_min = (pos_min.z - org.z) / dirN.z;
            float t_z_max = (pos_max.z - org.z) / dirN.z;

            bool rayHit = RBPhysUtil.RangeOverlap(t_x_min, t_x_max, t_y_min, t_y_max, t_z_min, t_z_max, out float r);

            if (rayHit)
            {
                Vector3 pos = org + dirN * r;

                Vector3 normal = Vector3.zero;
                normal.x = RBPhysUtil.F32Sign101(xyzN.x);
                normal.y = RBPhysUtil.F32Sign101(xyzN.y);
                normal.z = RBPhysUtil.F32Sign101(xyzN.z);

                list.Add((r, pos, normal));
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool SphereRayOverlapXYZN(Vector3 sphere_pos, float sphere_radius, Vector3 org, Vector3 dirN, Vector3 xyzN, ref List<(float t, Vector3 pos, Vector3 normal)> list, bool allowNegativeValue)
        {
            Vector3 p = sphere_pos - org;
            float b = Vector3.Dot(dirN, p);
            float c = Vector3.Dot(p, p) - (sphere_radius * sphere_radius);

            float b2 = b * b;
            float s = b2 - c;
            if (s < 0)
            {
                return false;
            }

            s = Mathf.Sqrt(s);

            float t1 = b - s;
            float t2 = b + s;

            Vector3 p1 = org + dirN * t1;
            Vector3 n1 = (p1 - sphere_pos).normalized;
            Vector3 p2 = org + dirN * t2;
            Vector3 n2 = (p2 - sphere_pos).normalized;

            bool pd1 = (t1 > 0 || allowNegativeValue) && RBPhysUtil.IsV3Sign101EpsilonEqualAll(xyzN, n1);
            bool pd2 = (t2 > 0 || allowNegativeValue) && RBPhysUtil.IsV3Sign101EpsilonEqualAll(xyzN, n2);

            if (pd1 && !pd2)
            {
                list.Add((t1, p1, n1));
                return true;
            }
            else if (!pd1 && pd2)
            {
                list.Add((t2, p2, n2));
                return true;
            }
            else if (pd1 && pd2)
            {
                list.Add((t1, p1, n1));
                list.Add((t2, p2, n2));
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool CylinderRayOverlapXYZN(Vector3 cylinder_center, float cylinder_height, float cylinder_radius, Vector3 org, Vector3 dirN, Vector3 xyzN, ref List<(float t, Vector3 pos, Vector3 normal)> list, bool allowNegativeValue)
        {
            Vector3 p;
            if (xyzN.x == 0) p = Vector3.right;
            else if (xyzN.y == 0) p = Vector3.up;
            else p = Vector3.forward;

            p *= cylinder_height; // p1 --> p2

            Vector3 p1 = cylinder_center - p / 2f;
            Vector3 p2 = cylinder_center + p / 2f;

            Vector3 ep1 = p1 - org;
            Vector3 ep2 = (p2 - org) - ep1;

            float dsv = Vector3.Dot(ep2, dirN);
            float dpv = Vector3.Dot(ep1, dirN);
            float dss = Vector3.Dot(ep2, ep2);
            float dps = Vector3.Dot(ep1, ep2);
            float dpp = Vector3.Dot(ep1, ep1);

            if (dss == 0)
            {
                return false;
            }

            float a = 1 - (dsv * dsv) / dss;
            float b = dpv - (dps * dsv) / dss;
            float c = dpp - (dps * dps) / dss - (cylinder_radius * cylinder_radius);

            if (a == 0)
            {
                return false;
            }

            float s = b * b - a * c;
            if (s < 0)
            {
                return false;
            }

            s = Mathf.Sqrt(s);

            float t1 = (b - s) / a;
            Vector3 q1 = org + dirN * t1;
            Vector3 n1 = Vector3.ProjectOnPlane(q1 - cylinder_center, p);

            float t2 = (b + s) / a;
            Vector3 q2 = org + dirN * t2;
            Vector3 n2 = Vector3.ProjectOnPlane(q2 - cylinder_center, p);

            bool pd1 = (t1 > 0 || allowNegativeValue) && RBPhysUtil.IsV3Sign101EpsilonEqualAll(xyzN, n1);
            bool pd2 = (t2 > 0 || allowNegativeValue) && RBPhysUtil.IsV3Sign101EpsilonEqualAll(xyzN, n2);

            if (pd1 && !pd2)
            {
                list.Add((t1, p1, n1));
                return true;
            }
            else if (!pd1 && pd2)
            {
                list.Add((t2, p2, n2));
                return true;
            }
            else if (pd1 && pd2)
            {
                list.Add((t1, p1, n1));
                list.Add((t2, p2, n2));
                return true;
            }

            return false;
        }
    }
}