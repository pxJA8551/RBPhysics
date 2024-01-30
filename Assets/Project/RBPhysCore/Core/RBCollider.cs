using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return (_parent?.isActiveAndEnabled ?? false) ? _parent : null; } }
        public abstract RBGeometryType GeometryType { get; }

        public Vector3 GameObjectPos { get; private set; }
        public Quaternion GameObjectRot { get; private set; }

        [HideInInspector] public float beta = 0.7f;
        public float restitution = 0.5f; //�����W��
        public float friction = 0.5f; //���C�W��

        public RBTrajectory Trajectory { get { return _trajectory; } }

        RBTrajectory _trajectory;

        void Awake()
        {
            UpdateTransform();
        }

        void OnDestroy()
        {
        }

        void OnEnable()
        {
            RBPhysCore.AddCollider(this);

            if (ParentRigidbody != null)
            {
                RBPhysCore.SwitchToRigidbody(this);
            }
        }

        private void OnDisable()
        {
            RBPhysCore.RemoveCollider(this);
        }

        public void FixedUpdate() { }

        public RBCollider()
        {
            _trajectory = new RBTrajectory();
        }

        public void SetParentRigidbody(RBRigidbody r)
        {
            if (r != null)
            {
                _parent = r;
            }
        }

        public void ClearParentRigidbody()
        {
            _parent = null;
        }

        public void UpdateTransform()
        {
            GameObjectPos = gameObject.transform.position;
            GameObjectRot = gameObject.transform.rotation;
            _trajectory.Update(this);
        }

        public abstract float CalcVolume();
        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot);
        public abstract RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot);

        public virtual RBColliderSphere CalcSphere()
        {
            return CalcSphere(GameObjectPos, GameObjectRot);
        }

        public virtual RBColliderAABB CalcAABB()
        {
            return CalcAABB(GameObjectPos, GameObjectRot);
        }

        public virtual RBColliderOBB CalcOBB()
        {
            return CalcOBB(GameObjectPos, GameObjectRot);
        }

        public virtual RBColliderCapsule CalcCapsule()
        {
            return CalcCapsule(GameObjectPos, GameObjectRot);
        }

        public abstract Vector3 GetColliderCenter(Vector3 pos, Quaternion rot);

        public virtual Vector3 GetColliderCenter()
        {
            return GetColliderCenter(GameObjectPos, GameObjectRot);
        }
    }
}