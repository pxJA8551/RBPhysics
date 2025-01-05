using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log(string.Format("CPU: {0} / {1}cores", SystemInfo.processorType, SystemInfo.processorCount));
            Debug.Log(string.Format("GPU: {0} / {1}MB API: {2}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize, SystemInfo.graphicsDeviceType));

            Application.targetFrameRate = -1;
        }

        async void FixedUpdate()
        {
            await PhysicsFrame();
        }

        async Task PhysicsFrame()
        {
            await RBPhysController.MainComputer.OpenPhysicsFrameWindowAsync();

            if (this == null) return;
            StartCoroutine(WaitForFixedUpdate());

            await RBPhysController.MainComputer.ClosePhysicsFrameWindow();
            await RBPhysController.MainComputer.ApplyObjectTransforms();
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