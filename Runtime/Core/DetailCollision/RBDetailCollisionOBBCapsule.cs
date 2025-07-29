using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;
using static RBPhys.FixedSortedAppendList7_Float;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBCapsule
        {
            const float FAST_CLIP_ASSERT_EPSILON = .00003f;

            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                Profiler.BeginSample("DetailTest/OBB-Capsule");
                var r = CalcDetailCollision(obb_a, capsule_b);

                Profiler.EndSample();
                return new Penetration(r.p, r.pA, r.pB);
            }

            enum FaceType
            {
                XNegative,
                XPositive,
                YNegative,
                YPositive,
                ZNegative,
                ZPositive
            }

            struct LineSegmentType
            {
                public enum SlabType
                {
                    Negative,
                    Slab,
                    Positive
                }

                public SlabType x;
                public SlabType y;
                public SlabType z;

                public float t0;
                public float t1;
            }

            public static Penetration CalcDetailCollision(RBColliderOBB obb_a, RBColliderCapsule capsule_b)
            {
                var rotInv = Quaternion.Inverse(obb_a.rot);

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 Ws2Ls(Vector3 ws) => rotInv * (ws - obb_a.pos);

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 Ls2Ws(Vector3 ls) => obb_a.pos + (obb_a.rot * ls);

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 LsDir2Ws(Vector3 ls) => obb_a.rot * ls;

                (Vector3 p0, Vector3 p1) = capsule_b.GetEdge();
                Vector3 p0Ls = Ws2Ls(p0);
                Vector3 p1Ls = Ws2Ls(p1);

                Vector3 dLs = p1Ls - p0Ls;
                float dLsL = dLs.magnitude;

                if (dLsL < 0.0001f)
                {
                    //ãÖÇ∆ÇµÇƒåvéZ
                    return DetailCollisionOBBSphere.CalcDetailCollision(obb_a, new RBColliderSphere(p0, capsule_b.radius));
                }

                Vector3 dLsN = dLs / dLsL;

                const float EPSILON0 = .000001f;

                int d_x = (int)F32Sign101Epsilon(dLsN.x, EPSILON0);
                int d_y = (int)F32Sign101Epsilon(dLsN.y, EPSILON0);
                int d_z = (int)F32Sign101Epsilon(dLsN.z, EPSILON0);

                float t_x_min = (0 - p0Ls.x) / dLs.x;
                float t_x_max = (obb_a.size.x - p0Ls.x) / dLs.x;

                float t_y_min = (0 - p0Ls.y) / dLs.y;
                float t_y_max = (obb_a.size.y - p0Ls.y) / dLs.y;

                float t_z_min = (0 - p0Ls.z) / dLs.z;
                float t_z_max = (obb_a.size.z - p0Ls.z) / dLs.z;

                FixedSortedAppendList7_Float t_sorted = new FixedSortedAppendList7_Float();

                if (d_x != 0)
                {
                    t_sorted.Append(t_x_min);
                    t_sorted.Append(t_x_max);
                }

                if (d_y != 0)
                {
                    t_sorted.Append(t_y_min);
                    t_sorted.Append(t_y_max);
                }

                if (d_z != 0)
                {
                    t_sorted.Append(t_z_min);
                    t_sorted.Append(t_z_max);
                }

                Debug.Assert(t_sorted.Count >= 2);
                Debug.Assert(t_sorted.Count <= 6);

                FixedAppendList7<LineSegmentType> segments = new FixedAppendList7<LineSegmentType>();

                {
                    segments.Fill(t_sorted.Count + 1);

                    for (int i = 0; i < t_sorted.Count; i++)
                    {
                        var t = t_sorted[i];
                        var tLast = float.NegativeInfinity;
                        if (0 < i) tLast = t_sorted[i - 1];

                        segments[i] = CalcLineSegment(t, tLast);
                    }

                    segments[t_sorted.Count] = CalcLineSegmentLS(t_sorted[t_sorted.Count - 1]);

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    LineSegmentType CalcLineSegment(float t, float tLast)
                    {
                        LineSegmentType segment = new LineSegmentType();
                        segment.x = CalcSlabType(t, t_x_min, t_x_max, d_x, obb_a.size.x, p0Ls.x);
                        segment.y = CalcSlabType(t, t_y_min, t_y_max, d_y, obb_a.size.y, p0Ls.y);
                        segment.z = CalcSlabType(t, t_z_min, t_z_max, d_z, obb_a.size.z, p0Ls.z);

                        segment.t0 = tLast;
                        segment.t1 = t;

                        return segment;
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    LineSegmentType CalcLineSegmentLS(float t)
                    {
                        LineSegmentType segment = new LineSegmentType();
                        segment.x = CalcSlabTypeLS(t, d_x, obb_a.size.x, p0Ls.x);
                        segment.y = CalcSlabTypeLS(t, d_y, obb_a.size.y, p0Ls.y);
                        segment.z = CalcSlabTypeLS(t, d_z, obb_a.size.z, p0Ls.z);

                        segment.t0 = t;
                        segment.t1 = float.PositiveInfinity;

                        return segment;
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    LineSegmentType.SlabType CalcSlabType(float t, float t_min, float t_max, int d, float size, float p0Ls)
                    {
                        LineSegmentType.SlabType slabType;

                        if (d == 1)
                        {
                            if (t <= t_min) slabType = LineSegmentType.SlabType.Negative;
                            else if (t <= t_max) slabType = LineSegmentType.SlabType.Slab;
                            else slabType = LineSegmentType.SlabType.Positive;
                        }
                        else if (d == -1)
                        {
                            if (t <= t_max) slabType = LineSegmentType.SlabType.Positive;
                            else if (t <= t_min) slabType = LineSegmentType.SlabType.Slab;
                            else slabType = LineSegmentType.SlabType.Negative;
                        }
                        else
                        {
                            if (p0Ls < 0) slabType = LineSegmentType.SlabType.Negative;
                            else if (size < p0Ls) slabType = LineSegmentType.SlabType.Positive;
                            else slabType = LineSegmentType.SlabType.Slab;
                        }

                        return slabType;
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    LineSegmentType.SlabType CalcSlabTypeLS(float t, int d, float size, float p0Ls)
                    {
                        LineSegmentType.SlabType slabType;

                        if (d == 1)
                        {
                            slabType = LineSegmentType.SlabType.Positive;
                        }
                        else if (d == -1)
                        {
                            slabType = LineSegmentType.SlabType.Negative;
                        }
                        else
                        {
                            if (p0Ls < 0) slabType = LineSegmentType.SlabType.Negative;
                            else if (size < p0Ls) slabType = LineSegmentType.SlabType.Positive;
                            else slabType = LineSegmentType.SlabType.Slab;
                        }

                        return slabType;
                    }
                }

                Debug.Assert(segments.Count >= 3);
                Debug.Assert(segments.Count <= 7);

                //Line intersecting OBB (1)
                {
                    int index = segments.FindIndex(item => item.x == LineSegmentType.SlabType.Slab && item.y == LineSegmentType.SlabType.Slab && item.z == LineSegmentType.SlabType.Slab);
                    if (index != -1)
                    {
                        var seg = segments[index];

                        if (IsTValid(seg.t0, seg.t1))
                        {
                            var tp0Ls = T2Ls(0);
                            var tp1Ls = T2Ls(1);

                            //vAB: OBB --> Capsule

                            float d = 0;
                            Vector3 cpLs = Vector3.zero;
                            Vector3 vAB = Vector3.zero;

                            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                            void CalcFurthest(float d0, float d1, Vector3 p0, Vector3 p1, out float d, out Vector3 cp)
                            {
                                if (IsF32EpsilonEqual(d0, d1, .00001f)) { d = d0; cp = (p0 + p1) / 2f; }
                                else if (d0 > d1) { d = d0; cp = p0; }
                                else { d = d1; cp = p1; }
                            }

                            {
                                CalcFurthest(tp0Ls.x, tp1Ls.x, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                d = temp_d;
                                cpLs = temp_cpLs;
                                vAB = Vector3.right;
                            }

                            {
                                CalcFurthest(obb_a.size.x - tp0Ls.x, obb_a.size.x - tp1Ls.x, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                if (temp_d < d)
                                {
                                    d = temp_d;
                                    cpLs = temp_cpLs;
                                    vAB = Vector3.left;
                                }
                            }

                            {
                                CalcFurthest(tp0Ls.y, tp1Ls.y, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                if (temp_d < d)
                                {
                                    d = temp_d;
                                    cpLs = temp_cpLs;
                                    vAB = Vector3.up;
                                }
                            }

                            {
                                CalcFurthest(obb_a.size.y - tp0Ls.y, obb_a.size.y - tp1Ls.y, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                if (temp_d < d)
                                {
                                    d = temp_d;
                                    cpLs = temp_cpLs;
                                    vAB = Vector3.down;
                                }
                            }

                            {
                                CalcFurthest(tp0Ls.z, tp1Ls.z, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                if (temp_d < d)
                                {
                                    d = temp_d;
                                    cpLs = temp_cpLs;
                                    vAB = Vector3.forward;
                                }
                            }

                            {
                                CalcFurthest(obb_a.size.z - tp0Ls.z, obb_a.size.z - tp1Ls.z, tp0Ls, tp1Ls, out float temp_d, out Vector3 temp_cpLs);
                                if (temp_d < d)
                                {
                                    d = temp_d;
                                    cpLs = temp_cpLs;
                                    vAB = Vector3.back;
                                }
                            }

                            if (d > .00001f)
                            {
                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, vAB);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                return CalcPenetrationWs(contactLs_obb, contactLs_capsule, -CalcPenetration(contactLs_obb, cpLs));
                            }
                            else
                            {
                                Debug.Assert(false);
                            }
                        }
                    }
                }

                Penetration penetration = default;
                float _cached_pLSqr = capsule_b.radius * capsule_b.radius;

                // Nearest point exists at OBB-Vtx (8)
                {
                    foreach (var seg in segments.Values.Where(item => item.x != LineSegmentType.SlabType.Slab && item.y != LineSegmentType.SlabType.Slab && item.z != LineSegmentType.SlabType.Slab))
                    {
                        if (IsTValid(seg.t0, seg.t1))
                        {
                            (var t0, var t1) = TClip(seg.t0, seg.t1);

                            var tp0Ls = T2Ls(t0);
                            var tp1Ls = T2Ls(t1);

                            Vector3 contactLs_obb = Segment2Vtx(seg);
                            Vector3 cpLs = ProjectPointToEdge(contactLs_obb, tp0Ls, tp1Ls);

                            var p = contactLs_obb - cpLs;
                            if (!ClipPenetration(p.sqrMagnitude, out float pL)) break;

                            var pD = capsule_b.radius - pL;

                            Vector3 penetrationLs;

                            if (pD > 0 && pL > .00001f)
                            {
                                penetrationLs = (p / pL) * pD;
                            }
                            else break;

                            Vector3 contactLs_capsule = ExtrudeLine(cpLs, p / pL);

                            Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < penetrationLs.sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                            penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, penetrationLs);
                            _cached_pLSqr = pL * pL;
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    Vector3 Segment2Vtx(LineSegmentType segment)
                    {
                        Vector3 v = Vector3.zero;
                        if (segment.x == LineSegmentType.SlabType.Positive) v.x = obb_a.size.x;
                        if (segment.y == LineSegmentType.SlabType.Positive) v.y = obb_a.size.y;
                        if (segment.z == LineSegmentType.SlabType.Positive) v.z = obb_a.size.z;

                        return v;
                    }
                }

                // Nearest point exists at OBB-Edge (12)
                {
                    foreach (var seg in segments.Values.Where(item => ClipSegment(item)))
                    {
                        if (IsTValid(seg.t0, seg.t1))
                        {
                            (var t0, var t1) = TClip(seg.t0, seg.t1);

                            var tp0Ls = T2Ls(t0);
                            var tp1Ls = T2Ls(t1);
                            var edge = Segment2Edge(seg);

                            CalcNearest(edge.v0, edge.v1, tp0Ls, tp1Ls, out Vector3 contactLs_obb, out Vector3 cpLs, out bool parallel);

                            if (!parallel)
                            {
                                var p = contactLs_obb - cpLs;
                                if (!ClipPenetration(p.sqrMagnitude, out float pL)) break;

                                var pD = capsule_b.radius - pL;

                                Vector3 penetrationLs;
                                if (pD > 0 && pL > .00001f)
                                {
                                    penetrationLs = (p / pL) * pD;
                                }
                                else break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, p / pL);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < penetrationLs.sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, penetrationLs);
                                _cached_pLSqr = pL * pL;
                            }
                            else
                            {
                                cpLs = (tp0Ls + tp1Ls) / 2f;
                                contactLs_obb = ClipOBB(cpLs);

                                var p = contactLs_obb - cpLs;
                                if (!ClipPenetration(p.sqrMagnitude, out float pL)) break;

                                var pD = capsule_b.radius - pL;

                                Vector3 penetrationLs;

                                if (pD > 0 && pL > .00001f)
                                {
                                    penetrationLs = (p / pL) * pD;
                                }
                                else break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, p / pL);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < penetrationLs.sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, penetrationLs);
                                _cached_pLSqr = pL * pL;
                            }

                            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                            (Vector3 v0, Vector3 v1) Segment2Edge(LineSegmentType segment)
                            {
                                Vector3 v = Vector3.zero;
                                if (segment.x == LineSegmentType.SlabType.Negative) v.x = 0;
                                else if (segment.x == LineSegmentType.SlabType.Positive) v.x = obb_a.size.x;
                                if (segment.y == LineSegmentType.SlabType.Negative) v.y = 0;
                                else if (segment.y == LineSegmentType.SlabType.Positive) v.y = obb_a.size.y;
                                if (segment.z == LineSegmentType.SlabType.Negative) v.z = 0;
                                else if (segment.z == LineSegmentType.SlabType.Positive) v.z = obb_a.size.z;

                                Vector3 v0 = v;
                                Vector3 v1 = v;

                                if (segment.x == LineSegmentType.SlabType.Slab)
                                {
                                    v0.x = 0;
                                    v1.x = obb_a.size.x;
                                    return (v0, v1);
                                }

                                if (segment.y == LineSegmentType.SlabType.Slab)
                                {
                                    v0.y = 0;
                                    v1.y = obb_a.size.y;
                                    return (v0, v1);
                                }

                                if (segment.z == LineSegmentType.SlabType.Slab)
                                {
                                    v0.z = 0;
                                    v1.z = obb_a.size.z;
                                    return (v0, v1);
                                }

                                throw new Exception();
                            }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    bool ClipSegment(LineSegmentType segment)
                    {
                        int count = 0;
                        if (segment.x == LineSegmentType.SlabType.Slab) count++;
                        if (segment.y == LineSegmentType.SlabType.Slab) count++;
                        if (segment.z == LineSegmentType.SlabType.Slab) count++;

                        return count == 1;
                    }
                }

                // Nearest point exists at OBB-Face (6)
                {
                    foreach (var seg in segments.Values.Where(item => ClipSegment(item)))
                    {
                        if (IsTValid(seg.t0, seg.t1))
                        {
                            (var t0, var t1) = TClip(seg.t0, seg.t1);

                            var tp0Ls = T2Ls(t0);
                            var tp1Ls = T2Ls(t1);

                            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                            void CalcNearest(float d0, float d1, Vector3 p0, Vector3 p1, out float d, out Vector3 cp)
                            {
                                if (IsF32EpsilonEqual(d0, d1, .00001f)) { d = d0; cp = (p0 + p1) / 2f; }
                                else if (d0 < d1) { d = d0; cp = p0; }
                                else { d = d1; cp = p1; }
                            }

                            if (seg.x == LineSegmentType.SlabType.Negative)
                            {
                                CalcNearest(-tp0Ls.x, -tp1Ls.x, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.right);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                            else if (seg.x == LineSegmentType.SlabType.Positive)
                            {
                                CalcNearest(tp0Ls.x - obb_a.size.x, tp1Ls.x - obb_a.size.x, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.left);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                            else if (seg.y == LineSegmentType.SlabType.Negative)
                            {
                                CalcNearest(-tp0Ls.y, -tp1Ls.y, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.up);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                            else if (seg.y == LineSegmentType.SlabType.Positive)
                            {
                                CalcNearest(tp0Ls.y - obb_a.size.y, tp1Ls.y - obb_a.size.y, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.down);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                            else if (seg.z == LineSegmentType.SlabType.Negative)
                            {
                                CalcNearest(-tp0Ls.z, -tp1Ls.z, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.forward);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                            else if (seg.z == LineSegmentType.SlabType.Positive)
                            {
                                CalcNearest(tp0Ls.z - obb_a.size.z, tp1Ls.z - obb_a.size.z, tp0Ls, tp1Ls, out float d, out Vector3 cpLs);
                                if (!ClipDistance(d)) break;

                                Vector3 contactLs_capsule = ExtrudeLine(cpLs, Vector3.back);
                                Vector3 contactLs_obb = ClipOBB(cpLs);

                                Debug.Assert(!penetration.IsValid | penetration.PSqrMagnitude < CalcPenetration(contactLs_obb, cpLs).sqrMagnitude + FAST_CLIP_ASSERT_EPSILON);
                                penetration = CalcPenetrationWs(contactLs_obb, contactLs_capsule, CalcPenetration(contactLs_obb, cpLs));
                            }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    bool ClipSegment(LineSegmentType segment)
                    {
                        int count = 0;
                        if (segment.x == LineSegmentType.SlabType.Slab) count++;
                        if (segment.y == LineSegmentType.SlabType.Slab) count++;
                        if (segment.z == LineSegmentType.SlabType.Slab) count++;

                        return count == 2;
                    }

                    bool ClipDistance(float d)
                    {
                        if (!penetration.IsValid || penetration.PMagnitude < capsule_b.radius - d)
                        {
                            return true;
                        }

                        return false;
                    }
                }

                if (penetration.IsValid) return penetration;
                else return default;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Penetration CalcPenetrationWs(Vector3 contactLsObb, Vector3 contactLsCapsule, Vector3 penetrationLs)
                {
                    Vector3 contactWs_obb = Ls2Ws(contactLsObb);
                    Vector3 contactWs_capsule = Ls2Ws(contactLsCapsule);
                    Vector3 penetrationWs = LsDir2Ws(penetrationLs);

                    return new Penetration(penetrationWs, contactWs_obb, contactWs_capsule);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 ExtrudeLine(Vector3 p, Vector3 dirN) => p + dirN * capsule_b.radius;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                bool ClipPenetration(float pLSqr, out float pL)
                {
                    if (pLSqr < _cached_pLSqr || !penetration.IsValid)
                    {
                        pL = Mathf.Sqrt(pLSqr);
                        return true;
                    }
                    else
                    {
                        pL = 0;
                        return false;
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 CalcPenetration(Vector3 contactLs_obb, Vector3 capsule_cpLs)
                {
                    var p = contactLs_obb - capsule_cpLs;
                    var pL = p.magnitude;
                    var pD = capsule_b.radius - pL;

                    if (pD > 0 && pL > .00001f)
                    {
                        return (p / pL) * pD;
                    }
                    else return Vector3.zero;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 ClipOBB(Vector3 p)
                {
                    Vector3 ret;
                    ret.x = Mathf.Clamp(p.x, 0, obb_a.size.x);
                    ret.y = Mathf.Clamp(p.y, 0, obb_a.size.y);
                    ret.z = Mathf.Clamp(p.z, 0, obb_a.size.z);

                    return ret;
                }

                // ì¸èoóÕÇ∆Ç‡Ç… t0 < t1
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                (float t0, float t1) TClip(float t0Segment, float t1Segment)
                {
                    var t0 = Mathf.Max(t0Segment, 0);
                    var t1 = Mathf.Min(t1Segment, 1);

                    return (t0, t1);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                bool IsTValid(float t0Segment, float t1Segment)
                {
                    return !IsF32EpsilonEqual(t0Segment, t1Segment) && t0Segment < 1 && 0 < t1Segment;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                Vector3 T2Ls(float t) => p0Ls + dLs * t;
            }
        }
    }

    struct FixedAppendList7<T> where T : struct
    {
        T _v0;
        T _v1;
        T _v2;
        T _v3;
        T _v4;
        T _v5;
        T _v6;

        public int Count => _count;
        int _count;

        public T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                switch (i)
                {
                    case 0: return _v0;
                    case 1: return _v1;
                    case 2: return _v2;
                    case 3: return _v3;
                    case 4: return _v4;
                    case 5: return _v5;
                    case 6: return _v6;
                    default: throw new IndexOutOfRangeException();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (i < 0 || _count <= i) throw new IndexOutOfRangeException();

                switch (i)
                {
                    case 0: _v0 = value; return;
                    case 1: _v1 = value; return;
                    case 2: _v2 = value; return;
                    case 3: _v3 = value; return;
                    case 4: _v4 = value; return;
                    case 5: _v5 = value; return;
                    case 6: _v6 = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fill(int count)
        {
            _count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(T val)
        {
            if (_count >= 7) throw new FixedListFullException();
            this[_count++] = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any()
        {
            return _count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any(Predicate<T> predicate)
        {
            for (int i = 0; i < _count; i++)
            {
                if (predicate(this[i])) return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindIndex(Predicate<T> predicate)
        {
            for (int i = 0; i < _count; i++)
            {
                if (predicate(this[i])) return i;
            }

            return -1;
        }

        public IEnumerable<T> Values => GetValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return this[i];
            }

            yield break;
        }
    }

    struct FixedSortedAppendList7_Float
    {
        float _v0;
        float _v1;
        float _v2;
        float _v3;
        float _v4;
        float _v5;
        float _v6;

        public int Count => _count;
        int _count;

        public float this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                switch (i)
                {
                    case 0: return _v0;
                    case 1: return _v1;
                    case 2: return _v2;
                    case 3: return _v3;
                    case 4: return _v4;
                    case 5: return _v5;
                    case 6: return _v6;
                    default: throw new IndexOutOfRangeException();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set
            {
                switch (i)
                {
                    case 0: _v0 = value; return;
                    case 1: _v1 = value; return;
                    case 2: _v2 = value; return;
                    case 3: _v3 = value; return;
                    case 4: _v4 = value; return;
                    case 5: _v5 = value; return;
                    case 6: _v6 = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(float val)
        {
            if (_count >= 7) throw new FixedListFullException();

            for (int i = 0; i < _count; i++)
            {
                var v = this[i];

                if (val < v)
                {
                    Insert(i, val);
                    return;
                }
            }

            Insert(_count, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Insert(int index, float val)
        {
            if (index < 0 || 7 <= index) throw new IndexOutOfRangeException();

            for (int i = _count; index < i; i--)
            {
                var v0 = this[i - 1];
                this[i] = v0;
            }

            this[index] = val;

            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<float> GetValues()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return this[i];
            }

            yield break;
        }

        [Serializable]
        public class FixedListFullException : Exception { }
    }
}