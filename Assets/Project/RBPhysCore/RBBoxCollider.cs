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
        [SerializeField] Vector3 size;
        [SerializeField] Vector3 center;
        [SerializeField] Vector3 rotationEuler { get { return _localRot.eulerAngles; } set { _localRot = Quaternion.Euler(value); } }

        Quaternion _localRot;

        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + center, Mathf.Sqrt(Mathf.Pow(size.x, 2) + Mathf.Pow(size.y, 2) + Mathf.Pow(size.z, 2)) / 2f);
        }

        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            Vector3 s = size;

            Quaternion r = rot * _localRot;

            Vector3 xyz = r * new Vector3(-s.x, -s.y, -s.z);
            Vector3 Xyz = r * new Vector3(s.x, -s.y, -s.z);
            Vector3 xYz = r * new Vector3(-s.x, s.y, -s.z);
            Vector3 XYz = r * new Vector3(s.x, s.y, -s.z);
            Vector3 xyZ = r * new Vector3(-s.x, -s.y, s.z);
            Vector3 XyZ = r * new Vector3(s.x, -s.y, s.z);
            Vector3 xYZ = r * new Vector3(-s.x, s.y, s.z);
            Vector3 XYZ = r * new Vector3(s.x, s.y, s.z);

            Vector3[] arr = new Vector3[] { xyz, Xyz, xYz, XYz, xyZ, XyZ, xYZ, XYZ };

            var xp = arr.Select(item => item.x).ToArray();
            var yp = arr.Select(item => item.y).ToArray();
            var zp = arr.Select(item => item.z).ToArray();

            float x_min = Mathf.Min(xp);
            float y_min = Mathf.Min(yp);
            float z_min = Mathf.Min(zp);
            float x_max = Mathf.Max(xp);
            float y_max = Mathf.Max(yp);
            float z_max = Mathf.Max(zp);

            RBColliderAABB aabb = new RBColliderAABB(pos + center, new Vector3(x_max - x_min, y_max - y_min, z_max - z_min));
            return aabb;
        }

        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + center, rot * _localRot * Vector3.forward, size);
        }
    }
}
