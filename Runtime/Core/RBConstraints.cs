using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace RBPhys
{
    public partial class RBPhysComputer
    {
        public interface IStdSolver
        {
            void StdSolverInit(float dt, bool isPrimaryInit) { }
            void StdSolverIteration(int iterationCount) { }
        }

        public interface IStdSolverPrediction
        {
            void StdSolverInitPrediction(float dt, bool isPrimaryInit) { }
            void StdSolverIterationPrediction(int iterationCount) { }
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