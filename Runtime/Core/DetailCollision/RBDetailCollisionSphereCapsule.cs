using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysUtil;
using static RBPhys.RBVectorUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionSphereCapsule
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderSphere sphere_a, RBColliderCapsule capsule_b)
            {
                var r = CalcDetailCollision(sphere_a, capsule_b);
                return new Penetration(r.p, r.pA, r.pB, default);
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

            public static Penetration CalcDetailCollisionInfoCCD(RBColliderSphere sphere_a, RBColliderCapsule capsule_b, Vector3 velocity)
            {
                float length = velocity.magnitude;
                Vector3 dirN = velocity / length;

                if (length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, capsule_b);
                }

                var p = RBSphereCast.SphereCastCapsule.CalcSphereCollision(capsule_b, sphere_a.pos, dirN, length, sphere_a.radius, true);
                Vector3 pA = (p.position + velocity * RBPhysCore.PhysTime.SolverSetDeltaTime) + p.normal * capsule_b.radius;
                Vector3 pB = p.position;

                if (!p.IsValidHit || length < Mathf.Abs(p.length))
                {
                    return CalcDetailCollisionInfo(sphere_a, capsule_b);
                }

                return new Penetration(Vector3.Project(pB - pA, p.normal), pA, pB, default);
            }
        }
    }
}