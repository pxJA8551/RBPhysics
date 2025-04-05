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
        public float length;

        public void Initialize()
        {
            curve_lsPos_x = null;
            curve_lsPos_y = null;
            curve_lsPos_z = null;
            curve_lsRotQuat_x = null;
            curve_lsRotQuat_y = null;
            curve_lsRotQuat_z = null;
            curve_lsRotQuat_w = null;
        }

        public bool Validate(bool noLog = false)
        {
            if (curve_lsPos_x == null)
            {
                if (!noLog) Debug.LogWarning("RBPhysAnimation.TRSAnimationCurve -- Validation falied. No LS_POS_X data found.");
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
                }
            }

            return false;
        }

#endif

        public void SampleTRSAnimation(float time, Vector3 pos, Quaternion rot, out Vector3 lsPos, out Quaternion lsRot)
        {
            float cTime = EvaluateTime(time, RBPhysAnimationType.Once);

            lsPos = pos;
            lsRot = rot;

            lsPos.x = GetFloatValue(curve_lsPos_x, cTime, lsPos.x);
            lsPos.y = GetFloatValue(curve_lsPos_y, cTime, lsPos.y);
            lsPos.z = GetFloatValue(curve_lsPos_z, cTime, lsPos.z);

            lsRot.x = GetFloatValue(curve_lsRotQuat_x, cTime, lsRot.x);
            lsRot.y = GetFloatValue(curve_lsRotQuat_y, cTime, lsRot.y);
            lsRot.z = GetFloatValue(curve_lsRotQuat_z, cTime, lsRot.z);
            lsRot.w = GetFloatValue(curve_lsRotQuat_w, cTime, lsRot.w);
        }

        float GetFloatValue(AnimationCurve curve, float time, float v)
        {
            if (curve != null && curve.length > 0) // curve.length = keys.Length ‚Ç‚¤‚µ‚Ä
            {
                return curve.Evaluate(time);
            }
            else
            {
                return v;
            }
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