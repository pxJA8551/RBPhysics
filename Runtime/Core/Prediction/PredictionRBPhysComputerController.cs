using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class PredictionRBPhysComputerController
    {
        public RBPhysComputer PhysComputer { get { return _physComputer; } }
        RBPhysComputer _physComputer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CreatePhysComputer(float deltaTime)
        {
            _physComputer = new RBPhysComputer(deltaTime, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DisposePhysComputer()
        {
            _physComputer?.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CreateVirtual(GameObject obj, bool recursive)
        {
            if (_physComputer == null) throw new Exception();

            var vComps = obj.GetComponents<RBVirtualComponent>();

            if (vComps.Any())
            {
                var vt = RBVirtualTransform.FindOrCreate(obj, _physComputer);

                foreach (var c in vComps)
                {
                    c.CreateVirtualComponent(vt);
                }
            }

            if (recursive)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    var cg = obj.transform.GetChild(i);
                    CreateVirtual(cg.gameObject, recursive);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBVirtualTransform CreateVirtualTransform(GameObject obj)
        {
            if (_physComputer == null) throw new Exception();

            var vt = RBVirtualTransform.FindOrCreate(obj, _physComputer);

            var vComps = obj.GetComponents<RBVirtualComponent>();
            foreach (var c in vComps)
            {
                c.CreateVirtualComponent(vt);
            }

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                var cg = obj.transform.GetChild(i);
                CreateVirtual(cg.gameObject, true);
            }

            return vt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void PhysicsFrame()
        {
            _physComputer.OpenPhysicsFrameWindowAsync().Wait();
            _physComputer.ClosePhysicsFrameWindow();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SyncObjectTransforms()
        {
            if (_physComputer == null) throw new Exception();
            _physComputer.SyncObjectTransforms();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SyncBaseVTransforms()
        {
            if (_physComputer == null) throw new Exception();
            _physComputer.SyncBaseVTransforms();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SyncBaseVComponents()
        {
            if (_physComputer == null) throw new Exception();
            _physComputer.SyncBaseVComponents();
        }
    }
}