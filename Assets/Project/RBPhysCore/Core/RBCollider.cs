using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _parent; } }
        public abstract RBColliderDetailType DetailType { get; }

        public Vector3 GameObjectPos { get; private set; }
        public Quaternion GameObjectRot { get; private set; }
        public Vector3 GameObjectLossyScale { get; private set; }

        void Awake()
        {
            RBPhysCore.AddCollider(this);
        }

        void OnDestroy()
        {
            RBPhysCore.RemoveCollider(this);
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
            GameObjectLossyScale = gameObject.transform.lossyScale;
        }

        public RBRigidbody GetParentRigidbody()
        {
            return _parent;
        }

        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, Vector3 scale);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, Vector3 scale);

        public virtual RBColliderSphere CalcSphere()
        {
            return CalcSphere(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public virtual RBColliderAABB CalcAABB()
        {
            return CalcAABB(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public virtual RBColliderOBB CalcOBB()
        {
            return CalcOBB(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }

        public abstract Vector3 GetColliderCenter(Vector3 pos, Quaternion rot, Vector3 scale);

        public virtual Vector3 GetColliderCenter()
        {
            return GetColliderCenter(GameObjectPos, GameObjectRot, GameObjectLossyScale);
        }
    }
}