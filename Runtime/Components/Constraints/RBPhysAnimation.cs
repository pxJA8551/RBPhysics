using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    [RequireComponent(typeof(RBRigidbody))]
    public class RBPhysAnimation : RBVirtualComponent
    {
        const int PHYS_ANIM_INTERGRADE = 2;
        const float PHYS_ANIM_RESOLUTION_BETA = .25f;

        public AnimationClip baseAnimationClip;

        public AnimationClip AnimationClip { get { return _animationClip; } }
        public float AnimationLength { get { return _animationLength; } }

        [SerializeField] AnimationClip _animationClip;
        float _animationLength;

        public RBPhysTRSAnimationCurve trsCurve;

        [NonSerialized] public float ctrlTime = 0;
        [NonSerialized] public float ctrlSpeed = 0;
        [HideInInspector] public RBPhysAnimationLinker linker;
        [SerializeField] RBPhysAnimationType animationType;

        [SerializeField] public Transform parentTransform;
        Matrix4x4 _parentOffset;

        public RBRigidbody rbRigidbody;
        public bool playing;
        public bool enablePhysProceduralAnimation = true;

        public float ext_lambda_compensation = 1;

        public bool destabilizeSpd = false;
        public float destabilizeSpdSlop = .15f;

        public Guid ValidatorSrcGuid { get { return _validatorSrcGuid; } }
        Guid _validatorSrcGuid;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentAwake()
        {
            SetParentTransformOffset();

            _animationLength = 0;
            if (_animationClip != null) _animationLength = _animationClip.length;

            if (playing)
            {
                PlayAnimation();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetParentTransformOffset()
        {
            if (parentTransform != null)
            {
                _parentOffset = parentTransform.localToWorldMatrix;
            }
            else
            {
                _parentOffset = Matrix4x4.identity;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float CalcAnimSpd(float ctrlSpd)
        {
            if (VTransform.IsPredictionVTransform)
            {
                return ctrlSpd;
            }
            else
            {
                //if (destabilizeSpd) return (ctrlSpd * UnityEngine.Random.Range(1 - destabilizeSpdSlop, 1 + destabilizeSpdSlop));
                return ctrlSpd;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rba = obj.AddComponent<RBPhysAnimation>();
            return rba;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rba = vComponent as RBPhysAnimation;
            if (rba == null) throw new Exception();
            CopyPhysAnimation(rba);
            SetParentTransformOffset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyPhysAnimation(RBPhysAnimation anim)
        {
            baseAnimationClip = anim.baseAnimationClip;

            _animationClip = anim.AnimationClip;

            _animationLength = 0;
            if (_animationClip != null) _animationLength = _animationClip.length;

            trsCurve = anim.trsCurve;

            ctrlTime = anim.ctrlTime;
            ctrlSpeed = anim.ctrlSpeed;
            linker = anim.linker;
            animationType = anim.animationType;

            rbRigidbody = (anim.rbRigidbody?.FindOrCreateVirtualComponent(PhysComputer) as RBRigidbody) ?? null;

            playing = anim.playing;
            enablePhysProceduralAnimation = anim.enablePhysProceduralAnimation;

            ext_lambda_compensation = anim.ext_lambda_compensation;

            _targetWsPos = anim._targetWsPos;
            _targetWsRot = anim._targetWsRot;

            _lsBasePos = anim._lsBasePos;
            _lsBaseRot = anim._lsBaseRot;

            parentTransform = anim.parentTransform;

            destabilizeSpdSlop = anim.destabilizeSpdSlop;
            destabilizeSpd = anim.destabilizeSpd;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentOnEnable()
        {
            PhysComputer.AddStdSolver(StdSolverInit, StdSolverIteration);
            PhysComputer.AddPhysObject(BeforeSolver, AfterSolver);

            _validatorSrcGuid = Guid.NewGuid();
            if (rbRigidbody != null) rbRigidbody.ExpObjectTrajectory.forceSleeping = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentOnDisable()
        {
            PhysComputer.RemoveStdSolver(StdSolverInit, StdSolverIteration);
            PhysComputer.RemovePhysObject(BeforeSolver, AfterSolver);

            if (rbRigidbody != null) rbRigidbody.ExpObjectTrajectory.forceSleeping = false;
            rbRigidbody = null;
            _validatorSrcGuid = Guid.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForceSetCtrlTime(float ctrlTime)
        {
            this.ctrlTime = ClampAnimTime(ctrlTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ClampAnimTime(float time)
        {
            return Mathf.Clamp(time, 0, Mathf.Max(AnimationLength, trsCurve?.length ?? 0));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PlayAnimation()
        {
            ctrlSpeed = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StopAnimation()
        {
            ctrlSpeed = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AttachLinker(RBPhysAnimationLinker linker)
        {
            this.linker = linker;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DetachLinker()
        {
            this.linker = null;
        }

        protected float _solverDeltaTime = 0;

        protected Vector3 _targetWsPos;
        protected Quaternion _targetWsRot;

        protected Vector3 _lsBasePos;
        protected Quaternion _lsBaseRot;

        public virtual void BeforeSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!VEnabled) return;

            SetAnim();

            ctrlTime += dt * CalcAnimSpd(ctrlSpeed);
            ctrlTime = ClampAnimTime(ctrlTime);

            if (trsCurve != null)
            {
                if (enablePhysProceduralAnimation)
                {
                    SetBasePos();
                    SampleApplyTRSAnimation(ctrlTime, dt, _lsBasePos, _lsBaseRot, true);
                }
                else
                {
                    SetBasePos();
                    SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);
                }
            }

            LinkAnimationTime();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetAnim()
        {
            if (animationType == RBPhysAnimationType.Loop) SetAnimSpeed_Loop();
            else if (animationType == RBPhysAnimationType.Ping_Pong) SetAnimSpeed_PingPong();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetAnimSpeed_Loop()
        {
            const float EPSILON = .01f;

            if (ctrlSpeed > 0 && trsCurve.length - EPSILON < ctrlTime)
            {
                ctrlTime = 0;
            }
            else if (ctrlSpeed < 0 && ctrlTime < EPSILON)
            {
                ctrlTime = trsCurve.length;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetAnimSpeed_PingPong()
        {
            const float EPSILON = .01f;

            if (ctrlSpeed > 0 && trsCurve.length - EPSILON < ctrlTime)
            {
                ctrlSpeed = -ctrlSpeed;
            }
            else if (ctrlSpeed < 0 && ctrlTime < EPSILON)
            {
                ctrlSpeed = -ctrlSpeed;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetBasePos()
        {
            var parentOffsetInv = _parentOffset.inverse;
            _lsBasePos = parentOffsetInv.MultiplyPoint3x4(VTransform.WsPosition);
            _lsBaseRot = parentOffsetInv.rotation * VTransform.WsRotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void AddLinkedAnimationTime(float add)
        {
            if (linker != null)
            {
                linker.AddLinkedAnimationTime(add);
            }
            else
            {
                ctrlTime += add;
            }

            LinkAnimationTime();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual float LinkAnimationTime()
        {
            linker?.LinkCtrlTime();
            return ctrlTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float CalcLinkedInvMass()
        {
            if (linker != null)
            {
                return Mathf.Lerp(linker.CalcLinkedInvMass(), rbRigidbody.InverseMass, linker.linkIsolation);
            }
            else
            {
                return rbRigidbody.InverseMass;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 CalcLinkedInvInertiaTensorWs()
        {
            if (linker != null)
            {
                return Vector3.Lerp(linker.CalcLinkedInvInertiaTensorWs(), rbRigidbody.InverseInertiaWs, linker.linkIsolation);
            }
            else
            {
                return rbRigidbody.InverseInertiaWs;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetInvMass()
        {
            return rbRigidbody.InverseMass;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetInvInertiaTensorWs()
        {
            return (rbRigidbody.InverseInertiaWs);
        }

        public virtual void AfterSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!VEnabled) return;

            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleApplyTRSAnimation(ctrlTime, dt, _lsBasePos, _lsBaseRot, false);
            }
            else
            {
                rbRigidbody.ExpVelocity = Vector3.zero;
                rbRigidbody.ExpAngularVelocity = Vector3.zero;
                rbRigidbody.VTransform.SetWsPositionAndRotation(_targetWsPos, _targetWsRot);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StdSolverInit(float dt, RBPhysComputer.SolverInfo info)
        {
            _solverDeltaTime = dt;
        }

        public void StdSolverIteration(RBPhysComputer.SolverInfo info)
        {
            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);

                if (_solverDeltaTime > 0)
                {
                    CalcTRSAnimVelocity(_solverDeltaTime, info.solver_sync_max);
                    LinkAnimationTime();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SampleApplyTRSAnimation(float time, float deltaTime, Vector3 basePos, Quaternion baseRot, bool createTrajValidator)
        {
            SampleSetTRSAnimation(time, basePos, baseRot);
            CalcTRSAnimBaseVelocity(deltaTime, createTrajValidator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SampleSetTRSAnimation(float time, Vector3 basePos, Quaternion baseRot)
        {
            trsCurve.SampleTRSAnimation(time, basePos, baseRot, out Vector3 targetLsPos, out Quaternion targetLsRot);
            LsToWs(targetLsPos, targetLsRot, out _targetWsPos, out _targetWsRot);
        }

        void CalcTRSAnimBaseVelocity(float solverDelta, bool createTrajValidator)
        {
            Vector3 objWs2CgWs = rbRigidbody.CenterOfGravityWorld - rbRigidbody.VTransform.WsPosition;

            Quaternion rot = (_targetWsRot * Quaternion.Inverse(rbRigidbody.VTransform.WsRotation));
            Vector3 vd = (_targetWsPos - rbRigidbody.VTransform.WsPosition) + (rot * objWs2CgWs - objWs2CgWs);

            Vector3 vel = vd / solverDelta;

            rot.ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            Vector3 angVel = axis * (angleDeg * Mathf.Deg2Rad) / solverDelta;

            rbRigidbody.ExpVelocity = vel;
            rbRigidbody.ExpAngularVelocity = angVel;
            if (createTrajValidator) rbRigidbody.AddTrajectoryAlternateValidator(new RBPhysAnimationTrajAltnValidator(this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CalcTRSAnimVelocity(float solverDelta, int solverIters)
        {
            float intergradeTime = ctrlTime;

            PhysAnimIntergrade(ref intergradeTime, solverDelta, solverIters);
        }

        void CalcTRSAnimAdditionalVelocity(float solverDelta, out Vector3 velAdd, out Vector3 angVelAdd)
        {
            Vector3 vel = (_targetWsPos - rbRigidbody.CalcExpPos(solverDelta)) / solverDelta;

            SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);

            (_targetWsRot * Quaternion.Inverse(rbRigidbody.CalcExpRot(solverDelta))).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            Vector3 angVel = axis * (angleDeg * Mathf.Deg2Rad) / solverDelta;

            velAdd = vel;
            angVelAdd = angVel;
        }

        void PhysAnimIntergrade(ref float intergradeTime, float solverDelta, int solverIters)
        {
            float delta = solverDelta * (1f / PHYS_ANIM_INTERGRADE) / solverIters;

            float invMass = CalcLinkedInvMass();
            Vector3 invInertiaTensor = CalcLinkedInvInertiaTensorWs();

            for (int i = 0; i < PHYS_ANIM_INTERGRADE; i++)
            {
                CalcTRSAnimAdditionalVelocity(_solverDeltaTime, out Vector3 velResist, out Vector3 angVelResist);

                if (velResist.sqrMagnitude > 0 || angVelResist.sqrMagnitude > 0)
                {
                    velResist *= -1;
                    angVelResist *= -1;

                    float lambda = 0;
                    float lambdaAnim = 0;

                    if (0 < intergradeTime)
                    {
                        float t = Mathf.Clamp(intergradeTime - delta, 0, trsCurve.length);
                        lambdaAnim += CalcAnimLambda(t, invMass, invInertiaTensor, out Vector3 dPos, out Vector3 dRot);

                        Vector3 evalVDirN = dPos.normalized;
                        Vector3 evalAVDirN = dRot.normalized;

                        lambda -= Vector3.Dot(velResist, evalVDirN) / rbRigidbody.InverseMass + Vector3.Scale(Vector3.Dot(angVelResist, evalAVDirN) * evalAVDirN, RBPhysUtil.V3Rcp(rbRigidbody.InverseInertiaWs)).magnitude;
                    }

                    if (intergradeTime < trsCurve.length)
                    {
                        float t = Mathf.Clamp(intergradeTime + delta, 0, trsCurve.length);
                        lambdaAnim += CalcAnimLambda(t, invMass, invInertiaTensor, out Vector3 dPos, out Vector3 dRot);

                        Vector3 evalVDirN = dPos.normalized;
                        Vector3 evalAVDirN = dRot.normalized;

                        lambda += Vector3.Dot(velResist, evalVDirN) / rbRigidbody.InverseMass + Vector3.Scale(Vector3.Dot(angVelResist, evalAVDirN) * evalAVDirN, RBPhysUtil.V3Rcp(rbRigidbody.InverseInertiaWs)).magnitude;
                    }

                    float dc = (RBPhysUtil.F32Sign11(lambda) * Mathf.Max(0, Mathf.Abs(lambda) - ext_lambda_compensation)) / lambdaAnim;
                    dc = float.IsNaN(dc) || float.IsInfinity(dc) ? 0 : dc;

                    AddLinkedAnimationTime(dc * delta * PHYS_ANIM_RESOLUTION_BETA);
                    intergradeTime = ctrlTime;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float CalcAnimLambda(float time, float invMass, Vector3 invInertiaTensor, out Vector3 dPos, out Vector3 dRot)
        {
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
            LsToWs(intgTargetPos, intgTargetRot, out Vector3 intgTargetPosWs, out Quaternion intgTargetRotWs);
            CalcDPosTarget(intgTargetPosWs, intgTargetRotWs, out dPos, out dRot);

            if (linker == null)
            {
                return (dPos.magnitude / _solverDeltaTime) / invMass + Vector3.Scale((dRot / _solverDeltaTime), RBPhysUtil.V3Rcp(invInertiaTensor)).magnitude;
            }

            return linker.CalcLinkedAnimLambda(time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void LsToWs(Vector3 lsPos, Quaternion lsRot, out Vector3 wsPos, out Quaternion wsRot)
        {
            wsPos = _parentOffset.MultiplyPoint3x4(lsPos);
            wsRot = _parentOffset.rotation * lsRot;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CalcDPos(Vector3 pos, Quaternion rot, out Vector3 dPos, out Vector3 dRot)
        {
            dPos = pos - rbRigidbody.VTransform.WsPosition;

            (rot * Quaternion.Inverse(rbRigidbody.VTransform.WsRotation)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            dRot = axis * (angleDeg * Mathf.Deg2Rad);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CalcDPosTarget(Vector3 pos, Quaternion rot, out Vector3 dPos, out Vector3 dRot)
        {
            dPos = pos - _targetWsPos;

            (rot * Quaternion.Inverse(_targetWsRot)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            dRot = axis * (angleDeg * Mathf.Deg2Rad);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CalcTRSAnimFrame(float time)
        {
            SetBasePos();

            if (trsCurve != null)
            {
                trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, out Vector3 lsPos, out Quaternion lsRot);
                LsToWs(lsPos, lsRot, out Vector3 wsPos, out Quaternion wsRot);

                VTransform.SetWsPositionAndRotation(wsPos, wsRot);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float CalcLinkedAnimLambda(float time, float linkedInvMass, Vector3 linkedInvInertiaTensorWs)
        {
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
            LsToWs(intgTargetPos, intgTargetRot, out Vector3 intgTargetPosWs, out Quaternion intgTargetRotWs);
            CalcDPosTarget(intgTargetPosWs, intgTargetRotWs, out Vector3 dPos, out Vector3 dRot);

            return (dPos.magnitude / _solverDeltaTime) / linkedInvMass + Vector3.Scale((dRot / _solverDeltaTime), RBPhysUtil.V3Rcp(linkedInvInertiaTensorWs)).magnitude;
        }
    }

    public enum RBPhysAnimationType
    {
        Once,
        Loop,
        Ping_Pong
    }
}