using UnityEngine;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBSphere
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderSphere sphere_b)
            {
                var r = CalcDetailCollision(obb_a, sphere_b);
                return new Penetration(r.p, r.pA, r.pB);
            }

            public static (Vector3 p, Vector3 pA, Vector3 pB) CalcDetailCollision(RBColliderOBB obb_a, RBColliderSphere sphere_b)
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
                        return (vp, pA, sphere_b.pos);
                    }

                    if (Mathf.Abs(vd.y) == vdMin)
                    {
                        var vp = new Vector3(0, -vd.y, 0);
                        return (vp, pA, sphere_b.pos);
                    }

                    if (Mathf.Abs(vd.z) == vdMin)
                    {
                        var vp = new Vector3(0, 0, -vd.z);
                        return (vp, pA, sphere_b.pos);
                    }

                    return (Vector3.zero, pA, sphere_b.pos);
                }

                Vector3 pdN = pd / pdL;
                Vector3 pB = sphere_b.pos + pdN * sphere_b.radius;
                float sub = sphere_b.radius - pdL;

                return (sub > 0 ? pdN * sub : Vector3.zero, pA, pB);
            }

            public static Penetration CalcDetailCollisionInfoCCD(RBColliderOBB obb_a, RBColliderSphere sphere_b, Vector3 ccdOffset_a, Vector3 ccdOffset_b)
            {
                var vd_a = ccdOffset_a;
                var vd_b = ccdOffset_b;

                var relOffset = vd_b - vd_a;

                const float EPSILON = .00001f;

                float length = relOffset.magnitude;
                if (length < EPSILON) return CalcDetailCollisionInfo(obb_a, sphere_b);

                Vector3 dirN = relOffset / length;

                RBColliderOBB vlObb = obb_a;
                vlObb.pos -= vd_a;

                Vector3 org = sphere_b.pos - vd_b;
                var p = RBSphereCast.SphereCastOBB.CalcSphereCollision(vlObb, org, dirN, length, sphere_b.radius, false);

                if (!p.IsValidHit || length < p.length || Vector3.Dot(p.normal, dirN) >= 0)
                {
                    return CalcDetailCollisionInfo(obb_a, sphere_b);
                }

                float vr = (p.length / length);

                Vector3 vlCa = p.position;
                Vector3 vlCb = p.position - relOffset * vr;

                Vector3 pA = vlCa + vd_a;
                Vector3 pB = vlCb + vd_b;

                float t = Vector3.Dot(pB - pA, p.normal);

                return new Penetration(p.normal * t, pA, pB);
            }
        }
    }
}