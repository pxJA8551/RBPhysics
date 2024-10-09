using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace RBPhys
{
    public class RBCapsuleCollider : RBCollider
    {
        const RBGeometryType GEOMETRY_TYPE = RBGeometryType.Capsule;

        [SerializeField] Vector3 _center = Vector3.zero;
        [SerializeField] Vector3 _rotationEuler = Vector3.zero;
        [SerializeField] float _radius = 0.5f;
        [SerializeField] float _height = 1f;

        public override RBGeometryType GeometryType { get { return GEOMETRY_TYPE; } }

        public Vector3 Center { get { return _center; } set { _center = value; } }
        public Quaternion LocalRot { get { return Quaternion.Euler(_rotationEuler); } set { _rotationEuler = value.eulerAngles; } }
        public float Radius { get { return _radius; } set { _radius = Mathf.Abs(value); } }
        public float Height { get { return _height; } set { _height = Mathf.Abs(value); } }

        public float MutipliedRadius { get { return _radius * colliderSizeMultiplier; } }
        public float MutipliedHeight { get { return _height * colliderSizeMultiplier; } }
        public Vector3 MutipliedCenter { get { return _center * colliderSizeMultiplier; } }

        public override int Layer { get { return gameObject?.layer ?? 0; } }

        public RBCapsuleColliderVirtual CreateVirtual(RBVirtualTransform vTransform)
        {
            var v = vTransform.AddCollider<RBCapsuleColliderVirtual>();
            AddVirtualCollider(v);
            v.CopyCollider(this);
            v.SetVTransform(vTransform);
            v.VInititalize(this);

            return v;
        }

        public void CopyCollider(RBCapsuleCollider c)
        {
            useCCD = c.useCCD;
            allowSoftClip = c.allowSoftClip;
            _center = c._center;
            _rotationEuler = c._rotationEuler;
            _radius = c._radius;
            _height = c._height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            float r = MutipliedRadius;
            float vSphere = (4f * Mathf.PI * r * r * r) / 3f;
            float vCylinder = (Mathf.PI * r * r) * MutipliedHeight;

            return vSphere + vCylinder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcUnscaledVolume()
        {
            float r = _radius;
            float vSphere = (4f * Mathf.PI * r * r * r) / 3f;
            float vCylinder = (Mathf.PI * r * r) * _height;

            return vSphere + vCylinder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, float delta)
        {
            return new RBColliderSphere(pos + MutipliedCenter, MutipliedHeight / 2f + MutipliedRadius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, float delta)
        {
            var p = GetEdge(pos, rot);

            Vector3 r = Vector3.one * MutipliedRadius;

            Vector3 min = Vector3.Min(p.begin, p.end) - r;
            Vector3 max = Vector3.Max(p.begin, p.end) + r;

            return new RBColliderAABB((min + max) / 2f, max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, float delta)
        {
            Vector3 extents = new Vector3(MutipliedRadius * 2, MutipliedRadius, MutipliedRadius * 2);
            return new RBColliderOBB(pos + MutipliedCenter - extents, rot * LocalRot, extents * 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return pos + MutipliedCenter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (Vector3 begin, Vector3 end) GetEdge(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;
            Vector3 v = r * new Vector3(0, MutipliedHeight / 2f, 0);
            return (pos + MutipliedCenter + v, pos + MutipliedCenter - v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot, float delta)
        {
            return new RBColliderCapsule(pos + MutipliedCenter, rot * LocalRot, MutipliedRadius, MutipliedHeight);
        }

        private void Reset()
        {
            AutoAlign();
        }

        public void AutoAlign()
        {
            GameObject g = gameObject;

            if (g.TryGetComponent(out MeshRenderer mr))
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, "Aligned RBSphereCollider");
#endif

                Vector3 aabbSize = Vector3.Scale(mr.localBounds.size, gameObject.transform.lossyScale);
                Vector3 aabbCenter = Vector3.Scale(mr.localBounds.center, gameObject.transform.lossyScale);

                _radius = Mathf.Max(aabbSize.x, aabbSize.z) / 2f;
                _height = Mathf.Max(aabbSize.y - _radius * 2, 0);
                _center = aabbCenter;

                SetValidate();
            }
        }

        public void SetValidate()
        {
            _height = Mathf.Abs(_height);
            _radius = Mathf.Abs(_radius);
        }
    }
}