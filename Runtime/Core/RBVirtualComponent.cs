using RBPhys;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class RBVirtualComponent : MonoBehaviour
{
    public bool VEnabled { get { return _vEnabled; } }
    bool _vEnabled;

    protected abstract RBVirtualComponent CreateVirtualInternal();

    public RBPhysComputer PhysComputer { get { return GetPhysComputer(); } }

    public RBVirtualTransform VTransform { get { return GetVirtualTransform(); } }
    RBVirtualTransform _vTransform;

    void Awake()
    {
        FindOrCreateVirtualTransform();
        ComponentAwake();
    }

    void OnEnable()
    {
        _vEnabled = true;
        ComponentOnEnable();
    }

    void OnDisable()
    {
        _vEnabled = false;
        ComponentOnDisable();
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
    void FindOrCreateVirtualTransform()
    {
        _vTransform = RBVirtualTransform.FindOrCreate(gameObject);
        if (_vTransform == null) throw new Exception();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBVirtualComponent CreateVirtual(RBVirtualTransform vTransform)
    {
        if (vTransform == null) throw new NotImplementedException();

        var vc = CreateVirtualInternal();
        vc.SetVirtualTransform(vTransform);
        return vc;
    }

    protected virtual void ComponentAwake() { }
    protected virtual void ComponentOnEnable() { }
    protected virtual void ComponentOnDisable() { }
}