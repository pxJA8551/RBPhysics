using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public struct Penetration
        {
            public Vector3 p
            {
                get => _p;

                set
                {
                    SetPenetration(value);
                }
            }

            public float PMagnitude => _pMagnitude;
            public float PSqrMagnitude => _pSqrMagnitude;

            public Vector3 pA;
            public Vector3 pB;

            public bool IsValid => _p != Vector3.zero;
            public Penetration inverted { get { return GetInverted(); } }

            Vector3 _p;
            float _pMagnitude;
            float _pSqrMagnitude;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Penetration(Vector3 p, Vector3 pA, Vector3 pB)
            {
                _p = p;
                _pMagnitude = p.magnitude;
                _pSqrMagnitude = p.sqrMagnitude;

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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetPenetration(Vector3 p)
            {
                _p = p;
                _pMagnitude = p.magnitude;
                _pSqrMagnitude = p.sqrMagnitude;
            }

            public static bool operator <(Penetration x, Penetration y)
            {
                return x.p.sqrMagnitude < y.p.sqrMagnitude;
            }

            public static bool operator >(Penetration x, Penetration y)
            {
                return x.p.sqrMagnitude > y.p.sqrMagnitude;
            }

            public static bool operator <=(Penetration x, Penetration y)
            {
                return x.p.sqrMagnitude <= y.p.sqrMagnitude;
            }

            public static bool operator >=(Penetration x, Penetration y)
            {
                return x.p.sqrMagnitude >= y.p.sqrMagnitude;
            }
        }
    }
}