using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Profiling;
using static RBPhys.RBPhysStats;

namespace RBPhys
{
    public partial class RBPhysComputer
    {
        class RBPhysDiagnostics
        {
            object _lock = new object();
            ObjectStats _objStats = default;
            CallbackStats _callbackStats = default;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clear()
            {
                _objStats = default;
                _callbackStats = default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CountObjects(List<RBRigidbody> rigidbodies, List<RBCollider> colliders)
            {
                Profiler.BeginSample("RBPhysDiagnostics-CountObjects");

                int rigidbodyCount = rigidbodies?.Count ?? 0;
                int active = rigidbodies?.Count(item => item.IsStaticOrSleeping) ?? 0;
                int sleeping = rigidbodyCount - active;
                int colliderCount = colliders?.Count ?? 0;

                lock (_lock)
                {
                    _objStats = default;
                    _objStats.rigidbodies = rigidbodyCount;
                    _objStats.activeRigidbodies = active;
                    _objStats.sleepingRigidbodies = sleeping;
                    _objStats.colliders = colliderCount;
                }

                Profiler.EndSample();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CountCallbacks(Delegate[] beforeSolver, Delegate[] afterSolver, Delegate[] solverInit, Delegate[] solverIter, List<RBCollider> colliders)
            {
                Profiler.BeginSample("RBPhysDiagnostics-CountCallbacks");

                lock (_lock)
                {
                    _callbackStats = default;
                    _callbackStats.physObj_beforeSolver = beforeSolver?.Length ?? 0;
                    _callbackStats.physObj_afterSolver = afterSolver?.Length ?? 0;
                    _callbackStats.solvers_init = solverInit?.Length ?? 0;
                    _callbackStats.solvers_iter = solverIter?.Length ?? 0;
                    _callbackStats.onCollision = colliders?.Sum(c => c.onCollision?.GetInvocationList()?.Length ?? 0) ?? 0;
                }

                Profiler.EndSample();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public RBPhysStats PackStates()
            {
                lock (_lock)
                {
                    return new RBPhysStats(_objStats, _callbackStats);
                }
            }
        }
    }

    public struct RBPhysStats
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysStats(ObjectStats objStats, CallbackStats callbackStats)
        {
            this.objStats = objStats;
            this.callbackStats = callbackStats;
        }

        public ObjectStats objStats;
        public CallbackStats callbackStats;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyStats(RBPhysStats stats)
        {
            this.objStats = stats.objStats;
            this.callbackStats = stats.callbackStats;
        }

        public struct ObjectStats
        {
            public int rigidbodies;
            public int sleepingRigidbodies;
            public int activeRigidbodies;
            public int colliders;

            bool _validate;

            public void MarkAsValid()
            {
                _validate = true;
            }

            public bool IsValid { get { return _validate; } }
        }

        public struct CallbackStats
        {
            public int solvers_init;
            public int solvers_iter;
            public int physObj_beforeSolver;
            public int physObj_afterSolver;
            public int onCollision;

            bool _validate;

            public void MarkAsValid()
            {
                _validate = true;
            }

            public bool IsValid { get { return _validate; } }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Validate()
        {
            return objStats.IsValid && callbackStats.IsValid;
        }
    }
}