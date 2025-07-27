using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBBoxCollider : RBCollider
    {
        const RBGeometryType GEOMETRY_TYPE = RBGeometryType.OBB;

        [SerializeField] Vector3 _size = Vector3.one;
        [SerializeField] Vector3 _center = Vector3.zero;
        [SerializeField] Vector3 _rotationEuler = Vector3.zero;

        public override RBGeometryType GeometryType { get { return GEOMETRY_TYPE; } }

        public Vector3 Center { get { return _center; } set { _center = value; } }
        public Vector3 Size { get { return _size; } set { _size = RBPhysUtil.V3Abs(value); } }
        public Quaternion LocalRot { get { return Quaternion.Euler(_rotationEuler); } set { _rotationEuler = value.eulerAngles; } }

        public override int Layer { get { return gameObject.layer; } }

        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rbc = obj.AddComponent<RBBoxCollider>();
            return rbc;
        }

        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rbc = vComponent as RBBoxCollider;
            if (rbc == null) throw new Exception();
            CopyCollider(rbc);
        }

        public void CopyCollider(RBBoxCollider c)
        {
            allowSoftClip = c.allowSoftClip;
            _size = c._size;
            _center = c._center;
            _rotationEuler = c._rotationEuler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            return Mathf.Abs(Size.x * Size.y * Size.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcUnscaledVolume()
        {
            return Mathf.Abs(_size.x * _size.y * _size.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + rot * Center, Mathf.Sqrt(Mathf.Pow(Size.x, 2) + Mathf.Pow(Size.y, 2) + Mathf.Pow(Size.z, 2)) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;

            float size_prjX = RBPhysUtil.GetOBBAxisSize(Size, r, Vector3.right);
            float size_prjY = RBPhysUtil.GetOBBAxisSize(Size, r, Vector3.up);
            float size_prjZ = RBPhysUtil.GetOBBAxisSize(Size, r, Vector3.forward);

            RBColliderAABB aabb = new RBColliderAABB(pos + rot * Center, new Vector3(size_prjX, size_prjY, size_prjZ));
            return aabb;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + rot * (Center - LocalRot * Size / 2f), rot * LocalRot, Size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return pos + rot * Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot)
        {
            throw new System.NotImplementedException();
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
                UnityEditor.Undo.RecordObject(this, "Aligned RBSphereCollider");
#endif

                Vector3 aabbSize = Vector3.Scale(mr.localBounds.size, gameObject.transform.lossyScale);
                Vector3 aabbCenter = Vector3.Scale(mr.localBounds.center, gameObject.transform.lossyScale);

                _size = aabbSize;
                _center = aabbCenter;

                SetValidate();
            }
        }

        public void SetValidate()
        {
            _size = RBPhysUtil.V3Abs(_size);
        }
    }
}
