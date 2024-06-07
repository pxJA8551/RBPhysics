using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.VFX;

namespace RBPhys
{
    public static class RBVectorUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToLine(Vector3 p, Vector3 begin, Vector3 dirN)
        {
            return begin + dirN * Vector3.Dot(p - begin, dirN);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToEdge(Vector3 p, Vector3 begin, Vector3 end)
        {
            float dn = (end - begin).magnitude;
            if (dn == 0)
            {
                return begin;
            }

            Vector3 dirN = (end - begin) / dn;
            return begin + dirN * Mathf.Clamp(Vector3.Dot(p - begin, dirN), 0, dn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToEdge(Vector3 p, Vector3 begin, Vector3 end, out bool outsideEdge)
        {
            float dn = (end - begin).magnitude;
            if (dn == 0)
            {
                outsideEdge = true;
                return begin;
            }

            Vector3 dirN = (end - begin) / dn;
            float dd = Vector3.Dot(p - begin, dirN);
            outsideEdge = dd < 0 || dn < dd;

            return begin + dirN * Mathf.Clamp(dd, 0, dn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToPlane(Vector3 p, Vector3 planeNormal, Vector3 planeCenter)
        {
            return planeCenter + Vector3.ProjectOnPlane(p - planeCenter, planeNormal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRect(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 n)
        {
            if (Vector3.Dot(Vector3.Cross(p - a, b - p), n) >= 0 && Vector3.Dot(Vector3.Cross(p - b, c - p), n) >= 0 && Vector3.Dot(Vector3.Cross(p - c, d - p), n) >= 0 && Vector3.Dot(Vector3.Cross(p - d, a - p), n) >= 0)
            {
                return true;
            }
            else if (Vector3.Dot(Vector3.Cross(p - a, b - p), n) <= 0 && Vector3.Dot(Vector3.Cross(p - b, c - p), n) <= 0 && Vector3.Dot(Vector3.Cross(p - c, d - p), n) <= 0 && Vector3.Dot(Vector3.Cross(p - d, a - p), n) <= 0)
            {
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToRect(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 cx, Vector3 n)
        {
            Vector3 prjP = ProjectPointToPlane(p, n, cx);

            if (IsInRect(p, a, b, c, d, n))
            {
                return prjP;
            }
            else
            {
                Vector3 prjA = ProjectPointToEdge(prjP, a, b);
                Vector3 prjB = ProjectPointToEdge(prjP, b, c);
                Vector3 prjC = ProjectPointToEdge(prjP, c, d);
                Vector3 prjD = ProjectPointToEdge(prjP, d, a);

                float dt = (prjA - prjP).sqrMagnitude;

                float dMin = dt;
                Vector3 prjR = prjA;

                dt = (prjB - prjP).sqrMagnitude;
                if (dt < dMin)
                {
                    dMin = dt;
                    prjR = prjB;
                }

                dt = (prjC - prjP).sqrMagnitude;
                if (dt < dMin)
                {
                    dMin = dt;
                    prjR = prjC;
                }

                dt = (prjD - prjP).sqrMagnitude;
                if (dt < dMin)
                {
                    dMin = dt;
                    prjR = prjD;
                }

                return prjR;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReverseProject(float d, Vector3 prjDirN, Vector3 revPrjDirN)
        {
            float div = Vector3.Dot(prjDirN, revPrjDirN);
            return div != 0 ? revPrjDirN * (d / div) : Vector3.zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReverseProjectLineToLine(ref Vector3 beginA, ref Vector3 endA, Vector3 beginB, Vector3 endB)
        {
            float ebA = (endA - beginA).magnitude;
            Vector3 prjDirN = ebA > 0 ? (endA - beginA) / ebA : Vector3.zero;

            Vector3 bDirN = (endB - beginB).normalized;
            Vector3 avg = (beginA + endA) / 2;

            Vector3 prjAvg = ProjectPointToLine(avg, beginB, bDirN);

            Vector3 pbB = beginB - prjAvg;
            Vector3 peB = endB - prjAvg;

            float db = Mathf.Clamp(Vector3.Dot(prjDirN, ReverseProject(pbB.magnitude + RBPhys.RBPhysUtil.F32Sign101(Vector3.Dot(pbB, bDirN)), bDirN, prjDirN)), -ebA / 2, ebA / 2);
            float de = Mathf.Clamp(Vector3.Dot(prjDirN, ReverseProject(peB.magnitude + RBPhys.RBPhysUtil.F32Sign101(Vector3.Dot(peB, bDirN)), bDirN, prjDirN)), -ebA / 2, ebA / 2);

            beginA = avg + prjDirN * db;
            endA = avg + prjDirN * de;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearestLine(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB)
        {
            Vector3 dirAN = (endA - beginA).normalized;
            Vector3 dirBN = (endB - beginB).normalized;

            float dotAB = Vector3.Dot(dirAN, dirBN);
            float div = 1 - dotAB * dotAB;

            Vector3 aToB = beginB - beginA;

            float r1 = (Vector3.Dot(aToB, dirAN) - dotAB * Vector3.Dot(aToB, dirBN)) / div;

            return beginA + r1 * dirAN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearest(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB, out Vector3 nearestA, out Vector3 nearestB, out bool parallel)
        {
            parallel = false;

            float ebA = (endA - beginA).magnitude;
            float ebB = (endB - beginB).magnitude;
            Vector3 dirAN = ebA > 0 ? (endA - beginA) / ebA : Vector3.zero;
            Vector3 dirBN = ebB > 0 ? (endB - beginB) / ebB : Vector3.zero;

            float dotAB = Vector3.Dot(dirAN, dirBN);
            float div = 1 - dotAB * dotAB;

            if (div == 0)
            {
                nearestA = RBPhysUtil.V3NaN;
                nearestB = RBPhysUtil.V3NaN;
                parallel = true;
                return;
            }

            Vector3 aToB = (beginB - beginA);

            float r1 = (Vector3.Dot(aToB, dirAN) - dotAB * Vector3.Dot(aToB, dirBN)) / div;
            float r2 = (dotAB * Vector3.Dot(aToB, dirAN) - Vector3.Dot(aToB, dirBN)) / div;

            nearestA = beginA + Mathf.Clamp(r1, 0, ebA) * dirAN;
            nearestB = beginB + Mathf.Clamp(r2, 0, ebB) * dirBN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearestP(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB, out Vector3 nearestA, out Vector3 nearestB, out bool parallel, int solverIter = 6)
        {
            CalcNearest(beginA, endA, beginB, endB, out nearestA, out nearestB, out parallel);

            for (int iter = 0; iter < solverIter; iter++)
            {
                nearestA = ProjectPointToEdge(nearestB, beginA, endA);
                nearestB = ProjectPointToEdge(nearestA, beginB, endB);

                if (parallel) return;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearestOnPlaneP(Vector3 beginA, Vector3 endA, Vector3 planeDirN, Vector3 planeCenter, out Vector3 nearestA, out Vector3 nearestB, out bool parallel, int solverIter = 6)
        {
            nearestA = beginA;
            nearestB = CalcNearestOnPlane(beginA, endA, planeDirN, planeCenter, out parallel);

            for (int iter = 0; iter < solverIter; iter++)
            {
                nearestA = ProjectPointToEdge(nearestB, beginA, endA);
                nearestB = ProjectPointToPlane(nearestA, planeDirN, planeCenter);

                if (parallel) return;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearestOnRectP(Vector3 beginA, Vector3 endA, Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 planeDirN, Vector3 planeCenter, out Vector3 nearestA, out Vector3 nearestB, out bool parallel, int solverIter = 6)
        {
            nearestA = beginA;
            nearestB = CalcNearestOnPlane(beginA, endA, planeDirN, planeCenter, out parallel);

            for (int iter = 0; iter < solverIter; iter++)
            {
                nearestA = ProjectPointToEdge(nearestB, beginA, endA);
                nearestB = ProjectPointToRect(nearestA, a, b, c, d, planeCenter, planeDirN);

                if (parallel) return;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearest(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB, out bool invalid)
        {
            float ebA = (endA - beginA).magnitude;
            float ebB = (endB - beginB).magnitude;
            Vector3 dirAN = ebA > 0 ? (endA - beginA) / ebA : Vector3.zero;
            Vector3 dirBN = ebB > 0 ? (endB - beginB) / ebB : Vector3.zero;

            float dotAB = Vector3.Dot(dirAN, dirBN);
            float div = 1 - dotAB * dotAB;

            Vector3 aToB = beginB - beginA;

            float r1 = (Vector3.Dot(aToB, dirAN) - dotAB * Vector3.Dot(aToB, dirBN)) / div;
            float r2 = (dotAB * Vector3.Dot(aToB, dirAN) - Vector3.Dot(aToB, dirBN)) / div;

            invalid = !(0 <= r1 && r1 <= ebA && 0 <= r2 && r2 <= ebB);
            return beginA + r1 * dirAN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearest(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB)
        {
            return CalcNearest(beginA, endA, beginB, endB, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearestOnPlane(Vector3 edgeBegin, Vector3 edgeEnd, Vector3 planeNormal, Vector3 planeCenter, out bool parallel)
        {
            parallel = false;

            Vector3 dirN = edgeEnd - edgeBegin;
            float dirL = dirN.magnitude;

            if (dirL == 0)
            {
                return edgeBegin;
            }

            dirN = dirN / dirL;

            Vector3 prjBegin = planeCenter + Vector3.ProjectOnPlane(edgeBegin - planeCenter, planeNormal);
            Vector3 pd = prjBegin - edgeBegin;
            float dc = Vector3.Dot(pd, dirN);

            if (dc == 0)
            {
                parallel = true;
                return edgeBegin;
            }

            float ff = 1 / dc;

            if (0 <= ff && ff <= dirL)
            {
                return edgeBegin + (dirN * Mathf.Clamp(ff, 0, dirL));
            }
            else
            {
                return ProjectPointToPlane(edgeBegin + (dirN * Mathf.Clamp(ff, 0, dirL)), planeNormal, planeCenter);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3XZero(Vector3 v)
        {
            v.x = 0;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3YZero(Vector3 v)
        {
            v.y = 0;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3ZZero(Vector3 v)
        {
            v.z = 0;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3Multiply(Vector3 v, float a, float b, float c)
        {
            v.x *= a;
            v.y *= b;
            v.z *= c;
            return v;
        }
    }
}