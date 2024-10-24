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
        protected override void Awake() { }
        protected override void OnEnable() { }
        protected override void OnDisable() { }

        public override bool vActive_And_vEnabled { get { return _vEnabled && (_vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        RBVirtualTransform _vTransform;

        public RBRigidbody BaseRigidbody { get { return _baseRigidbody; } }
        RBRigidbody _baseRigidbody;

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

        internal override void UpdateExpTrajectoryMultiThreaded(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectoryMultiThreaded(dt, Position, Rotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, _vTransform.layer);
        }

        internal override void UpdateExpTrajectory(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectoryMultiThreaded(dt, Position, Rotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, _vTransform.layer);
        }

        public override void AddCollider(RBCollider c)
        {
            Array.Resize(ref _colliders, _colliders.Length + 1);
            _colliders[_colliders.Length - 1] = c;

            _vTransform.physComputer.SwitchToRigidbody(c);
            c.SetParentRigidbody(this);
            c.UpdateTransform(0);
        }

        public void VInititalize(RBRigidbody baseRigidbody)
        {
            FindColliders();
            UpdateTransform(0);
            if (_colliders.Any()) RecalculateInertiaTensor();

            if (!isSleeping || sleepGrace != 5)
            {
                isSleeping = false;
                sleepGrace = 0;
            }

            if (sleepUntilInteraction)
            {
                PhysSleep();
            }
            else
            {
                PhysAwakeForce();
            }

            _baseRigidbody = baseRigidbody;

            SetEnableInternal(true);

            baseRigidbody?.AddVirtualRigidbody(this);
        }

        private void OnDestroy()
        {
            _baseRigidbody?.AddVirtualRigidbody(this);
        }

        void OnVEnabled()
        {
            _vTransform.physComputer.AddRigidbody(this);

            foreach (var c in _colliders)
            {
                _vTransform.physComputer.SwitchToRigidbody(c);
                c.UpdateTransform(0);
            }
        }

        void OnVDisabled()
        {
            _vTransform.physComputer.RemoveRigidbody(this);

            foreach (var c in _colliders)
            {
                _vTransform.physComputer.SwitchToCollider(c);
                c.UpdateTransform(0);
            }
        }

        public void ReInitialize()
        {
            var rb = _baseRigidbody;
            if (rb != null)
            {
                CopyRigidbody(rb);
            }
            else
            {
                Destroy(this);
            }
        }

        public void SetTransform(RBVirtualTransform vTransform)
        {
            this._vTransform = vTransform;
        }

        internal override void ApplyTransform(float dt, TimeScaleMode physTimeScaleMode)
        {
            if (!IgnoreVelocity)
            {
                float vm = _expVelocity.magnitude;
                float avm = _expAngularVelocity.magnitude;

                if (physTimeScaleMode == TimeScaleMode.Prograde)
                {
                    _expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm - drag);
                    _expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }
                else
                {
                    _expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm + drag * DRAG_RETG_MULTIPLIER);
                    _expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm + angularDrag * ANGULAR_DRAG_RETG_MULTIPLIER);
                }

                _expVelocity = Vector3.ClampMagnitude(_expVelocity, rbRigidbody_velocity_max);
                _expAngularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, rbRigidbody_ang_velocity_max);

                if (Mathf.Abs(_expVelocity.x) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.x = 0;
                if (Mathf.Abs(_expVelocity.z) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.z = 0;
                if (Mathf.Abs(_expAngularVelocity.x) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.x = 0;
                if (Mathf.Abs(_expAngularVelocity.y) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.y = 0;
                if (Mathf.Abs(_expAngularVelocity.z) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.z = 0;

                _velocity = _expVelocity;
                _angularVelocity = _expAngularVelocity;


                _vTransform.Position = _position + (_velocity * dt);
                _vTransform.Rotation = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized) * _rotation;
            }

            UpdateTransform(dt);
            UpdateExpTrajectory(dt);
        }

        internal override void UpdateTransform(float delta, bool updateColliders = true)
        {
            Position = _vTransform.Position;
            Rotation = _vTransform.Rotation;

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