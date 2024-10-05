using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class PredictionRBPhysComputerController
    {
        const int PREDICTION_FRAMES_COUNT = 200; // dt = .01f
        const float PREDICTION_DELTATIME = .05f;

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

            if (vObj.TryGetComponent(out RBRigidbody r))
            {
                r.CreateVirtual(vTransform);
            }

            foreach (var c in vObj.GetComponents<RBCollider>())
            {
                if (c.GeometryType == RBGeometryType.OBB)
                {
                    var obb = c as RBBoxCollider;
                    obb.CreateVirtual(vTransform);
                }
                else if (c.GeometryType == RBGeometryType.Sphere)
                {
                    var obb = c as RBSphereCollider;
                    obb.CreateVirtual(vTransform);
                }
                else if (c.GeometryType == RBGeometryType.Capsule)
                {
                    var obb = c as RBCapsuleCollider;
                    obb.CreateVirtual(vTransform);
                }
            }

            if (recursive)
            {
                for (int i = 0; i < vObj.transform.childCount; i++)
                {
                    var childObj = vObj.transform.GetChild(i);
                    if (childObj != null)
                    {
                        var vChildTransform = CreateVirtual(childObj.gameObject, vTransform, true);
                        vTransform.AddChildren(vChildTransform);
                    }
                }
            }

            return vTransform;
        }

        public void InitPrediction()
        {
            CreatePrediction();
            InitPredictionComputer();
        }

        public async Task InitIntergradePrediction()
        {
            if (_predictionComputer != null)
            {
                await IntergradeComputeFor(PREDICTION_FRAMES_COUNT);
            }
        }

        public void CreatePrediction()
        {
            _predictionComputer = new RBPhysComputer(true, PREDICTION_DELTATIME);
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
                _predictionComputer.OpenPhysicsFrameWindow();
                _predictionComputer.ClosePhysicsFrameWindow();
            });
        }

        public void DisposePredictionComputer()
        {
            _predictionComputer?.Dispose();
        }
    }
}