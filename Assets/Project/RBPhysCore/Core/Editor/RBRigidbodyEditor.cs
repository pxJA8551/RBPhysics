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

        bool sleepDefault;

        const float HANDLE_SIZE = 1;
        const float HANDLE_DOT_SIZE = 0.003f;

        private void OnEnable()
        {
            mass = serializedObject.FindProperty("mass");
            sleeping = serializedObject.FindProperty("isSleeping");
            sleepGrace = serializedObject.FindProperty("sleepGrace");
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
            sleepDefault = EditorGUILayout.Toggle("Sleep until interaction", sleepDefault);

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