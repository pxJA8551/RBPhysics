using RBPhys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBPhysCore;

public static partial class RBSphereCast
{
    public static class SphereCastCapsule
    {
        public static RBColliderCastHitInfo CalcSphereCollision(RBColliderCapsule capsule, Vector3 org, Vector3 dirN, float length, float radius, bool allowNegativeDist)
        {
            float pRadius = capsule.radius + radius;

            var edge = capsule.GetEdge();
            Vector3 d = edge.end - edge.begin;

            var s1Info = RBRaycast.RaycastSphere.CalcRayCollision(new RBColliderSphere(edge.begin, pRadius), org, dirN, length);
            if (s1Info.IsValidHit && Vector3.Dot(d, s1Info.position - edge.begin) < 0)
            {
                return s1Info;
            }

            var s2Info = RBRaycast.RaycastSphere.CalcRayCollision(new RBColliderSphere(edge.end, pRadius), org, dirN, length);
            if (s2Info.IsValidHit && Vector3.Dot(-d, s1Info.position - edge.end) < 0)
            {
                return s2Info;
            }

            if (Vector3.Dot(edge.begin - edge.end, s1Info.position - edge.begin) > 0 && Vector3.Dot(edge.end - edge.begin, s1Info.position - edge.end) > 0)
            {
                Vector3 ep1 = edge.begin - org;
                Vector3 ep2 = (edge.end - org) - ep1;

                float dsv = Vector3.Dot(ep2, dirN);
                float dpv = Vector3.Dot(ep1, dirN);
                float dss = Vector3.Dot(ep2, ep2);
                float dps = Vector3.Dot(ep1, ep2);
                float dpp = Vector3.Dot(ep1, ep1);

                if (dss == 0)
                {
                    return default;
                }

                float a = 1 - (dsv * dsv) / dss;
                float b = dpv - (dps * dsv) / dss;
                float c = dpp - (dps * dps) / dss - (pRadius * pRadius);

                if (a == 0)
                {
                    return default;
                }

                float s = b * b - a * c;
                if (s < 0)
                {
                    return default;
                }

                s = Mathf.Sqrt(s);

                float t1 = (b - s) / a;
                float t2 = (b + s) / a;

                float t = t1;

                if (!((t > 0 || allowNegativeDist) && t <= length) || ((t2 > 0 || allowNegativeDist) && t2 <= length && t2 < t))
                {
                    t = t2;
                }

                if ((t > 0 || allowNegativeDist) && t <= length)
                {
                    Vector3 pos = org + dirN * t;
                    Vector3 normal = Vector3.ProjectOnPlane(pos - capsule.pos, d).normalized;

                    RBColliderCastHitInfo info = new RBColliderCastHitInfo();
                    info.SetHit(pos - normal * radius, normal, t);

                    return info;
                }
            }

            return default;
        }
    }
}