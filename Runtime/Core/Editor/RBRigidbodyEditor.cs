using RBPhys;
using UnityEditor;
using UnityEngine;

namespace RBPhysEditor
{
    [CustomEditor(typeof(RBRigidbody))]
    public class RBRigidbodyEditor : Editor
    {
        SerializedProperty mass;
        SerializedProperty inertiaTensorMultiplier;
        SerializedProperty drag;
        SerializedProperty angularDrag;
        SerializedProperty sleeping;
        SerializedProperty sleepCount;
        SerializedProperty useGravity;
        SerializedProperty sleepDefault;
        SerializedProperty infInertiaTensor;

        const float HANDLE_SIZE = 1;
        const float HANDLE_DOT_SIZE = 0.003f;

        private void OnEnable()
        {
            mass = serializedObject.FindProperty("mass");
            inertiaTensorMultiplier = serializedObject.FindProperty("inertiaTensorMultiplier");
            sleeping = serializedObject.FindProperty("isSleeping");
            sleepCount = serializedObject.FindProperty("_sleepCount");
            useGravity = serializedObject.FindProperty("useGravity");
            drag = serializedObject.FindProperty("drag");
            angularDrag = serializedObject.FindProperty("angularDrag");
            sleepDefault = serializedObject.FindProperty("sleepUntilInteraction");
            infInertiaTensor = serializedObject.FindProperty("setInfInertiaTensorOnInit");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (sleeping.boolValue == true && sleepCount.intValue == 5)
            {
                sleepDefault.boolValue = true;
            }
            else
            {
                sleepDefault.boolValue = false;
            }

            mass.floatValue = Mathf.Abs(mass.floatValue);
            inertiaTensorMultiplier.floatValue = Mathf.Abs(inertiaTensorMultiplier.floatValue);
            drag.floatValue = Mathf.Abs(drag.floatValue);
            angularDrag.floatValue = Mathf.Abs(angularDrag.floatValue);

            EditorGUILayout.PropertyField(mass);
            EditorGUILayout.PropertyField(inertiaTensorMultiplier);
            EditorGUILayout.PropertyField(drag);
            EditorGUILayout.PropertyField(angularDrag);
            EditorGUILayout.PropertyField(useGravity);

            sleepDefault.boolValue = EditorGUILayout.Toggle("Sleep until interaction", sleepDefault.boolValue);

            if (sleepDefault.boolValue)
            {
                sleepCount.intValue = 5;
                sleeping.boolValue = true;
            }
            else
            {
                sleepCount.intValue = 0;
                sleeping.boolValue = false;
            }

            EditorGUILayout.LabelField(string.Format("Sleep:{0}({1})", sleeping.boolValue ? "TRUE" : "FALSE", sleepCount.intValue));

            EditorGUILayout.PropertyField(infInertiaTensor);

            serializedObject.ApplyModifiedProperties();
        }
    }
}