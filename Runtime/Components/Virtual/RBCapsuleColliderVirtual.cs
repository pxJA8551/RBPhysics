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
    public class RBCapsuleColliderVirtual : RBCapsuleCollider
    {
        protected override void OnEnable() { }
        protected override void OnDisable() { }

        public override bool vActive_And_vEnabled { get { return _vEnabled && (_vTransform?.Active ?? false); } }

        public bool vEnabled { get { return _vEnabled; } set { SetEnableInternal(value); } }
        bool _vEnabled;

        protected RBVirtualTransform _vTransform;

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

        void FindRigidbody()
        {
            var r = GetComponentInParent<RBRigidbody>();
            r?.AddCollider(this);
        }

        public void VInititalize()
        {
            Awake();
            FindRigidbody();
            SetEnableInternal(true);
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

        public void SetTransform(RBVirtualTransform vTransform)
        {
            this._vTransform = vTransform;
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