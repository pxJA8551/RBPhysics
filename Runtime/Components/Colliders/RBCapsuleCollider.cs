using System;
using System.Runtime.CompilerServices;
using UnityEngine;

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

        public override int Layer { get { return gameObject.layer; } }

        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rbc = obj.AddComponent<RBCapsuleCollider>();
            return rbc;
        }

        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rbc = vComponent as RBCapsuleCollider;
            if (rbc == null) throw new Exception();
            CopyCollider(rbc);
        }

        public void CopyCollider(RBCapsuleCollider c)
        {
            allowSoftClip = c.allowSoftClip;
            _center = c._center;
            _rotationEuler = c._rotationEuler;
            _radius = c._radius;
            _height = c._height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            float r = Radius;
            float vSphere = (4f * Mathf.PI * r * r * r) / 3f;
            float vCylinder = (Mathf.PI * r * r) * Height;

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
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + Center, Height / 2f + Radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            var p = GetEdge(pos, rot);

            Vector3 r = Vector3.one * Radius;

            Vector3 min = Vector3.Min(p.begin, p.end) - r;
            Vector3 max = Vector3.Max(p.begin, p.end) + r;

            return new RBColliderAABB((min + max) / 2f, max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            Vector3 extents = new Vector3(Radius * 2, Radius, Radius * 2);
            return new RBColliderOBB(pos + Center - extents, rot * LocalRot, extents * 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return pos + Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (Vector3 begin, Vector3 end) GetEdge(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;
            Vector3 v = r * new Vector3(0, Height / 2f, 0);
            return (pos + Center + v, pos + Center - v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot)
        {
            return new RBColliderCapsule(pos + Center, rot * LocalRot, Radius, Height);
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
                UnityEditor.Undo.RecordObject(this, "Aligned RBCapsuleCollider");
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