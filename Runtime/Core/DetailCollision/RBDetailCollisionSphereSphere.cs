using UnityEngine;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionSphereSphere
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderSphere sphere_a, RBColliderSphere sphere_b)
            {
                var r = CalcDetailCollision(sphere_a, sphere_b);
                return new Penetration(r.p, r.pA, r.pB, default);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderSphere sphere_a, RBColliderSphere sphere_b)
            {
                Vector3 d = (sphere_a.pos - sphere_b.pos);
                Vector3 dN = d.normalized;
                float dp = (sphere_a.radius + sphere_b.radius) - d.magnitude;
                Vector3 penetration = dp > 0 ? dN * dp : Vector3.zero;

                return (penetration, sphere_a.pos - dN * sphere_a.radius, sphere_b.pos + dN * sphere_b.radius);
            }

            public static Penetration CalcDetailCollisionInfoCCD(float delta, RBColliderSphere sphere_a, RBColliderSphere sphere_b, Vector3 vel_a, Vector3 vel_b)
            {
                var vd_a = vel_a * delta;
                var vd_b = vel_b * delta;

                Vector3 relVel = vd_b - vd_a;

                float length = relVel.magnitude;
                Vector3 dirN = relVel / length;

                if (length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                var sphere_a_org = sphere_a;
                sphere_a_org.pos -= vd_a;

                Vector3 org = sphere_b.pos - vd_b;
                var p = RBSphereCast.SphereCastSphere.CalcSphereCollision(sphere_a_org, org, dirN, length, sphere_b.radius, false);

                if (!p.IsValidHit || length < p.length)
                {
                    return default;
                }

                float vr = (p.length / length);

                Vector3 lvCa = p.position;
                Vector3 lvCb = p.position - relVel * vr;

                Vector3 pA = lvCa + vd_a * vr;
                Vector3 pB = lvCa + vd_b * vr;

                float t = (pB - pA).magnitude;
                return new Penetration(p.normal * t, pA, pB, default);
            }
        }
    }
}