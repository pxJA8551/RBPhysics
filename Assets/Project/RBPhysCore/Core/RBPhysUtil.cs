using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace RBPhys
{
    public static class RBPhysUtil
    {
        public const float EPSILON_FLOAT32 = 0.000001f;

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
        public static Vector3 CalcContactPointOnSameNormal((Vector3 dir, Vector3 center) line_a, (Vector3 dir, Vector3 center) line_b, Vector3 planeNormal)
        {
            Vector3 rB = ProjectPointOnPlane(line_b.center, Vector3.Cross(planeNormal, line_a.dir), line_a.center) - line_b.center;
            float length = rB.magnitude / Mathf.Sqrt(1 - Mathf.Pow(Vector3.Dot(line_a.dir.normalized, line_b.dir.normalized), 2));
            Vector3 rbContact = length * -line_b.dir + line_b.center;

            return rbContact;
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

            (Vector3 dir, Vector3 center) edgeLined = (d, edge.begin);
            Vector3 contact = CalcContactPointOnSameNormal(edgeLined, linePrjOn, Vector3.Cross(edgeLined.dir, linePrjOn.dir));

            Vector3 r = edge.begin - contact;

            Vector3 revPrjBegin = contact + (revPrjEnd * (r.magnitude / d.magnitude * Mathf.Sign(Vector3.Dot(r, d))));
            revPrjEnd += revPrjBegin;

            return (revPrjBegin, revPrjEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReverseProject(Vector3 projected, Vector3 prjOnDir, Vector3 revPrjDir)
        {
            return revPrjDir.normalized * (projected.magnitude / Vector3.Dot(prjOnDir.normalized, revPrjDir.normalized));
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
            Vector3 dN = (edge.end - edge.begin).normalized;
            return edge.begin + (dN * Mathf.Max(0, Vector3.Dot(p - edge.begin, dN)));
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