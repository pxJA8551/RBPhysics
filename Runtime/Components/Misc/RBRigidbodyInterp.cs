using RBPhys;
using System;
using System.Collections;
using System.Collections.Generic;
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
            if (rbRigidbody == null || !rbRigidbody.VEnabled || (_lastFixedUpdate == 0) || (Time.fixedDeltaTime <= 0)) return;
            if (!interpPosition && !interpRotation) return;

            double elapsed = Time.timeAsDouble - _lastFixedUpdate;
            float d = (float)(elapsed / Time.fixedDeltaTime);

            var vel = rbRigidbody.Velocity;
            var angVel = rbRigidbody.AngularVelocity;

            rbRigidbody.CalcVel2Ws(vel, angVel, d * Time.fixedDeltaTime, out var wsPos, out var wsRot);

            if (interpPosition) rbRigidbody.transform.position = wsPos;
            if (interpRotation) rbRigidbody.transform.rotation = wsRot;
        }
    }
}