using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace RBPhys
{
    public class RBBoxColliderVirtual : RBBoxCollider
    {
        protected override void OnEnable() { }
        protected override void OnDisable() { }

        public override bool vActive_And_vEnabled { get { return _vEnabled && (_vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        void SetEnableInternal(bool state)
        {
            _vEnabled = state;
            if (state) OnVEnabled();
            else OnVDisabled();
        }

        protected RBVirtualTransform _vTransform;

        public RBBoxCollider BaseCollider { get { return _baseCollider; } }
        RBBoxCollider _baseCollider;

        public void SetVTransform(RBVirtualTransform vTransform)
        {
            _vTransform = vTransform;
        }

        public void ReInitialize()
        {
            var c = _baseCollider;
            if (c != null)
            {
                CopyCollider(c);
            }
            else
            {
                Destroy(this);
            }
        }

        void FindRigidbody()
        {
            var r = GetComponentsInParent<RBRigidbodyVirtual>(true).FirstOrDefault();
            r?.AddCollider(this);
        }

        public void VInititalize(RBBoxCollider baseCollider)
        {
            Awake();
            FindRigidbody();
            SetEnableInternal(true);

            _baseCollider = baseCollider;
            _baseCollider?.AddVirtualCollider(this);
        }

        private void OnDestroy()
        {
            _baseCollider?.RemoveVirtualCollider(this);
        }

        void OnVEnabled()
        {
            if (!(_vTransform?.Validate() ?? false)) return;

            _vTransform.physComputer.AddCollider(this);

            if (ParentRigidbody != null)
            {
                _vTransform.physComputer.SwitchToRigidbody(this);
            }
        }

        void OnVDisabled()
        {
            if (!(_vTransform?.Validate() ?? false)) return;

            _vTransform.physComputer.RemoveCollider(this);
        }

        public override void UpdateTransform(float delta)
        {
            GameObjectPos = _vTransform?.Position ?? Vector3.zero;
            GameObjectRot = _vTransform?.Rotation ?? Quaternion.identity;

            _expPos = GameObjectPos;
            _expRot = GameObjectRot;

            var vParent = _parent as RBRigidbodyVirtual;
            _hasParentRigidbodyInFrame = vParent?.vActive_And_vEnabled ?? false;

            if (GeometryType == RBGeometryType.Sphere && useCCD) _expTrajectory.Update(this, GameObjectPos, GameObjectRot, delta);
            else _expTrajectory.Update(this, _expPos, _expRot, delta);
        }
    }
}