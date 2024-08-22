using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using RBPhys;

namespace RBPhysEditor
{
    [CustomEditor(typeof(RBBoxCollider))]
    [CanEditMultipleObjects]
    public class RBBoxColliderEditor : Editor
    {
        SerializedProperty center;
        SerializedProperty rotationEuler;
        SerializedProperty size;

        bool scaleEditMode = false;
        bool rotationEditMode = false;
        const float HANDLE_SIZE = 1;
        const float HANDLE_DOT_SIZE = 0.003f;
        Tool t;

        int ctrlId_xp;
        int ctrlId_xn;
        int ctrlId_yp;
        int ctrlId_yn;
        int ctrlId_zp;
        int ctrlId_zn;

        bool selected_xp;
        bool selected_xn;
        bool selected_yp;
        bool selected_yn;
        bool selected_zp;
        bool selected_zn;

        private void OnEnable()
        {
            center = serializedObject.FindProperty("_center");
            rotationEuler = serializedObject.FindProperty("_rotationEuler");
            size = serializedObject.FindProperty("_size");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(center);
            EditorGUILayout.PropertyField(rotationEuler);
            EditorGUILayout.PropertyField(size);

            if (serializedObject.targetObjects.Length == 1)
            {
                EditorGUILayout.Space(1);
                GUILayout.BeginHorizontal();
                GUI.enabled = !rotationEditMode;
                GUI.color = scaleEditMode ? Color.red : new Color(0.8f, 0.8f, 0.8f, 1f);
                if (GUILayout.Button(scaleEditMode ? "終了              " : "スケールを編集"))
                {
                    scaleEditMode = !scaleEditMode;
                    SceneView.RepaintAll();
                }
                GUI.enabled = !scaleEditMode;
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUI.color = rotationEditMode ? Color.red : new Color(0.8f, 0.8f, 0.8f, 1f);
                if (GUILayout.Button(rotationEditMode ? "終了              " : "回転を編集    "))
                {
                    rotationEditMode = !rotationEditMode;
                    SceneView.RepaintAll();
                }
                GUI.enabled = true;
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(3);

            serializedObject.ApplyModifiedProperties();

            if (scaleEditMode || rotationEditMode)
            {
                Tools.current = Tool.None;
            }
            else
            {
                if (Tools.current == Tool.None)
                {
                    Tools.current = t;
                }
                else
                {
                    t = Tools.current;
                }
            }

            SceneView.RepaintAll();

            foreach (var g in serializedObject.targetObjects)
            {
                var s = (g as RBBoxCollider);

                if (s != null)
                {
                    s.SetValidate();
                }
            }
        }

        public void OnSceneGUI()
        {
            RBBoxCollider collider = target as RBBoxCollider;

            if (target != null && collider.isActiveAndEnabled)
            {
                Vector3 pos = collider.transform.position;
                Quaternion rot = collider.transform.rotation;

                Vector3 xSize = new Vector3(collider.Size.x / 2f, 0, 0);
                Vector3 ySize = new Vector3(0, collider.Size.y / 2f, 0);
                Vector3 zSize = new Vector3(0, 0, collider.Size.z / 2f);

                Quaternion colRot = rot * collider.LocalRot;

                Handles.color = Color.blue;
                Handles.matrix = Matrix4x4.TRS(pos + rot * collider.Center, colRot, Vector3.one);

                //Bug (https://issuetracker.unity3d.com/issues/object-with-the-wrong-color-is-drawn-when-using-the-handles-dot-drawwirecube-and-handles-dot-color-functions)
                //Handles.DrawWireCube(Vector3.zero, collider.Size);

                {
                    float lineThickness = HANDLE_SIZE * 1.75f;
                    Handles.DrawLine(xSize + ySize + zSize, -xSize + ySize + zSize, lineThickness);
                    Handles.DrawLine(xSize + ySize + zSize, xSize + ySize - zSize, lineThickness);
                    Handles.DrawLine(-xSize + ySize - zSize, -xSize + ySize + zSize, lineThickness);
                    Handles.DrawLine(-xSize + ySize - zSize, xSize + ySize - zSize, lineThickness);
                    Handles.DrawLine(xSize -ySize + zSize, -xSize - ySize + zSize, lineThickness);
                    Handles.DrawLine(xSize - ySize + zSize, xSize - ySize - zSize, lineThickness);
                    Handles.DrawLine(-xSize - ySize - zSize, -xSize - ySize + zSize, lineThickness);
                    Handles.DrawLine(-xSize - ySize - zSize, xSize - ySize - zSize, lineThickness);
                    Handles.DrawLine(xSize + ySize + zSize, xSize - ySize + zSize, lineThickness);
                    Handles.DrawLine(-xSize + ySize + zSize, -xSize - ySize + zSize, lineThickness);
                    Handles.DrawLine(xSize + ySize - zSize, xSize - ySize - zSize, lineThickness);
                    Handles.DrawLine(-xSize + ySize - zSize, -xSize - ySize - zSize, lineThickness);
                }

                if (scaleEditMode)
                {
                    if (Event.current.type == EventType.MouseDown)
                    {
                        if (HandleUtility.nearestControl == ctrlId_xp)
                        {
                            selected_xp = true;
                        }
                        else if (HandleUtility.nearestControl == ctrlId_xn)
                        {
                            selected_xn = true;
                        }
                        else if (HandleUtility.nearestControl == ctrlId_yp)
                        {
                            selected_yp = true;
                        }
                        else if (HandleUtility.nearestControl == ctrlId_yn)
                        {
                            selected_yn = true;
                        }
                        else if (HandleUtility.nearestControl == ctrlId_zp)
                        {
                            selected_zp = true;
                        }
                        else if (HandleUtility.nearestControl == ctrlId_zn)
                        {
                            selected_zn = true;
                        }
                    }
                    else
                    {

                        ctrlId_xp = GUIUtility.GetControlID(FocusType.Passive);
                        ctrlId_xn = GUIUtility.GetControlID(FocusType.Passive);
                        ctrlId_yp = GUIUtility.GetControlID(FocusType.Passive);
                        ctrlId_yn = GUIUtility.GetControlID(FocusType.Passive);
                        ctrlId_zp = GUIUtility.GetControlID(FocusType.Passive);
                        ctrlId_zn = GUIUtility.GetControlID(FocusType.Passive);
                        Vector3 c = collider.Center;
                        Handles.color = Color.magenta;
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_xp, xSize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_xn, -xSize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_yp, ySize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_yn, -ySize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_zp, zSize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_zn, -zSize, colRot, HANDLE_DOT_SIZE, Event.current.type);
                    }

                    if (Event.current.type == EventType.MouseUp)
                    {
                        selected_xp = false;
                        selected_xn = false;
                        selected_yp = false;
                        selected_yn = false;
                        selected_zp = false;
                        selected_zn = false;
                    }

                    if (selected_xp)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (xSize * 2), pos + rot * collider.Center + colRot * (-xSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newXSize = (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * -xSize)).x;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .x");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * -xSize)) / 2f;
                        if (newXSize < 0)
                        {
                            selected_xp = false;
                            selected_xn = true;
                        }
                        collider.Size = new Vector3(newXSize, size.vector3Value.y, size.vector3Value.z);
                        Repaint();
                    }
                    else if (selected_xn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (xSize * 2), pos + rot * collider.Center + colRot * (-xSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newXSize = -(Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * xSize)).x;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .x");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * xSize)) / 2f;
                        if (newXSize < 0)
                        {
                            selected_xp = true;
                            selected_xn = false;
                        }
                        collider.Size = new Vector3(newXSize, size.vector3Value.y, size.vector3Value.z);
                        Repaint();
                    }
                    else if (selected_yp)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (ySize * 2), pos + rot * collider.Center + colRot * (-ySize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newYSize = (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * -ySize)).y;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .y");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * -ySize)) / 2f;
                        if (newYSize < 0)
                        {
                            selected_yp = false;
                            selected_yn = true;
                        }
                        collider.Size = new Vector3(size.vector3Value.x, newYSize, size.vector3Value.z);
                        Repaint();
                    }
                    else if (selected_yn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (ySize * 2), pos + rot * collider.Center + colRot * (-ySize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newYSize = -(Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * ySize)).y;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .y");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * ySize)) / 2f;
                        if (newYSize < 0)
                        {
                            selected_yp = true;
                            selected_yn = false;
                        }
                        collider.Size = new Vector3(size.vector3Value.x, newYSize, size.vector3Value.z);
                        Repaint();
                    }
                    else if (selected_zp)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (zSize * 2), pos + rot * collider.Center + colRot * (-zSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newZSize = (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * -zSize)).z;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .z");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * -zSize)) / 2f;
                        if (newZSize < 0)
                        {
                            selected_zp = false;
                            selected_zn = true;
                        }
                        collider.Size = new Vector3(size.vector3Value.x, size.vector3Value.y, newZSize);
                        Repaint();
                    }
                    else if (selected_zn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + colRot * (zSize * 2), pos + rot * collider.Center + colRot * (-zSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newZSize = -(Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) - colRot * zSize)).z;
                        Undo.RecordObject(target, "Changed Collider Center/Scale .z");
                        collider.Center += (Quaternion.Inverse(colRot) * (p - (pos + rot * collider.Center) + colRot * zSize)) / 2f;
                        if (newZSize < 0)
                        {
                            selected_zp = true;
                            selected_zn = false;
                        }
                        collider.Size = new Vector3(size.vector3Value.x, size.vector3Value.y, newZSize);
                        Repaint();
                    }
                }
                else if (rotationEditMode)
                {
                    EditorGUI.BeginChangeCheck();
                    Handles.matrix = Matrix4x4.TRS(pos + rot * collider.Center, rot, Vector3.one);
                    Quaternion q = Handles.RotationHandle(collider.LocalRot, Vector3.zero);
                    if (EditorGUI.EndChangeCheck()) 
                    {
                        Undo.RecordObject(target, "Changed Collider LocalRotation");
                        collider.LocalRot = q;
                    }
                }
                else
                {
                    selected_xp = false;
                    selected_xn = false;
                    selected_yp = false;
                    selected_yn = false;
                    selected_zp = false;
                    selected_zn = false;
                }
            }
        }
    }
}