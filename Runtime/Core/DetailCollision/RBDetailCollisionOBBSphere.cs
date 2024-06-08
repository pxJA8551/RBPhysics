using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public static class DetailCollisionOBBSphere
        {
            public static Penetration CalcDetailCollisionInfo(RBColliderOBB obb_a, RBColliderSphere sphere_b)
            {
                var r = CalcDetailCollision(obb_a, sphere_b);
                return new Penetration(r.p, r.pA, r.pB, default);
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
                    return (Vector3.zero, pA, sphere_b.pos);
                }

                Vector3 pdN = pd / pdL;
                Vector3 pB = sphere_b.pos + pdN * sphere_b.radius;
                float sub = sphere_b.radius - pdL;

                return (sub > 0 ? pdN * sub : Vector3.zero, pA, pB);
            }
        }
    }
}