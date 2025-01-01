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
                Vector3 lastVelocity = lastVelocityB - lastVelocityA;
                lastVelocity *= delta;

                float length = lastVelocity.magnitude;
                Vector3 dirN = lastVelocity / length;

                if(length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                Vector3 org = sphere_b.pos - lastVelocity;
                var p = RBSphereCast.SphereCastSphere.CalcSphereCollision(sphere_a, org, dirN, length, sphere_b.radius, false);

                Vector3 pA = p.position;
                Vector3 pB = (p.position + lastVelocityB * delta);

                if (!p.IsValidHit || length < Mathf.Abs(p.length))
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                return new Penetration(Vector3.Project(pA - pB, p.normal), pA, pB, default);
            }
        }
    }
}