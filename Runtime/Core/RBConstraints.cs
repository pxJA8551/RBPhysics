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
                Guid TrajectoryGuid { get; }
            }

            public abstract class RBPhysStateValidator : IRBPhysStateValidator
            {
                public abstract bool Validate();

                public Guid TrajectoryGuid { get { return _trajectoryGuid; } }

                readonly Guid _trajectoryGuid;

                public RBPhysStateValidator(Guid trajectoryGuid)
                {
                    _trajectoryGuid = trajectoryGuid;
                }
            }
        }
    }
}