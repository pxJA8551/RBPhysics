using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

namespace RBPhys
{
    public static class RBPhysUtil
    {
        public const float EPSILON_FLOAT32 = 0.000001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float V3Volume(Vector3 v)
        {
            return Mathf.Abs(v.x * v.y * v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3Rcp(Vector3 v)
        {
            return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
        }

        static float sqrt3Inv = 1 / Mathf.Sqrt(3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void V3FromOrthogonalBasis(Vector3 v, out Vector3 a, out Vector3 b)
        {
            if (Mathf.Abs(v.x) >= sqrt3Inv)
            {
                a = new Vector3(v.y, -v.x, 0).normalized;
            }
            else
            {
                a = new Vector3(0, v.z, -v.y).normalized;
            }

            b = Vector3.Cross(a, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Less(Vector3 v, Vector3 lessThan)
        {
            return v.x < lessThan.x && v.y < lessThan.y && v.z < lessThan.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Greater(Vector3 v, Vector3 greaterThan)
        {
            return v.x > greaterThan.x && v.y > greaterThan.y && v.z > greaterThan.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32EpsilonEqual(float a, float b, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(a - b) <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3EpsilonEqual(Vector3 a, Vector3 b, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32EpsilonEqual(a.x, b.x, epsilon) && IsF32EpsilonEqual(a.y, b.y, epsilon) && IsF32EpsilonEqual(a.z, b.z, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PredictPosRot(RBRigidbody rb, out Vector3 position, out Quaternion rotation, float dt)
        {
            position = rb.Position + rb.ExpVelocity * dt;
            rotation = rb.Rotation * Quaternion.AngleAxis(rb.ExpAngularVelocity.magnitude * Mathf.Rad2Deg * dt, rb.ExpAngularVelocity.normalized);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2)
        {
            float a_min = Mathf.Min(a_x1, a_x2);
            float a_max = Mathf.Max(a_x1, a_x2);
            float b_min = Mathf.Min(b_x1, b_x2);
            float b_max = Mathf.Max(b_x1, b_x2);
            
            return !(a_max < b_min || b_max < a_min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetOBBAxisSize(Vector3 size, Quaternion rot, Vector3 axis)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, size.z), axis));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(size.x, 0, 0), axis));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, size.y, 0), axis));
            return fwd + right + up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointOnPlane(Vector3 p, Vector3 planeNormal, Vector3 planeCenter)
        {
            return planeCenter + Vector3.ProjectOnPlane(p - planeCenter, planeNormal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 begin, Vector3 end) ProjectEdgeOnPlane((Vector3 begin, Vector3 end) edge, Vector3 planeNormal, Vector3 planeCenter)
        {
            return (ProjectPointOnPlane(edge.begin, planeNormal, planeCenter), ProjectPointOnPlane(edge.end, planeNormal, planeCenter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearest((Vector3 dir, Vector3 center) line_a, (Vector3 dir, Vector3 center) line_b, out Vector3 aNearest, out Vector3 bNearest)
        {
            Vector3 aDirN = line_a.dir.normalized;
            Vector3 bDirN = line_b.dir.normalized;

            float dotAb = Vector3.Dot(aDirN, bDirN);
            float div = 1 - dotAb * dotAb;

            Vector3 aToB = line_b.center - line_a.center;

            float r1 = (Vector3.Dot(aToB, aDirN) - dotAb * Vector3.Dot(aToB, bDirN)) / div;
            float r2 = (dotAb * Vector3.Dot(aToB, aDirN) - Vector3.Dot(aToB, bDirN)) / div;

            Vector3 aBegin = line_a.center;
            Vector3 bBegin = line_b.center;

            aNearest = aBegin + r1 * aDirN;
            bNearest = bBegin + r2 * bDirN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearest((Vector3 dir, Vector3 center) line_a, (Vector3 dir, Vector3 center) line_b)
        {
            CalcNearest(line_a, line_b, out Vector3 aNearest, out Vector3 bNearest);
            return aNearest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 begin, Vector3 end) ProjectEdgeOnLine((Vector3 begin, Vector3 end) edge, (Vector3 dir, Vector3 center) linePrjOn)
        {
            Vector3 prjBegin = ProjectPointOnLine(edge.begin, linePrjOn);
            Vector3 prjEnd = ProjectPointOnLine(edge.end, linePrjOn);

            return (prjBegin, prjEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 begin, Vector3 end) ReverseProjectEdgeOnLine((Vector3 begin, Vector3 end) edge, (Vector3 dir, Vector3 center) linePrjOn)
        {
            Vector3 d = edge.end - edge.begin;
            Vector3 revPrjEnd = ReverseProject(d, d, linePrjOn.dir);

            if (Vector3.Cross(d, linePrjOn.dir) == Vector3.zero)
            {
                return ProjectEdgeOnLine(edge, linePrjOn);
            }

            (Vector3 dir, Vector3 center) edgeLined = (d, edge.begin);

            Vector3 contact = CalcNearest(edgeLined, linePrjOn);
            Vector3 r = edge.begin - contact;

            float div = d.magnitude * Mathf.Sign(Vector3.Dot(d, r));

            if (div == 0)
            {
                return (contact, contact);
            }

            Vector3 revPrjBegin = contact + (revPrjEnd * (r.magnitude / div));
            revPrjEnd += revPrjBegin;

            return (revPrjBegin, revPrjEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReverseProject(Vector3 projected, Vector3 prjOnDir, Vector3 revPrjDir)
        {
            float div = Vector3.Dot(prjOnDir.normalized, revPrjDir.normalized);

            if (div == 0)
            {
                return Vector3.zero;
            }

            return revPrjDir.normalized * (projected.magnitude / div);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 begin, Vector3 end) ProjectEdgeOnEdge((Vector3 begin, Vector3 end) edge, (Vector3 begin, Vector3 end) edgePrjOn)
        {
            Vector3 prjBegin = ProjectPointOnEdge(edge.begin, edgePrjOn);
            Vector3 prjEnd = ProjectPointOnEdge(edge.end, edgePrjOn);
            (Vector3 begin, Vector3 end) prjEdge = (prjBegin, prjEnd);

            prjBegin = ProjectPointOnEdge(edgePrjOn.begin, prjEdge);
            prjEnd = ProjectPointOnEdge(edgePrjOn.end, prjEdge);

            return (prjBegin, prjEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointOnLine(Vector3 p, (Vector3 dir, Vector3 center) edge)
        {
            return edge.center + Vector3.Project(p - edge.center, edge.dir);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointOnEdge(Vector3 p, (Vector3 begin, Vector3 end) edge)
        {
            Vector3 dN = (edge.end - edge.begin);
            return edge.begin + (dN.normalized * Mathf.Clamp(Vector3.Dot(dN, p - edge.begin), 0, dN.magnitude));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 begin, Vector3 end) GetDuplicatedEdge((Vector3 begin, Vector3 end) edge1, (Vector3 begin, Vector3 end) edge2)
        {
            return ProjectEdgeOnEdge(edge1, edge2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointOnRect(Vector3 point, Vector3[] rectPointsClockwise, Vector3 rectPlaneNormal)
        {
            rectPlaneNormal = Vector3.Normalize(rectPlaneNormal);

            Vector3 prjP = ProjectPointOnPlane(point, rectPlaneNormal, rectPointsClockwise[0]);

            bool isInside = RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[0] - prjP, prjP - rectPointsClockwise[1]).normalized, rectPlaneNormal), -1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[1] - prjP, prjP - rectPointsClockwise[2]).normalized, rectPlaneNormal), -1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[2] - prjP, prjP - rectPointsClockwise[3]).normalized, rectPlaneNormal), -1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[3] - prjP, prjP - rectPointsClockwise[0]).normalized, rectPlaneNormal), -1, 0.01f);
            isInside |= RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[0] - prjP, prjP - rectPointsClockwise[1]).normalized, rectPlaneNormal), 1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[1] - prjP, prjP - rectPointsClockwise[2]).normalized, rectPlaneNormal), 1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[2] - prjP, prjP - rectPointsClockwise[3]).normalized, rectPlaneNormal), 1, 0.01f) && RBPhysUtil.IsF32EpsilonEqual(Vector3.Dot(Vector3.Cross(rectPointsClockwise[3] - prjP, prjP - rectPointsClockwise[0]).normalized, rectPlaneNormal), 1, 0.01f);

            if (isInside)
            {
                return prjP;
            }
            else
            {
                Vector3 prjA = ProjectPointOnEdge(prjP, (rectPointsClockwise[0], rectPointsClockwise[1]));
                Vector3 prjB = ProjectPointOnEdge(prjP, (rectPointsClockwise[1], rectPointsClockwise[2]));
                Vector3 prjC = ProjectPointOnEdge(prjP, (rectPointsClockwise[2], rectPointsClockwise[3]));
                Vector3 prjD = ProjectPointOnEdge(prjP, (rectPointsClockwise[3], rectPointsClockwise[0]));

                Vector3[] prjs = new Vector3[4] { prjA, prjB, prjC, prjD };
                float dMin = -1;
                Vector3 prjR = Vector3.zero;
                foreach (Vector3 prj in prjs)
                {
                    float d = Vector3.Distance(prj, prjP);
                    if (dMin == -1 || d < dMin)
                    {
                        dMin = d;
                        prjR = prj;
                    }
                }

                return prjR;
            }
        }
    }
}