using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using RBPhys;
using UnityEngine.Assertions.Must;

namespace RBPhysEditor
{
    [CustomEditor(typeof(RBCapsuleCollider))]
    [CanEditMultipleObjects]
    public class RBCapsuleColliderEditor : Editor
    {
        SerializedProperty center;
        SerializedProperty height;
        SerializedProperty rotationEuler;
        SerializedProperty radius;

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
            height = serializedObject.FindProperty("_height");
            radius = serializedObject.FindProperty("_radius");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(center);
            EditorGUILayout.PropertyField(rotationEuler);
            EditorGUILayout.PropertyField(height);
            EditorGUILayout.PropertyField(radius);

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
        }

        public void OnSceneGUI()
        {
            RBCapsuleCollider collider = target as RBCapsuleCollider;

            if (target != null && collider.isActiveAndEnabled)
            {
                Vector3 pos = collider.transform.position;
                Quaternion rot = collider.transform.rotation;
                Quaternion colRot = rot * collider.LocalRot;

                Vector3 xSize = new Vector3(collider.Radius, 0, 0);
                Vector3 ySize = new Vector3(0, collider.Height / 2f + collider.Radius, 0);
                Vector3 yHeightSize = new Vector3(0, collider.Height / 2f, 0);
                Vector3 zSize = new Vector3(0, 0, collider.Radius);

                //Bug (https://issuetracker.unity3d.com/issues/object-with-the-wrong-color-is-drawn-when-using-the-handles-dot-drawwirecube-and-handles-dot-color-functions)
                Handles.color = Color.cyan;

                Handles.matrix = Matrix4x4.TRS(pos + rot * collider.Center, colRot, Vector3.one);
                Handles.DrawWireDisc(Vector3.zero, ySize.normalized, collider.Radius);
                Handles.DrawLine(yHeightSize + xSize, -yHeightSize + xSize);
                Handles.DrawLine(yHeightSize - xSize, -yHeightSize - xSize);
                Handles.DrawLine(yHeightSize + zSize, -yHeightSize + zSize);
                Handles.DrawLine(yHeightSize - zSize, -yHeightSize - zSize);
                Handles.DrawWireDisc(yHeightSize, yHeightSize.normalized, collider.Radius);
                Handles.DrawWireDisc(-yHeightSize, yHeightSize.normalized, collider.Radius);
                Handles.DrawWireArc(yHeightSize, xSize.normalized, -zSize.normalized, 180, collider.Radius);
                Handles.DrawWireArc(yHeightSize, zSize.normalized, xSize.normalized, 180, collider.Radius);
                Handles.DrawWireArc(-yHeightSize, xSize.normalized, zSize.normalized, 180, collider.Radius);
                Handles.DrawWireArc(-yHeightSize, zSize.normalized, -xSize.normalized, 180, collider.Radius);

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
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_xp, xSize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_xn, -xSize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_yp, ySize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_yn, -ySize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_zp, zSize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
                        RBColliderEditorUtil.DotHandleCapConstSize(ctrlId_zn, -zSize, Quaternion.identity, HANDLE_DOT_SIZE, Event.current.type);
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
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (xSize * 2), pos + rot * collider.Center + rot * (-xSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newXSize = (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * -xSize)).x;
                        Undo.RecordObject(target, "Changed Collider Center/Radius");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * -xSize)) / 2f;
                        if (newXSize < 0)
                        {
                            selected_xp = false;
                            selected_xn = true;
                        }
                        collider.Radius = newXSize / 2f;
                        Repaint();
                    }
                    else if (selected_xn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (xSize * 2), pos + rot * collider.Center + rot * (-xSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newXSize = -(Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * xSize)).x;
                        Undo.RecordObject(target, "Changed Collider Center/Radius");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * xSize)) / 2f;
                        if (newXSize < 0)
                        {
                            selected_xp = true;
                            selected_xn = false;
                        }
                        collider.Radius = newXSize / 2f;
                        Repaint();
                    }
                    else if (selected_yp)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (ySize * 2), pos + rot * collider.Center + rot * (-ySize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newYSize = (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * -ySize)).y;
                        Undo.RecordObject(target, "Changed Collider Center/Height");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * -ySize)) / 2f;
                        if (newYSize < 0)
                        {
                            selected_yp = false;
                            selected_yn = true;
                        }
                        collider.Height = newYSize - collider.Radius * 2;
                        Repaint();
                    }
                    else if (selected_yn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (ySize * 2), pos + rot * collider.Center + rot * (-ySize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newYSize = -(Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * ySize)).y;
                        Undo.RecordObject(target, "Changed Collider Center/Height");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * ySize)) / 2f;
                        if (newYSize < 0)
                        {
                            selected_yp = true;
                            selected_yn = false;
                        }
                        collider.Height = newYSize - collider.Radius * 2;
                        Repaint();
                    }
                    else if (selected_zp)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (zSize * 2), pos + rot * collider.Center + rot * (-zSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newzSize = (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * -zSize)).z;
                        Undo.RecordObject(target, "Changed Collider Center/Radius");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * -zSize)) / 2f;
                        if (newzSize < 0)
                        {
                            selected_zp = false;
                            selected_zn = true;
                        }
                        collider.Radius = newzSize / 2f;
                        Repaint();
                    }
                    else if (selected_zn)
                    {
                        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        Vector3 p = RBVectorUtil.CalcNearestLine(pos + rot * collider.Center + rot * (zSize * 2), pos + rot * collider.Center + rot * (-zSize * 2), r.origin, r.origin + r.direction * 10000f);
                        float newzSize = -(Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) - rot * zSize)).z;
                        Undo.RecordObject(target, "Changed Collider Center/Radius");
                        collider.Center += (Quaternion.Inverse(rot) * (p - (pos + rot * collider.Center) + rot * zSize)) / 2f;
                        if (newzSize < 0)
                        {
                            selected_zp = true;
                            selected_zn = false;
                        }
                        collider.Radius = newzSize / 2f;
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