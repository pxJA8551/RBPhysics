using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        SemaphoreSlim _mainPhysLoopSemaphore = new SemaphoreSlim(1, 1);

        [HideInInspector][NonSerialized] public bool enableStats = false;

        public RBPhysStats Stats { get { return _stats; } }
        RBPhysStats _stats;

        private void Awake()
        {
            Debug.Log(string.Format("CPU: {0} / {1}cores", SystemInfo.processorType, SystemInfo.processorCount));
            Debug.Log(string.Format("GPU: {0} / {1}MB API: {2}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize, SystemInfo.graphicsDeviceType));

            Application.targetFrameRate = -1;
        }

        async void FixedUpdate()
        {
            await _mainPhysLoopSemaphore.WaitAsync();

            try
            {
                await PhysicsFrameAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                _mainPhysLoopSemaphore.Release();
            }
        }

        async Task PhysicsFrameAsync()
        {
            await RBPhysController.MainComputer.PhysicsFrameAsync();

            if (this == null) return;
            StartCoroutine(WaitForFixedUpdate());

            await RBPhysController.MainComputer.ApplyObjectTransformsAsync();

            if (enableStats)
            {
                var pcStats = await RBPhysController.MainComputer.GetStatsAsync();

                lock (_stats)
                {
                    _stats.CopyStats(pcStats);
                }
            }
        }

        IEnumerator WaitForFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
        }

        private void OnDestroy()
        {
            RBPhysController.DisposeMainComputer();
        }
    }
}