using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public abstract class RBCollider : MonoBehaviour
    {
        protected RBRigidbody _parent;

        public RBRigidbody ParentRigidbody { get { return _hasParentRigidbodyInFrame ? _parent : null; } }
        public abstract RBGeometryType GeometryType { get; }

        public Vector3 GameObjectPos { get; protected set; }
        public Quaternion GameObjectRot { get; protected set; }

        List<RBCollider> _virtualColliders = new List<RBCollider>();

        public virtual bool vActive_And_vEnabled { get { return enabled && gameObject.activeSelf; } }

        [NonSerialized] public float cr_kp = .45f; //Õ“Ë‰ðÁˆ— PƒQƒCƒ“
        [NonSerialized] public float cr_ki = 15f; //Õ“Ë‰ðÁˆ— IƒQƒCƒ“
        [NonSerialized] public float cr_kd = .1f; //Õ“Ë‰ðÁˆ— DƒQƒCƒ“

        [NonSerialized] public float restitution = 0.7f; //”½”­ŒW”
        [NonSerialized] public float friction = 0.5f; //–€ŽCŒW”

        public RBTrajectory ExpTrajectory { get { return _expTrajectory; } }

        [NonSerialized] public float colliderSizeMultiplier = 1f;

        public bool IgnoreCollision { get { return _stackVal_ignoreCollision_ifGreaterThanZero > 0; } }
        public bool useCCD;
        public bool allowSoftClip;

        int _stackVal_ignoreCollision_ifGreaterThanZero = 0;

        protected RBTrajectory _expTrajectory;

        protected Vector3 _expPos;
        protected Quaternion _expRot;

        protected bool _hasParentRigidbodyInFrame = false;

        public OnCollision onCollision;

        public virtual int Layer { get; }

        public void AddVirtualCollider(RBCollider collider)
        {
            if (!_virtualColliders.Contains(collider))
            {
                _virtualColliders.Add(collider);
            }
        }

        public void RemoveVirtualCollider(RBCollider collider)
        {
            _virtualColliders.Remove(collider);
        }

        public int VirtualColliders(ref RBCollider[] colliders)
        {
            if (colliders == null) colliders = new RBCollider[Mathf.Max(_virtualColliders.Count, 1)];
            if (colliders.Length < _virtualColliders.Count) Array.Resize(ref colliders, _virtualColliders.Count);

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i] = _virtualColliders.ElementAtOrDefault(i);
            }

            return _virtualColliders.Count;
        }

        public void SetIgnoreCollision()
        {
            _stackVal_ignoreCollision_ifGreaterThanZero++;
        }

        public void SetDecrIgnoreCollision()
        {
            _stackVal_ignoreCollision_ifGreaterThanZero--;
        }

        protected virtual void Awake()
        {
            UpdateTransform(0);
        }

        protected virtual void OnEnable()
        {
            RBPhysController.AddCollider(this);

            if (ParentRigidbody != null)
            {
                RBPhysController.SwitchToRigidbody(this);
            }
        }

        protected virtual void OnDisable()
        {
            RBPhysController.RemoveCollider(this);
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

        public virtual void UpdateTransform(float delta)
        {
            GameObjectPos = gameObject?.transform.position ?? Vector3.zero;
            GameObjectRot = gameObject?.transform.rotation ?? Quaternion.identity;

            _expPos = GameObjectPos;
            _expRot = GameObjectRot;

            _hasParentRigidbodyInFrame = _parent?.vActive_And_vEnabled ?? false;

            if (GeometryType == RBGeometryType.Sphere && useCCD) _expTrajectory.Update(this, GameObjectPos, GameObjectRot, gameObject?.layer ?? 0, delta);
            else _expTrajectory.Update(this, _expPos, _expRot, gameObject?.layer ?? 0, delta);
        }

        public void UpdateExpTrajectoryMultiThreaded(float delta, Vector3 rbPos, Quaternion rbRot, Vector3 intergratedPos, Quaternion intergratedRot)
        {
            Vector3 relPos = GameObjectPos - rbPos;
            Quaternion relRot = GameObjectRot * Quaternion.Inverse(rbRot);

            _expPos = intergratedPos + relPos;
            _expRot = intergratedRot * relRot;

            if (GeometryType == RBGeometryType.Sphere && useCCD) _expTrajectory.Update(this, GameObjectPos, GameObjectRot, _expTrajectory.Layer, delta);
            else _expTrajectory.Update(this, _expPos, _expRot, _expTrajectory.Layer, delta);
        }

        internal void OnCollision(RBCollider col, RBCollisionInfo info)
        {
            if(onCollision != null) onCollision(col, info);
        }

        public abstract float CalcVolume();
        public abstract float CalcUnscaledVolume();
        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot, float delta);

        public virtual RBColliderSphere CalcSphere(float delta)
        {
            return CalcSphere(GameObjectPos, GameObjectRot, delta);
        }

        public virtual RBColliderAABB CalcAABB(float delta)
        {
            return CalcAABB(GameObjectPos, GameObjectRot, delta);
        }

        public virtual RBColliderOBB CalcOBB(float delta)
        {
            return CalcOBB(GameObjectPos, GameObjectRot, delta);
        }

        public virtual RBColliderCapsule CalcCapsule(float delta)
        {
            return CalcCapsule(GameObjectPos, GameObjectRot, delta);
        }

        public abstract Vector3 GetColliderCenter(Vector3 pos, Quaternion rot);

        public virtual Vector3 GetColliderCenter()
        {
            return GetColliderCenter(GameObjectPos, GameObjectRot);
        }

        public virtual RBColliderSphere CalcExpSphere(float delta)
        {
            return CalcSphere(_expPos, _expRot, delta);
        }

        public virtual RBColliderAABB CalcExpAABB(float delta)
        {
            return CalcAABB(_expPos, _expRot, delta);
        }

        public virtual RBColliderOBB CalcExpOBB(float delta)
        {
            return CalcOBB(_expPos, _expRot, delta);
        }

        public virtual RBColliderCapsule CalcExpCapsule(float delta)
        {
            return CalcCapsule(_expPos, _expRot, delta);
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