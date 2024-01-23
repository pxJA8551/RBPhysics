using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        [SerializeField] bool _disableUnityPhysics = true;

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
    }
}