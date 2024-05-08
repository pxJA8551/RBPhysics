using RBPhys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBPhysCore;

public static partial class RBRaycast
{
    public static class RaycastCaspule
    {
        //http://marupeke296.com/COL_3D_No26_RayToCapsule.html

        public static RBColliderCastHitInfo CalcRayCollision(RBColliderCapsule capsule, Vector3 org, Vector3 dirN, float length, bool allowBackFaceCollision = false)
        {
            var edge = capsule.GetEdge();
            Vector3 d = edge.end - edge.begin;

            var s1Info = RaycastSphere.CalcRayCollision(new RBColliderSphere(edge.begin, capsule.radius), org, dirN, length, allowBackFaceCollision);
            if (s1Info.IsValidHit && Vector3.Dot(d, s1Info.position - edge.begin) < 0)
            {
                return s1Info;
            }

            var s2Info = RaycastSphere.CalcRayCollision(new RBColliderSphere(edge.end, capsule.radius), org, dirN, length, allowBackFaceCollision);
            if (s2Info.IsValidHit && Vector3.Dot(-d, s1Info.position - edge.end) < 0)
            {
                return s2Info;
            }

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
                float c = dpp - (dps * dps) / dss - (capsule.radius * capsule.radius);

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

                if (!allowBackFaceCollision && (t1 < 0))
                {
                    return default;
                }

                float t = t1;

                if (!(t > 0 && t <= length) || (t2 > 0 && t2 <= length && t2 < t))
                {
                    t = t2;
                }

                if ((t > 0) && t <= length)
                {
                    Vector3 pos = org + dirN * t;

                    RBColliderCastHitInfo info = new RBColliderCastHitInfo();
                    info.SetHit(pos, Vector3.ProjectOnPlane(pos - capsule.pos, d).normalized, t, t1 < 0);

                    return info;
                }
            }

            return default;
        }
    }
}