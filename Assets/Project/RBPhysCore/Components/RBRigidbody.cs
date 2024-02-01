using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbody : MonoBehaviour
    {
        const float SLEEP_VEL_MAX_SQRT = 0.01f * 0.01f;
        const float SLEEP_ANGVEL_MAX_SQRT = 0.1f * 0.1f;

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

        Guid _guid;

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

        public Guid Guid { get { return _guid; } }

        public RBTrajectory ExpObjectTrajectory { get { return _expObjTrajectory; } }

        RBTrajectory _expObjTrajectory;

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
            _guid = Guid.NewGuid();
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

        public void GetRigidbodyAABB()
        {

        }

        public void ApplyTransform(float dt)
        {
            _velocity = _expVelocity;
            _angularVelocity = _expAngularVelocity;

            transform.position = _position + (_velocity * dt);
            transform.rotation = Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized) * _rotation;

            UpdateTransform();
            UpdateExpTrajectory(dt);

            if (IsUnderSleepLevel())
            {
                //PhysSleep();
                //_expVelocity = Vector3.zero;
                //_expAngularVelocity = Vector3.zero;
            }
            else
            {
                PhysAwake();
            }
        }

        public void UpdateTransform(bool updateColliders = true)
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

        public void UpdateExpTrajectory(float dt, bool updateColliders = true)
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

        public void UpdateColliderExpTrajectory(float dt)
        {
            (Vector3 pos, Quaternion rot) r = GetIntergrated(dt);

            foreach (RBCollider c in _colliders)
            {
                c.UpdateExpTrajectory(Position, Rotation, r.pos, r.rot);
            }
        }

        public (Vector3 pos, Quaternion rot) GetIntergrated(float dt)
        {
            return (_position + _expVelocity * dt, Quaternion.AngleAxis(_expAngularVelocity.magnitude * Mathf.Rad2Deg * dt, _expAngularVelocity.normalized) * _rotation);
        }

        public bool IsUnderSleepLevel()
        {
            return _velocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _angularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT;
        }

        public bool IsExpUnderSleepLevel()
        {
            return _expVelocity.sqrMagnitude < SLEEP_VEL_MAX_SQRT && _expAngularVelocity.sqrMagnitude < SLEEP_ANGVEL_MAX_SQRT;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysAwake()
        {
            isSleeping = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PhysSleep()
        {
            isSleeping = true;
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