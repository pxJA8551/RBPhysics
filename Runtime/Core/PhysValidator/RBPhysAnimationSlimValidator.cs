using System;
using System.Runtime.CompilerServices;

namespace RBPhys
{
    public class RBPhysAnimationSlimValidator : RBTrajectoryAlternateValidator
    {
        public const float CTRL_TIME_EPSILON = .025f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Validate()
        {
            return vPhysAnimation != null && RBPhysUtil.IsF32EpsilonEqual(vPhysAnimation.AnimSpeed, animCtrlSpeed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysAnimationSlimValidator(RBPhysAnimationSlim physAnim) : base(physAnim?.ValidatorSrcGuid ?? Guid.Empty)
        {
            vPhysAnimation = physAnim;
            animCtrlSpeed = physAnim?.AnimSpeed ?? 0;
        }

        public readonly RBPhysAnimationSlim vPhysAnimation;
        public readonly float animCtrlSpeed;
    }
}