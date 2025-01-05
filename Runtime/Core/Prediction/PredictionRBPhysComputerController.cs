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
                var vtBase = RBVirtualTransform.FindOrCreate(obj);
                var vt = RBVirtualTransform.FindOrCreate(obj, _physComputer, vtBase);

                foreach (var c in vComps)
                {
                    c.FindOrCreateVirtualComponent(vt);
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

            var vtBase = RBVirtualTransform.FindOrCreate(obj);
            var vt = RBVirtualTransform.FindOrCreate(obj, _physComputer, vtBase);

            var vComps = obj.GetComponents<RBVirtualComponent>();
            foreach (var c in vComps)
            {
                c.FindOrCreateVirtualComponent(vt);
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
            await Task.Run(async () =>
            {
                for (int i = 0; i < frames; i++)
                {
                    if (_physComputer == null || cxl.IsCancellationRequested) return;
                    await PhysicsFrame();
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        async Task PhysicsFrame()
        {
            await _physComputer.OpenPhysicsFrameWindowAsync();
            await _physComputer.ClosePhysicsFrameWindow();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncObjectTransforms()
        {
            if (_physComputer == null) throw new Exception();
            await _physComputer.SyncObjectTransformsAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncBaseVTransforms()
        {
            if (_physComputer == null) throw new Exception();
            await _physComputer.SyncBaseVTransformsAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task ApplyObjectTransforms()
        {
            if (_physComputer == null) throw new Exception();
            await _physComputer.ApplyObjectTransformsAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncBaseVComponents()
        {
            if (_physComputer == null) throw new Exception();
            await _physComputer.SyncBaseVComponentsAsync();
        }
    }
}