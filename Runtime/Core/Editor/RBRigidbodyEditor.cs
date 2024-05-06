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
    [CanEditMultipleObjects]
    public class RBRigidbodyEditor : Editor
    {
        SerializedProperty mass;
        SerializedProperty sleeping;
        SerializedProperty sleepGrace;
        SerializedProperty useGravity;

        bool sleepDefault;

        const float HANDLE_SIZE = 1;
        const float HANDLE_DOT_SIZE = 0.003f;

        private void OnEnable()
        {
            mass = serializedObject.FindProperty("mass");
            sleeping = serializedObject.FindProperty("isSleeping");
            sleepGrace = serializedObject.FindProperty("sleepGrace");
            useGravity = serializedObject.FindProperty("useGravity");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (sleeping.boolValue == true && sleepGrace.intValue == 5)
            {
                sleepDefault = true;
            }
            else
            {
                sleepDefault = false;
            }

            EditorGUILayout.PropertyField(mass);
            EditorGUILayout.PropertyField(useGravity);
            sleepDefault = EditorGUILayout.Toggle("Sleep until interaction", sleepDefault);

            EditorGUILayout.LabelField(string.Format("Sleep:{0}({1})", sleeping.boolValue ? "TRUE" : "FALSE", sleepGrace.intValue));

            if (sleepDefault)
            {
                sleeping.boolValue = true;
                sleepGrace.intValue = 5;
            }
            else
            {
                sleeping.boolValue = false;
                sleepGrace.intValue = 0;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}