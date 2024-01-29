using RBPhys;
using System;
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
        public override float CalcVolume()
        {
            return (4f * Mathf.PI * _radius * _radius * _radius) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + _center, _radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderAABB(pos + _center, Vector3.one * _radius * 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            Vector3 size = Vector3.one * _radius * 2;
            return new RBColliderOBB(pos + _center - size / 2f, rot, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return pos + _center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot)
        {
            throw new System.NotImplementedException();
        }
    }
}