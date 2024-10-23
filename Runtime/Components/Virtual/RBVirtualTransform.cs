using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class RBVirtualTransform : MonoBehaviour
    {
        public RBPhysComputer physComputer { get { return _physComputer; } }
        RBPhysComputer _physComputer;

        public Vector3 Position { get { return _position; } set { SetPosition(value); } }
        Vector3 _position;

        public Quaternion Rotation { get { return _rotation; } set { SetRotation(value); } }
        Quaternion _rotation;

        public bool Active { get { return _active; } }
        bool _active = true;

        public GameObject baseObj { get { return _baseObj; } }
        GameObject _baseObj;

        public RBVirtualTransform parent { get { return _parent; } }
        RBVirtualTransform _parent;
        Vector3 offsetPos;
        Quaternion offsetRot;

        List<RBVirtualTransform> _children = new List<RBVirtualTransform>();
        
        public int ChildCount { get { return _children?.Count ?? 0; } }

        public int layer { get { return _layer; } }
        int _layer;

        public T AddCollider<T>() where T : RBCollider
        {
            if (typeof(T) != typeof(RBBoxColliderVirtual) && typeof(T) != typeof(RBSphereColliderVirtual) && typeof(T) != typeof(RBCapsuleColliderVirtual)) throw new NotImplementedException();

            var c = gameObject.AddComponent<T>();
            return c;
        }

        public RBRigidbodyVirtual AddRigidbody()
        {
            var r = gameObject.AddComponent<RBRigidbodyVirtual>();
            return r;
        }

        public RBPhysAnimationVirtual AddPhysAnimation()
        {
            var a = gameObject.AddComponent<RBPhysAnimationVirtual>();
            return a;
        }

        public void Initialize(RBPhysComputer physComputer, GameObject baseObj, RBVirtualTransform parent)
        {
            _physComputer = physComputer;
            _baseObj = baseObj;

            var t = baseObj?.transform;
            if (t != null)
            {
                _position = t.position;
                _rotation = t.rotation;
                _layer = baseObj?.layer ?? 0;
                _active = baseObj.activeSelf;
            }

            _parent = parent;
        }

        public void ReInitialize()
        {
            var t = _baseObj.transform;
            if (t != null)
            {
                _position = t.position;
                _rotation = t.rotation;
                _layer = baseObj?.layer ?? 0;
                _active = baseObj.activeSelf;
            }
            else
            {
                Destroy(this);
            }
        }

        void UpdateGameObjectTransform()
        {
            if (_parent != null)
            {
                transform.position = _parent._position + offsetPos;
                transform.rotation = offsetRot * _parent._rotation;
            }
            else
            {
                transform.position = _position;
                transform.rotation = _rotation;
            }

            foreach (var c in _children)
            {
                c.UpdateGameObjectTransform();
            }
        }

        public void AddChildren(RBVirtualTransform vTransform)
        {
            if (!_children.Contains(vTransform))
            {
                _children.Add(vTransform);
            }
        }

        public void RemoveChildren(RBVirtualTransform vTransform)
        {
            _children.Remove(vTransform);
        }

        public void SetPosition(Vector3 pos)
        {
            _position = pos;
        }

        public void SetRotation(Quaternion rot)
        {
            _rotation = rot;
        }

        public bool Validate()
        {
            return physComputer != null;
        }

        public Vector3 TransformPoint(Vector3 posLs)
        {
            return Matrix4x4.TRS(Position, Rotation, Vector3.one).MultiplyPoint3x4(posLs);
        }

        public Vector3 InverseTransformPoint(Vector3 posWs)
        {
            return Matrix4x4.TRS(Position, Rotation, Vector3.one).inverse.MultiplyPoint3x4(posWs);
        }
    }
}
