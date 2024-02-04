using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            return Mathf.Abs(_size.x * _size.y * _size.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + rot * Center,  Mathf.Sqrt(Mathf.Pow(_size.x, 2) + Mathf.Pow(_size.y, 2) + Mathf.Pow(_size.z, 2)) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;

            float size_prjX =  RBPhysUtil.GetOBBAxisSize(_size, r, Vector3.right);
            float size_prjY = RBPhysUtil.GetOBBAxisSize(_size, r, Vector3.up);
            float size_prjZ = RBPhysUtil.GetOBBAxisSize(_size, r, Vector3.forward);

            RBColliderAABB aabb = new RBColliderAABB(pos + rot * Center, new Vector3(size_prjX, size_prjY, size_prjZ));
            return aabb;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + rot * (Center - LocalRot * _size / 2f), rot * LocalRot, _size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return Center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot)
        {
            throw new System.NotImplementedException();
        }
    }
}
