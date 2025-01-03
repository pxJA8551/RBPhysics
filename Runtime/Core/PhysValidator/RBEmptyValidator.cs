using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBEmptyValidator : RBPhysComputer.RBPhysStateValidator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBEmptyValidator() : base(Guid.Empty, true) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBEmptyValidator(Guid guid) : base(guid, true) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Validate()
        {
            return false;
        }
    }
}