using UnityEngine;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionCapsuleCapsule
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                var r = CalcDetailCollision(capsule_a, capsule_b);
                return new Penetration(r.p, r.pA, r.pB);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                var edge_a = capsule_a.GetEdge();
                var edge_b = capsule_b.GetEdge();

                CalcNearest(edge_a.begin, edge_a.end, edge_b.begin, edge_b.end, out Vector3 peA, out Vector3 peB, out bool parallel);

                if (parallel)
                {
                    Vector3 ppA = (edge_a.begin + edge_a.end) / 2f;
                    Vector3 ppB = (edge_b.begin + edge_b.end) / 2f;
                    Vector3 pCenter = ppA + ppB;
                    Vector3 prA = ProjectPointToLine(pCenter, edge_a.begin, edge_a.end);
                    Vector3 prB = ProjectPointToLine(pCenter, edge_b.begin, edge_b.end);

                    Vector3 vd = (prB - prA);
                    float vdL = vd.magnitude;
                    vd /= vdL;

                    return (vd * (vdL - (capsule_a.radius + capsule_b.radius)), prA, prB);
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