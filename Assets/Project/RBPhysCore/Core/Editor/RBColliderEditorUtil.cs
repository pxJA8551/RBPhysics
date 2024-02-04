using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RBPhys
{
    public static class RBColliderEditorUtil
    {
        public static void DotHandleCapConstSize(int controlId, Vector3 pos, Quaternion rot, float size, EventType eventType)
        {
            float s = Vector3.Distance(pos, SceneView.currentDrawingSceneView.camera.transform.position) * size * 0.15f;
            Handles.DotHandleCap(controlId, pos, rot, s, eventType);
        }
    }
}