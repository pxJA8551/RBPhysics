using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public partial class RBPhysComputer
    {
        public interface IStdSolver
        {
            void StdSolverInit(float dt, bool isPrimaryInit, SolverInfo solverInfo) { }
            void StdSolverIteration(int iterationCount, SolverInfo solverInfo) { }
        }

        public interface IStdSolverPrediction
        {
            void StdSolverInitPrediction(float dt, bool isPrimaryInit, SolverInfo solverInfo) { }
            void StdSolverIterationPrediction(int iterationCount, SolverInfo solverInfo) { }
        }

        public interface IRBPhysObject
        {
            void BeforeSolver(float delta, TimeScaleMode timeScaleMode) { }
            void AfterSolver(float delta, TimeScaleMode timeScaleMode) { }
        }

        public interface IRBPhysObjectPrediction
        {
            void BeforeSolverPrediction(float delta, TimeScaleMode timeScaleMode) { }
            void AfterSolverPrediction(float delta, TimeScaleMode timeScaleMode) { }
        }

        public delegate void OnCollision(RBCollider col, RBCollisionInfo info);

        public struct SolverInfo
        {
            public readonly bool multiThreadedPrediction;

            public readonly int solver_iter_max;
            public readonly int solver_sync_max;

            public readonly int solver_iter_count;
            public readonly int solver_sync_count;

            public SolverInfo(in RBPhysComputer computer, int iterCount, int syncCount)
            {
                multiThreadedPrediction = computer.multiThreadPredictionMode;

                solver_iter_max = computer.cpu_std_solver_max_iter;
                solver_sync_max = computer.cpu_std_solver_internal_sync_per_iteration;

                solver_iter_count = iterCount;
                solver_sync_count = syncCount;
            }
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