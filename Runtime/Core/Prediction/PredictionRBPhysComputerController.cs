using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class PredictionRBPhysComputerController
    {
        public RBPhysComputer PhysComputer { get { return _physComputer; } }
        RBPhysComputer _physComputer;

        public void CreatePhysComputer(float deltaTime)
        {
            _physComputer = new RBPhysComputer(deltaTime, true);
        }

        public void DisposePhysComputer()
        {
            _physComputer?.Dispose();
        }

        public RBVirtualTransform CreateVirtual(GameObject obj, bool recursive)
        {
            if (_physComputer == null) throw new Exception();

            var vt = RBVirtualTransform.FindOrCreate(obj, _physComputer);

            var vComps = obj.GetComponents<RBVirtualComponent>();
            foreach (var c in vComps) 
            {
                c.CreateVirtualComponent(vt);
            }

            if (recursive)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    var cg = obj.transform.GetChild(i);
                    CreateVirtual(cg.gameObject, recursive);
                }
            }

            return vt; 
        }

        public async Task IntergradeComputeFor(int frames, CancellationToken cxl)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < frames; i++)
                {
                    if (_physComputer == null || cxl.IsCancellationRequested) return;
                    PhysicsFrame();
                }
            });
        }

        void PhysicsFrame()
        {
            _physComputer.OpenPhysicsFrameWindowAsync().Wait();
            _physComputer.ClosePhysicsFrameWindow();
        }

        public void SyncVirtual()
        {
            if (_physComputer == null) throw new Exception();
            _physComputer.SyncObjectTransforms();
        }
    }
}