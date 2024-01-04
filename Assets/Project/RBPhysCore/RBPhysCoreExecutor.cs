using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysCoreExecutor : MonoBehaviour
    {
        void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            RBPhysCore.SimulateFixedStep(dt);
        }
    }
}