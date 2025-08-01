using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBSphere
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderSphere sphere_b)
            {
                Profiler.BeginSample("DetailTest/OBB-Sphere");
                var p = CalcDetailCollision(obb_a, sphere_b);

                Profiler.EndSample();

                return p;
            }

            public static Penetration CalcDetailCollision(RBColliderOBB obb_a, RBColliderSphere sphere_b)
            {
                Vector3 d = (sphere_b.pos - obb_a.Center);

                Vector3 aFwdN = obb_a.GetAxisForwardN();
                Vector3 aRightN = obb_a.GetAxisRightN();
                Vector3 aUpN = obb_a.GetAxisUpN();

                float dpx = Mathf.Clamp(Vector3.Dot(aRightN, d), -obb_a.size.x / 2, obb_a.size.x / 2);
                float dpy = Mathf.Clamp(Vector3.Dot(aUpN, d), -obb_a.size.y / 2, obb_a.size.y / 2);
                float dpz = Mathf.Clamp(Vector3.Dot(aFwdN, d), -obb_a.size.z / 2, obb_a.size.z / 2);

                Vector3 pA = obb_a.Center + obb_a.rot * new Vector3(dpx, dpy, dpz);
                Vector3 pd = pA - sphere_b.pos;
                float pdL = pd.magnitude;

                if (pdL == 0)
                {
                    var vd = (sphere_b.pos - obb_a.Center);

                    var vdMin = Mathf.Min(Mathf.Abs(vd.x), Mathf.Abs(vd.y), Mathf.Abs(vd.z));

                    if (Mathf.Abs(vd.x) == vdMin)
                    {
                        var vp = new Vector3(-vd.x, 0, 0);
                        return new Penetration(vp, pA, sphere_b.pos);
                    }

                    if (Mathf.Abs(vd.y) == vdMin)
                    {
                        var vp = new Vector3(0, -vd.y, 0);
                        return new Penetration(vp, pA, sphere_b.pos);
                    }

                    if (Mathf.Abs(vd.z) == vdMin)
                    {
                        var vp = new Vector3(0, 0, -vd.z);
                        return new Penetration(vp, pA, sphere_b.pos);
                    }

                    return new Penetration(Vector3.zero, pA, sphere_b.pos);
                }

                Vector3 pdN = pd / pdL;
                Vector3 pB = sphere_b.pos + pdN * sphere_b.radius;
                float sub = sphere_b.radius - pdL;

                return new Penetration(sub > 0 ? pdN * sub : Vector3.zero, pA, pB);
            }
        }
    }
}