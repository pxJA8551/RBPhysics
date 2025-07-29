using UnityEngine;
using UnityEngine.Profiling;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionSphereCapsule
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderSphere sphere_a, RBColliderCapsule capsule_b)
            {
                Profiler.BeginSample("DetailTest/Sphere-Capsule");
                var r = CalcDetailCollision(sphere_a, capsule_b);

                Profiler.EndSample();
                return new Penetration(r.p, r.pA, r.pB);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderSphere sphere_a, RBColliderCapsule capsule_b)
            {
                var edge = capsule_b.GetEdge();
                Vector3 peB = ProjectPointToEdge(sphere_a.pos, edge.begin, edge.end);
                Vector3 pDirN = peB - sphere_a.pos;
                float pDirL = pDirN.magnitude;
                if (pDirL == 0)
                {
                    return (Vector3.zero, sphere_a.pos, peB);
                }
                pDirN = pDirN / pDirL;

                Vector3 pA = sphere_a.pos + pDirN * sphere_a.radius;
                Vector3 pB = peB - pDirN * capsule_b.radius;

                float dp = pDirL - (sphere_a.radius + capsule_b.radius);
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