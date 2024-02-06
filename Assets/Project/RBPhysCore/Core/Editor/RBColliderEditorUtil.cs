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
            Vector3 handlePos = Handles.matrix.GetPosition();
            float s = Vector3.Distance(handlePos + pos, SceneView.currentDrawingSceneView.camera.transform.position) * size * 2f;
            Handles.DotHandleCap(controlId, pos, rot, s, eventType);
        }
    }
}