using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public class RBPhysAnimationTrajAltnValidator : RBTrajectoryAlternateValidator
    {
        public const float CTRL_TIME_EPSILON = .01f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Validate()
        {
            return vPhysAnimation != null && RBPhysUtil.IsF32EpsilonEqual(vPhysAnimation.ctrlTime, animCtrlSpeed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysAnimationTrajAltnValidator(RBPhysAnimation physAnim) : base(physAnim?.ValidatorSrcGuid ?? Guid.Empty)
        {
            vPhysAnimation = physAnim;
            animCtrlSpeed = physAnim?.ctrlSpeed ?? 0;
        }

        public readonly RBPhysAnimation vPhysAnimation;
        public readonly float animCtrlSpeed;
    }
}