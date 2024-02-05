using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RBPhys;

namespace RBPhysEditor
{
    public static class RBColliderEditorUtil
    {
        public static void DotHandleCapConstSize(int controlId, Vector3 pos, Quaternion rot, float size, EventType eventType)
        {
            float s = Vector3.Distance(pos, SceneView.currentDrawingSceneView.camera.transform.position) * size * 0.8f;
            Handles.DotHandleCap(controlId, pos, rot, s, eventType);
        }
    }
}