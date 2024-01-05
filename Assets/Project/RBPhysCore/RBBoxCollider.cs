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

        [SerializeField] Vector3 size;
        [SerializeField] Vector3 center;
        [SerializeField] Vector3 rotationEuler { get { return _localRot.eulerAngles; } set { _localRot = Quaternion.Euler(value); } }

        public override RBColliderDetailType DetailType { get { return DETAIL_TYPE; } }

        Quaternion _localRot;

        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + center, Mathf.Sqrt(Mathf.Pow(size.x, 2) + Mathf.Pow(size.y, 2) + Mathf.Pow(size.z, 2)) / 2f);
        }

        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * _localRot;
            Vector3 sDir = r * size / 2f;

            float size_prjX = Vector3.Dot(Vector3.right, sDir);
            float size_prjY = Vector3.Dot(Vector3.up, sDir);
            float size_prjZ = Vector3.Dot(Vector3.forward, sDir);

            RBColliderAABB aabb = new RBColliderAABB(pos + center, new Vector3(size_prjX, size_prjY, size_prjZ));
            return aabb;
        }

        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + center, rot * _localRot, size);
        }
    }
}
