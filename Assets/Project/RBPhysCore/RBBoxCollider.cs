using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor;
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
        public Vector3 Size { get { return Vector3.Scale(_size, gameObject.transform.lossyScale); } }
        public Quaternion LocalRot { get { return Quaternion.Euler(_rotationEuler); } set { _rotationEuler = value.eulerAngles; } }

        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + rot * Center, Mathf.Sqrt(Mathf.Pow(Size.x, 2) + Mathf.Pow(Size.y, 2) + Mathf.Pow(Size.z, 2)) / 2f);
        }

        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;
            Vector3 sDir = r * Size;

            float size_prjX = RBPhysUtil.CalcOBBAxisSize(Size, rot, Vector3.right);
            float size_prjY = RBPhysUtil.CalcOBBAxisSize(Size, rot, Vector3.up);
            float size_prjZ = RBPhysUtil.CalcOBBAxisSize(Size, rot, Vector3.forward);

            RBColliderAABB aabb = new RBColliderAABB(pos + rot * Center, new Vector3(size_prjX, size_prjY, size_prjZ));
            return aabb;
        }

        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + rot * Center, rot * LocalRot, Size);
        }
    }
}
