using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RBPhys
{
    [CreateAssetMenu(menuName = "RBPhys/RBPhysTRSAnimationCurve")]
    public class RBPhysTRSAnimationCurve : ScriptableObject
    {
        public AnimationCurve curve_lsPos_x;
        public AnimationCurve curve_lsPos_y;
        public AnimationCurve curve_lsPos_z;
        public AnimationCurve curve_lsRotQuat_x;
        public AnimationCurve curve_lsRotQuat_y;
        public AnimationCurve curve_lsRotQuat_z;
        public AnimationCurve curve_lsRotQuat_w;
        public AnimationCurve curve_lsScale_x;
        public AnimationCurve curve_lsScale_y;
        public AnimationCurve curve_lsScale_z;
        public float length;

        public bool Validate(bool noLog = false)
        {
            if (curve_lsPos_x == null)
            {
                if(!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_X data found.");
                return false;
            }

            if (curve_lsPos_y == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_Y data found.");
                return false;
            }

            if (curve_lsPos_z == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_Z data found.");
                return false;
            }

            if (curve_lsRotQuat_x == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_X data found.");
                return false;
            }

            if (curve_lsRotQuat_y == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_Y data found.");
                return false;
            }

            if (curve_lsRotQuat_z == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_Z data found.");
                return false;
            }

            if (curve_lsRotQuat_w == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_W data found.");
                return false;
            }

            return true;
        }
        public bool Validate(bool noLog, out string errorStr)
        {
            errorStr = "";

            if (curve_lsPos_x == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_X data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsPos_y == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_Y data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsPos_z == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_Z data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsRotQuat_x == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_X data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsRotQuat_y == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_Y data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsRotQuat_z == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_Z data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            if (curve_lsRotQuat_w == null)
            {
                errorStr = "RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_ROT_W data found.";
                if (!noLog) Debug.LogWarning(errorStr);
                return false;
            }

            return true;
        }

#if UNITY_EDITOR

        public bool TrySetCurve(AnimationClip clip, EditorCurveBinding c)
        {
            if (c.type == typeof(Transform) && c.path == "")
            {
                switch (c.propertyName)
                {
                    case "m_LocalPosition.x":
                        curve_lsPos_x = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalPosition.y":
                        curve_lsPos_y = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalPosition.z":
                        curve_lsPos_z = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalRotation.x":
                        curve_lsRotQuat_x = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalRotation.y":
                        curve_lsRotQuat_y = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalRotation.z":
                        curve_lsRotQuat_z = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalRotation.w":
                        curve_lsRotQuat_w = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalScale.x":
                        curve_lsScale_x = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalScale.y":
                        curve_lsScale_y = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;

                    case "m_LocalScale.z":
                        curve_lsScale_z = new AnimationCurve(AnimationUtility.GetEditorCurve(clip, c).keys);
                        return true;
                }
            }

            return false;
        }

#endif

        public void SampleTRSAnimation(float time, Vector3 pos, Quaternion rot, RBPhysAnimationType animType, out Vector3 lsPos, out Quaternion lsRot)
        {
            float cTime = EvaluateTime(time, animType);

            lsPos = pos;
            lsRot = rot;

            lsPos.x = curve_lsPos_x?.Evaluate(cTime) ?? lsPos.x;
            lsPos.y = curve_lsPos_y?.Evaluate(cTime) ?? lsPos.y;
            lsPos.z = curve_lsPos_z?.Evaluate(cTime) ?? lsPos.z;

            lsRot.x = curve_lsRotQuat_x?.Evaluate(cTime) ?? lsRot.x;
            lsRot.y = curve_lsRotQuat_y?.Evaluate(cTime) ?? lsRot.y;
            lsRot.z = curve_lsRotQuat_z?.Evaluate(cTime) ?? lsRot.z;
            lsRot.w = curve_lsRotQuat_w?.Evaluate(cTime) ?? lsRot.w;
        }

        public void SampleTRSAnimation(float time, Vector3 pos, Quaternion rot, Vector3 scale, RBPhysAnimationType animType, out Vector3 lsPos, out Quaternion lsRot, out Vector3 lsScale)
        {
            float cTime = EvaluateTime(time, animType);

            lsPos = pos;
            lsScale = scale;
            lsRot = rot;

            lsPos.x = curve_lsPos_x?.Evaluate(cTime) ?? lsPos.x;
            lsPos.y = curve_lsPos_y?.Evaluate(cTime) ?? lsPos.y;
            lsPos.z = curve_lsPos_z?.Evaluate(cTime) ?? lsPos.z;

            lsRot.x = curve_lsRotQuat_x?.Evaluate(cTime) ?? lsRot.x;
            lsRot.y = curve_lsRotQuat_y?.Evaluate(cTime) ?? lsRot.y;
            lsRot.z = curve_lsRotQuat_z?.Evaluate(cTime) ?? lsRot.z;
            lsRot.w = curve_lsRotQuat_w?.Evaluate(cTime) ?? lsRot.w;

            lsScale.x = curve_lsScale_x?.Evaluate(cTime) ?? lsScale.x;
            lsScale.y = curve_lsScale_y?.Evaluate(cTime) ?? lsScale.y;
            lsScale.z = curve_lsScale_z?.Evaluate(cTime) ?? lsScale.z;
        }

        public float EvaluateTime(float t, RBPhysAnimationType animType)
        {
            switch (animType)
            {
                case RBPhysAnimationType.Once:
                    return Mathf.Clamp(t, 0, length);

                case RBPhysAnimationType.Loop:
                    return Mathf.Clamp(t % length, 0, length);

                case RBPhysAnimationType.Ping_Pong:
                    float f = (length * 2);
                    return Mathf.Clamp(length - Mathf.Abs((t % f) - length), 0, length);
            }

            return t;
        }
    }
}