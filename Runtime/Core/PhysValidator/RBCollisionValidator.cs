using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBCollisionValidator : RBTrajectoryValidator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBCollisionValidator(RBTrajectory traj) : base(traj) { }
    }
}