using RBPhys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysAnimationVirtual : RBPhysAnimation
    {
        public override bool vActive_And_vEnabled { get { return _vEnabled && (_vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        RBVirtualTransform _vTransform;

        public RBPhysAnimation BasePhysAnimation { get { return _basePhysAnimation; } }
        RBPhysAnimation _basePhysAnimation;

        protected override void Awake()
        {
            if (playing)
            {
                PlayAnimation();
            }
        }

        protected override void OnEnable() { }
        protected override void OnDisable() { }
        protected override void LateUpdate() { }

        public void SetVTransform(RBVirtualTransform vTransform)
        {
            _vTransform = vTransform;
        }

        void SetEnableInternal(bool state)
        {
            _vEnabled = state;
            if (state) OnVEnabled();
            else OnVDisabled();
        }

        void OnVEnabled()
        {
            _vTransform.physComputer.AddStdSolver(StdSolverInit, StdSolverIteration);
            _vTransform.physComputer.AddPhysObject(BeforeSolver, AfterSolver);
        }

        void OnVDisabled()
        {
            _vTransform.physComputer.RemoveStdSolver(StdSolverInit, StdSolverIteration);
            _vTransform.physComputer.RemovePhysObject(BeforeSolver, AfterSolver);
        }

        public void ReInitialize()
        {
            var a = _basePhysAnimation;
            if (a != null)
            {
                CopyPhysAnimation(a);
            }
            else
            {
                Destroy(this);
            }
        }

        public void VInititalize(RBPhysAnimation basePhysAnimation, RBRigidbodyVirtual rbRigidbody)
        {
            this.rbRigidbody = rbRigidbody;
            Awake();
            SetEnableInternal(true);

            _basePhysAnimation = basePhysAnimation;
        }

        public override void BeforeSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!_vEnabled) return;

            var parentTransform = _vTransform.parent;
            if (parentTransform != null)
            {
                _useParentTransform = true;
                _parentTransformPos = parentTransform.Position;
                _parentTransformRot = parentTransform.Rotation;
                _parentTransformScale = Vector3.one;
            }
            else
            {
                _useParentTransform = false;
            }

            ctrlTime += dt * ctrlSpeed;
            ctrlTime = Mathf.Clamp(ctrlTime, 0, Mathf.Max(AnimationClip?.length ?? 0, trsCurve?.length ?? 0));

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
        }

        void SetBasePos()
        {
            var parentTransform = _vTransform.parent;
            if (parentTransform != null)
            {
                _lsBasePos = parentTransform.InverseTransformPoint(_vTransform.Position);
                _lsBaseRot = Quaternion.Inverse(parentTransform.Rotation) * _vTransform.Rotation;
            }
            else
            {
                _lsBasePos = _vTransform.Position;
                _lsBaseRot = _vTransform.Rotation;
            }
        }

        public override void StdSolverIteration(int iterationCount, RBPhysComputer.SolverInfo info)
        {
            if (enablePhysProceduralAnimation && trsCurve != null)
            {
                SampleSetTRSAnimation(ctrlTime, _lsBasePos, _lsBaseRot);

                if (_solverDeltaTime > 0)
                {
                    CalcTRSAnimVelocity(_solverDeltaTime, info.solver_iter_max * info.solver_sync_max);
                }
            }
        }

        public override void AfterSolver(float dt, TimeScaleMode timeScaleMode)
        {
            if (!_vEnabled) return;

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

        protected override void AddLinkedAnimationTime(float add) { }
        protected override float LinkAnimationTime() { throw new NotImplementedException(); }
    }
}