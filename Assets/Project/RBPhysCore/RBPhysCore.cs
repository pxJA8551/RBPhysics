using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysCore
    {
        static List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        static List<RBCollider> _colliders = new List<RBCollider>();

        static RBTrajectory[] _activeTrajectories;
        static RBTrajectory[] _staticTrajectories;

        static RBTrajectory[] _trajectories_orderByXMin;

        public static void AddRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Add(rb);
        }

        public static void RemoveRigidbody(RBRigidbody rb)
        {
            _rigidbodies.Remove(rb);
        }

        public static void AddCollider(RBCollider c)
        {
            _colliders.Add(c);
        }

        public static void RemoveCollider(RBCollider c)
        {
            _colliders.Remove(c);
        }

        public static void SimulateFixedStep(float dt)
        {
            // ====== 物理フレームウインドウ ここまで ======

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplyTransform();
            }

            // ====== 物理フレームウインドウ ここから ======

            //動的・静的軌道計算

            if (_activeTrajectories.Length != _rigidbodies.Count) 
            {
                _activeTrajectories = new RBTrajectory[_rigidbodies.Count];
            }

            if (_staticTrajectories.Length != _colliders.Count) 
            {
                _staticTrajectories = new RBTrajectory[_colliders.Count];
            }

            for (int i = 0; i < _rigidbodies.Count; i++)
            {
                _activeTrajectories[i] = new RBTrajectory(_rigidbodies[i], dt);
            }

            for (int i = 0; i < _colliders.Count; i++)
            {
                if (_colliders[i].GetParentRigidbody() == null)
                {
                    _activeTrajectories[i] = new RBTrajectory(_colliders[i]);
                }
                else
                {
                    _activeTrajectories[i] = new RBTrajectory();
                }
            }

            //衝突検知（ブロードフェーズ）

            List<(RBTrajectory, RBTrajectory)> collideInNextFrame = new List<(RBTrajectory, RBTrajectory)>();

            {
                if (_trajectories_orderByXMin.Length != _activeTrajectories.Length + _staticTrajectories.Length)
                {

                }

                //AABBのx最小値で昇順ソート
                _trajectories_orderByXMin = _activeTrajectories;
                _trajectories_orderByXMin.AddRange(_staticTrajectories);
                _trajectories_orderByXMin.OrderBy(item => item.trajectoryAABB.GetMin().x);

                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    RBTrajectory activeTraj = _trajectories_orderByXMin[i];

                    if (activeTraj.isValidTrajectory && !activeTraj.isStatic) 
                    {
                        float x_min = activeTraj.trajectoryAABB.GetMin().x;
                        float x_max = activeTraj.trajectoryAABB.GetMax().x;

                        for (int j = i + 1; j < _trajectories_orderByXMin.Length; j++)
                        {
                            RBTrajectory targetTraj = _trajectories_orderByXMin[j];

                            if (targetTraj.isValidTrajectory)
                            {
                                float x_min_target = targetTraj.trajectoryAABB.GetMin().x;
                                float x_max_target = targetTraj.trajectoryAABB.GetMax().x;

                                if (x_max < x_min_target)
                                {
                                    break;
                                }

                                if (RBPhysUtil.RangeOverlap(x_min, x_max, x_min_target, x_max_target))
                                {
                                    collideInNextFrame.Add((activeTraj, targetTraj));
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < collideInNextFrame.Count; i++)
                {
                    (RBTrajectory, RBTrajectory) trajPair = collideInNextFrame[i];

                    if (!trajPair.Item1.trajectoryAABB.OverlapAABB(trajPair.Item2.trajectoryAABB))
                    {
                        collideInNextFrame.RemoveAt(i);
                    }
                }
            }

            //衝突検知（ナローフェーズ）

            {
                foreach (var trajPair in collideInNextFrame)
                {

                }
            }
        }

        static void VerifyVelocity(RBRigidbody rb)
        {

        }
    }

    public abstract class RBCollider : MonoBehaviour
    {
        RBRigidbody _parent;
        RBColliderDetailType detailType;

        public RBRigidbody ParentRigidbody { get { return _parent; } }

        void Awake()
        {
            RBPhysCore.AddCollider(this);
        }

        void OnDestroy()
        {
            RBPhysCore.RemoveCollider(this);
        }

        public void SetParentRigidbody(RBRigidbody r)
        {
            if (r != null)
            {
                _parent = r;
            }
        }

        public void ClearParentRigidbody()
        {
            _parent = null;
        }

        public RBRigidbody GetParentRigidbody()
        {
            return _parent;
        }

        public abstract RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot);
        public abstract RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot);
        public abstract RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot);
    }

    public struct RBColliderAABB
    {
        public bool isValidAABB;
        public Vector3 Center { get; private set; }
        public Vector3 Size { get ; private set; }
        public Vector3 Extents { get { return Size / 2f; } }

        public RBColliderAABB(Vector3 center, Vector3 size)
        {
            isValidAABB = true;
            this.Center = center;
            this.Size = RBPhysUtil.V3Abs(size);
        }

        public Vector3 GetMin()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Min(p, q);
        }

        public Vector3 GetMax()
        {
            Vector3 p = Center - Extents;
            Vector3 q = Center + Extents;
            return Vector3.Max(p, q);
        }

        public void Encapsulate(Vector3 point)
        {
            if (isValidAABB)
            {
                if (!ContainsPoint(point))
                {
                    Vector3 res_min = Vector3.Min(GetMin(), point);
                    Vector3 res_max = Vector3.Max(GetMax(), point);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;
                }
            }
            else
            {
                Center = point;
                Size = Vector3.zero;
            }
        }
        
        public void Encapsulate(RBColliderAABB aabb)
        {
            if (aabb.isValidAABB)
            {
                if (isValidAABB)
                {
                    Vector3 res_min = Vector3.Min(GetMin(), aabb.GetMin());
                    Vector3 res_max = Vector3.Max(GetMax(), aabb.GetMax());

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;
                }
                else
                {
                    Center = aabb.Center;
                    Size = aabb.Size;
                }
            }
        }

        public bool ContainsPoint(Vector3 point)
        {
            return isValidAABB && RBPhysUtil.IsV3Less(GetMin(), point) && RBPhysUtil.IsV3Less(point, GetMax());
        }

        public bool OverlapAABB(RBColliderAABB ext)
        {
            if (isValidAABB && ext.isValidAABB)
            {
                Vector3 min = GetMin();
                Vector3 max = GetMax();
                Vector3 extMin = ext.GetMin();
                Vector3 extMax = ext.GetMax();

                if (!RBPhysUtil.RangeOverlap(min.x, max.x, extMin.x, extMax.x)) return false;
                if (!RBPhysUtil.RangeOverlap(min.y, max.y, extMin.y, extMax.y)) return false;
                if (!RBPhysUtil.RangeOverlap(min.z, max.z, extMin.z, extMax.z)) return false;

                return true;
            }

            return false;
        }
    }

    public struct RBColliderOBB
    {
        public Vector3 pos;
        public Vector3 dirNormal;
        public Vector3 size;
        public bool isValidOBB;

        public RBColliderOBB(Vector3 pos, Vector3 dirNormal, Vector3 size)
        {
            this.pos = pos;
            this.dirNormal = dirNormal;
            this.size = size;
            isValidOBB = true;
        }
    }

    public struct RBColliderSphere
    {
        public Vector3 pos;
        public float radius;
        public bool isValidSphere;

        public RBColliderSphere(Vector3 pos, float radius)
        {
            this.pos = pos;
            this.radius = radius;
            isValidSphere = true;
        }
    }

    public enum RBColliderDetailType
    {
        OBB,
        Sphere,
        Mesh,
        AABB
    }

    public struct RBTrajectory
    {
        public RBColliderAABB trajectoryAABB;

        public bool isValidTrajectory;

        public readonly RBRigidbody rigidbody;
        public readonly bool isStatic;

        public RBTrajectory(RBRigidbody rigidbody, float dt)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            RBPhysUtil.PredictPosRot(rigidbody, out Vector3 nextPos, out Quaternion nextRot, dt);

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.isActiveAndEnabled)
                {
                    aabb.Encapsulate(c.CalcAABB(nextPos, nextRot));
                }
            }

            trajectoryAABB = aabb;
            this.rigidbody = rigidbody;
            isStatic = false;
            isValidTrajectory = true;
        }

        public RBTrajectory(RBCollider collider)
        {
            trajectoryAABB = collider.CalcAABB(collider.transform.position, collider.transform.rotation);
            rigidbody = null;
            isStatic = true;
            isValidTrajectory = true;
        }
    }
}