using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using static RBPhys.RBPhysComputer;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbodyVirtual : RBRigidbody
    {
        public RBRigidbodyVirtual()
        {
        }

        protected override void OnEnable() { }
        protected override void OnDisable() { }

        RBVirtualTransform vTransform;

        public bool vActive_And_vEnabled { get { return _vEnabled && (vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        void SetEnableInternal(bool state)
        {
            _vEnabled = state;
            if (state) OnVEnabled();
            else OnVDisabled();
        }

        void OnVEnabled()
        {
            vTransform.physComputer.AddRigidbody(this);

            foreach (var c in _colliders)
            {
                vTransform.physComputer.SwitchToRigidbody(c);
                c.UpdateTransform(0);
            }
        }

        void OnVDisabled()
        {
            vTransform.physComputer.RemoveRigidbody(this);

            foreach (var c in _colliders)
            {
                vTransform.physComputer.SwitchToCollider(c);
                c.UpdateTransform(0);
            }
        }


        public void SetTransform(RBVirtualTransform vTransform)
        {
            this.vTransform = vTransform;
        }

        internal override void ApplyTransform(float dt, TimeScaleMode physTimeScaleMode)
        {
            if (!IgnoreVelocity)
            {
                _expVelocity = Vector3.ClampMagnitude(_expVelocity, rbRigidbody_velocity_max);
                _expAngularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, rbRigidbody_ang_velocity_max);

                if (Mathf.Abs(_expVelocity.x) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.x = 0;
                if (Mathf.Abs(_expVelocity.z) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.z = 0;
                if (Mathf.Abs(_expAngularVelocity.x) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.x = 0;
                if (Mathf.Abs(_expAngularVelocity.y) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.y = 0;
                if (Mathf.Abs(_expAngularVelocity.z) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.z = 0;

                if (physTimeScaleMode == TimeScaleMode.Prograde)
                {
                    float vm = _expVelocity.magnitude;
                    float avm = _expAngularVelocity.magnitude;
                    _velocity = (vm > 0 ? (_expVelocity / vm) : Vector3.zero) * Mathf.Max(0, vm - drag);
                    _angularVelocity = (avm > 0 ? (_expAngularVelocity / avm) : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }
                else if (physTimeScaleMode == TimeScaleMode.Retrograde)
                {
                    float vm = _expVelocity.magnitude;
                    float avm = _expAngularVelocity.magnitude;
                    _velocity = (vm > 0 ? (_expVelocity / vm) : Vector3.zero) * Mathf.Max(0, vm + drag);
                    _angularVelocity = (avm > 0 ? (_expAngularVelocity / avm) : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }

                vTransform.Position = _position + (_velocity * dt);
                vTransform.Rotation = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized) * _rotation;
            }

            UpdateTransform(dt);
            UpdateExpTrajectory(dt);
        }

        internal override void UpdateTransform(float delta, bool updateColliders = true)
        {
            Position = vTransform.Position;
            Rotation = vTransform.Rotation;

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateTransform(delta);
                }
            }
        }
    }
}