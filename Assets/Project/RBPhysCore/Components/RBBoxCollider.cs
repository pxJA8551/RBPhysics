using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBBoxCollider : RBCollider
    {
        const RBColliderDetailType DETAIL_TYPE = RBColliderDetailType.OBB;

        [SerializeField] Vector3 _size = Vector3.one;
        [SerializeField] Vector3 _center = Vector3.zero;
        [SerializeField] Vector3 _rotationEuler = Vector3.zero;

        public override RBColliderDetailType DetailType { get { return DETAIL_TYPE; } }

        public Vector3 Center { get { return _center; } set { _center = value; } }
        public Vector3 Size { get { return _size; } }
        public Quaternion LocalRot { get { return Quaternion.Euler(_rotationEuler); } set { _rotationEuler = value.eulerAngles; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            Vector3 size = Vector3.Scale(Size, scale);

            return new RBColliderSphere(pos + rot * Center, Mathf.Sqrt(Mathf.Pow(size.x, 2) + Mathf.Pow(size.y, 2) + Mathf.Pow(size.z, 2)) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            Vector3 size = Vector3.Scale(Size, scale);

            Quaternion r = rot * LocalRot;

            float size_prjX =  RBPhysUtil.GetOBBAxisSize(size, rot, Vector3.right);
            float size_prjY = RBPhysUtil.GetOBBAxisSize(size, rot, Vector3.up);
            float size_prjZ = RBPhysUtil.GetOBBAxisSize(size, rot, Vector3.forward);

            RBColliderAABB aabb = new RBColliderAABB(pos + rot * Center, new Vector3(size_prjX, size_prjY, size_prjZ));
            return aabb;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            Vector3 size = Vector3.Scale(Size, scale);

            return new RBColliderOBB(pos + rot * (Center - size / 2f), rot * LocalRot, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            return pos + rot * Vector3.Scale(Center, scale);
        }
    }
}
