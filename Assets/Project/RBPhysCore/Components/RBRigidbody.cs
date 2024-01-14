using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RBPhys.RBPhysUtil;

namespace RBPhys
{
    public class RBRigidbody : MonoBehaviour
    {
        public float mass;
        public Vector3 inertiaTensor;
        public Quaternion inertiaTensorRotation;

        public float InverseMass { get { return 1 / mass; } }

        public Vector3 InverseInertia { get { return V3Rcp(inertiaTensor); } }
        public Vector3 InverseInertiaWs { get { return inertiaTensorRotation * V3Rcp(inertiaTensor); } }

        Vector3 _centerOfGravity;

        Vector3 _velocity;
        Vector3 _angularVelocity;
        Vector3 _expVelocity;
        Vector3 _expAngularVelocity;
        Vector3 _position;
        Quaternion _rotation;

        RBCollider[] _colliders;

        [HideInInspector] public Vector3 Velocity { get { return _velocity; } }
        [HideInInspector] public Vector3 AngularVelocity { get { return _angularVelocity; } }
        [HideInInspector] public Vector3 ExpVelocity { get { return _expVelocity; } set { _expVelocity = value; } }
        [HideInInspector] public Vector3 ExpAngularVelocity { get { return _expAngularVelocity; } set { _expAngularVelocity = value; } }
        [HideInInspector] public Vector3 Position { get { return _position; } set { _position = value; } }
        [HideInInspector] public Quaternion Rotation { get { return _rotation; } set { _rotation = value; } }

        [HideInInspector] public Vector3 CenterOfGravity { get { return _centerOfGravity; } set { _centerOfGravity = value; } }
        [HideInInspector] public Vector3 CenterOfGravityWorld { get { return Position + Rotation * _centerOfGravity; } }

        void Awake()
        {
            RBPhysCore.AddRigidbody(this);
            FindColliders();
            UpdateTransform();
            RecalculateInertiaTensor();
        }

        void OnDestroy()
        {
            RBPhysCore.RemoveRigidbody(this);
            ReleaseColliders();
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

        public void UpdateTransform()
        {
            Position = transform.position;
            Rotation = transform.rotation;

            foreach (RBCollider c in _colliders)
            {
                c.UpdateTransform();
            }
        }

        public void ApplyTransform(float dt)
        {
            _velocity = _expVelocity;
            _angularVelocity = _expAngularVelocity;

            transform.position = Position + (_velocity * dt);
            transform.rotation = Rotation * Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized);

            UpdateTransform();
        }

        public void RecalculateInertiaTensor()
        {
            
        }
    }
}