using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        SemaphoreSlim _mainPhysLoopSemaphore = new SemaphoreSlim(1, 1);

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
            await RBPhysController.MainComputer.OpenPhysicsFrameWindowAsync();

            if (this == null) return;
            StartCoroutine(WaitForFixedUpdate());

            await RBPhysController.MainComputer.ClosePhysicsFrameWindow();
            await RBPhysController.MainComputer.ApplyObjectTransformsAsync();
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