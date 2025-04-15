using UnityEngine;

namespace RBPhys
{
    public class RBRigidbodyInterp : MonoBehaviour
    {
        public RBRigidbody rbRigidbody;

        float _lastFixedUpdate;

        public bool interpPosition = true;
        public bool interpRotation = true;

        private void FixedUpdate()
        {
            _lastFixedUpdate = Time.time;
        }

        private void OnEnable()
        {
            _lastFixedUpdate = 0;
        }

        private void LateUpdate()
        {
            if (!interpPosition && !interpRotation) return;

            if (rbRigidbody == null || !rbRigidbody.VEnabled || (_lastFixedUpdate == 0) || (Time.fixedDeltaTime <= 0)) return;
            if (!rbRigidbody.interpTraj.PushedLast || !rbRigidbody.interpTraj.PushedLast2) return;

            float elapsed = Time.time - _lastFixedUpdate;
            float t = (float)(elapsed / RBPhysController.MainComputer.timeParams.fixedDeltaTime);

            if (interpPosition)
            {
                var wsPos = Vector3.Lerp(rbRigidbody.interpTraj.PositionLast2, rbRigidbody.interpTraj.PositionLast, t);
                rbRigidbody.transform.position = wsPos;
            }

            if (interpRotation)
            {
                var wsRot = Quaternion.Lerp(rbRigidbody.interpTraj.RotationLast2, rbRigidbody.interpTraj.RotationLast, t);
                rbRigidbody.transform.rotation = wsRot;
            }
        }
    }
}