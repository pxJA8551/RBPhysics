using RBPhys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RBVirtualTransform : MonoBehaviour
{
    RBPhysComputer _physComputer;

    RBVirtualTransform _parent;
    GameObject _baseObject;

    Matrix4x4 _rawTrs;
    Matrix4x4 _wsTrs;
    Matrix4x4 _wsTrsInv;

    public Matrix4x4 RawTRS { get { return _rawTrs; } set { SetRawTRS(value); } }
    public Matrix4x4 WsTRS { get { return _wsTrs; } set { SetWsTRS(value); } }
    public Matrix4x4 WsTRSInv { get { return _wsTrsInv; } }
    
    public Vector3 WsPosition { get { return _wsTrs.GetPosition(); } }
    public Quaternion WsRotation { get { return _wsTrs.rotation; } }

    public Vector3 RawPosition { get { return _rawTrs.GetPosition(); } }
    public Quaternion RawRotation { get { return _rawTrs.rotation; } }

    int _layer;

    public RBPhysComputer PhysComputer { get { return GetPhysComputer(); } }

    public RBVirtualTransform Parent { get { return _parent; } }

    public GameObject BaseObject { get { return _baseObject; } }
    public Transform BaseTransform { get { return _baseObject?.transform; } }

    public int Layer { get { return _layer; } }

    List<RBVirtualTransform> _children;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RBVirtualTransform CreateVirtual(GameObject baseObject, RBPhysComputer physComputer = null)
    {
        var vTransform = baseObject.GetComponent<RBVirtualTransform>();

        if (vTransform == null)
        {
            vTransform = baseObject.AddComponent<RBVirtualTransform>();

            vTransform._wsTrs = Matrix4x4.identity;
            vTransform._rawTrs = Matrix4x4.identity;
            vTransform._baseObject = baseObject;

            vTransform.CopyBaseObjectTransform();
            vTransform.OnCreate();

            vTransform._physComputer = physComputer ?? RBPhysController.MainComputer;
        }

        return vTransform;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBPhysComputer GetPhysComputer()
    {
        if (_physComputer == null) throw new NotImplementedException();
        return _physComputer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PhysComputerInit()
    {
        _physComputer.AddVirtualTransform(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PhysComputerDetach()
    {
        _physComputer.RemoveVirtualTransform(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void OnCreate()
    {
        var parent = _baseObject.transform.parent?.GetComponentInParent<RBVirtualTransform>(true);
        if (parent != null) parent.FindChildren();

        FindChildren();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void OnRemove()
    {
        foreach (var v in _children)
        {
            v.RemoveParent();
        }

        if (_parent != null) _parent.FindChildren();
    }

    private void OnDisable()
    {
        PhysComputerDetach();
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FindChildren()
    {
        _children = new List<RBVirtualTransform>();
        _children.Clear();
        FindChildrenRecursive(this, transform, ref _children);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void FindChildrenRecursive(RBVirtualTransform org, Transform obj, ref List<RBVirtualTransform> children)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            var t = obj.transform.GetChild(i);
            var vt = t.GetComponent<RBVirtualTransform>();

            if (vt == null)
            {
                FindChildrenRecursive(org, t, ref children);
            }
            else
            {
                vt.SetParent(org);
                children.Add(vt);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBVirtualTransform(GameObject baseObject)
    {
        _baseObject = baseObject;
        CopyBaseObjectTransform();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBVirtualTransform(Transform transform)
    {
        SetRawTRS(transform.localToWorldMatrix);
        _layer = transform.gameObject.layer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RBVirtualTransform(Vector3 pos, Quaternion rot, Vector3 scale, int layer)
    {
        SetRawTRS(Matrix4x4.TRS(pos, rot, scale));
        _layer = layer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyBaseObjectTransform()
    {
        if (_baseObject == null) throw new System.Exception();

        SetWsTRS(_baseObject.transform.localToWorldMatrix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ApplyBaseObjectTransform()
    {
        if (_baseObject == null) throw new System.Exception();

        var wsTrs = _wsTrs;

        _baseObject.transform.position = wsTrs.GetPosition();
        _baseObject.transform.rotation = wsTrs.rotation;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWsTRS(Matrix4x4 wsTrs)
    {
        Debug.Assert(wsTrs.ValidTRS());

        _wsTrs = wsTrs;
        _wsTrsInv = wsTrs.inverse;

        if (_parent == null)
        {
            _rawTrs = wsTrs;
        }
        else
        {
            _rawTrs = _parent._wsTrs.inverse * wsTrs;
        }

        SetChildrenWsTRS();

        Debug.Assert(ValidTRS());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetRawTRS(Matrix4x4 rawTrs)
    {
        Debug.Assert(rawTrs.ValidTRS());

        _rawTrs = rawTrs;

        Matrix4x4 wsTrs;
        if (_parent == null)
        {
            wsTrs = rawTrs;
        }
        else
        {
            wsTrs = _parent._wsTrs * rawTrs;
        }

        _wsTrs = wsTrs;
        _wsTrsInv = wsTrs.inverse;

        SetChildrenWsTRS();

        Debug.Assert(ValidTRS());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetParentWsTRS(Matrix4x4 parentWsTRS)
    {
        _wsTrs = parentWsTRS * _rawTrs;
        SetChildrenWsTRS();
        Debug.Assert(ValidTRS());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWsPosition(Vector3 wsPos)
    {
        var trs = _wsTrs;
        trs.m03 = wsPos.x;
        trs.m13 = wsPos.y;
        trs.m23 = wsPos.z;

        SetWsTRS(trs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWsRotation(Quaternion wsRot)
    {
        var trs = Matrix4x4.Rotate(wsRot * Quaternion.Inverse(_wsTrs.rotation)) * _wsTrs;
        SetWsTRS(trs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWsPositionAndRotation(Vector3 wsPos, Quaternion wsRot)
    {
        var trs = Matrix4x4.Rotate(wsRot * Quaternion.Inverse(_wsTrs.rotation)) * _wsTrs;
        trs.m03 = wsPos.x;
        trs.m13 = wsPos.y;
        trs.m23 = wsPos.z;

        SetWsTRS(trs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetChildrenWsTRS()
    {
        if (_children == null) return;

        foreach (var vt in _children)
        {
            vt.SetParentWsTRS(_wsTrs);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ValidTRS()
    {
        return _rawTrs.ValidTRS() && _wsTrs.ValidTRS();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetParent(RBVirtualTransform parent)
    {
        _parent = parent;
        SetWsTRS(_wsTrs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveParent()
    {
        _parent = null;
        SetWsTRS(_wsTrs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 TransformPoint(Vector3 p)
    {
        return _wsTrs.MultiplyPoint(p);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 InverseTransformPoint(Vector3 p)
    {
        return _wsTrsInv.MultiplyPoint(p);
    }
}