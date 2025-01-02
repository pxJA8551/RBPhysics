using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static RBPhys.RBPhysUtil;

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

            public static Penetration CalcDetailCollisionInfoCCD(float delta, RBColliderSphere sphere_a, RBColliderSphere sphere_b, Vector3 lastVelocityA, Vector3 lastVelocityB)
            {
                Vector3 relLastVelocity = lastVelocityB - lastVelocityA;

                float length = relLastVelocity.magnitude;
                Vector3 dirN = relLastVelocity / length;

                if (length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                var sphere_a_org = sphere_a;
                sphere_a_org.pos -= lastVelocityA;

                Vector3 org = sphere_b.pos - lastVelocityB;
                var p = RBSphereCast.SphereCastSphere.CalcSphereCollision(sphere_a_org, org, dirN, length, sphere_b.radius, false);

                if (!p.IsValidHit || length < p.length) 
                {
                    return default;
                }

                float tr = 1 - (p.length / length);

                Vector3 pA = (p.position - lastVelocityA) + lastVelocityA * tr;
                Vector3 pB = (p.position - lastVelocityA) + lastVelocityB * tr;

                float t = Vector3.Dot(pB - pA, p.normal);
                return new Penetration(p.normal * t, pA, pB, default);
            }
        }
    }
}