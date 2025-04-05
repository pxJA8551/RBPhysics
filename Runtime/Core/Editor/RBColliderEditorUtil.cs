using UnityEditor;
using UnityEngine;

namespace RBPhysEditor
{
    public static class RBColliderEditorUtil
    {
        public static void DotHandleCapConstSize(int controlId, Vector3 pos, Quaternion rot, float size, EventType eventType)
        {
            Vector3 handlePos = Handles.matrix.GetPosition();
            float s = Vector3.Distance(handlePos + pos, SceneView.currentDrawingSceneView.camera.transform.position) * size * 2f;
            var c = Handles.color;
            Handles.color = Color.white;
            Handles.DotHandleCap(controlId, pos, rot, s * 1.5f, eventType);
            Handles.color = c;
            Handles.DotHandleCap(controlId, pos, rot, s, eventType);
        }
    }
}