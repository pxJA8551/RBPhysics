using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RBPhys
{
    [RequireComponent(typeof(RBRigidbody))]
    public class RBPhysAnimation : MonoBehaviour, RBPhysCore.RBConstraints.IStdSolver, RBPhysCore.RBConstraints.IRBPhysObject
    {
        const int PHYS_ANIM_INTERGRADE = 1;
        const float PHYS_ANIM_RESOLUTION_BETA = 1f;

        public AnimationClip BaseAnimationClip
        {
            get
            {
                return _baseAnimationClip;
            }

            set
            {
                _baseAnimationClip = value;
                animationClip = RecontructAnimationClip(_baseAnimationClip);
            }
        }

        [SerializeField] AnimationClip _baseAnimationClip;
        [HideInInspector] public AnimationClip animationClip;
        [NonSerialized] public float ctrlTime = 0;
        [NonSerialized] public float ctrlSpeed = 0;
        [HideInInspector] public RBPhysAnimationLinker linker;

        TRSAnimationCurve trsCurve = new TRSAnimationCurve();

        [SerializeField] public Transform parentTransform;
        bool _useParentTransform;
        Vector3 _parentTransformPos;
        Quaternion _parentTransformRot;
        Vector3 _parentTransformScale;

        public RBRigidbody rbRigidbody;
        public bool playing;
        public bool enablePhysicallyProcedualAnim = true;

        public bool interp = true;
        public bool velocityInterp = true;
        public float interpMultiplier = 1.0f;

        public float AnimationLength { get { return animationClip?.length ?? -1; } }

        public void Awake()
        {
            PlayAnimation();

            rbRigidbody = GetComponent<RBRigidbody>();

            if (_baseAnimationClip != null)
            {
                animationClip = RecontructAnimationClip(_baseAnimationClip);
            }

            RBPhysCore.AddStdSolver(this);
            RBPhysCore.AddPhysObject(this);
        }

        public AnimationClip RecontructAnimationClip(AnimationClip baseAnimation)
        {
            var anim = Instantiate(baseAnimation);

            trsCurve.length = anim.length;

            EditorCurveBinding[] curves = AnimationUtility.GetCurveBindings(anim);
            List<(EditorCurveBinding, AnimationCurve)> setCurves = new List<(EditorCurveBinding, AnimationCurve)>();

            foreach (var c in curves)
            {
                if (!trsCurve.TrySetCurve(anim, c))
                {
                    setCurves.Add((c, AnimationUtility.GetEditorCurve(anim, c)));
                }
            }

            anim.ClearCurves();
            AnimationUtility.SetEditorCurves(anim, setCurves.Select(item => item.Item1).ToArray(), setCurves.Select(item => item.Item2).ToArray());

            return anim;
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

        public void BeforeSolver()
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

            ctrlTime += Time.fixedDeltaTime * ctrlSpeed;

            if (enablePhysicallyProcedualAnim) 
            {
                _lsBasePos = parentTransform?.InverseTransformPoint(rbRigidbody.Position) ?? rbRigidbody.Position;
                _lsBaseRot = Quaternion.Inverse(parentTransform?.localRotation ?? Quaternion.identity) * rbRigidbody.Rotation;

                SampleApplyTRSAnimation(ctrlTime, Time.fixedDeltaTime, _lsBasePos, _lsBaseRot);
            }

            _solverTime = Time.time;
            LinkAnimationTime();
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

        public void AfterSolver()
        {
            _ctrlTimeDeltaP = (ctrlTime - _ctrlTimeLast) / (Time.fixedDeltaTime * ctrlSpeed);

            if (enablePhysicallyProcedualAnim)
            {
                SampleApplyTRSAnimation(ctrlTime, Time.fixedDeltaTime, _lsBasePos, _lsBaseRot);
            }
        }

        public void StdSolverInit(float dt, bool isPrimaryInit)
        {
            _solverDeltaTime = dt;
        }

        public void StdSolverIteration(int iterationCount)
        {
            if (enablePhysicallyProcedualAnim)
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
            trsCurve.SampleTRSAnimation(time, basePos, baseRot, out Vector3 targetLsPos, out Quaternion targetLsRot);
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
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
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

        void LsToWs(Vector3 lsPos, Quaternion lsRot, Vector3 lsScale, out Vector3 wsPos, out Quaternion wsRot, out Vector3 wsScale)
        {
            if (_useParentTransform)
            {
                wsPos = _parentTransformPos + _parentTransformRot * Vector3.Scale(_parentTransformScale, lsPos);
                wsRot = _parentTransformRot * lsRot;
                wsScale = Vector3.Scale(_parentTransformScale, lsScale);
                return;
            }

            wsPos = lsPos;
            wsRot = lsRot;
            wsScale = lsScale;
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
            _lsBasePos = parentTransform?.InverseTransformPoint(rbRigidbody.Position) ?? rbRigidbody.Position;
            _lsBaseRot = Quaternion.Inverse(parentTransform?.localRotation ?? Quaternion.identity) * rbRigidbody.Rotation;
            Vector3 lsBaseScale = Vector3.Scale(RBPhysUtil.V3Rcp(parentTransform?.lossyScale ?? Vector3.one), _parentTransformRot * transform.lossyScale);

            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, lsBaseScale, out Vector3 lsPos, out Quaternion lsRot, out Vector3 lsScale);
            LsToWs(lsPos, lsRot, lsScale, out Vector3 wsPos, out Quaternion wsRot, out Vector3 wsScale);

            transform.position = wsPos;
            transform.rotation = wsRot;
            transform.localScale = wsScale;
        }

        private void LateUpdate()
        {
            float interpDelta = Time.time - _solverTime;
            float time = interp ? (ctrlTime + (interpDelta * (velocityInterp ? _ctrlTimeDeltaP : 1)) * interpMultiplier) : ctrlTime;

            if (!enablePhysicallyProcedualAnim)
            {
                time = interp ? (ctrlTime + interpDelta * interpMultiplier) : ctrlTime;
            }

            if (animationClip != null)
            {
                animationClip.SampleAnimation(gameObject, time);
            }

            CalcTRSAnimFrame(time);
        }

        public void OnDestroy()
        {
            RBPhysCore.RemoveStdSolver(this);
            RBPhysCore.RemovePhysObject(this);
        }

        public float CalcLinkedAnimLambda(float time, float linkedInvMass, Vector3 linkedInvInertiaTensorWs)
        {
            trsCurve.SampleTRSAnimation(time, _lsBasePos, _lsBaseRot, out Vector3 intgTargetPos, out Quaternion intgTargetRot);
            LsToWs(intgTargetPos, intgTargetRot, out Vector3 intgTargetPosWs, out Quaternion intgTargetRotWs);
            CalcDPosTarget(intgTargetPosWs, intgTargetRotWs, out Vector3 dPos, out Vector3 dRot);

            return (dPos.magnitude / _solverDeltaTime) / linkedInvMass + Vector3.Scale((dRot / _solverDeltaTime), RBPhysUtil.V3Rcp(linkedInvInertiaTensorWs)).magnitude;
        }

        class TRSAnimationCurve
        {
            public AnimationCurve curve_lsPos_x;
            public AnimationCurve curve_lsPos_y;
            public AnimationCurve curve_lsPos_z;
            public AnimationCurve curve_lsRotEuler_x;
            public AnimationCurve curve_lsRotEuler_y;
            public AnimationCurve curve_lsRotEuler_z;
            public AnimationCurve curve_lsScale_x;
            public AnimationCurve curve_lsScale_y;
            public AnimationCurve curve_lsScale_z;
            public float length;

            public bool TrySetCurve(AnimationClip clip, EditorCurveBinding c)
            {
                if (c.type == typeof(Transform) && c.path == "")
                {
                    switch (c.propertyName)
                    {
                        case "m_LocalPosition.x":
                            curve_lsPos_x = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalPosition.y":
                            curve_lsPos_y = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalPosition.z":
                            curve_lsPos_z = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalRotation.x":
                            curve_lsRotEuler_x = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalRotation.y":
                            curve_lsRotEuler_y = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalRotation.z":
                            curve_lsRotEuler_z = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalScale.x":
                            curve_lsScale_x = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalScale.y":
                            curve_lsScale_y = AnimationUtility.GetEditorCurve(clip, c);
                            return true;

                        case "m_LocalScale.z":
                            curve_lsScale_z = AnimationUtility.GetEditorCurve(clip, c);
                            return true;
                    }
                }

                return false;
            }

            public void SampleTRSAnimation(float time, Vector3 pos, Quaternion rot, out Vector3 lsPos, out Quaternion lsRot)
            {
                var rEuler = rot.eulerAngles;

                lsPos = pos;

                lsPos.x = curve_lsPos_x?.Evaluate(time) ?? lsPos.x;
                lsPos.y = curve_lsPos_y?.Evaluate(time) ?? lsPos.y;
                lsPos.z = curve_lsPos_z?.Evaluate(time) ?? lsPos.z;

                rEuler.x = curve_lsRotEuler_x?.Evaluate(time) ?? rEuler.x;
                rEuler.y = curve_lsRotEuler_y?.Evaluate(time) ?? rEuler.y;
                rEuler.z = curve_lsRotEuler_z?.Evaluate(time) ?? rEuler.z;

                lsRot = Quaternion.Euler(rEuler);
            }

            public void SampleTRSAnimation(float time, Vector3 pos, Quaternion rot, Vector3 scale, out Vector3 lsPos, out Quaternion lsRot, out Vector3 lsScale)
            {
                var rEuler = rot.eulerAngles;

                lsPos = pos;
                lsScale = scale;

                lsPos.x = curve_lsPos_x?.Evaluate(time) ?? lsPos.x;
                lsPos.y = curve_lsPos_y?.Evaluate(time) ?? lsPos.y;
                lsPos.z = curve_lsPos_z?.Evaluate(time) ?? lsPos.z;

                rEuler.x = curve_lsRotEuler_x?.Evaluate(time) ?? rEuler.x;
                rEuler.y = curve_lsRotEuler_y?.Evaluate(time) ?? rEuler.y;
                rEuler.z = curve_lsRotEuler_z?.Evaluate(time) ?? rEuler.z;

                lsScale.x = curve_lsScale_x?.Evaluate(time) ?? lsScale.x;
                lsScale.y = curve_lsScale_y?.Evaluate(time) ?? lsScale.y;
                lsScale.z = curve_lsScale_z?.Evaluate(time) ?? lsScale.z;

                lsRot = Quaternion.Euler(rEuler);
            }
        }
    }
}