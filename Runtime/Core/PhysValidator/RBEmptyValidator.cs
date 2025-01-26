using System;
using System.Runtime.CompilerServices;

namespace RBPhys
{
    public class RBEmptyValidator : RBPhysComputer.RBPhysStateValidator
    {
        public static readonly RBEmptyValidator emptyValidator = new RBEmptyValidator();

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