using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionCapsuleCapsule
        {
            public static (Vector3 p, Vector3 pA, Vector3 pB, DetailCollisionInfo info) CalcDetailCollisionInfo(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                var r = CalcDetailCollision(capsule_a, capsule_b);
                return (r.p, r.pA, r.pB, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderCapsule capsule_a, RBColliderCapsule capsule_b)
            {
                var edge_a = capsule_a.GetEdge();
                var edge_b = capsule_b.GetEdge();

                CalcNearest(edge_a.begin, edge_a.end, edge_b.begin, edge_b.end, out Vector3 peA, out Vector3 peB);

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