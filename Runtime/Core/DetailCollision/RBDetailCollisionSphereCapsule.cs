using UnityEngine;
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

            public static Penetration CalcDetailCollisionInfoCCD(float delta, RBColliderSphere sphere_a, RBColliderCapsule capsule_b, Vector3 vel_a, Vector3 vel_b)
            {
                var vd_a = vel_a * delta;
                var vd_b = vel_b * delta;

                Vector3 relVel = vd_a - vd_b;

                float length = relVel.magnitude;
                Vector3 dirN = relVel / length;

                if (length == 0) return CalcDetailCollisionInfo(sphere_a, capsule_b);

                RBColliderCapsule vlCapsule = capsule_b;
                vlCapsule.pos -= vd_b;

                Vector3 org = sphere_a.pos - vd_a;
                var p = RBSphereCast.SphereCastCapsule.CalcSphereCollision(capsule_b, org, dirN, length, sphere_a.radius, false);

                if (!p.IsValidHit || length < p.length)
                {
                    return default;
                }

                float vr = (p.length / length);

                Vector3 vlCa = p.position - relVel * vr;
                Vector3 vlCb = p.position;

                Vector3 pA = vlCa + vd_a * vr;
                Vector3 pB = vlCb + vd_b * vr;

                float t = Vector3.Dot(pB - pA, p.normal);
                return new Penetration(p.normal * t, pA, pB, default);
            }
        }
    }
}