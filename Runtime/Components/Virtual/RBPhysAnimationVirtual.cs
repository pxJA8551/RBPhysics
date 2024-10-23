using RBPhys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    [RequireComponent(typeof(RBRigidbodyVirtual))]
    internal class RBPhysAnimationVirtual : RBPhysAnimation
    {
        public override bool vActive_And_vEnabled { get { return _vEnabled && (_vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        RBVirtualTransform _vTransform;

        protected override void Awake()
        {
            rbRigidbody = GetComponent<RBRigidbodyVirtual>();

            if (playing)
            {
                PlayAnimation();
            }
        }

        protected override void OnEnable() { }
        protected override void OnDisable() { }

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
    }
}