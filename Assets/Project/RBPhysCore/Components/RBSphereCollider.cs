using RBPhys;
using System.Collections;
using System.Collections.Generic;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume(Vector3 scale)
        {
            scale = RBPhysUtil.V3Abs(scale);
            float r = Mathf.Max(scale.x, scale.y, scale.z) * _radius;
            return (4f * Mathf.PI * r * r * r) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            scale = RBPhysUtil.V3Abs(scale);
            float r = Mathf.Max(scale.x, scale.y, scale.z) * _radius;
            return new RBColliderSphere(pos + _center, r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            scale = RBPhysUtil.V3Abs(scale);
            float r = Mathf.Max(scale.x, scale.y, scale.z) * _radius * 2;
            return new RBColliderAABB(pos + _center, Vector3.one * r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            scale = RBPhysUtil.V3Abs(scale);
            float r = Mathf.Max(scale.x, scale.y, scale.z) * _radius * 2;
            Vector3 size = Vector3.one * r;
            return new RBColliderOBB(pos + _center - size / 2f, rot, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            return pos + _center;
        }
    }
}