using JetBrains.Annotations;
using RBPhys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class RBVirtualTransform : MonoBehaviour
{
    RBPhysComputer _physComputer;

    RBVirtualTransform _parent;
    GameObject _baseObject;

    public Matrix4x4 TempOffsetTrs { get { return _tempOffsetTrs; } }
    Matrix4x4 _tempOffsetTrs;

    Matrix4x4 _rawTrs;
    Matrix4x4 _wsTrs;
    Matrix4x4 _wsTrsInv;

    public Matrix4x4 RawTRS { get { return _rawTrs; } set { SetRawTRS(value); } }
    public Matrix4x4 WsTRS { get { return _wsTrs; } set { SetWsTRS(value); } }
    public Matrix4x4 WsTRSInv { get { return _wsTrsInv; } }
    
    public Vector3 WsPosition { get { return _wsTrs.GetPosition(); } }
    public Quaternion WsRotation { get { return _wsTrs.rotation; } }
    public Vector3 WsLossyScale { get { return _wsTrs.lossyScale; } }

    public Vector3 RawPosition { get { return _rawTrs.GetPosition(); } }
    public Quaternion RawRotation { get { return _rawTrs.rotation; } }
    public Vector3 RawLossyScale { get { return _rawTrs.lossyScale; } }

    int _layer;

    public RBPhysComputer PhysComputer { get { return GetPhysComputer(); } }

    public RBVirtualTransform Parent { get { return _parent; } }

    public GameObject BaseObject { get { return _baseObject; } }
    public Transform BaseTransform { get { return _baseObject?.transform; } }

    public bool IsPredictionVTransform { get { return GetPhysComputer().isPredictionComputer; } }

    public int Layer { get { return _layer; } }

    List<RBVirtualTransform> _children;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RBVirtualTransform FindOrCreate(GameObject baseObject, RBPhysComputer physComputer = null)
    {
        var vTransforms = baseObject.GetComponents<RBVirtualTransform>();

        var comp = physComputer ?? RBPhysController.MainComputer;

        var vt = vTransforms.FirstOrDefault(item => IsDuplicatingVTransform(item, baseObject.transform, comp));
        if (vt != null) return vt;

        return Create(baseObject, comp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static RBVirtualTransform Create(GameObject baseObject, RBPhysComputer physComputer)
    {
        var vTransform = baseObject.AddComponent<RBVirtualTransform>();
        vTransform.Init(baseObject, physComputer);

        Debug.Assert(vTransform._physComputer != null);
        vTransform.PhysComputerInit();

        return vTransform;
    }

    private void Init(GameObject baseObject, RBPhysComputer physComputer)
    {
        _baseObject = baseObject;
        _physComputer = physComputer;
        _wsTrs = Matrix4x4.identity;
        _rawTrs = Matrix4x4.identity;
        _tempOffsetTrs = Matrix4x4.identity;

        CopyBaseObjectTransform();
        OnCreate();
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

    private void OnDestroy()
    {
        PhysComputerDetach();
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

        if (_parent == null)
        {
            var wsTrs = _tempOffsetTrs.inverse * _baseObject.transform.localToWorldMatrix;
            SetWsTRS(wsTrs);
        }
        else
        {
            var rawTrs = _parent.BaseTransform.localToWorldMatrix.inverse * (_tempOffsetTrs.inverse * _baseObject.transform.localToWorldMatrix);

            SetRawTRS(rawTrs);
        }

        _layer = _baseObject.layer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ApplyBaseObjectTransform()
    {
        if (_baseObject == null) throw new System.Exception();

        if (_parent == null)
        {
            var wsTrs = _wsTrs;

            _baseObject.transform.position = wsTrs.GetPosition();
            _baseObject.transform.rotation = wsTrs.rotation;
        }
        else
        {
            var wsTrs = _parent.BaseObject.transform.localToWorldMatrix * _rawTrs;

            _baseObject.transform.position = wsTrs.GetPosition();
            _baseObject.transform.rotation = wsTrs.rotation;
        }

        _tempOffsetTrs = Matrix4x4.identity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTempPosition(Vector3 tempPos)
    {
        var local2WorldTrs = Matrix4x4.TRS(tempPos, _wsTrs.rotation, Vector3.one);

        Debug.Assert(local2WorldTrs.ValidTRS());
        if (local2WorldTrs.ValidTRS()) _tempOffsetTrs = local2WorldTrs * Matrix4x4.TRS(_wsTrs.GetPosition(), _wsTrs.rotation, Vector3.one).inverse;

        _baseObject.transform.position = tempPos;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTempRotation(Quaternion tempRot)
    {
        var local2WorldTrs = Matrix4x4.TRS(_wsTrs.GetPosition(), tempRot, Vector3.one);

        Debug.Assert(local2WorldTrs.ValidTRS());
        if (local2WorldTrs.ValidTRS()) _tempOffsetTrs = local2WorldTrs * Matrix4x4.TRS(_wsTrs.GetPosition(), _wsTrs.rotation, Vector3.one).inverse;

        _baseObject.transform.rotation = tempRot;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTempPositionAndRotation(Vector3 tempPos, Quaternion tempRot)
    {
        var local2WorldTrs = Matrix4x4.TRS(tempPos, tempRot, Vector3.one);

        Debug.Assert(local2WorldTrs.ValidTRS());
        if (local2WorldTrs.ValidTRS()) _tempOffsetTrs = local2WorldTrs * Matrix4x4.TRS(_wsTrs.GetPosition(), _wsTrs.rotation, Vector3.one).inverse;

        _baseObject.transform.position = tempPos;
        _baseObject.transform.rotation = tempRot;
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
        var wsTrs = parentWsTRS * _rawTrs;
        _wsTrs = wsTrs;
        _wsTrsInv = wsTrs.inverse;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDuplicatingVTransform(RBVirtualTransform vt0, RBVirtualTransform vt1)
    {
        if (vt0?.BaseTransform == null) throw new NotImplementedException();
        if (vt1?.BaseTransform == null) throw new NotImplementedException();

        if (vt0.BaseTransform != vt1.BaseTransform) return false;
        if (vt0.PhysComputer != vt1.PhysComputer) return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDuplicatingVTransform(RBVirtualTransform vt, Transform baseTransform, RBPhysComputer physComputer)
    {
        if (vt?.BaseTransform == null) throw new NotImplementedException();
        if (baseTransform == null) throw new NotImplementedException();

        if (vt.BaseTransform != baseTransform) return false;
        if (vt.PhysComputer != physComputer) return false;

        return true;
    }
}