using System;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBPhysCore;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _hasParentRigidbodyInFrame ? _parent : null; } }
        public abstract RBGeometryType GeometryType { get; }

        public Vector3 GameObjectPos { get; private set; }
        public Quaternion GameObjectRot { get; private set; }

        [NonSerialized] public float cr_kp = .45f; //�Փˉ������� P�Q�C��
        [NonSerialized] public float cr_ki = 15f; //�Փˉ������� I�Q�C��
        [NonSerialized] public float cr_kd = .1f; //�Փˉ������� D�Q�C��

        [NonSerialized] public float restitution = 0.7f; //�����W��
        [NonSerialized] public float friction = 0.5f; //���C�W��

        public RBTrajectory ExpTrajectory { get { return _expTrajectory; } }

        [NonSerialized] public float colliderSizeMultiplier = 1f;

        public bool IgnoreCollision { get { return _stackVal_ignoreCollision_ifGreaterThanZero > 0; } }
        public bool useCCD;
        public bool allowSoftClip;

        int _stackVal_ignoreCollision_ifGreaterThanZero = 0;

        RBTrajectory _expTrajectory;

        Vector3 _expPos;
        Quaternion _expRot;

        bool _hasParentRigidbodyInFrame = false;

        List<RBConstraints.IRBOnCollision> collisionCallbacks = new List<RBConstraints.IRBOnCollision>();

        public void SetIgnoreCollision()
        {
            _stackVal_ignoreCollision_ifGreaterThanZero++;
        }

        public void SetDecrIgnoreCollision()
        {
            _stackVal_ignoreCollision_ifGreaterThanZero--;
        }

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
            _expTrajectory = new RBTrajectory();
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
            GameObjectPos = gameObject?.transform.position ?? Vector3.zero;
            GameObjectRot = gameObject?.transform.rotation ?? Quaternion.identity;

            _expPos = GameObjectPos;
            _expRot = GameObjectRot;

            _hasParentRigidbodyInFrame = _parent?.isActiveAndEnabled ?? false;

            _expTrajectory.Update(this, gameObject?.layer ?? 0);
        }

        public void UpdateExpTrajectory(Vector3 rbPos, Quaternion rbRot, Vector3 intergratedPos, Quaternion intergratedRot)
        {
            Vector3 relPos = GameObjectPos - rbPos;
            Quaternion relRot = GameObjectRot * Quaternion.Inverse(rbRot);

            _expPos = intergratedPos + relPos;
            _expRot = intergratedRot * relRot;

            _expTrajectory.Update(this, _expPos, _expRot);
        }

        internal void OnCollision(RBTrajectory traj)
        {
            foreach (var c in collisionCallbacks)
            {
                c?.OnCollision(traj);
            }
        }

        public void AddCollisionCallback(RBConstraints.IRBOnCollision c)
        {
            collisionCallbacks.Add(c);
        }

        public void RemoveCollisionCallback(RBConstraints.IRBOnCollision c)
        {
            collisionCallbacks.Remove(c);
        }

        public abstract float CalcVolume();
        public abstract float CalcUnscaledVolume();
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

        public virtual RBColliderSphere CalcExpSphere()
        {
            return CalcSphere(_expPos, _expRot);
        }

        public virtual RBColliderAABB CalcExpAABB()
        {
            return CalcAABB(_expPos, _expRot);
        }

        public virtual RBColliderOBB CalcExpOBB()
        {
            return CalcOBB(_expPos, _expRot);
        }

        public virtual RBColliderCapsule CalcExpCapsule()
        {
            return CalcCapsule(_expPos, _expRot);
        }

        public Vector3 ExpToCurrent(Vector3 expPos)
        {
            return GameObjectPos + GameObjectRot * (Quaternion.Inverse(_expRot) * (expPos - _expPos));
        }

        public Vector3 ExpToCurrentVector(Vector3 expVector)
        {
            return GameObjectRot * (Quaternion.Inverse(_expRot) * expVector);
        }
    }
}