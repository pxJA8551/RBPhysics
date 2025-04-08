using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static RBPhys.RBPhysComputer;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbody : RBVirtualComponent
    {
        const float SLEEP_VEL_MAX_SQRT = 0.05f * 0.05f;
        const float SLEEP_ANGVEL_MAX_SQRT = .1f * .1f;
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

        public bool IsStaticOrSleeping { get { return isSleeping || _expObjTrajectory.IsIgnoredTrajectory; } }

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

        bool _infInertiaTensorX;
        bool _infInertiaTensorY;
        bool _infInertiaTensorZ;

        Vector3 _invInertiaWsScale = Vector3.one;

        public bool isSleeping = false;
        public int sleepGrace = 0;
        public bool useGravity = true;

        public bool sleepUntilInteraction;
        public bool setInfInertiaTensorOnInit;

        public InterpTrajectory interpTraj;

        [NonSerialized] public RBCollider[] colliding = new RBCollider[2];
        [NonSerialized] public int collidingCount = 0;

        public RBTrajectory ExpObjectTrajectory { get { return _expObjTrajectory; } }

        protected RBTrajectory _expObjTrajectory;

        public OnCollision onCollision;

        public List<RBPhysStateValidator> validators = new List<RBPhysStateValidator>();
        public List<RBTrajectoryAlternateValidator> trajAltnValidators = new List<RBTrajectoryAlternateValidator>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddValidator(RBPhysStateValidator validator)
        {
            validators.Add(validator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddTrajectoryAlternateValidator(RBTrajectoryAlternateValidator trajAltnValidator)
        {
            trajAltnValidators.Add(trajAltnValidator);
        }

        public Vector3 InverseInertiaWs
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Vector3 i = inertiaTensor * inertiaTensorMultiplier;
                Quaternion r = VTransform.WsRotation * inertiaTensorRotation;
                return Vector3.Scale(r * (Quaternion.Inverse(r) * V3Rcp(i)), _invInertiaWsScale);
            }
        }

        protected override void ComponentAwake()
        {
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
            }
        }

        protected override void ComponentOnEnable()
        {
            PhysComputer.AddRigidbody(this);

            UpdateColliders();
            RecalculateInertiaTensor();
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
                c.ClearParentRigidbody();
            }

            _colliders.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override RBVirtualComponent CreateVirtual(GameObject obj)
        {
            var rb = obj.AddComponent<RBRigidbody>();
            return rb;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SyncVirtual(RBVirtualComponent vComponent)
        {
            var rb = vComponent as RBRigidbody;
            if (rb == null) throw new Exception();
            CopyRigidbody(rb);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            _infInertiaTensorX = rb._infInertiaTensorX;
            _infInertiaTensorY = rb._infInertiaTensorY;
            _infInertiaTensorZ = rb._infInertiaTensorZ;

            _invInertiaWsScale = rb._invInertiaWsScale;

            isSleeping = rb.isSleeping;
            sleepGrace = rb.sleepGrace;
            useGravity = rb.useGravity;

            interpTraj = rb.interpTraj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBRigidbody()
        {
            _expObjTrajectory = new RBTrajectory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateColliders()
        {
            _colliders = new List<RBCollider>();

            foreach (var c in GetComponents<RBCollider>())
            {
                if (c.Ident(PhysComputer) && c.ParentRigidbody == null) c.SetParentRigidbody(this);
            }

            FindChildrenRecursive(transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCollider(RBCollider collider)
        {
            if (_colliders == null) _colliders = new List<RBCollider>();

            if (!_colliders.Contains(collider))
            {
                _colliders.Add(collider);
            }

            PhysComputer.SwitchToRigidbody(collider);
            collider.UpdateTransform(0);

            RecalculateInertiaTensor();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    if (c != null && c.Ident(PhysComputer) && c.ParentRigidbody == null)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReinitializeColliders()
        {
            UpdateColliders();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBCollider[] GetColliders()
        {
            return _colliders.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetColliderSizeMultiplier(float multiplier)
        {
            foreach (var c in _colliders)
            {
                c.colliderSizeMultiplier = multiplier;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InvertVelocity()
        {
            _velocity = -_velocity;
            _angularVelocity = -_angularVelocity;

            _expVelocity = -_expVelocity;
            _expAngularVelocity = -_expAngularVelocity;
        }

        internal virtual void ApplyTransform(float dt, TimeScaleMode physTimeScaleMode)
        {
            if (!_expObjTrajectory.IsIgnoredTrajectory)
            {
                float vm = _expVelocity.magnitude;
                float avm = _expAngularVelocity.magnitude;

                if (physTimeScaleMode.IsProg())
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

                PushInterpTraj(VTransform.WsPosition, VTransform.WsRotation);

                CalcVel2Ws(_velocity, _angularVelocity, dt, out var wsPos, out var wsRot);
                VTransform.SetWsPositionAndRotation(wsPos, wsRot);
            }
            else
            {
                _velocity = _expVelocity;
                _angularVelocity = _expAngularVelocity;
            }

            UpdateExpTrajectory(dt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushInterpTraj(Vector3 pos, Quaternion rot)
        {
            var pushInterp = interpTraj;
            pushInterp.PushLast(pos, rot);
            interpTraj = pushInterp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearInterpTraj()
        {
            interpTraj = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CalcVel2Ws(Vector3 vel, Vector3 angVel, float dt, out Vector3 wsPosOut, out Quaternion wsRotOut)
        {
            CalcVel2Ws(VTransform.WsPosition, VTransform.WsRotation, vel, angVel, dt, out wsPosOut, out wsRotOut);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CalcVel2Ws(Vector3 wsPos, Quaternion wsRot, Vector3 vel, Vector3 angVel, float dt, out Vector3 wsPosOut, out Quaternion wsRotOut)
        {
            float length = angVel.magnitude;
            var rot = Quaternion.AngleAxis(length * Mathf.Rad2Deg * dt, angVel / length);
            if (length == 0) rot = Quaternion.identity;

            var vd = wsRot * _centerOfGravity;

            wsPosOut = wsPos + (vel * dt) + (vd - (rot * vd));
            wsRotOut = rot * wsRot;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ClearValidators()
        {
            validators.Clear();
            trajAltnValidators.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal virtual void UpdateExpTrajectory(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(0);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectory(dt, VTransform.WsPosition, VTransform.WsRotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, VTransform.Layer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void UpdateColliderExpTrajectory(float dt)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(0);

            for (int i = 0; i < _colliders.Count; i++)
            {
                _colliders[i].UpdateExpTrajectory(dt, VTransform.WsPosition, VTransform.WsRotation, r.pos, r.rot);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OnCollision(RBCollider col, RBCollisionInfo info)
        {
            if (onCollision != null) onCollision(col, info);

            foreach (var c in _colliders)
            {
                c?.OnCollision(col, info);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public struct InterpTrajectory
        {
            Vector3 _positionLast;
            Quaternion _rotationLast;
            bool _pushedLast;

            Vector3 _positionLast2;
            Quaternion _rotationLast2;
            bool _pushedLast2;

            public Vector3 PositionLast { get { return _positionLast; } }
            public Quaternion RotationLast { get { return _rotationLast; } }
            public bool PushedLast { get { return _pushedLast; } }

            public Vector3 PositionLast2 { get { return _positionLast2; } }
            public Quaternion RotationLast2 { get { return _rotationLast2; } }
            public bool PushedLast2 { get { return _pushedLast2; } }

            public void PushLast(Vector3 pos, Quaternion rot)
            {
                _pushedLast2 = _pushedLast;
                _positionLast2 = _positionLast;
                _rotationLast2 = _rotationLast;

                _pushedLast = true;
                _positionLast = pos;
                _rotationLast = rot;
            }
        }
    }
}