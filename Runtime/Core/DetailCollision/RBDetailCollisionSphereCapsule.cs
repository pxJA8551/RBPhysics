using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
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

            public static Penetration CalcDetailCollisionInfoCCD(float delta, RBColliderSphere sphere_a, RBColliderCapsule capsule_b, Vector3 lastVelocity)
            {
                lastVelocity *= delta;

                float length = lastVelocity.magnitude;
                Vector3 dirN = lastVelocity / length;

                if (length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, capsule_b);
                }

                Vector3 org = sphere_a.pos - lastVelocity;
                var p = RBSphereCast.SphereCastCapsule.CalcSphereCollision(capsule_b, org, dirN, length, sphere_a.radius, false);

                if (!p.IsValidHit || length < p.length)
                {
                    return default;
                }

                Vector3 pA = p.position;
                Vector3 pB = p.position + dirN * (length - p.length);

                float t = Vector3.Dot(pB - pA, p.normal);
                return new Penetration(p.normal * Mathf.Min(t, 0), pA, pB, default);
            }
        }
    }
}