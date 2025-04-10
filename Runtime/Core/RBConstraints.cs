using System;
using UnityEditor;

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
            public readonly int solverSubtick;
            public readonly int solverMaxIter;

            public readonly int subtickCount;
            public readonly int iterCount;

            public SolverInfo(RBPhysComputer comp, int subtick, int iter)
            {
                this.solverSubtick = comp.solver_subtick;
                this.solverMaxIter = comp.solver_iter_per_subtick;

                this.subtickCount = subtick;
                this.iterCount = iter;
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