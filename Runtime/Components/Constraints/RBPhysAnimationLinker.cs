using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysAnimationLinker : MonoBehaviour
    {
        public RBPhysAnimation[] linkedAnimations;
        public float[] linkedTOffsets;

        public float linkIsolation = 0;

        public void ReInitializeLinker()
        {
            foreach (var a in linkedAnimations)
            {
                a?.AttachLinker(this);
            }
        }

        private void OnDestroy()
        {
            foreach (var a in linkedAnimations)
            {
                a?.DetachLinker();
            }
        }

        private void OnEnable()
        {
            ReInitializeLinker();
        }

        private void OnDisable()
        {
            foreach (var a in linkedAnimations)
            {
                a?.DetachLinker();
            }
        }

        public void LinkCtrlTime()
        {
            float timeSum = 0;
            float mSum = 0;

            for (int i = 0; i < linkedAnimations.Length; i++)
            {
                var anim = linkedAnimations[i];

                if (anim?.enablePhysProceduralAnimation ?? false)
                {
                    float tOffset = linkedTOffsets.ElementAtOrDefault(i);

                    float m = anim.GetInvMass() + anim.GetInvInertiaTensorWs().magnitude;
                    timeSum += (anim.ctrlTime - tOffset) * m;

                    mSum += m;
                }
            }

            if (mSum > 0)
            {
                timeSum /= mSum;

                for (int i = 0; i < linkedAnimations.Length; i++)
                {
                    if (linkedAnimations[i]?.enablePhysProceduralAnimation ?? false)
                    {
                        float tOffset = linkedTOffsets.ElementAtOrDefault(i);
                        linkedAnimations[i].ctrlTime += Mathf.Lerp((timeSum + tOffset) - linkedAnimations[i].ctrlTime, 0, linkIsolation);
                    }
                }
            }
        }

        public void AddLinkedAnimationTime(float add)
        {
            for (int i = 0; i < linkedAnimations.Length; i++)
            {
                if (linkedAnimations[i]?.enablePhysProceduralAnimation ?? false)
                {
                    linkedAnimations[i].ctrlTime += add;
                }
            }
        }

        public float CalcLinkedInvMass()
        {
            float invMassSum = 0;

            for (int i = 0; i < linkedAnimations.Length; i++)
            {
                var anim = linkedAnimations[i];

                if (anim?.enablePhysProceduralAnimation ?? false)
                {
                    invMassSum += anim.GetInvMass();
                }
            }

            return invMassSum;
        }

        public Vector3 CalcLinkedInvInertiaTensorWs()
        {
            Vector3 invInertiaTensorWs = Vector3.zero;

            for (int i = 0; i < linkedAnimations.Length; i++)
            {
                var anim = linkedAnimations[i];

                if (anim?.enablePhysProceduralAnimation ?? false)
                {
                    invInertiaTensorWs += anim.GetInvInertiaTensorWs();
                }
            }

            return invInertiaTensorWs;
        }

        public float CalcLinkedAnimLambda(float time)
        {
            float lambda = 0;
            float invMass = CalcLinkedInvMass();
            Vector3 invInertiaTensorWs = CalcLinkedInvInertiaTensorWs();

            for (int i = 0; i < linkedAnimations.Length; i++)
            {
                var anim = linkedAnimations[i];

                if (anim?.enablePhysProceduralAnimation ?? false)
                {
                    float tOffset = linkedTOffsets.ElementAtOrDefault(i);
                    float tIsolation = anim.ctrlTime + Mathf.Lerp((time + tOffset) - anim.ctrlTime, 0, linkIsolation);
                    lambda += anim.CalcLinkedAnimLambda(tIsolation, invMass, invInertiaTensorWs);
                }
            }

            return lambda;
        }
    }
}