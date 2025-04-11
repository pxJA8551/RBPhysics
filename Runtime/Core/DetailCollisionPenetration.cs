using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public struct Penetration
        {
            public Penetration inverted { get { return GetInverted(); } }

            public Vector3 p;
            public Vector3 pA;
            public Vector3 pB;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Penetration(Vector3 p, Vector3 pA, Vector3 pB)
            {
                this.p = p;
                this.pA = pA;
                this.pB = pB;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Invert()
            {
                p = -p;
                (pA, pB) = (pB, pA);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Penetration GetInverted()
            {
                return new Penetration(-p, pB, pA);
            }
        }
    }
}