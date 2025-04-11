using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public abstract class RBVirtualComponent : MonoBehaviour
    {
        public bool VEnabled { get { return _vEnabled; } }
        bool _vEnabled;

        protected abstract RBVirtualComponent CreateVirtual(GameObject obj);
        protected abstract void SyncVirtual(RBVirtualComponent vComponent);

        public RBPhysComputer PhysComputer { get { return GetPhysComputer(); } }

        public RBVirtualTransform VTransform { get { return GetVirtualTransform(); } }
        RBVirtualTransform _vTransform;

        public RBVirtualComponent BaseVComponent { get { return _baseVComponent; } }
        RBVirtualComponent _baseVComponent;

        public int DerivedChildCount { get { return _derivedChildren.Count; } }
        List<RBVirtualComponent> _derivedChildren = new List<RBVirtualComponent>();

        void Awake()
        {
            FindOrCreateVirtualTransform();
            ComponentAwake();
        }

        void OnEnable()
        {
            _vEnabled = true;
            ComponentOnEnable();

            var physComp = GetPhysComputer();
            if (physComp != null) physComp.AddVirtualComponent(this);

            _vTransform.AddVComponent(this);
        }

        void OnDisable()
        {
            _vEnabled = false;
            ComponentOnDisable();

            var physComp = GetPhysComputer();
            if (physComp != null) physComp.RemoveVirtualComponent(this);

            if (_baseVComponent != null) _baseVComponent.RemoveChild(this);
            _vTransform.RemoveVComponent(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVirtualTransform(RBVirtualTransform vTransform)
        {
            if (vTransform == null) throw new NotImplementedException();

            OnDisable();

            _vTransform = vTransform;
            OnEnable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualTransform GetVirtualTransform()
        {
            if (_vTransform == null) throw new NotImplementedException();
            return _vTransform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysComputer GetPhysComputer()
        {
            if (_vTransform == null) throw new NotImplementedException();
            return _vTransform.PhysComputer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FindOrCreateVirtualTransform()
        {
            _vTransform = RBVirtualTransform.FindOrCreate(gameObject);
            _vTransform.AddVComponent(this);

            if (_vTransform == null) throw new Exception();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualComponent FindOrCreateVirtualComponent(RBPhysComputer physComputer)
        {
            if (physComputer == null) throw new NotImplementedException();

            var vtBase = _vTransform;
            if (vtBase == null) vtBase = RBVirtualTransform.FindOrCreate(gameObject);

            var vt = RBVirtualTransform.FindOrCreate(gameObject, physComputer, vtBase);

            var vc = FindVirtualComponent(physComputer);
            if (vc != null) return vc;

            vc = CreateVirtual(gameObject);

            if (vc._vTransform == null) vc.FindOrCreateVirtualTransform();
            if (vc._vTransform == null) throw new NotImplementedException();

            vc._baseVComponent = this;
            vc.SetVirtualTransform(vt);

            _derivedChildren.Add(vc);

            vc.SyncVirtual(this);
            return vc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualComponent FindVirtualComponent(RBPhysComputer physComputer)
        {
            foreach (var v in _derivedChildren)
            {
                if (v.IdentBase(physComputer, this, true))
                {
                    return v;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RemoveChild(RBVirtualComponent child)
        {
            _derivedChildren.Remove(child);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SyncVirtualComponent()
        {
            Debug.Assert(_baseVComponent != null);
            if (_baseVComponent == null) return;

            SyncVirtual(_baseVComponent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualComponent GetChild(int index)
        {
            if (index < 0 || _derivedChildren.Count <= index) throw new IndexOutOfRangeException();
            return _derivedChildren[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Ident(RBPhysComputer physComputer, bool allowDisabled = false)
        {
            if (!VEnabled && !allowDisabled) return false;
            if (PhysComputer != physComputer) return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IdentBase(RBPhysComputer physComputer, RBVirtualComponent baseVComponent, bool allowDisabled = false)
        {
            if (!Ident(physComputer, allowDisabled)) return false;
            if (_baseVComponent != baseVComponent) return false;
            return true;
        }

        protected virtual void ComponentAwake() { }
        protected virtual void ComponentOnEnable() { }
        protected virtual void ComponentOnDisable() { }
    }
}