using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        void FixedUpdate()
        {
            StartCoroutine(PhysicsFrame(Time.fixedDeltaTime));
        }

        IEnumerator PhysicsFrame(float dt)
        {
            RBPhysCore.OpenPhysicsFrameWindow(dt);

            yield return new WaitForFixedUpdate();

            RBPhysCore.ClosePhysicsFrameWindow(dt);
        }
    }
}