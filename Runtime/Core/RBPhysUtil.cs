using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysUtil
    {
        public const float EPSILON_FLOAT32 = 0.00001f;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3NanAny(Vector3 v)
        {
            return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3NanAll(Vector3 v)
        {
            return float.IsNaN(v.x) && float.IsNaN(v.y) && float.IsNaN(v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32Valid(float v)
        {
            return float.IsFinite(v) && !float.IsNaN(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3ValidAll(Vector3 v)
        {
            return IsF32Valid(v.x) && IsF32Valid(v.y) && IsF32Valid(v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3ValidAny(Vector3 v)
        {
            return IsF32Valid(v.x) || IsF32Valid(v.y) || IsF32Valid(v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Sign101EqualAll(Vector3 v1, Vector3 v2)
        {
            return F32Sign101(v1.x) == F32Sign101(v2.x) && F32Sign101(v1.y) == F32Sign101(v2.y) && F32Sign101(v1.z) == F32Sign101(v2.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Sign101EqualAny(Vector3 v1, Vector3 v2)
        {
            return F32Sign101(v1.x) == F32Sign101(v2.x) || F32Sign101(v1.y) == F32Sign101(v2.y) || F32Sign101(v1.z) == F32Sign101(v2.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Sign101EpsilonEqualAll(Vector3 v1, Vector3 v2, float epsilon = EPSILON_FLOAT32)
        {
            return F32Sign101Epsilon(v1.x, epsilon) == F32Sign101Epsilon(v2.x, epsilon) && F32Sign101Epsilon(v1.y, epsilon) == F32Sign101Epsilon(v2.y, epsilon) && F32Sign101Epsilon(v1.z, epsilon) == F32Sign101Epsilon(v2.z, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsV3Sign101EpsilonEqualAny(Vector3 v1, Vector3 v2, float epsilon = EPSILON_FLOAT32)
        {
            return F32Sign101Epsilon(v1.x, epsilon) == F32Sign101Epsilon(v2.x, epsilon) || F32Sign101Epsilon(v1.y, epsilon) == F32Sign101Epsilon(v2.y, epsilon) || F32Sign101Epsilon(v1.z, epsilon) == F32Sign101Epsilon(v2.z, epsilon);
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
        public static bool IsV3EpsilonInfEqual(Vector3 a, Vector3 b, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32EpsilonInfEqual(a.x, b.x, epsilon) && IsF32EpsilonInfEqual(a.y, b.y, epsilon) && IsF32EpsilonInfEqual(a.z, b.z, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsQuaternionEpsilonEqual(Quaternion a, Quaternion b, float epsilon = EPSILON_FLOAT32)
        {
            return IsF32EpsilonEqual(a.x, b.x, epsilon) && IsF32EpsilonEqual(a.y, b.y, epsilon) && IsF32EpsilonEqual(a.z, b.z, epsilon) && IsF32EpsilonEqual(a.w, b.w, epsilon);
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
        public static bool IsF32EpsilonInfEqual(float a, float b, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(a - b) <= epsilon || (float.IsInfinity(a) && float.IsInfinity(b) && (a < 0 == b < 0));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32AbsEpsilonEqual(float value, float valueAbsEqualTo, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(Mathf.Abs(value) - valueAbsEqualTo) <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsF32AbsEpsilonInfEqual(float value, float valueAbsEqualTo, float epsilon = EPSILON_FLOAT32)
        {
            return Mathf.Abs(Mathf.Abs(value) - valueAbsEqualTo) <= epsilon || (float.IsInfinity(value) && float.IsInfinity(valueAbsEqualTo));
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
        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2, float epsilonHalf)
        {
            float a_min = Mathf.Min(a_x1, a_x2) - epsilonHalf;
            float a_max = Mathf.Max(a_x1, a_x2) + epsilonHalf;
            float b_min = Mathf.Min(b_x1, b_x2) - epsilonHalf;
            float b_max = Mathf.Max(b_x1, b_x2) + epsilonHalf;

            return !(a_max < b_min || b_max < a_min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2, float c_x1, float c_x2, out float r_min, out float r_max, out int i_min, out int i_max)
        {
            float a_min = Mathf.Min(a_x1, a_x2);
            float a_max = Mathf.Max(a_x1, a_x2);
            float b_min = Mathf.Min(b_x1, b_x2);
            float b_max = Mathf.Max(b_x1, b_x2);
            float c_min = Mathf.Min(c_x1, c_x2);
            float c_max = Mathf.Max(c_x1, c_x2);

            r_min = a_min;
            i_min = 0;

            if (b_min > r_min)
            {
                r_min = b_min;
                i_min = 1;
            }

            if (c_min > r_min)
            {
                r_min = c_min;
                i_min = 2;
            }

            r_max = a_max;
            i_max = 0;

            if (b_max < r_max)
            {
                r_max = b_max;
                i_max = 1;
            }

            if (c_max < r_max)
            {
                r_max = c_max;
                i_max = 2;
            }

            return r_min <= r_max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RangeOverlap(float a_x1, float a_x2, float b_x1, float b_x2, float c_x1, float c_x2, out float r)
        {
            float a_min = Mathf.Min(a_x1, a_x2);
            float a_max = Mathf.Max(a_x1, a_x2);
            float b_min = Mathf.Min(b_x1, b_x2);
            float b_max = Mathf.Max(b_x1, b_x2);
            float c_min = Mathf.Min(c_x1, c_x2);
            float c_max = Mathf.Max(c_x1, c_x2);

            r = a_min;

            if (b_min > r)
            {
                r = b_min;
            }

            if (c_min > r)
            {
                r = c_min;
            }

            float r_max = a_max;

            if (b_max < r_max)
            {
                r_max = b_max;
            }

            if (c_max < r_max)
            {
                r_max = c_max;
            }

            return r <= r_max;
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