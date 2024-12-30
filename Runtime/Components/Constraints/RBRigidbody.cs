using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using static RBPhys.RBPhysComputer;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    [DisallowMultipleComponent]
    public class RBRigidbody : RBVirtualComponent
    {
        const float SLEEP_VEL_MAX_SQRT = 0.10f * 0.10f;
        const float SLEEP_ANGVEL_MAX_SQRT = 1.0f * 1.0f;
        protected const float XZ_VELOCITY_MIN_CUTOUT = .01f;
        protected const float ANG_VELOCITY_MIN_CUTOUT = .05f;
        const float SLEEP_VEL_ADD_SQRT = .2f * .2f;
        const int SLEEP_GRACE_FRAMES = 5; //, of no practical use

        protected const float DRAG_RETG_MULTIPLIER = 0f;
        protected const float ANGULAR_DRAG_RETG_MULTIPLIER = 0f;

        public float mass = 1;
        public float inertiaTensorMultiplier = 1;
        public float drag = 0.0001f;
        public float angularDrag = 0.01f;

        [NonSerialized] public Vector3 inertiaTensor;
        [NonSerialized] public Quaternion inertiaTensorRotation;

        public bool IgnoreVelocity { get { return _stackVal_ignoreVelocity_ifGreaterThanZero > 0; } }

        int _stackVal_ignoreVelocity_ifGreaterThanZero = 0;

        Vector3 _centerOfGravity;

        Vector3 _velocity;
        Vector3 _angularVelocity;
        Vector3 _expVelocity;
        Vector3 _expAngularVelocity;

        List<RBCollider> _colliders;

        public Vector3 Velocity { get { return _velocity; } }
        public Vector3 AngularVelocity { get { return _angularVelocity; } }
        public Vector3 ExpVelocity { get { return _expVelocity; } set { _expVelocity = value; } }
        public Vector3 ExpAngularVelocity { get { return _expAngularVelocity; } set { _expAngularVelocity = value; } }

        public Vector3 CenterOfGravity { get { return _centerOfGravity; } set { _centerOfGravity = value; } }
        public Vector3 CenterOfGravityWorld { get { return VTransform.WsPosition + VTransform.WsRotation * _centerOfGravity; } }

        public float InverseMass { get { return 1 / mass; } }

        public bool infInertiaTensorX { get { return _infInertiaTensorX; } set { _infInertiaTensorX = value; _invInertiaWsScale.x = value ? 0 : 1; } }
        public bool infInertiaTensorY { get { return _infInertiaTensorY; } set { _infInertiaTensorY = value; _invInertiaWsScale.y = value ? 0 : 1; } }
        public bool infInertiaTensorZ { get { return _infInertiaTensorZ; } set { _infInertiaTensorZ = value; _invInertiaWsScale.z = value ? 0 : 1; } }

        public bool infInertiaTensorLsX { get { return _infInertiaTensorLsX; } set { _infInertiaTensorLsX = value; _invInertiaLsScale.x = value ? 0 : 1; } }
        public bool infInertiaTensorLsY { get { return _infInertiaTensorLsY; } set { _infInertiaTensorLsY = value; _invInertiaLsScale.y = value ? 0 : 1; } }
        public bool infInertiaTensorLsZ { get { return _infInertiaTensorLsZ; } set { _infInertiaTensorLsZ = value; _invInertiaLsScale.z = value ? 0 : 1; } }

        bool _infInertiaTensorX;
        bool _infInertiaTensorY;
        bool _infInertiaTensorZ;

        bool _infInertiaTensorLsX;
        bool _infInertiaTensorLsY;
        bool _infInertiaTensorLsZ;

        Vector3 _invInertiaWsScale = Vector3.one;
        Vector3 _invInertiaLsScale = Vector3.one;

        public bool isSleeping = false;
        public int sleepGrace = 0;
        public bool useGravity = true;

        public bool sleepUntilInteraction;
        public bool setInfInertiaTensorOnInit;

        [NonSerialized] public RBCollider[] colliding = new RBCollider[2];
        [NonSerialized] public int collidingCount = 0;

        public RBTrajectory ExpObjectTrajectory { get { return _expObjTrajectory; } }

        protected RBTrajectory _expObjTrajectory;

        public OnCollision onCollision;

        public List<RBPhysStateValidator> validators = new List<RBPhysStateValidator>();

        public void CopyRigidbody(RBRigidbody rb)
        {
            mass = rb.mass;
            inertiaTensorMultiplier = rb.inertiaTensorMultiplier;
            drag = rb.drag;
            angularDrag = rb.angularDrag;
            inertiaTensor = rb.inertiaTensor;
            inertiaTensorRotation = rb.inertiaTensorRotation;
            _centerOfGravity = rb._centerOfGravity;
            _velocity = rb._velocity;
            _angularVelocity = rb._angularVelocity;
            _expVelocity = rb._expVelocity;
            _expAngularVelocity = rb._expAngularVelocity;
        }

        public void AddVaidator(RBPhysStateValidator validator)
        {
            validators.Add(validator);
        }

        public Vector3 InverseInertiaWs
        {
            get
            {
                Vector3 i = Vector3.Scale(inertiaTensor * inertiaTensorMultiplier, _invInertiaLsScale);
                Quaternion r = VTransform.WsRotation * inertiaTensorRotation;
                return Vector3.Scale(r * (Quaternion.Inverse(r) * V3Rcp(i)), _invInertiaWsScale);
            }
        }

        protected override void ComponentAwake()
        {
            UpdateColliders();
            RecalculateInertiaTensor();

            if (!isSleeping || sleepGrace > 0)
            {
                isSleeping = false;
                sleepGrace = 0;
            }

            if (sleepUntilInteraction)
            {
                PhysSleep();
            }
            else
            {
                PhysAwakeForce();
            }

            if (setInfInertiaTensorOnInit)
            {
                _infInertiaTensorX = true;
                _infInertiaTensorY = true;
                _infInertiaTensorZ = true;
                _infInertiaTensorLsX = true;
                _infInertiaTensorLsY = true;
                _infInertiaTensorLsZ = true;
            }
        }

        protected override void ComponentOnEnable()
        {
            PhysComputer.AddRigidbody(this);

            UpdateColliders();
            foreach (var c in _colliders)
            {
                PhysComputer.SwitchToRigidbody(c);
                c.UpdateTransform(0);
            }
        }

        protected override void ComponentOnDisable()
        {
            PhysComputer.RemoveRigidbody(this);

            foreach (var c in _colliders)
            {
                PhysComputer.SwitchToCollider(c);
                c.UpdateTransform(0);
            }
        }

        void OnDestroy()
        {
            ReleaseColliders();
        }

        public RBRigidbody()
        {
            _expObjTrajectory = new RBTrajectory();
        }

        void UpdateColliders()
        {
            _colliders = new List<RBCollider>();

            foreach (var c in GetComponents<RBCollider>())
            {
                if (c.VEnabled) c.SetParentRigidbody(this);
            }

            FindChildrenRecursive(transform);
        }

        public void AddCollider(RBCollider collider)
        {
            if (_colliders == null) _colliders = new List<RBCollider>();

            if(!_colliders.Contains(collider))
            {
                _colliders.Add(collider);
            }

            PhysComputer.SwitchToRigidbody(collider);
            collider.UpdateTransform(0);

            RecalculateInertiaTensor();
        }

        public void RemoveCollider(RBCollider collider)
        {
            _colliders?.Remove(collider);
            RecalculateInertiaTensor();
        }

        void FindChildrenRecursive(Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var t = transform.GetChild(i);
                var rb = t.GetComponent<RBRigidbody>();
                if (rb == null) 
                {
                    var c = t.GetComponent<RBCollider>();
                    if (c != null && c.VEnabled) 
                    {
                        c.SetParentRigidbody(this);
                    }
                    else
                    {
                        FindChildrenRecursive(t);
                    }
                }
                else
                {
                    Debug.Assert(false);
                }
            }
        }

        void ReleaseColliders()
        {
            foreach (var c in _colliders)
            {
                if (c != null)
                {
                    c.ClearParentRigidbody();
                }
            }
        }

        public void ReinitializeColliders()
        {
            UpdateColliders();
        }

        public RBCollider[] GetColliders()
        {
            return _colliders.ToArray();
        }

        public void SetColliderSizeMultiplier(float multiplier)
        {
            foreach (var c in _colliders)
            {
                c.colliderSizeMultiplier = multiplier;
            }
        }

        public void SetIgnoreVelocity()
        {
            _stackVal_ignoreVelocity_ifGreaterThanZero++;
        }

        public void SetDecrIgnoreVelocity()
        {
            if (_stackVal_ignoreVelocity_ifGreaterThanZero > 0) _stackVal_ignoreVelocity_ifGreaterThanZero--;
        }

        internal virtual void ApplyTransform(float dt, RBPhys.TimeScaleMode physTimeScaleMode)
        {
            if (!IgnoreVelocity)
            {
                float vm = _expVelocity.magnitude;
                float avm = _expAngularVelocity.magnitude;

                if (physTimeScaleMode == TimeScaleMode.Prograde)
                {
                    _expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm - drag);
                    _expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }
                else
                {
                    _expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm + drag * DRAG_RETG_MULTIPLIER);
                    _expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm + angularDrag * ANGULAR_DRAG_RETG_MULTIPLIER);
                }

                _expVelocity = Vector3.ClampMagnitude(_expVelocity, rbRigidbody_velocity_max);
                _expAngularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, rbRigidbody_ang_velocity_max);

                if (Mathf.Abs(_expVelocity.x) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.x = 0;
                if (Mathf.Abs(_expVelocity.z) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.z = 0;
                if (Mathf.Abs(_expAngularVelocity.x) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.x = 0;
                if (Mathf.Abs(_expAngularVelocity.y) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.y = 0;
                if (Mathf.Abs(_expAngularVelocity.z) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.z = 0;

                _velocity = _expVelocity;
                _angularVelocity = _expAngularVelocity;

                //var dCg = _centerOfGravity * VTransform.WsRotation;

                var rot = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized);
                var vd = VTransform.WsRotation * _centerOfGravity;
                var setRot = rot * VTransform.WsRotation;
                var setPos = VTransform.WsPosition + (_velocity * dt) + (vd - (rot * vd));

                VTransform.SetWsPositionAndRotation(setPos, setRot);
            }

            UpdateExpTrajectory(dt);
        }

        internal void ClearValidators()
        {
            validators.Clear();
        }

        internal virtual void UpdateExpTrajectory(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(0);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectoryMultiThreaded(dt, VTransform.WsPosition, VTransform.WsRotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, VTransform.Layer);
        }

        internal void UpdateColliderExpTrajectory(float dt)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(0);

            for (int i = 0; i < _colliders.Count; i++)
            {
                _colliders[i].UpdateExpTrajectoryMultiThreaded(dt, VTransform.WsPosition, VTransform.WsRotation, r.pos, r.rot);
            }
        }

        internal void OnCollision(RBCollider col, RBCollisionInfo info)
        {
            if (onCollision != null) onCollision(col, info);

            foreach (var c in _colliders)
            {
                c?.OnCollision(col, info);
            }
        }

        public (Vector3 pos, Quaternion rot) GetIntergrated(float dt)
        {
            return (VTransform.WsPosition + _expVelocity * dt, Quaternion.AngleAxis(_expAngularVelocity.magnitude * Mathf.Rad2Deg * dt, _expAngularVelocity.normalized) * VTransform.WsRotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsUnderSleepLevel()
        {
            return _velocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _angularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsExpUnderSleepLevel()
        {
            return _expVelocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _expAngularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsUnderSleepLevelOrSleeping()
        {
            return (_velocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _angularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT) || isSleeping;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsExpUnderSleepLevelOrSleeping()
        {
            return (_expVelocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _expAngularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT) || isSleeping;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysAwake()
        {
            isSleeping = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysAwakeForce()
        {
            isSleeping = false;
            sleepGrace = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysSleep()
        {
            _expVelocity = Vector3.zero;
            _expAngularVelocity = Vector3.zero;
            isSleeping = true;
            sleepGrace = SLEEP_GRACE_FRAMES;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void UpdatePhysSleepGrace()
        {
            if (IsExpUnderSleepLevel())
            {
                if (sleepGrace < SLEEP_GRACE_FRAMES && _expVelocity.sqrMagnitude < _velocity.sqrMagnitude + SLEEP_VEL_ADD_SQRT)
                {
                    sleepGrace++;
                }
            }
            else
            {
                PhysAwake();
                sleepGrace = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void TryPhysSleep()
        {
            if (!isSleeping && sleepGrace >= SLEEP_GRACE_FRAMES)
            {
                int sMin = SLEEP_GRACE_FRAMES;

                for (int i = 0; i < collidingCount; i++)
                {
                    sMin = Mathf.Min(sMin, colliding[i].ParentRigidbody?.sleepGrace ?? SLEEP_GRACE_FRAMES);
                }

                if (sMin >= SLEEP_GRACE_FRAMES)
                {
                    PhysSleep();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 CalcExpPos(float dt)
        {
            return VTransform.WsPosition + _expVelocity * dt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion CalcExpRot(float dt)
        {
            return Quaternion.AngleAxis(_expAngularVelocity.magnitude * Mathf.Rad2Deg * dt, _expAngularVelocity.normalized) * VTransform.WsRotation;
        }

        public void RecalculateInertiaTensor()
        {
            if (_colliders?.Any() ?? false)
            {
                ComputeMassAndInertia(_colliders, out inertiaTensor, out inertiaTensorRotation, out _centerOfGravity);
            }
            else
            {
                inertiaTensor = Vector3.zero;
                inertiaTensorRotation = Quaternion.identity;
                _centerOfGravity = default;
            }
        }

        void ComputeMassAndInertia(List<RBCollider> colliders, out Vector3 inertiaTensor, out Quaternion inertiaTensorRotation, out Vector3 cg)
        {
            inertiaTensor = Vector3.zero;
            inertiaTensorRotation = Quaternion.identity;

            float totalVolume = colliders.Select(item => item.CalcVolume()).Sum();

            RBInertiaTensor it = new RBInertiaTensor();

            foreach (RBCollider c in colliders)
            {
                float v = c.CalcVolume();
                float r = v / totalVolume;

                RBInertiaTensor geometryIt = new RBInertiaTensor();
                float m = mass * r;

                switch (c.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var box = c as RBBoxCollider;

                            Vector3 relPos = VTransform.InverseTransformPoint(box.GetColliderCenter());
                            Quaternion relRot = (box.VTransform.WsRotation * box.LocalRot) * Quaternion.Inverse(VTransform.WsRotation);

                            geometryIt.SetInertiaOBB(box.CalcOBB(0), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Sphere:
                        {
                            Vector3 relPos = VTransform.InverseTransformPoint(c.GetColliderCenter());
                            geometryIt.SetInertiaSphere(c.CalcSphere(0), relPos, Quaternion.identity);
                        }
                        break;

                    case RBGeometryType.Capsule:
                        {
                            var capsule = c as RBCapsuleCollider;

                            Vector3 relPos = VTransform.InverseTransformPoint(capsule.GetColliderCenter());
                            Quaternion relRot = (capsule.VTransform.WsRotation * capsule.LocalRot) * Quaternion.Inverse(VTransform.WsRotation);

                            geometryIt.SetInertiaCapsule(capsule.CalcCapsule(0), relPos, relRot);
                        }
                        break;
                }

                geometryIt.ScaleDensity(m / geometryIt.Mass);
                it.Merge(geometryIt);
            }

            Vector3 diagonalizedIt = RBMatrix3x3.Diagonalize(it.InertiaTensor, out Quaternion itRot);

            inertiaTensor = diagonalizedIt;
            inertiaTensorRotation = itRot;
            cg = it.CenterOfGravity;

            if (inertiaTensor == Vector3.zero)
            {
                Debug.LogError("No collider found. Error initializing InertiaTensor/InertiaTensorRotation.");
                inertiaTensor = Vector3.one;
                inertiaTensorRotation = Quaternion.identity;
            }
        }
    }
}