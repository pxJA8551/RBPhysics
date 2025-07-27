using UnityEngine;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public static partial class RBRaycast
    {
        public static class RaycastSphere
        {
            public static RBColliderCastHitInfo CalcRayCollision(RBColliderSphere sphere, Vector3 org, Vector3 dirN, float length, bool ignoreBackFaceCollision = true)
            {
                Vector3 p = sphere.pos - org;
                float b = Vector3.Dot(dirN, p);
                float c = Vector3.Dot(p, p) - (sphere.radius * sphere.radius);

                float b2 = b * b;
                float s = b2 - c;
                if (s < 0)
                {
                    return default;
                }

                s = Mathf.Sqrt(s);

                float t1 = b - s;
                float t2 = b + s;

                if (s > 0)
                {
                    float t = t1;

                    if (ignoreBackFaceCollision && (t1 < 0))
                    {
                        return default;
                    }

                    if (!(t > 0 && t <= length) || (t2 > 0 && t2 <= length && t2 < t))
                    {
                        t = t2;
                    }

                    if (t > 0 && t <= length)
                    {
                        Vector3 pos = org + dirN * t;

                        RBColliderCastHitInfo info = new RBColliderCastHitInfo();
                        info.SetHit(pos, (pos - sphere.pos) / sphere.radius, t, t1 < 0);

                        return info;
                    }
                }

                return default;
            }
        }
    }
}