using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public static partial class RBPhysCore
    {
        public static class RBConstraints
        {
            public interface IStdSolver
            {
                void StdSolverInit(float dt, bool isPrimaryInit) { }
                void StdSolverIteration(int iterationCount) { }
            }

            public interface IRBPhysObject
            {
                void BeforeSolver() { }
                void AfterSolver() { }
            }

            public interface IRBOnCollision
            {
                void OnCollision(RBTrajectory traj);
            }

            public interface IRBPhysStateValidator
            {
                bool Validate();
                void UpdateAfterSolver(float dt);
                Guid ValidatorPublisher { get; }
            }

            public abstract class RBPhysStateValidator : IRBPhysStateValidator
            {
                public abstract bool Validate();
                public abstract void UpdateAfterSolver(float dt);

                public Guid ValidatorPublisher { get { return _validatorPublisherGuid; } }

                readonly Guid _validatorPublisherGuid;

                public RBPhysStateValidator(Guid guid)
                {
                    _validatorPublisherGuid = guid;
                }
            }
        }
    }
}