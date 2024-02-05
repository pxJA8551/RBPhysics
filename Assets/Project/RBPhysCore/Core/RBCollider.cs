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

        public RBRigidbody ParentRigidbody { get { return _hasParentRigidbodyInFrame ? _parent : null; } }
        public abstract RBGeometryType GeometryType { get; }

        public Vector3 GameObjectPos { get; private set; }
        public Quaternion GameObjectRot { get; private set; }

        [HideInInspector] public float beta = 0.5f;
        public float restitution = 0.5f; //îΩî≠åWêî
        public float friction = 0.5f; //ñÄéCåWêî

        public RBTrajectory ExpTrajectory { get { return _expTrajectory; } }

        RBTrajectory _expTrajectory;

        Vector3 _expPos;
        Quaternion _expRot;

        bool _hasParentRigidbodyInFrame = false;

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
            GameObjectPos = gameObject.transform.position;
            GameObjectRot = gameObject.transform.rotation;

            _expPos = GameObjectPos;
            _expRot = GameObjectRot;

            _hasParentRigidbodyInFrame = _parent?.isActiveAndEnabled ?? false;

            _expTrajectory.Update(this);
        }

        public void UpdateExpTrajectory(Vector3 rbPos, Quaternion rbRot, Vector3 intergratedPos, Quaternion intergratedRot)
        {
            Vector3 relPos = GameObjectPos - rbPos;
            Quaternion relRot = GameObjectRot * Quaternion.Inverse(rbRot);

            _expPos = intergratedPos + relPos;
            _expRot = intergratedRot * relRot;

            _expTrajectory.Update(this, _expPos, _expRot);
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