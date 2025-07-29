using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionSphereSphere
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderSphere sphere_a, RBColliderSphere sphere_b)
            {
                Profiler.BeginSample("DetailTest/Sphere-Sphere");
                var r = CalcDetailCollision(sphere_a, sphere_b);

                Profiler.EndSample();

                return new Penetration(r.p, r.pA, r.pB);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderSphere sphere_a, RBColliderSphere sphere_b)
            {
                Vector3 d = (sphere_a.pos - sphere_b.pos);
                Vector3 dN = d.normalized;
                float dp = (sphere_a.radius + sphere_b.radius) - d.magnitude;
                Vector3 penetration = dp > 0 ? dN * dp : Vector3.zero;

                return (penetration, sphere_a.pos - dN * sphere_a.radius, sphere_b.pos + dN * sphere_b.radius);
            }
        }
    }
}