using RBPhys;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

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

        rbRigidbody.CalcVel2Ws(rbRigidbody.Velocity, rbRigidbody.AngularVelocity, d * Time.fixedDeltaTime, out var wsPos, out var wsRot);

        if (interpPosition && interpRotation) rbRigidbody.VTransform.SetTempPositionAndRotation(wsPos, wsRot);
        else if (interpPosition) rbRigidbody.VTransform.SetTempPosition(wsPos);
        else if (interpRotation) rbRigidbody.VTransform.SetTempRotation(wsRot);
    }
}