using RBPhys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        public float MutipliedRadius { get { return _radius * colliderSizeMultiplier; } }
        public Vector3 MutipliedCenter { get { return _center * colliderSizeMultiplier; } }

        public Vector3 GetMutlpliedPos(Vector3 pos)
        {
            if (colliderSizeMultiplierRigidbody != null)
            {
                return colliderSizeMultiplierRigidbody.Position;
            }
            else
            {
                return pos;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            return (4f * Mathf.PI * MutipliedRadius * MutipliedRadius * MutipliedRadius) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcUnscaledVolume()
        {
            return (4f * Mathf.PI * _radius * _radius * _radius) / 3f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(GetMutlpliedPos(pos) + MutipliedCenter, MutipliedRadius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderAABB(GetMutlpliedPos(pos) + MutipliedCenter, Vector3.one * MutipliedRadius * 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            Vector3 size = Vector3.one * MutipliedRadius * 2;
            return new RBColliderOBB(GetMutlpliedPos(pos) + MutipliedCenter - size / 2f, rot, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return GetMutlpliedPos(pos) + MutipliedCenter;
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
                Undo.RecordObject(this, "Aligned RBSphereCollider");
#endif

                Vector3 aabbSize = Vector3.Scale(mr.localBounds.size, gameObject.transform.lossyScale);
                Vector3 aabbCenter = Vector3.Scale(mr.localBounds.center, gameObject.transform.lossyScale);

                _radius = Mathf.Max(aabbSize.x, aabbSize.y, aabbSize.z) / 2f;
                _center = aabbCenter;
            }
        }
    }
}