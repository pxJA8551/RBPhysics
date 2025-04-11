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
                return new Penetration(r.p, r.pA, r.pB);
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

            public static Penetration CalcDetailCollisionInfoCCD(RBColliderSphere sphere_a, RBColliderCapsule capsule_b, Vector3 ccdOffset_a, Vector3 ccdOffset_b)
            {
                var vd_a = ccdOffset_a;
                var vd_b = ccdOffset_b;

                Vector3 relOffset = vd_a - vd_b;

                const float EPSILON = .00001f;

                float length = relOffset.magnitude;
                if (length < EPSILON) return CalcDetailCollisionInfo(sphere_a, capsule_b);

                Vector3 dirN = relOffset / length;

                RBColliderCapsule vlCapsule = capsule_b;
                vlCapsule.pos -= vd_b;

                Vector3 org = sphere_a.pos - vd_a;
                var p = RBSphereCast.SphereCastCapsule.CalcSphereCollision(capsule_b, org, dirN, length, sphere_a.radius, false);

                if (!p.IsValidHit || length < p.length || Vector3.Dot(p.normal, dirN) >= 0)
                {
                    return CalcDetailCollisionInfo(sphere_a, capsule_b);
                }

                float vr = (p.length / length);

                Vector3 vlCa = p.position - relOffset * vr;
                Vector3 vlCb = p.position;

                Vector3 pA = vlCa + vd_a;
                Vector3 pB = vlCb + vd_b;

                float t = Vector3.Dot(pB - pA, p.normal);
                return new Penetration(p.normal * t, pA, pB);
            }
        }
    }
}