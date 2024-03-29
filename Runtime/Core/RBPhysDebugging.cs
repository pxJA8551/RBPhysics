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

        public static void IsV3NormalAssert(Vector3 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3NormalAll(v), "Invalid vector3 detected. vec3 val =" + v);
#endif
        }

        public static void IsV3NormalAssert(Vector3 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3NormalAll(v), message);
#endif
        }

        public static void IsV2NormalAssert(Vector2 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = float.IsNormal(v.x) && float.IsNormal(v.y);
            Debug.Assert(isNormal, "Iinvalid vector2 detected. vec2 val =" + v);
#endif
        }

        public static void IsV2NormalAssert(Vector2 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = float.IsNormal(v.x) && float.IsNormal(v.y);
            Debug.Assert(isNormal, message);
#endif
        }

        public static void IsF32NormalAssert(float v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(float.IsNormal(v), "Invalid float detected. f32 val =" + v);
#endif
        }

        public static void IsF32NormalAssert(float v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(float.IsNaN(v), message);
#endif
        }

        public static void IsPenetrationNormalAssert(RBDetailCollision.Penetration p)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(p.p) && RBPhysUtil.IsV3NormalAll(p.pA) && RBPhysUtil.IsV3NormalAll(p.pB);
            Debug.Assert(isNormal, "Invalid penetration info detected. p(p, pA, pB) val =" + (p.p, p.pA, p.pB));
#endif
        }

        public static void IsPenetrationNormalAssert(RBDetailCollision.Penetration p, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(p.p) && RBPhysUtil.IsV3NormalAll(p.pA) && RBPhysUtil.IsV3NormalAll(p.pB);
            Debug.Assert(isNormal, message);
#endif
        }

        public static void IsCastHitNormalAssert(RBColliderCastHitInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(c.position) && RBPhysUtil.IsV3NormalAll(c.normal) && float.IsNormal(c.length);
            Debug.Assert(isNormal, "Invalid cast hit info detected. p(pos, normal, length) val =" + (c.position, c.normal, c.length));
#endif
        }

        public static void IsCastHitNormalAssert(RBColliderCastHitInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(c.position) && RBPhysUtil.IsV3NormalAll(c.normal) && float.IsNormal(c.length);
            Debug.Assert(isNormal, message);
#endif
        }

        public static void IsOverlapNormalAssert(RBColliderOverlapInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(c.position) && RBPhysUtil.IsV3NormalAll(c.normal);
            Debug.Assert(isNormal, "Invalid overlap info detected. p(pos, normal) val =" + (c.position, c.normal));
#endif
        }

        public static void IsOverlapNormalAssert(RBColliderOverlapInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isNormal = RBPhysUtil.IsV3NormalAll(c.position) && RBPhysUtil.IsV3NormalAll(c.normal);
            Debug.Assert(isNormal, message);
#endif
        }
    }
}