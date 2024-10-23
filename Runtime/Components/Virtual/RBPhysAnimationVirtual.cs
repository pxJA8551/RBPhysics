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
    }
}