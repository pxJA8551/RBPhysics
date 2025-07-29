using UnityEngine;
using UnityEngine.Profiling;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionCapsuleCapsule
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                Profiler.BeginSample("DetailTest/Capsule-Capsule");
                var r = CalcDetailCollision(capsule_a, capsule_b);

                Profiler.EndSample();
                return new Penetration(r.p, r.pA, r.pB);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                var edge_a = capsule_a.GetEdge();
                var edge_b = capsule_b.GetEdge();

                CalcNearest(edge_a.begin, edge_a.end, edge_b.begin, edge_b.end, out Vector3 peA, out Vector3 peB, out bool parallel);

                if (parallel)
                {
                    peA = edge_a.begin;
                    peB = ProjectPointToEdge(peA, edge_b.begin, edge_b.end);
                }

                Vector3 pDirN = peB - peA;
                float pDirL = pDirN.magnitude;
                if (pDirL == 0)
                {
                    return (Vector3.zero, Vector3.zero, Vector3.zero);
                }
                pDirN = pDirN / pDirL;

                Vector3 pA = peA + pDirN * capsule_a.radius;
                Vector3 pB = peB - pDirN * capsule_b.radius;

                float dp = pDirL - (capsule_a.radius + capsule_b.radius);
                if (dp > 0)
                {
                    return (Vector3.zero, Vector3.zero, Vector3.zero);
                }

                Vector3 penetration = pDirN * dp;
                return (penetration, pA, pB);
            }
        }
    }
}