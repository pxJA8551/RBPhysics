using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static RBPhys.RBPhysCore;

namespace RBPhys
{
    public static class RBPhysDebugging
    {
        public static void EditorAssert(bool condition)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(condition);
#endif
        }

        public static void EditorAssert(bool condition, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(condition, message);
#endif
        }

        public static void V3NaNAssert(Vector3 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3NanAny(v), "vector3 is NaN");
#endif
        }

        public static void V3NaNAssert(Vector3 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3NanAny(v), message);
#endif
        }

        public static void V2NaNAssert(Vector2 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = float.IsNaN(v.x) || float.IsNaN(v.y);
            Debug.Assert(isNaN, "vector2 is NaN");
#endif
        }

        public static void V2NaNAssert(Vector2 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = float.IsNaN(v.x) || float.IsNaN(v.y);
            Debug.Assert(isNaN, message);
#endif
        }

        public static void F32NaNAssert(float v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(float.IsNaN(v), "float is nan");
#endif
        }

        public static void F32NaNAssert(float v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(float.IsNaN(v), message);
#endif
        }

        public static void PenetrationNaNAssert(RBDetailCollision.Penetration p)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(p.p) || RBPhysUtil.IsV3NanAny(p.pA) || RBPhysUtil.IsV3NanAny(p.pB);
            Debug.Assert(isNaN, "Penetration info contains NaN vector3 value");
#endif
        }

        public static void PenetrationNaNAssert(RBDetailCollision.Penetration p, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(p.p) || RBPhysUtil.IsV3NanAny(p.pA) || RBPhysUtil.IsV3NanAny(p.pB);
            Debug.Assert(isNaN, message);
#endif
        }

        public static void CastHitNaNAssert(RBColliderCastHitInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(c.position) || RBPhysUtil.IsV3NanAny(c.normal) || float.IsNaN(c.dist);
            Debug.Assert(isNaN, "RBColliderCastHitInfo contains NaN value");
#endif
        }

        public static void CastHitNaNAssert(RBColliderCastHitInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(c.position) || RBPhysUtil.IsV3NanAny(c.normal) || float.IsNaN(c.dist);
            Debug.Assert(isNaN, message);
#endif
        }

        public static void OverlapNaNAssert(RBColliderOverlapInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(c.position) || RBPhysUtil.IsV3NanAny(c.normal);
            Debug.Assert(isNaN, "RBColliderOverlapInfo contains NaN value");
#endif
        }

        public static void OverlapNaNAssert(RBColliderOverlapInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNaN = RBPhysUtil.IsV3NanAny(c.position) || RBPhysUtil.IsV3NanAny(c.normal);
            Debug.Assert(isNaN, message);
#endif
        }
    }
}