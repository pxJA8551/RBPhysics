using RBPhys;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RBPhysAnimationSlim : RBVirtualComponent, IRBPhysAnimControllable
{
    public AnimationClip baseAnimationClip;

    [NonSerialized] float _ctrlTime = 0;
    [NonSerialized] float _ctrlSpeed = 0;

    Vector3 _positionLast;
    Vector3 _positionLast2;
    Quaternion _rotationLast;
    Quaternion _rotationLast2;
    bool _pushedLast;
    bool _pushedLast2;

    [SerializeField] AnimationClip _animationClip;
    public AnimationClip AnimationClip { get { return _animationClip; } }

    public bool interp;
    float _lastFixedUpdate;

    public float AnimationLength
    {
        get
        {
            if (_animationClip != null) return _animationClip.length;
            else return 0;
        }
    }

    public RBPhysTRSAnimationCurve trsCurve;
    public float TRSCurveLength
    {
        get
        {
            if (trsCurve != null) return trsCurve.length;
            else return 0;
        }
    }

    [SerializeField] public Transform fixedParent;
    Matrix4x4 _fixedParentTrs;

    public bool isPlaying { get { return _ctrlSpeed != 0; } }

    public Guid ValidatorSrcGuid { get { return _validatorSrcGuid; } }
    Guid _validatorSrcGuid;

    public List<RBCollider> physColliders;
    public bool enablePhysProcedualAnim = true;
    public float lambda_time_factor = .3f;

    Vector3 _animVel;
    Vector3 _animAngVel;
    Vector3 _prevImpulseVel;
    Vector3 _prevImpulseAngVel;

    public void FixedUpdate()
    {
        _lastFixedUpdate = Time.time;
    }

    void LateUpdate()
    {
        if (!interp) return;

        float elapsed = Time.time - _lastFixedUpdate;
        float t = (float)(elapsed / RBPhysController.MainComputer.timeParams.fixedDeltaTime);

        if (_pushedLast && _pushedLast2) 
        {

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCtrlTime(float ctrlTime)
    {
        _ctrlTime = ctrlTime;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCtrlSpeed(float ctrlSpeed)
    {
        bool play = (ctrlSpeed != 0);
        if (isPlaying != play) SetCollidersActive(play);

        _ctrlSpeed = ctrlSpeed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetCollidersActive(bool active)
    {
        foreach (var c in physColliders)
        {
            if (c != null) c.Trajectory.activeStatic = active;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override RBVirtualComponent CreateVirtual(GameObject obj)
    {
        var rba = obj.AddComponent<RBPhysAnimationSlim>();
        return rba;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SyncVirtual(RBVirtualComponent vComponent)
    {
        var rba = vComponent as RBPhysAnimationSlim;
        if (rba == null) throw new Exception();
        CopyPhysAnimation(rba);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ComponentOnEnable()
    {
        SetCtrlSpeed(1);

        PhysComputer.AddPhysObject(BeforeSolver, AfterSolver);
        _validatorSrcGuid = Guid.NewGuid();

        if (fixedParent != null) _fixedParentTrs = fixedParent.localToWorldMatrix;
        else _fixedParentTrs = Matrix4x4.identity;

        foreach (var c in physColliders)
        {
            if (c == null) continue;
            c.onCollision += OnCollision;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ComponentOnDisable()
    {
        PhysComputer.RemovePhysObject(BeforeSolver, AfterSolver);
        _validatorSrcGuid = Guid.NewGuid();

        SetCollidersActive(false);

        foreach (var c in physColliders)
        {
            if (c == null) continue;
            c.onCollision -= OnCollision;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyPhysAnimation(RBPhysAnimationSlim anim)
    {
        _ctrlTime = anim._ctrlTime;
        _ctrlSpeed = anim._ctrlSpeed;

        _animationClip = anim._animationClip;
        trsCurve = anim.trsCurve;

        interp = false;

        _pushedLast = false;
        _pushedLast2 = false;

        fixedParent = anim.fixedParent;

        if (fixedParent != null) _fixedParentTrs = fixedParent.localToWorldMatrix;
        else _fixedParentTrs = Matrix4x4.identity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void BeforeSolver(float dt, TimeScaleMode timeScaleMode)
    {
        if (!VEnabled) return;
        IntergradeAnim(dt, _ctrlSpeed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void AfterSolver(float dt, TimeScaleMode timeScaleMode)
    {
        if (!VEnabled) return;

        float vL = _animVel.magnitude;
        float avL = _animAngVel.magnitude;

        float vFactor = 0;
        if (vL > 0) vFactor = Vector3.Dot(_animVel / vL, _prevImpulseVel) / vL;

        float avFactor = 0;
        if (avL > 0) avFactor = Vector3.Dot(_animAngVel / avL, _prevImpulseAngVel) / avL;

        float extLambda = vFactor + avFactor;

        //Debug.Log((extLambda * lambda_time_factor * dt, extLambda, vFactor, avFactor, _prevImpulseVel, _prevImpulseAngVel, _animVel, _animAngVel));

        if (enablePhysProcedualAnim)
        {
            _ctrlTime += extLambda * lambda_time_factor * dt;
            _ctrlTime = ClampAnimTime(_ctrlTime);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IntergradeAnim(float dt, float ctrlSpeed)
    {
        _ctrlTime += dt * ctrlSpeed;
        _ctrlTime = ClampAnimTime(_ctrlTime);

        _prevImpulseVel = Vector3.zero;
        _prevImpulseAngVel = Vector3.zero;

        SampleAndApplyTRS(_ctrlTime, dt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ClampAnimTime(float time)
    {
        return Mathf.Clamp(time, 0, Mathf.Max(AnimationLength, TRSCurveLength));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SampleAndApplyTRS(float time, float dt)
    {
        PushInterp(VTransform.WsPosition, VTransform.WsRotation);

        trsCurve.SampleDeltaTRSAnimation(time, VTransform.RawPosition, VTransform.RawRotation, out var lsPos, out var lsRot, out var lsPos_d, out var lsRot_d);
        LsToWs(lsPos, lsRot, out Vector3 wsPos, out Quaternion wsRot);
        LsToWs(lsPos_d, lsRot_d, out Vector3 wsPos_d, out Quaternion wsRot_d);

        Delta2Velocity(dt, wsPos_d, wsRot_d, out _animVel, out _animAngVel);

        VTransform.SetWsPositionAndRotation(wsPos, wsRot);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void LsToWs(Vector3 lsPos, Quaternion lsRot, out Vector3 wsPos, out Quaternion wsRot)
    {
        wsPos = _fixedParentTrs.MultiplyPoint3x4(lsPos);
        wsRot = _fixedParentTrs.rotation * lsRot;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Delta2Velocity(float dt, Vector3 deltaPos, Quaternion deltaRot, out Vector3 vel, out Vector3 angVel)
    {
        vel = deltaPos / dt;

        deltaRot.ToAngleAxis(out float angleDeg, out Vector3 axis);
        angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
        angVel = axis * (angleDeg * Mathf.Deg2Rad) / dt;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetAnimSpeed(float speed)
    {
        _ctrlSpeed = speed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void StopAnim()
    {
        _ctrlSpeed = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void PushInterp(Vector3 pos, Quaternion rot)
    {
        _positionLast2 = _positionLast;
        _rotationLast2 = _rotationLast;
        _pushedLast2 = _pushedLast;

        _positionLast = pos;
        _rotationLast = rot;
        _pushedLast = true;
    }

    public void OnCollision(RBCollider col, RBCollisionInfo info)
    {
        Vector3 vImpact = -info.velAddRelative;

        Vector3 r = info.contactPoint - VTransform.WsPosition;
        float rL = r.magnitude;

        _prevImpulseVel += vImpact;
        _prevImpulseAngVel += Vector3.Cross(r / rL, vImpact / rL);
    }

    public float AnimLength { get { return Mathf.Max(AnimLength, TRSCurveLength); } }
    public float AnimSpeed { get { return _ctrlSpeed; } }
    public float AnimTime { get { return _ctrlTime; } }
}