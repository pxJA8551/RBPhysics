using UnityEngine;

namespace RBPhys
{
    public class RBRigidbodyInterp : MonoBehaviour
    {
        public RBRigidbody rbRigidbody;

        double _lastFixedUpdate;

        public bool interpPosition = true;
        public bool interpRotation = true;

        private void FixedUpdate()
        {
            _lastFixedUpdate = Time.timeAsDouble;
        }

        private void OnEnable()
        {
            _lastFixedUpdate = 0;
        }

        private void LateUpdate()
        {
            if (!interpPosition && !interpRotation) return;

            if (rbRigidbody == null || !rbRigidbody.VEnabled || (_lastFixedUpdate == 0) || (Time.fixedDeltaTime <= 0)) return;
            if (!rbRigidbody.interpTraj.PushedLast) return;

            double elapsed = Time.timeAsDouble - _lastFixedUpdate;
            float t = (float)(elapsed / RBPhysController.MainComputer.timeParams.fixedDeltaTime);

            if (interpPosition)
            {
                var wsPos = Vector3.Lerp(rbRigidbody.interpTraj.PositionLast, rbRigidbody.VTransform.WsPosition, t);
                rbRigidbody.transform.position = wsPos;
            }

            if (interpRotation)
            {
                var wsRot = Quaternion.Lerp(rbRigidbody.interpTraj.RotationLast, rbRigidbody.VTransform.WsRotation, t);
                rbRigidbody.transform.rotation = wsRot;
            }
        }
    }
}