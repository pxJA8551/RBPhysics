using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        RBPhysCore.RBPhysCoreProfiler profiler = new RBPhysCore.RBPhysCoreProfiler();

        void FixedUpdate()
        {
            StartCoroutine(PhysicsFrame(Time.fixedDeltaTime));
        }

        IEnumerator PhysicsFrame(float dt)
        {
            RBPhysCore.OpenPhysicsFrameWindow(dt, ref profiler);

            yield return new WaitForFixedUpdate();

            RBPhysCore.ClosePhysicsFrameWindow(dt);

            if (profiler.aabbCollisions > 0)
            {
                Debug.Log(profiler.GetLogTextCollisions());
            }
        }
    }
}