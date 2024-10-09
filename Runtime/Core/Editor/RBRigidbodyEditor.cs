using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using RBPhys;
using UnityEngine.UIElements;

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
        SerializedProperty sleepGrace;
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
            sleepGrace = serializedObject.FindProperty("sleepGrace");
            useGravity = serializedObject.FindProperty("useGravity");
            drag = serializedObject.FindProperty("drag");
            angularDrag = serializedObject.FindProperty("angularDrag");
            sleepDefault = serializedObject.FindProperty("sleepUntilInteraction");
            infInertiaTensor = serializedObject.FindProperty("setInfInertiaTensorOnInit");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (sleeping.boolValue == true && sleepGrace.intValue == 5)
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

            EditorGUILayout.LabelField(string.Format("Sleep:{0}({1})", sleeping.boolValue ? "TRUE" : "FALSE", sleepGrace.intValue));

            EditorGUILayout.PropertyField(infInertiaTensor);

            serializedObject.ApplyModifiedProperties();
        }
    }
}