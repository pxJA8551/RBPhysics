using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public abstract class RBCollider : RBVirtualComponent
    {
        protected RBRigidbody _parent;

        public bool HasEnabledParent { get { return _parent?.VEnabled ?? false; } }
        public RBRigidbody ParentRigidbody { get { return HasEnabledParent ? _parent : null; } }

        public abstract RBGeometryType GeometryType { get; }

        [NonSerialized] public float beta = .23f;

        [NonSerialized] public float restitution = 0.45f; //îΩî≠åWêî
        [NonSerialized] public float friction = 0.55f; //ñÄéCåWêî

        public RBTrajectory Trajectory { get { return _trajectory; } }

        [NonSerialized] public float colliderSizeMultiplier = 1f;

        public bool useCCD;
        public bool allowSoftClip;

        protected RBTrajectory _trajectory;

        public OnCollision onCollision;

        public virtual int Layer { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentAwake()
        {
            UpdateTransform(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentOnEnable()
        {
            PhysComputer.AddCollider(this);

            FindParent();
            if (ParentRigidbody != null)
            {
                PhysComputer.SwitchToRigidbody(this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ComponentOnDisable()
        {
            ReleaseParent();
            PhysComputer.RemoveCollider(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBCollider()
        {
            _trajectory = new RBTrajectory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FindParent()
        {
            var parent = GetComponentInParent<RBRigidbody>();
            if (parent != null && parent.Ident(PhysComputer))
            {
                SetParentRigidbody(parent);
            }
            else
            {
                ClearParentRigidbody();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleaseParent()
        {
            if (_parent != null)
            {
                _parent.RemoveCollider(this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetParentRigidbody(RBRigidbody r)
        {
            if (r != null)
            {
                _parent = r;
                r.AddCollider(this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearParentRigidbody()
        {
            _parent = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void UpdateTransform(float delta)
        {
            var pos = VTransform.WsPosition;
            var rot = VTransform.WsRotation;

            if (GeometryType == RBGeometryType.Sphere && useCCD) _trajectory.Update(this, pos, rot, VTransform.Layer, delta);
            else _trajectory.Update(this, pos, rot, VTransform.Layer, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OnCollision(RBCollider col, RBCollisionInfo info)
        {
            if (onCollision != null) onCollision(col, info);
        }

        public abstract float CalcVolume();
        public abstract float CalcUnscaledVolume();
        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot, float delta);
        public abstract RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot, float delta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual RBColliderSphere CalcSphere(float delta)
        {
            return CalcSphere(VTransform.WsPosition, VTransform.WsRotation, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual RBColliderAABB CalcAABB(float delta)
        {
            return CalcAABB(VTransform.WsPosition, VTransform.WsRotation, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual RBColliderOBB CalcOBB(float delta)
        {
            return CalcOBB(VTransform.WsPosition, VTransform.WsRotation, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual RBColliderCapsule CalcCapsule(float delta)
        {
            return CalcCapsule(VTransform.WsPosition, VTransform.WsRotation, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Vector3 GetColliderCenter(Vector3 pos, Quaternion rot);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Vector3 GetColliderCenter()
        {
            return GetColliderCenter(VTransform.WsPosition, VTransform.WsRotation);
        }
    }
}