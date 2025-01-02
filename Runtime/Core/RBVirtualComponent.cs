using RBPhys;
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

        public int ChildCount { get { return _children.Count; } }
        List<RBVirtualComponent> _children = new List<RBVirtualComponent>();

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
        }

        void OnDisable()
        {
            _vEnabled = false;
            ComponentOnDisable();

            var physComp = GetPhysComputer();
            if (physComp != null) physComp.RemoveVirtualComponent(this);

            _baseVComponent?.RemoveChild(this);
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
            if (_vTransform == null) throw new Exception();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualComponent FindOrCreateVirtualComponent(RBVirtualTransform vTransform)
        {
            if (vTransform == null) throw new NotImplementedException();

            var vc = FindVirutalComponent(vTransform);
            if (vc != null) return vc;

            vc = CreateVirtual(gameObject);
            vc._baseVComponent = this;
            vc.SetVirtualTransform(vTransform);

            _children.Add(this);

            return vc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualComponent FindVirutalComponent(RBVirtualTransform vTransform)
        {
            if(vTransform == null) throw new NotImplementedException();

            foreach (var v in gameObject.GetComponents<RBVirtualComponent>())
            {
                if (v.Ident(vTransform.PhysComputer)) 
                {
                    return v;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RemoveChild(RBVirtualComponent child)
        {
            _children.Remove(child);
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
            if (index < 0 || _children.Count <= index) throw new IndexOutOfRangeException();
            return _children[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Ident(RBPhysComputer physComp)
        {
            if (!VEnabled) return false;
            if (PhysComputer != physComp) return false;
            return true;
        }

        protected virtual void ComponentAwake() { }
        protected virtual void ComponentOnEnable() { }
        protected virtual void ComponentOnDisable() { }
    }
}