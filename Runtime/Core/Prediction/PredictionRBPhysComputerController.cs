using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class PredictionRBPhysComputerController
    {
        public RBPhysComputer PredictionComputer { get { return _predictionComputer; } }
        RBPhysComputer _predictionComputer;

        List<RBRigidbody> _rbRigidbody = new List<RBRigidbody>();
        List<RBCollider> _rbCols = new List<RBCollider>();
        List<RBPhysComputer.IStdSolverPrediction> _predSolvers = new List<RBPhysComputer.IStdSolverPrediction>();
        List<RBPhysComputer.IRBPhysObjectPrediction> _predPhysObjs = new List<RBPhysComputer.IRBPhysObjectPrediction>();

        List<RBRigidbody> _rbRigidbodyAddQueue = new List<RBRigidbody>();
        List<RBCollider> _rbColsAddQueue = new List<RBCollider>();
        List<RBPhysComputer.IStdSolverPrediction> _predSolversAddQueue = new List<RBPhysComputer.IStdSolverPrediction>();
        List<RBPhysComputer.IRBPhysObjectPrediction> _predPhysObjsAddQueue = new List<RBPhysComputer.IRBPhysObjectPrediction>();

        List<RBVirtualTransform> _vTransforms = new List<RBVirtualTransform>();

        public async Task ReInitializeAsync()
        {
            await _predictionComputer.WaitSemaphoreAsync(0);

            _predictionComputer.ReInitializeComputer();

            foreach (var vt in _vTransforms)
            {
                vt.ReInitialize();
            }

            foreach (var rb in _rbRigidbody)
            {
                var vRb = rb as RBRigidbodyVirtual;
                if (vRb)
                {
                    vRb.ReInitialize();
                }
            }

            foreach (var c in _rbCols)
            {
                if (c.GeometryType == RBGeometryType.OBB)
                {
                    var vOBB = c as RBBoxColliderVirtual;
                    if (vOBB != null)
                    {
                        vOBB.ReInitialize();
                    }
                }
                else if (c.GeometryType == RBGeometryType.Sphere)
                {
                    var vSphere = c as RBSphereColliderVirtual;
                    if (vSphere != null)
                    {
                        vSphere.ReInitialize();
                    }
                }
                else if (c.GeometryType == RBGeometryType.Capsule)
                {
                    var vCapsule = c as RBCapsuleColliderVirtual;
                    if (vCapsule != null)
                    {
                        vCapsule.ReInitialize();
                    }
                }
            }

            _predictionComputer.ReleaseSemaphore();
        }

        public void AddRigidbody(RBRigidbody rb)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.AddRigidbody(rb);
                if (!_rbRigidbody.Contains(rb)) _rbRigidbody.Add(rb);
            }
            else
            {
                _rbRigidbodyAddQueue.Add(rb);
            }
        }

        public void RemoveRigidbody(RBRigidbody rb)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.RemoveRigidbody(rb);
                _rbRigidbody.Remove(rb);
            }
            else
            {
                _rbRigidbodyAddQueue.Remove(rb);
            }
        }

        public void AddCollider(RBCollider c)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.AddCollider(c);
                if (!_rbCols.Contains(c)) _rbCols.Add(c);
            }
            else
            {
                _rbColsAddQueue.Add(c);
            }
        }

        public void RemoveCollider(RBCollider c)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.RemoveCollider(c);
                _rbCols.Remove(c);
            }
            else
            {
                _rbColsAddQueue.Remove(c);
            }
        }

        public void AddSolverPrediction(RBPhysComputer.IStdSolverPrediction predSolver)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.AddStdSolverPredication(predSolver);
                _predSolvers.Add(predSolver);
            }
            else
            {
                _predSolversAddQueue.Add(predSolver);
            }
        }

        public void RemoveSolverPrediction(RBPhysComputer.IStdSolverPrediction predSolver)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.RemoveStdSolverPredication(predSolver);
                _predSolvers.Remove(predSolver);
            }
            else
            {
                _predSolversAddQueue.Remove(predSolver);
            }
        }

        public void AddPhysObjectPrediction(RBPhysComputer.IRBPhysObjectPrediction predObj)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.AddPhysObjectPrediction(predObj);
                _predPhysObjs.Add(predObj);
            }
            else
            {
                _predPhysObjsAddQueue.Add(predObj);
            }
        }

        public void RemovePhysObjectPrediction(RBPhysComputer.IRBPhysObjectPrediction predObj)
        {
            if (_predictionComputer != null)
            {
                _predictionComputer.RemovePhysObjectPrediction(predObj);
                _predPhysObjs.Remove(predObj);
            }
            else
            {
                _predPhysObjsAddQueue.Remove(predObj);
            }
        }

        public RBVirtualTransform CreateVirtual(GameObject obj, RBVirtualTransform vParent, bool recursive = false)
        {
            GameObject vObj = new GameObject();
            vObj.transform.parent = vParent?.transform;
            vObj.name = "rbVTransform";
            var vTransform = vObj.AddComponent<RBVirtualTransform>();
            vTransform.Initialize(_predictionComputer, obj, vParent);
            _vTransforms.Add(vTransform);

            if (obj.TryGetComponent(out RBRigidbody r))
            {
                var vRb = r.CreateVirtual(vTransform);
                AddRigidbody(vRb);
            }

            foreach (var c in obj.GetComponents<RBCollider>())
            {
                if (c.GeometryType == RBGeometryType.OBB)
                {
                    var obb = c as RBBoxCollider;
                    var vObb = obb.CreateVirtual(vTransform);
                    AddCollider(vObb);
                }
                else if (c.GeometryType == RBGeometryType.Sphere)
                {
                    var sphere = c as RBSphereCollider;
                    var vSphere = sphere.CreateVirtual(vTransform);
                    AddCollider(vSphere);
                }
                else if (c.GeometryType == RBGeometryType.Capsule)
                {
                    var capusle = c as RBCapsuleCollider;
                    var vCapsule = capusle.CreateVirtual(vTransform);
                    AddCollider(vCapsule);
                }
            }

            if (recursive)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    var childObj = obj.transform.GetChild(i);
                    if (childObj != null)
                    {
                        var vChildTransform = CreateVirtual(childObj.gameObject, vTransform, true);
                        vTransform.AddChildren(vChildTransform);
                    }
                }
            }

            return vTransform;
        }

        public void InitPrediction(float deltaTime)
        {
            CreatePrediction(deltaTime);
            InitPredictionComputer();
        }

        public async Task InitIntergradePrediction(int frames)
        {
            if (_predictionComputer != null)
            {
                await IntergradeComputeFor(frames);
            }
        }

        public void CreatePrediction(float deltaTime)
        {
            _predictionComputer = new RBPhysComputer(true, deltaTime);
        }

        public void InitPredictionComputer()
        {
            if (_predictionComputer == null) return;

            foreach (var r in _rbRigidbodyAddQueue)
            {
                _predictionComputer.AddRigidbody(r);
            }
            _rbRigidbodyAddQueue.Clear();

            foreach (var c in _rbColsAddQueue)
            {
                _predictionComputer.AddCollider(c);
            }
            _rbColsAddQueue.Clear();
        }

        public async Task IntergradeComputeFor(int frameOffset)
        {
            await IntergradePredictionComputer(frameOffset);
        }

        public async Task IntergradePredictionComputer(int frameOffset)
        {
            for (int i = 0; i < frameOffset; i++)
            {
                await ComputePredictAsync();
            }
        }

        public async Task ComputePredictAsync()
        {
            await PhysicsFrame();
        }

        public async Task PhysicsFrame()
        {
            await Task.Run(() =>
            {
                _predictionComputer.OpenPhysicsFrameWindowAsync();
                _predictionComputer.ClosePhysicsFrameWindow();
            }).ConfigureAwait(false);
        }

        public void DisposePredictionComputer()
        {
            _predictionComputer?.Dispose();
        }
    }
}