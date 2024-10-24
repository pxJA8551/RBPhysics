using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        [SerializeField] bool _disableUnityPhysics = true;

        private void Awake()
        {
            Debug.Log(string.Format("CPU: {0} / {1}cores", SystemInfo.processorType, SystemInfo.processorCount));
            Debug.Log(string.Format("GPU: {0} / {1}MB API: {2}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize, SystemInfo.graphicsDeviceType));

            Application.targetFrameRate = -1;
        }

        async void FixedUpdate()
        {
            await PhysicsFrame();

            if (Physics.autoSimulation != !_disableUnityPhysics) 
            {
                Physics.autoSimulation = !_disableUnityPhysics;
            }
        }

        async Task PhysicsFrame()
        {
            await RBPhysController.MainComputer.OpenPhysicsFrameWindowAsync();

            StartCoroutine(WaitForFixedUpdate());

            RBPhysController.MainComputer.ClosePhysicsFrameWindow();
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