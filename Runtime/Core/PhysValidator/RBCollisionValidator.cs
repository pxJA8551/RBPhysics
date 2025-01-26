using System.Runtime.CompilerServices;

namespace RBPhys
{
    public class RBCollisionValidator : RBTrajectoryValidator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBCollisionValidator(RBTrajectory traj) : base(traj) { }
    }
}