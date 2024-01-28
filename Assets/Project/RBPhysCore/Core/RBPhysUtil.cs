using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysUtil
    {
        public const float EPSILON_FLOAT32 = 0.000001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float V3Volume(Vector3 v)
        {
            return Mathf.Abs(v.x * v.y * v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 V3Rcp(Vector3 v)
        {
            return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
        }

        static float sqrt3Inv = 1 / Mathf.Sqrt(3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void V3FromOrthogonalBasis(Vector3 v, out Vector3 a, out Vector3 b)
        {
            if (Mathf.Abs(v.x) >= sqrt3Inv)
            {
                a = new Vector3(v.y, -v.x, 0).normalized;
            }
            else
            {
                a = new Vector3(0, v.z, -v.y).normalized;
            }

            b = Vector3.Cross(a, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Less(Vector3 v, Vector3 lessThan)
        {
            return v.x < lessThan.x && v.y < lessThan.y && v.z < lessThan.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Greater(Vector3 v, Vector3 greaterThan)
        {
            return v.x > greaterThan.x && v.y > greaterThan.y && v.z > greaterThan.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3EpsilonEqual(Vector3 a, Vector3 b, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32EpsilonEqual(a.x, b.x, epsilon) && IsF32EpsilonEqual(a.y, b.y, epsilon) && IsF32EpsilonEqual(a.z, b.z, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3DotEpsilonEqual(Vector3 a, Vector3 b, float dotEqualTo, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32EpsilonEqual(Vector3.Dot(a, b), dotEqualTo, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3AbsDotEpsilonEqual(Vector3 a, Vector3 b, float dotAbsEqualTo, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32AbsEpsilonEqual(Vector3.Dot(a, b), dotAbsEqualTo, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32EpsilonEqual(float a, float b, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(a - b) <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32AbsEpsilonEqual(float value, float valueAbsEqualTo, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(Mathf.Abs(value) - valueAbsEqualTo) <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInt32Pow2(int x)
        {
            return x != 0 && (x & (x - 1)) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F32Sign101(float x)
        {
            return x > 0 ? 1 : x == 0 ? 0 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F32Sign101Epsilon(float x, float epsilon)
        {
            return Mathf.Abs(x) < epsilon ? 0 : (x > 0 ? 1 : -1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F32Sign11(float x)
        {
            return x > 0 ? 1 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PredictPosRot(RBRigidbody rb, out Vector3 position, out Quaternion rotation, float dt)
        {
            position = rb.Position + rb.ExpVelocity * dt;
            rotation = rb.Rotation * Quaternion.AngleAxis(rb.ExpAngularVelocity.magnitude * Mathf.Rad2Deg * dt, rb.ExpAngularVelocity.normalized);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2)
        {
            float a_min = Mathf.Min(a_x1, a_x2);
            float a_max = Mathf.Max(a_x1, a_x2);
            float b_min = Mathf.Min(b_x1, b_x2);
            float b_max = Mathf.Max(b_x1, b_x2);
            
            return !(a_max < b_min || b_max < a_min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetOBBAxisSize(Vector3 size, Quaternion rot, Vector3 axis)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, size.z), axis));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(size.x, 0, 0), axis));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, size.y, 0), axis));
            return fwd + right + up;
        }
    }
}