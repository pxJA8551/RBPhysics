#define RBPHYS_DEBUG_ASSERTION

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

        public static void IsV3ValidAssert(Vector3 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3ValidAll(v), "Invalid vector3 detected. vec3 val =" + v);
#endif
        }

        public static void IsV3ValidAssert(Vector3 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsV3ValidAll(v), message);
#endif
        }

        public static void IsV2ValidAssert(Vector2 v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsF32Valid(v.x) && RBPhysUtil.IsF32Valid(v.y);
            Debug.Assert(isValid, "Iinvalid vector2 detected. vec2 val =" + v);
#endif
        }

        public static void IsV2ValidAssert(Vector2 v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsF32Valid(v.x) && RBPhysUtil.IsF32Valid(v.y);
            Debug.Assert(isValid, message);
#endif
        }

        public static void IsF32ValidAssert(float v)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsF32Valid(v), "Invalid float detected. f32 val =" + v);
#endif
        }

        public static void IsF32ValidAssert(float v, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            Debug.Assert(RBPhysUtil.IsF32Valid(v), message);
#endif
        }

        public static void IsPenetrationValidAssert(RBDetailCollision.Penetration p)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(p.p) && RBPhysUtil.IsV3ValidAll(p.pA) && RBPhysUtil.IsV3ValidAll(p.pB);
            Debug.Assert(isValid, "Invalid penetration info detected. p(p, pA, pB) val =" + (p.p, p.pA, p.pB));
#endif
        }

        public static void IsPenetrationValidAssert(RBDetailCollision.Penetration p, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(p.p) && RBPhysUtil.IsV3ValidAll(p.pA) && RBPhysUtil.IsV3ValidAll(p.pB);
            Debug.Assert(isValid, message);
#endif
        }

        public static void IsCastHitValidAssert(RBColliderCastHitInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(c.position) && RBPhysUtil.IsV3ValidAll(c.normal) && RBPhysUtil.IsF32Valid(c.length);
            Debug.Assert(isValid, "Invalid cast hit info detected. p(pos, normal, length) val =" + (c.position, c.normal, c.length));
#endif
        }

        public static void IsCastHitValidAssert(RBColliderCastHitInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(c.position) && RBPhysUtil.IsV3ValidAll(c.normal) && RBPhysUtil.IsF32Valid(c.length);
            Debug.Assert(isValid, message);
#endif
        }

        public static void IsOverlapValidAssert(RBColliderOverlapInfo c)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(c.position) && RBPhysUtil.IsV3ValidAll(c.normal);
            Debug.Assert(isValid, "Invalid overlap info detected. p(pos, normal) val =" + (c.position, c.normal));
#endif
        }

        public static void IsOverlapValidAssert(RBColliderOverlapInfo c, string message)
        {
#if UNITY_EDITOR || RBPHYS_DEBUG_ASSERTION
            bool isValid = RBPhysUtil.IsV3ValidAll(c.position) && RBPhysUtil.IsV3ValidAll(c.normal);
            Debug.Assert(isValid, message);
#endif
        }
    }
}