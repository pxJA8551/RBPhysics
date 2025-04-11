using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBSphereCollider : RBCollider
    {
        const RBGeometryType GEOMETRY_TYPE = RBGeometryType.Sphere;

        [SerializeField] Vector3 _center = Vector3.zero;
        [SerializeField] float _radius = 0.5f;

        public override RBGeometryType GeometryType { get { return GEOMETRY_TYPE; } }

        public Vector3 Center { get { return _center; } set { _center = value; } }
        public float Radius { get { return _radius; } set { _radius = Mathf.Abs(value); } }

        public float MultipliedRadius { get { return _radius * colliderSizeMultiplier; } }
        public Vector3 MultipliedCenter { get { return _center * colliderSizeMultiplier; } }

        public override int Layer { get { return gameObject.layer; } }

        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rbc = obj.AddComponent<RBSphereCollider>();
            return rbc;
        }

        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rbc = vComponent as RBSphereCollider;
            if (rbc == null) throw new Exception();
            CopyCollider(rbc);
        }

        public void CopyCollider(RBSphereCollider c)
        {
            useCCD = c.useCCD;
            allowSoftClip = c.allowSoftClip;
            _center = c._center;
            _radius = c._radius;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            return (4f * Mathf.PI * MultipliedRadius * MultipliedRadius * MultipliedRadius) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcUnscaledVolume()
        {
            return (4f * Mathf.PI * _radius * _radius * _radius) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere((pos) + MultipliedCenter, MultipliedRadius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            if (useCCD)
            {
                Vector3 pos_current = pos;
                Vector3 pos_last = pos - _ccdOffset;

                Vector3 size = RBPhysUtil.V3Abs(pos_last - pos_current) + Vector3.one * MultipliedRadius * 2;
                Vector3 avgPos = (pos_current + pos_last) / 2f;

                return new RBColliderAABB(avgPos, size);
            }
            else
            {
                return new RBColliderAABB((pos) + MultipliedCenter, Vector3.one * MultipliedRadius * 2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return (pos) + MultipliedCenter;
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

                _radius = Mathf.Max(aabbSize.x, aabbSize.y, aabbSize.z) / 2f;
                _center = aabbCenter;

                SetValidate();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValidate()
        {
            _radius = Mathf.Abs(_radius);
        }
    }
}