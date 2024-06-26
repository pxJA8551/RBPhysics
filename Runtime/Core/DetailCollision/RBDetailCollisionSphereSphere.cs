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

            public static Penetration CalcDetailCollisionInfoCCD(RBColliderSphere sphere_a, RBColliderSphere sphere_b, Vector3 velocityA, Vector3 velocityB)
            {
                Vector3 velocity = velocityB - velocityA;

                float length = velocity.magnitude;
                Vector3 dirN = velocity / length;

                if(length == 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                var p = RBSphereCast.SphereCastSphere.CalcSphereCollision(sphere_a, sphere_b.pos, dirN, length, sphere_b.radius, true);
                Vector3 pA = p.position;
                Vector3 pB = (p.position + velocity * RBPhysCore.PhysTime.SolverSetDeltaTime) + p.normal * sphere_b.radius;

                if (!p.IsValidHit || length < Mathf.Abs(p.length))
                {
                    return CalcDetailCollisionInfo(sphere_a, sphere_b);
                }

                return new Penetration(pB - pA, pA, pB, default);
            }
        }
    }
}