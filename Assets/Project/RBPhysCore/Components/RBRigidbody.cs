using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbody : MonoBehaviour
    {
        const float SLEEP_VEL_MAX_SQRT = 0.1f * 0.1f;
        const float SLEEP_ANGVEL_MAX_SQRT = 0.3f * 0.3f;
        const int SLEEP_GRACE_FRAMES = 5;

        public float mass;
        [HideInInspector] public Vector3 inertiaTensor;
        [HideInInspector] public Quaternion inertiaTensorRotation;

        Vector3 _centerOfGravity;

        Vector3 _velocity;
        Vector3 _angularVelocity;
        Vector3 _expVelocity;
        Vector3 _expAngularVelocity;
        Vector3 _position;
        Quaternion _rotation;

        RBCollider[] _colliders;

        public Vector3 Velocity { get { return _velocity; } }
        public Vector3 AngularVelocity { get { return _angularVelocity; } }
        public Vector3 ExpVelocity { get { return _expVelocity; } set { _expVelocity = value; } }
        public Vector3 ExpAngularVelocity { get { return _expAngularVelocity; } set { _expAngularVelocity = value; } }
        public Vector3 Position { get { return _position; } set { _position = value; } }
        public Quaternion Rotation { get { return _rotation; } set { _rotation = value; } }

        public Vector3 CenterOfGravity { get { return _centerOfGravity; } set { _centerOfGravity = value; } }
        public Vector3 CenterOfGravityWorld { get { return Position + Rotation * _centerOfGravity; } }

        public float InverseMass { get { return 1 / mass; } }

        public bool isSleeping = false;
        public int sleepGrace = 0;

        [HideInInspector] public RBCollider[] colliding = new RBCollider[2];
        [HideInInspector] public int collidingCount = 0;

        public RBTrajectory ExpObjectTrajectory { get { return _expObjTrajectory; } }

        RBTrajectory _expObjTrajectory;

        /// <summary>
        /// 標準・優先物理ソルバーの実行前
        /// </summary>
        public delegate void BeforeSolverDelegate(RBRigidbody rb);

        /// <summary>
        /// 標準・優先物理ソルバーを全て試行完了した後で実行
        /// </summary>
        public delegate void AfterSolverDelegate(RBRigidbody rb);

        /// <summary>
        /// 標準物理ソルバーの初期化
        /// </summary>
        public delegate void StdInitDelegate(RBRigidbody rb);

        /// <summary>
        /// 標準物理ソルバー試行
        /// </summary>
        public delegate void StdResolveDelegate(RBRigidbody rb, int iterationCount);

        /// <summary>
        /// 優先物理ソルバーの初期化（標準物理ソルバーを全て試行完了した後で実行）
        /// </summary>
        public delegate void PriorInitDelegate(RBRigidbody rb);

        /// <summary>
        /// 優先物理ソルバー試行（標準物理ソルバーを全て試行完了した後で実行）
        /// </summary>
        public delegate void PriorResolveDelegate(RBRigidbody rb, int iterationCount);

        public BeforeSolverDelegate ds_beforeSolver;
        public BeforeSolverDelegate ds_afterSolver;
        public StdInitDelegate ds_stdSolverInit;
        public StdResolveDelegate ds_stdSolverIter;
        public PriorInitDelegate ds_priorSolverInit;
        public PriorResolveDelegate ds_priorSolverIter;

        public Vector3 InverseInertiaWs
        {
            get
            {
                Vector3 i = inertiaTensor;
                Quaternion r = Rotation * inertiaTensorRotation;
                return r * (Quaternion.Inverse(r) * V3Rcp(i));
            }
        }

        void Awake()
        {
            FindColliders();
            UpdateTransform();
            RecalculateInertiaTensor();
            isSleeping = false;
        }

        void OnDestroy()
        {
            ReleaseColliders();
        }

        void OnEnable()
        {
            RBPhysCore.AddRigidbody(this);

            foreach (var c in _colliders)
            {
                RBPhysCore.SwitchToRigidbody(c);
            }
        }

        private void OnDisable()
        {
            RBPhysCore.RemoveRigidbody(this);

            foreach (var c in _colliders)
            {
                RBPhysCore.SwitchToCollider(c);
            }
        }

        private void FixedUpdate() { }

        internal void BeforeSolver()
        {
            if (ds_beforeSolver != null)
            {
                ds_beforeSolver(this);
            }
        }

        internal void AfterSolver()
        {
            if (ds_afterSolver != null)
            {
                ds_afterSolver(this);
            }
        }

        internal void OnStdSolverInitialization()
        {
            if (ds_stdSolverInit != null)
            {
                ds_stdSolverInit(this);
            }
        }

        internal void OnStdSolverIteration(int iterationCount)
        {
            if (ds_stdSolverIter != null)
            {
                ds_stdSolverIter(this, iterationCount);
            }
        }

        internal void OnPriorSolverInitialization()
        {
            if (ds_priorSolverInit != null)
            {
                ds_priorSolverInit(this);
            }
        }

        internal void OnPriorSolverIteration(int iterationCount)
        {
            if (ds_priorSolverIter != null)
            {
                ds_priorSolverIter(this, iterationCount);
            }
        }

        public RBRigidbody()
        {
            _expObjTrajectory = new RBTrajectory();
        }

        void ChangeVelocity(Vector3 velocity, int solverIteration = 5)
        {

        }

        void ChangeAngularVelocity(Vector3 velocity, int solverIteration = 5)
        {

        }

        void FindColliders()
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

        internal void ApplyTransform(float dt)
        {
            _velocity = Vector3.ClampMagnitude(_expVelocity, Mathf.Max(0, _expVelocity.magnitude - 0.03f));
            _angularVelocity = Vector3.ClampMagnitude(_expAngularVelocity, Mathf.Max(0, _expAngularVelocity.magnitude - 0.07f));

            transform.position = _position + (_velocity * dt);
            transform.rotation = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized) * _rotation;

            UpdateTransform();
            UpdateExpTrajectory(dt);
        }

        internal void UpdateTransform(bool updateColliders = true)
        {
            Position = transform.position;
            Rotation = transform.rotation;

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateTransform();
                }
            }
        }

        internal void UpdateExpTrajectory(float dt, bool updateColliders = true)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            if (updateColliders)
            {
                foreach (RBCollider c in _colliders)
                {
                    c.UpdateExpTrajectory(Position, Rotation, r.pos, r.rot);
                }
            }

            _expObjTrajectory.Update(this, dt);
        }

        internal void UpdateColliderExpTrajectory(float dt)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            for(int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].UpdateExpTrajectory(Position, Rotation, r.pos, r.rot);
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
                if (sleepGrace < SLEEP_GRACE_FRAMES) 
                {
                    sleepGrace++;
                }
            }
            else
            {
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
                            geometryIt.SetInertiaOBB(c.CalcOBB(), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Sphere:
                        {
                            geometryIt.SetInertiaSphere(c.CalcSphere(), relPos, relRot);
                        }
                        break;

                    case RBGeometryType.Capsule:
                        {
                            geometryIt.SetInertiaCapsule(c.CalcCapsule(), relPos, relRot);
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
        }
    }
}