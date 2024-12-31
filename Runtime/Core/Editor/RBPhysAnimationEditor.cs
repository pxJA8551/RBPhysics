using RBPhys;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace RBPhys
{
    [CustomEditor(typeof(RBPhysAnimation))]
    [CanEditMultipleObjects]
    public class RBPhysAnimationEditor : Editor
    {
        SerializedProperty baseAnimationClip;
        SerializedProperty animationClip;
        SerializedProperty trsCurve;
        SerializedProperty linker;
        SerializedProperty animationType;

        SerializedProperty parentTransform;
        SerializedProperty playing;
        SerializedProperty enablePhysProceduralAnimation;

        SerializedProperty interp;
        SerializedProperty velocityInterp;
        SerializedProperty interpMultiplier;
        SerializedProperty ext_lambda_compensation;

        private void OnEnable()
        {
            baseAnimationClip = serializedObject.FindProperty("baseAnimationClip");

            animationClip = serializedObject.FindProperty("_animationClip");
            trsCurve = serializedObject.FindProperty("trsCurve");
            linker = serializedObject.FindProperty("linker");
            animationType = serializedObject.FindProperty("animationType");

            parentTransform = serializedObject.FindProperty("parentTransform");
            playing = serializedObject.FindProperty("playing");
            enablePhysProceduralAnimation = serializedObject.FindProperty("enablePhysProceduralAnimation");

            interp = serializedObject.FindProperty("interp");
            velocityInterp = serializedObject.FindProperty("velocityInterp");
            interpMultiplier = serializedObject.FindProperty("interpMultiplier");
            ext_lambda_compensation = serializedObject.FindProperty("ext_lambda_compensation");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(baseAnimationClip);

            if (baseAnimationClip.objectReferenceValue != null)
            {
                GUIConvertBaseAnim(baseAnimationClip.objectReferenceValue as AnimationClip, animationClip.objectReferenceValue as AnimationClip, trsCurve.objectReferenceValue as RBPhysTRSAnimationCurve);
            }

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(animationClip);
            EditorGUILayout.PropertyField(trsCurve);
            EditorGUI.indentLevel--;

            GUIValidateAnimation(baseAnimationClip.objectReferenceValue as AnimationClip, animationClip.objectReferenceValue as AnimationClip, trsCurve.objectReferenceValue as RBPhysTRSAnimationCurve);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(animationType);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(parentTransform);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(playing);
            EditorGUILayout.PropertyField(enablePhysProceduralAnimation);
            EditorGUILayout.PropertyField(linker);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(interp);
            EditorGUILayout.PropertyField(velocityInterp);
            EditorGUILayout.PropertyField(interpMultiplier);
            EditorGUILayout.PropertyField(ext_lambda_compensation);

            serializedObject.ApplyModifiedProperties();
        }

        public void GUIValidateAnimation(AnimationClip baseAnim, AnimationClip anim, RBPhysTRSAnimationCurve trsCurve)
        {
            if (baseAnim != null)
            {
                if (FindTRSAnimationCurve(baseAnim))
                {
                    if (trsCurve == null)
                    {
                        EditorGUILayout.HelpBox("TRS-Curve found in base animationClip. Consider converting base AnimationClip to RBPhysTRSAnimationCurve.", MessageType.Warning);
                        return;
                    }
                }
            }

            if (anim == null)
            {
                EditorGUILayout.HelpBox("No animationClip asset selected. RBPhysAnimation will be disabled at runtime.", MessageType.Warning);
                return;
            }

            if (FindTRSAnimationCurve(anim))
            {
                if (trsCurve == null)
                {
                    EditorGUILayout.HelpBox("TRS-Curve found in animationClip. Physically Procedural Animation will be disabled at runtime. Consider convering TRS-Curve to RBPhysTRSAnimationCurve.", MessageType.Warning);
                    GUIConvertAnim(anim, trsCurve);
                    return;
                }
                else
                {
                    EditorGUILayout.HelpBox("TRS-Curve found in animationClip. Consider converting TRS-Curve to RBPhysTRSAnimationCurve.", MessageType.Warning);
                    GUIConvertAnim(anim, trsCurve);
                    return;
                }
            }
            else
            {
                if (trsCurve == null)
                {
                    EditorGUILayout.HelpBox("RBPhysTRSAnimationCurve asset not selected. Physically Procedural Animation will be disabled at runtime.", MessageType.Info);
                    return;
                }
            }

            if (!trsCurve.Validate(true, out string errorStr))
            {
                EditorGUILayout.HelpBox("Invalid RBPhysTRSAnimationCurve found. Error: " + errorStr, MessageType.Info);
            }
        }

        void GUIConvertAnim(AnimationClip baseAnim, RBPhysTRSAnimationCurve baseTrsCurve)
        {
            if (GUILayout.Button("Convert TRS-Curve to RBPhysTRSAnimationCurve"))
            {
                ConvertAnim(baseAnim, baseTrsCurve);
            }
            EditorGUILayout.LabelField("--> Converting will replace set AnimClip/RBPhysTRSAnimCv", EditorStyles.miniBoldLabel);
            EditorGUILayout.Space();
        }

        void GUIConvertBaseAnim(AnimationClip baseAnimClip, AnimationClip anim, RBPhysTRSAnimationCurve trsCurve)
        {
            if (GUILayout.Button("Convert Base AnimationClip to RBPhysTRSAnimationCurve"))
            {
                ConvertBaseAnim(baseAnimClip, anim, trsCurve);
            }
            EditorGUILayout.LabelField("--> Converting will replace set AnimClip/RBPhysTRSAnimCv", EditorStyles.miniBoldLabel);
            EditorGUILayout.Space();
        }

        void ConvertAnim(AnimationClip baseAnimClip, RBPhysTRSAnimationCurve baseTrsCurve)
        {
            RecontructAnimationClip(baseAnimClip, out AnimationClip animClip, out RBPhysTRSAnimationCurve trsCurve);
            SaveAnimClip(animClip, baseAnimClip, baseAnimClip.name);
            SaveTRSCurve(trsCurve, baseTrsCurve, baseAnimClip.name);
        }

        void ConvertBaseAnim(AnimationClip baseAnim, AnimationClip baseAnimClip, RBPhysTRSAnimationCurve baseTrsCurve)
        {
            RecontructAnimationClip(baseAnim, out AnimationClip animClip, out RBPhysTRSAnimationCurve trsCurve);
            SaveAnimClip(animClip, baseAnimClip, baseAnim.name);
            SaveTRSCurve(trsCurve, baseTrsCurve, baseAnim.name);

            animationClip.objectReferenceValue = animClip;
            this.trsCurve.objectReferenceValue = trsCurve;
        }

        void SaveAnimClip(AnimationClip anim, AnimationClip baseAnim, string baseName)
        {
            bool b = baseAnim != null;

            anim.name = baseName + "_cv";

            string path = b ? AssetDatabase.GetAssetPath(baseAnim) : ""; // : application.datapath
            string savePath = EditorUtility.SaveFilePanelInProject(path, anim.name, "asset", "");
            if (AssetDatabase.AssetPathToGUID(savePath) != "")
            {
                if (EditorUtility.DisplayDialog("ファイルの上書き", "AnimationClipを上書きしますか？", "上書き", "キャンセル"))
                {
                    AssetDatabase.CreateAsset(anim, savePath);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(savePath))
                {
                    AssetDatabase.CreateAsset(anim, savePath);
                }
            }

            AssetDatabase.SaveAssets();
        }

        void SaveTRSCurve(RBPhysTRSAnimationCurve trsCurve, RBPhysTRSAnimationCurve baseTrsCurve, string baseName)
        {
            bool b = baseTrsCurve != null;

            trsCurve.name = baseName + "_cv_trs";

            string path = b ? AssetDatabase.GetAssetPath(baseTrsCurve) : ""; // : application.datapath
            string savePath = EditorUtility.SaveFilePanelInProject(path, trsCurve.name, "asset", "");
            if (AssetDatabase.AssetPathToGUID(savePath) != "")
            {
                if (EditorUtility.DisplayDialog("ファイルの上書き", "RBPhysTRSAnimationCurveを上書きしますか？", "上書き", "キャンセル"))
                {
                    AssetDatabase.CreateAsset(trsCurve, savePath);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(savePath))
                {
                    AssetDatabase.CreateAsset(trsCurve, savePath);
                }
            }

            AssetDatabase.SaveAssets();
        }

        bool FindTRSAnimationCurve(AnimationClip anim)
        {
            EditorCurveBinding[] curves = AnimationUtility.GetCurveBindings(anim);

            var trsCurve = CreateInstance<RBPhysTRSAnimationCurve>();

            foreach (var c in curves)
            {
                if (trsCurve.TrySetCurve(anim, c))
                {
                    return true;
                }
            }

            return false;
        }

        void RecontructAnimationClip(AnimationClip baseAnimation, out AnimationClip animClip, out RBPhysTRSAnimationCurve trsCurve)
        {
            trsCurve = CreateInstance<RBPhysTRSAnimationCurve>();

            animClip = AnimationClip.Instantiate(baseAnimation);
            animClip.name = baseAnimation.name;
            animClip.legacy = true;

            trsCurve.length = animClip.length;

            EditorCurveBinding[] curves = AnimationUtility.GetCurveBindings(animClip);
            List<(EditorCurveBinding, AnimationCurve)> setCurves = new List<(EditorCurveBinding, AnimationCurve)>();

            foreach (var c in curves)
            {
                if (!trsCurve.TrySetCurve(animClip, c))
                {
                    setCurves.Add((c, AnimationUtility.GetEditorCurve(animClip, c)));
                }
            }

            animClip.ClearCurves();
            AnimationUtility.SetEditorCurves(animClip, setCurves.Select(item => item.Item1).ToArray(), setCurves.Select(item => item.Item2).ToArray());

            if (!trsCurve.Validate())
            {
                Debug.LogWarning("RBPhysAnimation -- Validation falied.");
                EditorUtility.DisplayDialog("Conversion Failed", "RBPhysAnimation -- Validation falied.", "OK");
            }
        }
    }
}