using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public partial class RBPhysComputer
    {
        public delegate void StdSolverInit(float dt, SolverInfo solverInfo);
        public delegate void StdSolverIteration(SolverInfo solverInfo);

        public delegate void BeforeSolver(float delta, TimeScaleMode timeScaleMode);
        public delegate void AfterSolver(float delta, TimeScaleMode timeScaleMode);

        public delegate void ValidatorPreBeforeSolver(float delta, TimeScaleMode timeScaleMode);
        public delegate void ValidatorBeforeSolver(float delta, TimeScaleMode timeScaleMode);
        public delegate void ValidatorAfterSolver(float delta, TimeScaleMode timeScaleMode);

        public delegate void OnCollision(RBCollider col, RBCollisionInfo info);

        public struct SolverInfo
        {
            public readonly int solver_sync_max;
            public readonly int solver_sync_count;

            public SolverInfo(in RBPhysComputer computer, int syncCount)
            {
                solver_sync_max = computer.cpu_std_solver_internal_sync_per_iteration;
                solver_sync_count = syncCount;
            }
        }

        public interface IRBPhysStateValidator
        {
            bool Validate();
            bool ValidateSrc(Guid validatorSrcGuid);
            Guid ValidatorSrcGuid { get; }
        }

        public abstract class RBPhysStateValidator : IRBPhysStateValidator
        {
            public abstract bool Validate();

            public readonly bool permanentBranch;

            public Guid ValidatorSrcGuid { get { return _validatorSrcGuid; } }

            readonly Guid _validatorSrcGuid;

            public RBPhysStateValidator(Guid validatorSrcGuid, bool permanentBranch)
            {
                _validatorSrcGuid = validatorSrcGuid;
                this.permanentBranch = permanentBranch;
            }

            public bool ValidateSrc(Guid validatorSrcGuid)
            {
                return !permanentBranch && _validatorSrcGuid != Guid.Empty && _validatorSrcGuid == validatorSrcGuid;
            }
        }
    }
}