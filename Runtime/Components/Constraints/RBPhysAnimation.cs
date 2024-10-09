using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RBPhys
{
    [RequireComponent(typeof(RBRigidbody))]
    public class RBPhysAnimation : MonoBehaviour, RBPhysComputer.IStdSolver, RBPhysComputer.IRBPhysObject
    {
        const int PHYS_ANIM_INTERGRADE = 5;
        const float PHYS_ANIM_RESOLUTION_BETA = .25f;

        public AnimationClip baseAnimationClip;

        public AnimationClip animationClip;
        public RBPhysTRSAnimationCurve trsCurve;

        [NonSerialized] public float ctrlTime = 0;
        [NonSerialized] public float ctrlSpeed = 0;
        [HideInInspector] public RBPhysAnimationLinker linker;
        [SerializeField] RBPhysAnimationType animationType;

        [SerializeField] public Transform parentTransform;
        bool _useParentTransform;
        Vector3 _parentTransformPos;
        Quaternion _parentTransformRot;
        Vector3 _parentTransformScale;

        public RBRigidbody rbRigidbody;
        public bool playing;
        public bool enablePhysProceduralAnimation = true;

        public bool interp = true;
        public bool velocityInterp = true;
        public float interpMultiplier = 1.0f;

        public float external_force_sens = 1;

        public float AnimationLength { get { return Mathf.Max(animationClip?.length ?? 0, trsCurve?.length ?? 0); } } 

        public void Awake()
        {
            rbRigidbody = GetComponent<RBRigidbody>();

            if (playing)
            {
                PlayAnimation();
            }
        }

        void OnEnable()
        {
            RBPhysController.AddStdSolver(this);
            RBPhysController.AddPhysObject(this);
        }

        void OnDisable()
        {
            RBPhysController.RemoveStdSolver(this);
            RBPhysController.RemovePhysObject(this);
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
        float _solverDeltaTime = 0;

        float _ctrlTimeLast;
        float _ctrlTimeDeltaP;

        Vector3 _targetWsPos;
        Quaternion _targetWsRot;

        Vector3 _lsBasePos;
        Quaternion _lsBaseRot;

        public void BeforeSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (parentTransform != null)
            {
                _useParentTransform = true;
                _parentTransformPos = parentTransform.position;
                _parentTransformRot = parentTransform.rotation;
                _parentTransformScale = parentTransform.lossyScale;
            }
            else
            {
                _useParentTransform = false;
            }

            _ctrlTimeLast = ctrlTime;

            ctrlTime += dt * ctrlSpeed;
            ctrlTime = Mathf.Clamp(ctrlTime, 0, Mathf.Max(animationClip?.length ?? 0, trsCurve?.length ?? 0));

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
            if (parentTransform != null)
            {
                _lsBasePos = parentTransform.InverseTransformPoint(transform.position);
                _lsBaseRot = Quaternion.Inverse(parentTransform.rotation) * transform.rotation;
            }
            else
            {
                _lsBasePos = transform.position;
                _lsBaseRot = transform.rotation;
            }
        }

        void AddLinkedAnimationTime(float add)
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

        float LinkAnimationTime()
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

        public void AfterSolver(float dt, TimeScaleMode timeScaleMode)
        {
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
                rbRigidbody.Position = _targetWsPos;
                rbRigidbody.Rotation = _targetWsRot;
            }
        }

        public void StdSolverInit(float dt, bool isPrimaryInit)
        {
            _solverDeltaTime = dt;
        }

        public void StdSolverIteration(int iterationCount)
        {
            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);

                if (_solverDeltaTime > 0)
                {
                    CalcTRSAnimVelocity(_solverDeltaTime);
                    LinkAnimationTime();
                }
            }
        }

        void SampleApplyTRSAnimation(float time, float deltaTime, Vector3 basePos, Quaternion baseRot)
        {
            SampleSetTRSAnimation(time, basePos, baseRot);
            CalcTRSAnimBaseVelocity(deltaTime);
        }

        void SampleSetTRSAnimation(float time, Vector3 basePos, Quaternion baseRot)
        {
            trsCurve.SampleTRSAnimation(time, basePos, baseRot, animationType, out Vector3 targetLsPos, out Quaternion targetLsRot);
            LsToWs(targetLsPos, targetLsRot, out _targetWsPos, out _targetWsRot);
        }

        void CalcTRSAnimBaseVelocity(float solverDelta)
        {
            Vector3 vel = (_targetWsPos - rbRigidbody.Position) / solverDelta;

            (_targetWsRot * Quaternion.Inverse(rbRigidbody.Rotation)).ToAngleAxis(out float angleDeg, out Vector3 axis);
            angleDeg = angleDeg > 180 ? angleDeg - 360 : angleDeg;
            Vector3 angVel = axis * (angleDeg * Mathf.Deg2Rad) / solverDelta;

            rbRigidbody.ExpVelocity = vel;
            rbRigidbody.ExpAngularVelocity = angVel;
        }

        void CalcTRSAnimVelocity(float solverDelta)
        {
            float intergradeTime = ctrlTime;

            PhysAnimIntergrade(ref intergradeTime, solverDelta);
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

        void PhysAnimIntergrade(ref float intergradeTime, float solverDelta)
        {
            float delta = solverDelta * (1f / PHYS_ANIM_INTERGRADE);

            float invMass = CalcLinkedInvMass();
            Vector3 invInertiaTensor = CalcLinkedInvInertiaTensorWs();

            for (int i = 0; i < PHYS_ANIM_INTERGRADE; i++)
            {
                CalcTRSAnimAdditionalVelocity(_solverDeltaTime, out Vector3 velResist, out Vector3 angVelResist);

                if (velResist.sqrMagnitude > 0 || angVelResist.sqrMagnitude > 0)
                {
                    velResist *= -1;
                    angVelResist *= -1;

                    float f_v = velResist.magnitude / rbRigidbody.InverseMass;
                    float f_r = Vector3.Scale(angVelResist, RBPhysUtil.V3Rcp(rbRigidbody.InverseInertiaWs)).magnitude;

                    float d_a = -1;
                    float d_b = -1;

                    float dc_a = 0;
                    float dc_b = 0;

                    if (0 < intergradeTime)
                    {
                        float t = Mathf.Clamp(intergradeTime - delta, 0, trsCurve.length);
                        float lambda_anim = CalcAnimLambda(t, invMass, invInertiaTensor, out Vector3 dPos, out Vector3 dRot);

                        Vector3 evalVDirN = dPos.normalized;
                        Vector3 evalAVDirN = dRot.normalized;

                        float lambda = Vector3.Dot(velResist, evalVDirN) / rbRigidbody.InverseMass + Vector3.Scale(Vector3.Dot(angVelResist.normalized, evalAVDirN) * evalAVDirN, RBPhysUtil.V3Rcp(rbRigidbody.InverseInertiaWs)).magnitude;

                        float ddv = Vector3.Dot(evalVDirN, velResist.normalized);
                        float ddav = Vector3.Dot(evalAVDirN, angVelResist.normalized);

                        d_a = (f_v * ddv + f_r * ddav) / (f_v + f_r);

                        dc_a = lambda / lambda_anim;
                        dc_a = 1 + (dc_a - 1) * external_force_sens;
                    }

                    if (intergradeTime < trsCurve.length)
                    {
                        float t = Mathf.Clamp(intergradeTime + delta, 0, trsCurve.length);
                        float lambda_anim = CalcAnimLambda(t, invMass, invInertiaTensor, out Vector3 dPos, out Vector3 dRot);

                        Vector3 evalVDirN = dPos.normalized;
                        Vector3 evalAVDirN = dRot.normalized;

                        float lambda = Vector3.Dot(velResist, evalVDirN) / rbRigidbody.InverseMass + Vector3.Scale(Vector3.Dot(angVelResist.normalized, evalAVDirN) * evalAVDirN, RBPhysUtil.V3Rcp(rbRigidbody.InverseInertiaWs)).magnitude;

                        float ddv = Vector3.Dot(evalVDirN, velResist);
                        float ddav = Vector3.Dot(evalAVDirN, angVelResist);

                        d_b = (f_v * ddv + f_r * ddav) / (f_v + f_r);

                        dc_b = lambda / lambda_anim;
                        dc_b = 1 + (dc_b - 1) * external_force_sens;
                    }

                    if (d_a > d_b)
                    {
                        if (d_a > 0 && !float.IsInfinity(dc_a))
                        {
                            AddLinkedAnimationTime(-Mathf.Clamp01(dc_a) * delta * PHYS_ANIM_RESOLUTION_BETA);
                            intergradeTime = ctrlTime;
                        }
                    }
                    else
                    {
                        if (d_b > 0 && !float.IsInfinity(dc_b))
                        {
                            AddLinkedAnimationTime(Mathf.Clamp01(dc_b) * delta * PHYS_ANIM_RESOLUTION_BETA);
                            intergradeTime = ctrlTime;
                        }
                    }
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
                wsPos = _parentTransformPos + _parentTransformRot * Vector3.Scale(_parentTransformScale, lsPos);
                wsRot = _parentTransformRot * lsRot;
                return;
            }

            wsPos = lsPos;
            wsRot = lsRot;
        }

        void CalcDPos(Vector3 pos, Quaternion rot, out Vector3 dPos, out Vector3 dRot)
        {
            dPos = pos - rbRigidbody.Position;

            (rot * Quaternion.Inverse(rbRigidbody.Rotation)).ToAngleAxis(out float angleDeg, out Vector3 axis);
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
            Vector3 lsBaseScale;

            if (parentTransform != null)
            {
                lsBaseScale = transform.localScale;
            }
            else
            {
                lsBaseScale = transform.localScale;
            }

            if (trsCurve != null)
            {
                trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, lsBaseScale, animationType, out Vector3 lsPos, out Quaternion lsRot, out Vector3 lsScale);
                LsToWs(lsPos, lsRot, out Vector3 wsPos, out Quaternion wsRot);

                transform.position = wsPos;
                transform.rotation = wsRot;
                transform.localScale = lsScale;
            }
        }

        private void LateUpdate()
        {
            float interpDelta = _solverTime != 0 ? (Time.time - _solverTime) : 0;
            float time = interp ? (ctrlTime + (interpDelta * (velocityInterp ? _ctrlTimeDeltaP : 1)) * interpMultiplier) : ctrlTime;

            if (!enablePhysProceduralAnimation)
            {
                time = interp ? (ctrlTime + interpDelta * interpMultiplier) : ctrlTime;
            }

            if (animationClip != null && trsCurve != null)
            {
                animationClip.SampleAnimation(gameObject, time);
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