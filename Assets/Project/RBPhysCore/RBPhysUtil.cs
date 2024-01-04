using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysUtil
    {
        public const float EPSILON_FLOAT32 = 0.000001f;

        public static Vector3 V3Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        public static Vector3 V3Rcp(Vector3 v)
        {
            return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
        }

        public static bool IsV3Less(Vector3 v, Vector3 lessThan)
        {
            return v.x < lessThan.x && v.y < lessThan.y && v.z < lessThan.z;
        }

        public static bool IsV3Greater(Vector3 v, Vector3 greaterThan)
        {
            return v.x > greaterThan.x && v.y > greaterThan.y && v.z > greaterThan.z;
        }

        public static void PredictPosRot(RBRigidbody rb, out Vector3 position, out Quaternion rotation, float dt)
        {
            position = rb.Position + rb.ExpVelocity * dt;
            rotation = rb.Rotation * Quaternion.AngleAxis(rb.ExpAngularVelocity.magnitude * Mathf.Rad2Deg * dt, rb.ExpAngularVelocity.normalized);
        }

        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2)
        {
            float a_min = Mathf.Min(a_x1, a_x2);
            float a_max = Mathf.Max(a_x1, a_x2);
            float b_min = Mathf.Min(b_x1, b_x2);
            float b_max = Mathf.Max(b_x1, b_x2);
            
            return !(a_max < b_min || b_max < a_min);
        }
    }
}