using RBPhys;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class RBVirtualComponent : MonoBehaviour
{
    public bool VEnabled { get { return _vEnabled; } }
    bool _vEnabled;

    //public abstract RBVirtualComponent CreateVirtual();

    public RBPhysComputer PhysComputer { get { return GetPhysComputer(); } }

    public RBVirtualTransform VTransform { get { return GetVirtualTransform(); } }
    RBVirtualTransform _vTransform;

    void Awake()
    {
        CreateVirtualTransform();
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
    public RBVirtualTransform GetVirtualTransform()
    {
        if (_vTransform == null) CreateVirtualTransform();
        return _vTransform;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBPhysComputer GetPhysComputer()
    {
        if (_vTransform == null) CreateVirtualTransform();
        return _vTransform.PhysComputer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void CreateVirtualTransform()
    {
        _vTransform = RBVirtualTransform.CreateVirtual(gameObject);
        if (_vTransform == null) throw new Exception();
    }

    protected virtual void ComponentAwake() { }
    protected virtual void ComponentOnEnable() { }
    protected virtual void ComponentOnDisable() { }
}