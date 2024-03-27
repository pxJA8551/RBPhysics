using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public struct Penetration
        {
            public Vector3 p;
            public Vector3 pA;
            public Vector3 pB;
            public DetailCollisionInfo info;

            public Penetration(Vector3 p, Vector3 pA, Vector3 pB, DetailCollisionInfo info)
            {
                this.p = p;
                this.pA = pA;
                this.pB = pB;
                this.info = info;
            }
        }
    }
}