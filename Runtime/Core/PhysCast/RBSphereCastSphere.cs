using RBPhys;
using UnityEngine;
using static RBPhys.RBPhysComputer;

public static partial class RBSphereCast
{
    public static class SphereCastSphere
    {
        public static RBColliderCastHitInfo CalcSphereCollision(RBColliderSphere sphere, Vector3 org, Vector3 dirN, float length, float radius, bool allowNegativeDist)
        {
            float pRadius = sphere.radius + radius;

            Vector3 p = sphere.pos - org;
            float b = Vector3.Dot(dirN, p);
            float c = Vector3.Dot(p, p) - (pRadius * pRadius);

            float b2 = b * b;
            float s = b2 - c;

            if (s < 0)
            {
                return default;
            }

            s = Mathf.Sqrt(s);

            float t1 = b - s;
            float t2 = b + s;

            bool isBackface = (t1 > 0 && t2 > 0) || allowNegativeDist;

            float t = t1;

            if (!((t > 0 || allowNegativeDist) && t <= length) || ((t2 > 0 || allowNegativeDist) && t2 <= length && t2 < t))
            {
                t = t2;
            }

            if ((t > 0 || allowNegativeDist) && -length <= t && t <= length)
            {
                Vector3 pos = org + dirN * t;
                Vector3 n = ((pos - sphere.pos) / pRadius).normalized;

                RBColliderCastHitInfo info = new RBColliderCastHitInfo();
                info.SetHit(pos - n * radius, n, t);
                info.backFaceCollision = isBackface;

                return info;
            }

            return default;
        }
    }
}