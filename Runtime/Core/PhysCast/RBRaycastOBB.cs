using RBPhys;
using UnityEngine;
using static RBPhys.RBPhysComputer;

public static partial class RBRaycast
{
    public static class RaycastOBB
    {
        public static RBColliderCastHitInfo CalcRayCollision(RBColliderOBB obb, Vector3 org, Vector3 dirN, float length, bool ignoreBackFaceCollision = true)
        {
            Quaternion toLsRot = Quaternion.Inverse(obb.rot);

            Vector3 lsDirN = toLsRot * dirN;
            Vector3 lsOrg = toLsRot * (org - obb.pos);

            float t_x_min = -lsOrg.x / lsDirN.x;
            float t_x_max = (obb.size.x - lsOrg.x) / lsDirN.x;

            float t_y_min = -lsOrg.y / lsDirN.y;
            float t_y_max = (obb.size.y - lsOrg.y) / lsDirN.y;

            float t_z_min = -lsOrg.z / lsDirN.z;
            float t_z_max = (obb.size.z - lsOrg.z) / lsDirN.z;

            bool rayHit = RBPhysUtil.RangeOverlap(t_x_min, t_x_max, t_y_min, t_y_max, t_z_min, t_z_max, out float t_min, out float t_max, out int i_min, out int i_max);

            float t = t_min;
            float i_t = i_min;
            bool inv = true;

            if (ignoreBackFaceCollision && (t_min < 0))
            {
                return default;
            }

            if (t <= 0)
            {
                t = t_max;
                i_t = i_max;
                inv = false;
            }

            if (rayHit && t > 0 && t < length)
            {
                Vector3 lsHitPoint = lsOrg + lsDirN * t;
                Vector3 wsHitPoint = obb.pos + obb.rot * lsHitPoint;

                Vector3 normal = obb.GetAxisRightN();
                if (i_t == 1) normal = obb.GetAxisUpN();
                if (i_t == 2) normal = obb.GetAxisForwardN();
                if (!inv) normal *= -1;

                RBColliderCastHitInfo info = default;
                info.SetHit(wsHitPoint, normal, t, t_min < 0);
                return info;
            }
            else
            {
                return default;
            }
        }
    }
}