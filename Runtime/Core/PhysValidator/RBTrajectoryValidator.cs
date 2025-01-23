using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

namespace RBPhys
{
    public class RBTrajectoryValidator : RBPhysComputer.RBPhysStateValidator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Validate()
        {
            return !traj.IsIgnoredTrajectory && ((traj?.ValidateRetrogradeKeyGuid(retrogradeKeyGuid) ?? false) || ValidateAltnValidators());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBTrajectoryValidator(RBTrajectory traj) : base(Guid.Empty, traj.activeTraj)
        {
            Debug.Assert(!traj.IsIgnoredTrajectory);

            this.traj = traj;
            this.retrogradeKeyGuid = traj.RetrogradeKeyGuid;

            GetAltnValidators(traj);
        }

        void GetAltnValidators(RBTrajectory traj)
        {
            if (traj.IsStatic) return;

            var rb = traj?.Rigidbody;

            if (rb != null) 
            {
                var tvs = rb.trajAltnValidators;

                if (tvs.Count == 0)
                {
                    altnValidator = null;
                    altnValidators = null;
                }
                else if (tvs.Count == 1) 
                {
                    altnValidator = tvs[0];
                    altnValidators = null;
                }
                else
                {
                    altnValidator = null;
                    altnValidators = tvs.ToArray();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool ValidateAltnValidators()
        {
            if (altnValidator?.Validate() ?? false) return true;
            if (altnValidators?.All(item => item.Validate()) ?? false) return true;

            return false;
        }

        public readonly RBTrajectory traj;
        public readonly Guid retrogradeKeyGuid;

        public RBTrajectoryAlternateValidator altnValidator;
        public RBTrajectoryAlternateValidator[] altnValidators;
    }
}