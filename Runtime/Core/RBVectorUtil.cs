using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public static class RBVectorUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToEdge(Vector3 p, Vector3 begin, Vector3 end)
        {
            float dn = (end - begin).magnitude;
            if (dn == 0) return begin;

            Vector3 dirN = (end - begin) / dn;
            return begin + dirN * Mathf.Clamp(Vector3.Dot(p - begin, dirN), 0, dn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToEdge(Vector3 p, Vector3 org, Vector3 dirN, float dL)
        {
            if (dL == 0) return org;
            return org + dirN * Mathf.Clamp(Vector3.Dot(p - org, dirN), 0, dL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectPointToPlane(Vector3 p, Vector3 planeNormal, Vector3 planeCenter)
        {
            return planeCenter + Vector3.ProjectOnPlane(p - planeCenter, planeNormal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CalcNearest(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB, out Vector3 nearestA, out Vector3 nearestB)
        {
            CalcNearest(beginA, endA, beginB, endB, out nearestA, out nearestB, out _);
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
                nearestA = Vector3.zero;
                nearestB = Vector3.zero;
                parallel = true;
                return;
            }

            Vector3 aToB = (beginB - beginA);

            float r1 = (Vector3.Dot(aToB, dirAN) - dotAB * Vector3.Dot(aToB, dirBN)) / div;
            nearestA = beginA + Mathf.Clamp(r1, 0, ebA) * dirAN;

            EdgeProjection(nearestA, beginA, dirAN, ebA, beginB, dirBN, ebB, out nearestA, out nearestB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EdgeProjection(Vector3 vA, Vector3 orgA, Vector3 dirAN, float dLA, Vector3 orgB, Vector3 dirBN, float dLB, out Vector3 vProjA, out Vector3 vProjB)
        {
            vProjB = ProjectPointToEdge(vA, orgB, dirBN, dLB);
            vProjA = ProjectPointToEdge(vProjB, orgA, dirAN, dLA);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalcNearestUnclamped(Vector3 beginA, Vector3 endA, Vector3 beginB, Vector3 endB)
        {
            Vector3 dirAN = (endA - beginA).normalized;
            Vector3 dirBN = (endB - beginB).normalized;

            float dotAB = Vector3.Dot(dirAN, dirBN);
            float div = 1 - dotAB * dotAB;

            Vector3 aToB = beginB - beginA;

            float r1 = (Vector3.Dot(aToB, dirAN) - dotAB * Vector3.Dot(aToB, dirBN)) / div;

            return beginA + r1 * dirAN;
        }
    }
}