using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
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

        [SerializeField] protected AnimationClip _animationClip;
        protected float _animationLength;

        public RBPhysTRSAnimationCurve trsCurve;

        [NonSerialized] public float ctrlTime = 0;
        [NonSerialized] public float ctrlSpeed = 0;
        [HideInInspector] public RBPhysAnimationLinker linker;
        [SerializeField] RBPhysAnimationType animationType;

        [SerializeField] public RBVirtualTransform parentVTransform;
        protected bool _useParentTransform;

        public RBRigidbody rbRigidbody;
        public bool playing;
        public bool enablePhysProceduralAnimation = true;

        public bool interp = true;
        public bool velocityInterp = true;
        public float interpMultiplier = 1.0f;

        public float ext_lambda_compensation = 1;

        public virtual bool vActive_And_vEnabled { get { return enabled && gameObject.activeSelf; } }
        
        protected virtual void Awake()
        {
            rbRigidbody = GetComponent<RBRigidbody>();

            _animationLength = 0;
            if (_animationClip != null) _animationLength = _animationClip.length;

            if (playing)
            {
                PlayAnimation();
            }
        }

        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rba = obj.AddComponent<RBPhysAnimation>();
            rba.CopyPhysAnimation(this);
            return rba;
        }

        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rba = vComponent as RBPhysAnimation;
            if (rba == null) throw new Exception();
            CopyPhysAnimation(rba);
        }

        public void CopyPhysAnimation(RBPhysAnimation anim)
        {
            throw new NotImplementedException();

            baseAnimationClip = anim.baseAnimationClip;

            _animationClip = anim.AnimationClip;

            _animationLength = 0;
            if (_animationClip != null) _animationLength = _animationClip.length;

            trsCurve = anim.trsCurve;

            ctrlTime = anim.ctrlTime;
            ctrlSpeed = anim.ctrlSpeed;
            linker = anim.linker;
            animationType = anim.animationType;

            _useParentTransform = anim._useParentTransform;

            rbRigidbody = anim.rbRigidbody;
            playing = anim.playing;
            enablePhysProceduralAnimation = anim.enablePhysProceduralAnimation;

            interp = anim.interp;
            velocityInterp = anim.velocityInterp;
            interpMultiplier = anim.interpMultiplier;

            ext_lambda_compensation = anim.ext_lambda_compensation;

            _targetWsPos = anim._targetWsPos;
            _targetWsRot = anim._targetWsRot;

            _lsBasePos = anim._lsBasePos;
            _lsBaseRot = anim._lsBaseRot;
        }

        protected virtual void OnEnable()
        {
            PhysComputer.AddStdSolver(StdSolverInit, StdSolverIteration);
            PhysComputer.AddPhysObject(BeforeSolver, AfterSolver);
        }

        protected virtual void OnDisable()
        {
            PhysComputer.RemoveStdSolver(StdSolverInit, StdSolverIteration);
            PhysComputer.RemovePhysObject(BeforeSolver, AfterSolver);
        }

        public void PlayAnimation()
        {
            ctrlSpeed = 1;
        }

        public void StopAnimation()
        {
            ctrlSpeed = 0;
        }

        public void AttachLinker(RBPhysAnimationLinker linker)
        {
            this.linker = linker;
        }

        public void DetachLinker()
        {
            this.linker = null;
        }

        float _solverTime = 0;
        protected float _solverDeltaTime = 0;

        float _ctrlTimeLast;
        float _ctrlTimeDeltaP;

        protected Vector3 _targetWsPos;
        protected Quaternion _targetWsRot;

        protected Vector3 _lsBasePos;
        protected Quaternion _lsBaseRot;

        public virtual void BeforeSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!enabled) return;

            if (parentVTransform != null)
            {
                _useParentTransform = true;
            }
            else
            {
                _useParentTransform = false;
            }

            _ctrlTimeLast = ctrlTime;

            ctrlTime += dt * ctrlSpeed;
            ctrlTime = Mathf.Clamp(ctrlTime, 0, Mathf.Max(AnimationLength, trsCurve?.length ?? 0));

            if (trsCurve != null)
            {
                if (enablePhysProceduralAnimation)
                {
                    SetBasePos();
                    SampleApplyTRSAnimation(ctrlTime, dt, _lsBasePos, _lsBaseRot);
                }
                else
                {
                    SetBasePos();
                    SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);
                }
            }

            _solverTime = Time.time;
            LinkAnimationTime();
        }

        void SetBasePos()
        {
            if (parentVTransform != null)
            {
                _lsBasePos = parentVTransform.InverseTransformPoint(VTransform.WsPosition);
                _lsBaseRot = Quaternion.Inverse(parentVTransform.WsRotation) * VTransform.WsRotation;
            }
            else
            {
                _lsBasePos = VTransform.WsPosition;
                _lsBaseRot = VTransform.WsRotation;
            }
        }

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

        protected virtual float LinkAnimationTime()
        {
            linker?.LinkCtrlTime();
            return ctrlTime;
        }

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

        public float GetInvMass()
        {
            return rbRigidbody.InverseMass;
        }

        public Vector3 GetInvInertiaTensorWs()
        {
            return (rbRigidbody.InverseInertiaWs);
        }

        public virtual void AfterSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!enabled) return;

            if (ctrlSpeed == 0)
            {
                _ctrlTimeDeltaP = 0;
            }
            else
            {
                _ctrlTimeDeltaP = Mathf.Clamp01((ctrlTime - _ctrlTimeLast) / (dt * ctrlSpeed));
            }

            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleApplyTRSAnimation(ctrlTime, dt, _lsBasePos, _lsBaseRot);
            }
            else
            {
                rbRigidbody.VTransform.SetWsPositionAndRotation(_targetWsPos, _targetWsRot);
            }
        }

        public void StdSolverInit(float dt, RBPhysComputer.SolverInfo info)
        {
            _solverDeltaTime = dt;
        }

        public virtual void StdSolverIteration(int iterationCount, RBPhysComputer.SolverInfo info)
        {
            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);

                if (_solverDeltaTime > 0)
                {
                    CalcTRSAnimVelocity(_solverDeltaTime, info.solver_iter_max * info.solver_sync_max);
                    LinkAnimationTime();
                }
            }
        }

        protected void SampleApplyTRSAnimation(float time, float deltaTime, Vector3 basePos, Quaternion baseRot)
        {
            SampleSetTRSAnimation(time, basePos, baseRot);
            CalcTRSAnimBaseVelocity(deltaTime);
        }

        protected void SampleSetTRSAnimation(float time, Vector3 basePos, Quaternion baseRot)
        {
            trsCurve.SampleTRSAnimation(time, basePos, baseRot, animationType, out Vector3 targetLsPos, out Quaternion targetLsRot);
            LsToWs(targetLsPos, targetLsRot, out _targetWsPos, out _targetWsRot);
        }

        void CalcTRSAnimBaseVelocity(float solverDelta)
        {
            Vector3 vel = (_targetWsPos - rbRigidbody.VTransform.WsPosition) / solverDelta;

            (_targetWsRot * Quaternion.Inverse(rbRigidbody.VTransform.WsRotation)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            Vector3 angVel = axis * (angleDeg * Mathf.Deg2Rad) / solverDelta;

            rbRigidbody.ExpVelocity = vel;
            rbRigidbody.ExpAngularVelocity = angVel;
        }

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

                    float dc = lambda / lambdaAnim;
                    dc = float.IsNaN(dc) || float.IsInfinity(dc) ? 0 : dc;

                    AddLinkedAnimationTime(Mathf.Clamp(dc + dc, -1, 1) * delta * PHYS_ANIM_RESOLUTION_BETA);
                    intergradeTime = ctrlTime;
                }
            }
        }

        float CalcAnimLambda(float time, float invMass, Vector3 invInertiaTensor, out Vector3 dPos, out Vector3 dRot)
        {
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, animationType, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
            LsToWs(intgTargetPos, intgTargetRot, out Vector3 intgTargetPosWs, out Quaternion intgTargetRotWs);
            CalcDPosTarget(intgTargetPosWs, intgTargetRotWs, out dPos, out dRot);

            if (linker == null)
            {
                return (dPos.magnitude / _solverDeltaTime) / invMass + Vector3.Scale((dRot / _solverDeltaTime), RBPhysUtil.V3Rcp(invInertiaTensor)).magnitude;
            }

            return linker.CalcLinkedAnimLambda(time);
        }

        void LsToWs(Vector3 lsPos, Quaternion lsRot, out Vector3 wsPos, out Quaternion wsRot)
        {
            if (_useParentTransform)
            {
                wsPos = parentVTransform.TransformPoint(lsPos);
                wsRot = parentVTransform.WsRotation * lsRot;
                return;
            }

            wsPos = lsPos;
            wsRot = lsRot;
        }

        void CalcDPos(Vector3 pos, Quaternion rot, out Vector3 dPos, out Vector3 dRot)
        {

            dPos = pos - rbRigidbody.VTransform.WsPosition;

            (rot * Quaternion.Inverse(rbRigidbody.VTransform.WsRotation)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            dRot = axis * (angleDeg * Mathf.Deg2Rad);
        }

        void CalcDPosTarget(Vector3 pos, Quaternion rot, out Vector3 dPos, out Vector3 dRot)
        {
            dPos = pos - _targetWsPos;

            (rot * Quaternion.Inverse(_targetWsRot)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            dRot = axis * (angleDeg * Mathf.Deg2Rad);
        }

        void CalcTRSAnimFrame(float time)
        {
            SetBasePos();
            Vector3 lsBaseScale = VTransform.WsLossyScale;

            if (trsCurve != null)
            {
                trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, lsBaseScale, animationType, out Vector3 lsPos, out Quaternion lsRot, out Vector3 lsScale);
                LsToWs(lsPos, lsRot, out Vector3 wsPos, out Quaternion wsRot);

                VTransform.SetWsPositionAndRotation(wsPos, wsRot);
            }
        }

        protected virtual void LateUpdate()
        {
            float interpDelta = _solverTime != 0 ? (Time.time - _solverTime) : 0;
            float time = interp ? (ctrlTime + (interpDelta * (velocityInterp ? _ctrlTimeDeltaP : 1)) * interpMultiplier) : ctrlTime;

            if (!enablePhysProceduralAnimation)
            {
                time = interp ? (ctrlTime + interpDelta * interpMultiplier) : ctrlTime;
            }

            if (AnimationClip != null && trsCurve != null)
            {
                AnimationClip.SampleAnimation(gameObject, time);
            }

            CalcTRSAnimFrame(time);
        }

        public void OnDestroy()
        {
        }

        public float CalcLinkedAnimLambda(float time, float linkedInvMass, Vector3 linkedInvInertiaTensorWs)
        {
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, animationType, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
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