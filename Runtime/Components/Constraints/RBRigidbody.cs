using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static RBPhys.RBPhysComputer;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    [DisallowMultipleComponent]
    public class RBRigidbody : MonoBehaviour
    {
        const float SLEEP_VEL_MAX_SQRT = 0.03f * 0.03f;
        const float SLEEP_ANGVEL_MAX_SQRT = 0.03f * 0.03f;
        protected const float XZ_VELOCITY_MIN_CUTOUT = .002f;
        protected const float ANG_VELOCITY_MIN_CUTOUT = .02f;
        const int SLEEP_GRACE_FRAMES = 5; //, of no practical use

        public float mass = 1;
        public float inertiaTensorMultiplier = 1;
        public float drag = 0.035f;
        public float angularDrag = 0.07f;

        [NonSerialized] public Vector3 inertiaTensor;
        [NonSerialized] public Quaternion inertiaTensorRotation;

        public virtual bool vActive_And_vEnabled { get { return enabled && gameObject.activeSelf; } }

        public bool IgnoreVelocity { get { return _stackVal_ignoreVelocity_ifGreaterThanZero > 0; } }

        int _stackVal_ignoreVelocity_ifGreaterThanZero = 0;

        protected Vector3 _centerOfGravity;

        protected Vector3 _velocity;
        protected Vector3 _angularVelocity;
        protected Vector3 _expVelocity;
        protected Vector3 _expAngularVelocity;
        protected Vector3 _position;
        protected Quaternion _rotation;

        protected RBCollider[] _colliders;

        public Vector3 Velocity { get { return _velocity; } }
        public Vector3 AngularVelocity { get { return _angularVelocity; } }
        public Vector3 ExpVelocity { get { return _expVelocity; } set { _expVelocity = value; } }
        public Vector3 ExpAngularVelocity { get { return _expAngularVelocity; } set { _expAngularVelocity = value; } }
        public Vector3 Position { get { return _position; } set { _position = value; } }
        public Quaternion Rotation { get { return _rotation; } set { _rotation = value; } }

        public Vector3 CenterOfGravity { get { return _centerOfGravity; } set { _centerOfGravity = value; } }
        public Vector3 CenterOfGravityWorld { get { return Position + Rotation * _centerOfGravity; } }

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

        List<RBRigidbodyVirtual> _virtualRigidbodies = new List<RBRigidbodyVirtual>();

        public void AddVirtualRigidbody(RBRigidbodyVirtual rb)
        {
            if (!_virtualRigidbodies.Contains(rb))
            {
                _virtualRigidbodies.Add(rb);
            }
        }

        public void RemoveVirtualRigidbody(RBRigidbodyVirtual rb)
        {
            _virtualRigidbodies.Remove(rb);
        }

        public int VirtualRigidbodies(ref RBRigidbodyVirtual[] rigidbodies)
        {
            if (rigidbodies == null) rigidbodies = new RBRigidbodyVirtual[Mathf.Max(_virtualRigidbodies.Count, 1)];
            if (rigidbodies.Length < _virtualRigidbodies.Count) Array.Resize(ref rigidbodies, _virtualRigidbodies.Count);

            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i] = _virtualRigidbodies.ElementAtOrDefault(i);
            }

            return _virtualRigidbodies.Count;
        }

        public RBRigidbodyVirtual CreateVirtual(RBVirtualTransform vTransform)
        {
            var r = vTransform.AddRigidbody();
            r.CopyRigidbody(this);
            r.SetVTransform(vTransform);
            r.VInititalize(this);

            return r;
        }

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
            _position = rb._position;
            _rotation = rb._rotation;
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
                Quaternion r = Rotation * inertiaTensorRotation;
                return Vector3.Scale(r * (Quaternion.Inverse(r) * V3Rcp(i)), _invInertiaWsScale);
            }
        }

        protected virtual void Awake()
        {
            FindColliders();
            UpdateTransform(0);
            RecalculateInertiaTensor();

            if (!isSleeping || sleepGrace != 5)
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

        protected virtual void OnEnable()
        {
            RBPhysController.AddRigidbody(this);

            foreach (var c in _colliders)
            {
                RBPhysController.SwitchToRigidbody(c);
                c.UpdateTransform(0);
            }
        }

        protected virtual void OnDisable()
        {
            RBPhysController.RemoveRigidbody(this);

            foreach (var c in _colliders)
            {
                RBPhysController.SwitchToCollider(c);
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

        protected void FindColliders()
        {
            _colliders = GetComponentsInChildren<RBCollider>(true).ToArray();

            foreach (var c in _colliders)
            {
                if (c != null)
                {
                    c.SetParentRigidbody(this);
                }
            }
        }

        public virtual void AddCollider(RBCollider c)
        {
            Array.Resize(ref _colliders, _colliders.Length + 1);
            _colliders[_colliders.Length - 1] = c;

            RBPhysController.SwitchToRigidbody(c);
            c.UpdateTransform(0);
            c.SetParentRigidbody(this);

            RecalculateInertiaTensor();
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
            FindColliders();
        }

        public RBCollider[] GetColliders()
        {
            return _colliders;
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
            _stackVal_ignoreVelocity_ifGreaterThanZero--;
        }

        internal virtual void ApplyTransform(float dt, RBPhys.TimeScaleMode physTimeScaleMode)
        {
            if (!IgnoreVelocity)
            {
                _expVelocity = Vector3.ClampMagnitude(_expVelocity, rbRigidbody_velocity_max);
                _expAngularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, rbRigidbody_ang_velocity_max);

                if (Mathf.Abs(_expVelocity.x) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.x = 0;
                if (Mathf.Abs(_expVelocity.z) < XZ_VELOCITY_MIN_CUTOUT) _expVelocity.z = 0;
                if (Mathf.Abs(_expAngularVelocity.x) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.x = 0;
                if (Mathf.Abs(_expAngularVelocity.y) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.y = 0;
                if (Mathf.Abs(_expAngularVelocity.z) < ANG_VELOCITY_MIN_CUTOUT) _expAngularVelocity.z = 0;

                if (physTimeScaleMode == TimeScaleMode.Prograde)
                {
                    float vm = _expVelocity.magnitude;
                    float avm = _expAngularVelocity.magnitude;
                    _velocity = (vm > 0 ? (_expVelocity / vm) : Vector3.zero) * Mathf.Max(0, vm - drag);
                    _angularVelocity = (avm > 0 ? (_expAngularVelocity / avm) : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }
                else if (physTimeScaleMode == TimeScaleMode.Retrograde)
                {
                    float vm = _expVelocity.magnitude;
                    float avm = _expAngularVelocity.magnitude;
                    _velocity = (vm > 0 ? (_expVelocity / vm) : Vector3.zero) * Mathf.Max(0, vm + drag);
                    _angularVelocity = (avm > 0 ? (_expAngularVelocity / avm) : Vector3.zero) * Mathf.Max(0, avm - angularDrag);
                }

                transform.position = _position + (_velocity * dt);
                transform.rotation = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized) * _rotation;
            }

            UpdateTransform(dt);
            UpdateExpTrajectory(dt);
        }

        internal virtual void UpdateTransform(float delta, bool updateColliders = true)
        {
            Position = transform.position;
            Rotation = transform.rotation;

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateTransform(delta);
                }
            }
        }

        internal void ClearValidators()
        {
            validators.Clear();
        }

        internal virtual void UpdateExpTrajectoryMultiThreaded(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectoryMultiThreaded(dt, Position, Rotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, gameObject.layer);
        }

        internal virtual void UpdateExpTrajectory(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectoryMultiThreaded(dt, Position, Rotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, gameObject.layer);
        }

        internal void UpdateColliderExpTrajectory(float dt)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].UpdateExpTrajectoryMultiThreaded(dt, Position, Rotation, r.pos, r.rot);
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
            return (_position + _expVelocity * dt, Quaternion.AngleAxis(_expAngularVelocity.magnitude * Mathf.Rad2Deg * dt, _expAngularVelocity.normalized) * _rotation);
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
                _expVelocity = Vector3.zero;
                _expAngularVelocity = Vector3.zero;

                if (sleepGrace < SLEEP_GRACE_FRAMES)
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
            return _position + _expVelocity * dt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion CalcExpRot(float dt)
        {
            return Quaternion.AngleAxis(_expAngularVelocity.magnitude * Mathf.Rad2Deg * dt, _expAngularVelocity.normalized) * _rotation;
        }

        public void RecalculateInertiaTensor()
        {
            ComputeMassAndInertia(_colliders, out inertiaTensor, out inertiaTensorRotation, out _centerOfGravity);
        }

        void ComputeMassAndInertia(RBCollider[] colliders, out Vector3 inertiaTensor, out Quaternion inertiaTensorRotation, out Vector3 cg)
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

                Vector3 relPos = transform.InverseTransformPoint(c.GameObjectPos);
                Quaternion relRot = c.GameObjectRot * Quaternion.Inverse(Rotation);

                switch (c.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            geometryIt.SetInertiaOBB(c.CalcOBB(0), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Sphere:
                        {
                            geometryIt.SetInertiaSphere(c.CalcSphere(0), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Capsule:
                        {
                            geometryIt.SetInertiaCapsule(c.CalcCapsule(0), relPos, relRot);
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