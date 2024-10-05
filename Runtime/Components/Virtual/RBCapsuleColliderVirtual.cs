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
        public RBCapsuleColliderVirtual()
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable() 
        {
        }

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
            if (!(vTransform?.Validate() ?? false)) return;

            vTransform.physComputer.AddCollider(this);

            if (ParentRigidbody != null)
            {
                vTransform.physComputer.SwitchToRigidbody(this);
            }
        }

        void OnVDisabled()
        {
            if (!(vTransform?.Validate() ?? false)) return;

            vTransform.physComputer.RemoveCollider(this);
        }

        RBVirtualTransform vTransform;

        public void SetTransform(RBVirtualTransform vTransform)
        {
            this.vTransform = vTransform;
        }

        public override void UpdateTransform(float delta)
        {
            GameObjectPos = vTransform?.Position ?? Vector3.zero;
            GameObjectRot = vTransform?.Rotation ?? Quaternion.identity;

            _expPos = GameObjectPos;
            _expRot = GameObjectRot;

            var vParent = _parent as RBRigidbodyVirtual;
            _hasParentRigidbodyInFrame = vParent?.vActive_And_vEnabled ?? false;

            if (GeometryType == RBGeometryType.Sphere && useCCD) _expTrajectory.Update(this, GameObjectPos, GameObjectRot, delta);
            else _expTrajectory.Update(this, _expPos, _expRot, delta);
        }
    }
}