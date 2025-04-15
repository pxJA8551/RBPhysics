using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;
using static RBPhys.RBPhysComputer;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbody : RBVirtualComponent
    {
        const float SLEEP_VEL_MAX_SQRT = .15f * .15f;
        const float SLEEP_ANGVEL_MAX_SQRT = .1f * .1f;
        const int SLEEP_GRACE_FRAMES = 5;

        protected const float DRAG_RETG_MULTIPLIER = 0f;
        protected const float ANGULAR_DRAG_RETG_MULTIPLIER = 0f;

        public float mass = 1;
        public float inertiaTensorMultiplier = 1;
        public float drag = 0.0001f;
        public float angularDrag = 0.01f;

        [NonSerialized] public Vector3 inertiaTensor;
        [NonSerialized] public Quaternion inertiaTensorRotation;

        public bool IsStaticOrSleeping { get { return isSleeping || _objTrajectory.IsIgnoredTrajectory; } }

        Vector3 _centerOfGravity;

        Vector3 _frameWsPos;
        Quaternion _frameWsRot;

        Vector3 _subtickVelocity;
        Vector3 _subtickAngularVelocity;
        Vector3 _subtickVelocitySum;
        Vector3 _subtickAngularVelocitySum;

        Vector3 _velocity;
        Vector3 _angularVelocity;
        Vector3 _expVelocity;
        Vector3 _expAngularVelocity;

        List<RBCollider> _colliders;
        public int ColliderCount { get { return _colliders?.Count ?? 0; } }

        public Vector3 Velocity { get { return _velocity; } }
        public Vector3 AngularVelocity { get { return _angularVelocity; } }
        public Vector3 ExpVelocity { get { return _expVelocity; } set { _expVelocity = value; } }
        public Vector3 ExpAngularVelocity { get { return _expAngularVelocity; } set { _expAngularVelocity = value; } }

        public Vector3 SubtickVelocity { get { return _subtickVelocity; } set { _subtickVelocity = value; } }
        public Vector3 SubtickAngularVelocity { get { return _subtickAngularVelocity; } set { _subtickAngularVelocity = value; } }

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

        public int SleepCount { get { return _sleepCount; } }
        [SerializeField] int _sleepCount = 0;

        public bool useGravity = true;

        public bool sleepUntilInteraction;
        public bool setInfInertiaTensorOnInit;

        public InterpTrajectory interpTraj;

        [NonSerialized] public RBCollider[] colliding = new RBCollider[2];
        [NonSerialized] public int collidingCount = 0;

        public RBTrajectory ObjectTrajectory { get { return _objTrajectory; } }

        protected RBTrajectory _objTrajectory;

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
            if (!isSleeping || _sleepCount > 0)
            {
                isSleeping = false;
                _sleepCount = 0;
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
                c.UpdateTransform();
            }
        }

        protected override void ComponentOnDisable()
        {
            PhysComputer.RemoveRigidbody(this);

            if (_colliders == null) return;

            foreach (var c in _colliders)
            {
                PhysComputer.SwitchToCollider(c);
                c.UpdateTransform();
                c.ClearParentRigidbody();
            }

            _colliders.Clear();

            RecalculateInertiaTensor();
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
            _sleepCount = rb._sleepCount;
            useGravity = rb.useGravity;

            interpTraj = rb.interpTraj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBRigidbody()
        {
            _objTrajectory = new RBTrajectory();
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
            collider.UpdateTransform();

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
        public RBCollider GetCollider(int index)
        {
            return _colliders[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RBCollider> GetColliders()
        {
            if (_colliders != null)
            {
                foreach (var c in _colliders)
                {
                    yield return c;
                }
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetInvInertiaTensor(float multiplier = 1)
        {
            Vector3 i = inertiaTensor * inertiaTensorMultiplier * multiplier;
            Quaternion r = VTransform.WsRotation * inertiaTensorRotation;
            return Vector3.Scale(r * (Quaternion.Inverse(r) * V3Rcp(i)), _invInertiaWsScale);
        }

        internal virtual void ApplyTransform(float dt, TimeScaleMode physTimeScaleMode, VelocityOption velocityOption)
        {
            if (!_objTrajectory.IsIgnoredTrajectory)
            {
                float vm = _expVelocity.magnitude;
                float avm = _expAngularVelocity.magnitude;

                _expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm - drag);
                _expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm - angularDrag);

                //_expVelocity = (vm > 0 ? _expVelocity / vm : Vector3.zero) * Mathf.Max(0, vm + drag * DRAG_RETG_MULTIPLIER);
                //_expAngularVelocity = (avm > 0 ? _expAngularVelocity / avm : Vector3.zero) * Mathf.Max(0, avm + angularDrag * ANGULAR_DRAG_RETG_MULTIPLIER);

                _expVelocity = Vector3.ClampMagnitude(_expVelocity, velocityOption.velocity_max);
                _expAngularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, velocityOption.angularVelocity_max);

                _velocity = _expVelocity;
                _angularVelocity = _expAngularVelocity;

                PushInterpTraj(VTransform.WsPosition, VTransform.WsRotation);

                IntergradeWs(_frameWsPos, _frameWsRot, _velocity, _angularVelocity, dt, out var wsPos, out var wsRot);
                VTransform.SetWsPositionAndRotation(wsPos, wsRot);
            }
            else
            {
                _velocity = _expVelocity;
                _angularVelocity = _expAngularVelocity;
            }

            UpdateTransform();
            PushCCD(VTransform.WsPosition - _frameWsPos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal virtual void ApplySolverVelocity(SolverInfo info, float sdt, TimeScaleMode physTimeScalemode)
        {
            IntergradeWs(VTransform.WsPosition, VTransform.WsRotation, _subtickVelocity, _subtickAngularVelocity, sdt, out var wsPos, out var wsRot);
            VTransform.SetWsPositionAndRotation(wsPos, wsRot);

            _subtickVelocitySum += _subtickVelocity / info.solverSubtick;
            _subtickAngularVelocitySum += _subtickAngularVelocity / info.solverSubtick;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void PrepareSubtick()
        {
            _frameWsPos = VTransform.WsPosition;
            _frameWsRot = VTransform.WsRotation;

            _subtickVelocity = _expVelocity;
            _subtickAngularVelocity = _expAngularVelocity;

            _subtickVelocitySum = Vector3.zero;
            _subtickAngularVelocitySum = Vector3.zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ApplySubtick()
        {
            _expVelocity = _subtickVelocitySum;
            _expAngularVelocity = _subtickAngularVelocitySum;
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
        public void IntergradeWs(Vector3 wsPos, Quaternion wsRot, Vector3 vel, Vector3 angVel, float dt, out Vector3 wsPosOut, out Quaternion wsRotOut)
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
        internal virtual void UpdateTransform()
        {
            foreach (RBCollider c in _colliders)
            {
                c.UpdateTransform();
            }

            _objTrajectory.Update(this, VTransform.Layer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal virtual void PushCCD(Vector3 offset)
        {
            foreach (RBCollider c in _colliders)
            {
                c.PushCCD(offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OnCollision(RBCollider col, RBCollisionInfo info)
        {
            if (onCollision != null) onCollision(col, info);

            foreach (var c in _colliders)
            {
                if (c != null) c.OnCollision(col, info);
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
            _sleepCount = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysSleep()
        {
            _expVelocity = Vector3.zero;
            _expAngularVelocity = Vector3.zero;
            isSleeping = true;
            _sleepCount = SLEEP_GRACE_FRAMES;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void UpdatePhysSleepGrace()
        {
            if (IsExpUnderSleepLevel())
            {
                if (_sleepCount < SLEEP_GRACE_FRAMES)
                {
                    _sleepCount++;
                }
            }
            else
            {
                PhysAwake();
                _sleepCount = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void TryPhysSleep()
        {
            if (!isSleeping && _sleepCount >= SLEEP_GRACE_FRAMES)
            {
                int sMin = SLEEP_GRACE_FRAMES;

                for (int i = 0; i < collidingCount; i++)
                {
                    int sgf = SLEEP_GRACE_FRAMES;
                    if (colliding[i].ParentRigidbody != null) sgf = colliding[i].ParentRigidbody._sleepCount;
                    else if (colliding[i].Trajectory.activeStatic) sgf = 0;

                    sMin = Mathf.Min(sMin, sgf);
                }

                if (sMin >= SLEEP_GRACE_FRAMES)
                {
                    PhysSleep();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void TryPhysAwake()
        {
            if (isSleeping && !ObjectTrajectory.IsIgnoredTrajectory)
            {
                for (int i = 0; i < collidingCount; i++)
                {
                    var c = colliding[i];

                    if (c.ParentRigidbody != null)
                    {
                        if (!(c.ParentRigidbody.VEnabled && c.ParentRigidbody.IsStaticOrSleeping)) PhysAwake();
                        break;
                    }
                    else
                    {
                        if (c.Trajectory.activeStatic) PhysAwake();
                        break;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RBCollider> GetCollidings()
        {
            for (int i = 0; i < collidingCount; i++)
            {
                yield return colliding[i];
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

                            geometryIt.SetInertiaOBB(box.CalcOBB(), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Sphere:
                        {
                            Vector3 relPos = VTransform.InverseTransformPoint(c.GetColliderCenter());
                            geometryIt.SetInertiaSphere(c.CalcSphere(), relPos, Quaternion.identity);
                        }
                        break;

                    case RBGeometryType.Capsule:
                        {
                            var capsule = c as RBCapsuleCollider;

                            Vector3 relPos = VTransform.InverseTransformPoint(capsule.GetColliderCenter());
                            Quaternion relRot = (capsule.VTransform.WsRotation * capsule.LocalRot) * Quaternion.Inverse(VTransform.WsRotation);

                            geometryIt.SetInertiaCapsule(capsule.CalcCapsule(), relPos, relRot);
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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public struct VelocityOption
        {
            public float velocity_max;
            public float angularVelocity_max;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static VelocityOption GetDefault()
            {
                var option = new VelocityOption();

                option.velocity_max = RBPhysComputer.VELOCITY_MAX;
                option.angularVelocity_max = RBPhysComputer.ANG_VELOCITY_MAX;

                return option;
            }
        }
    }
}