using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        [SerializeField] bool _disableUnityPhysics = true;

        private void Awake()
        {
            //Debug.Log(string.Format("CPU: {0} / {1}cores", SystemInfo.processorType, SystemInfo.processorCount));
            //Debug.Log(string.Format("GPU: {0} / {1}MB API: {2}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize, SystemInfo.graphicsDeviceType));
            Application.targetFrameRate = -1;
        }

        void FixedUpdate()
        {
            StartCoroutine(PhysicsFrame(Time.fixedDeltaTime));

            if (Physics.autoSimulation != !_disableUnityPhysics) 
            {
                Physics.autoSimulation = !_disableUnityPhysics;
            }
        }

        IEnumerator PhysicsFrame(float dt)
        {
            RBPhysCore.OpenPhysicsFrameWindow(dt);

            yield return new WaitForFixedUpdate();

            RBPhysCore.ClosePhysicsFrameWindow(dt);
        }

        private void OnDestroy()
        {
            RBPhysCore.Dispose();
        }
    }
}