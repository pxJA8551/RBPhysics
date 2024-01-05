using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;

namespace RBPhys
{
    public class RBRigidbody : MonoBehaviour
    {
        public float mass;
        Vector3 inertiaTensor;
        Quaternion inertiaTensorRotation;

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

        void Awake()
        {
            RBPhysCore.AddRigidbody(this);
            FindColliders();
            UpdateTransform();
        }

        void OnDestroy()
        {
            RBPhysCore.RemoveRigidbody(this);
            ReleaseColliders();
        }

        void ChangeVelocity(Vector3 velocity)
        {

        }

        void ChangeAngularVelocity(Vector3 velocity)
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

        void UpdateTransform()
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }

        public void ApplyTransform(float dt)
        {
            _velocity = _expVelocity;
            _angularVelocity = _expAngularVelocity;

            transform.position = Position + (_velocity * dt);
            transform.rotation = Rotation * Quaternion.AngleAxis(_angularVelocity.magnitude * Mathf.Rad2Deg * dt, _angularVelocity.normalized);

            UpdateTransform();
        }
    }
}