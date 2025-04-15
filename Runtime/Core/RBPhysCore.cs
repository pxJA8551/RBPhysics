using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys
{
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, true)]
    public partial class RBPhysComputer
    {
        // dt = .01 ms

        public const int SOLVER_SUBTICKS = 12;
        public const int SOLVER_ITER_PER_SUBTICKS = 6;
        public const float SOLVER_ABORT_VELADD_SQRT = .00005f * .00005f;
        public const float SOLVER_ABORT_ANGVELADD_SQRT = .0005f * .0005f;
        public const float RETROGRADE_PHYS_RESTITUTION_MULTIPLIER = .525f;
        public const float RETROGRADE_PHYS_RESTITUTION_MIN = 1.1f;

        public const float RETROGRADE_PHYS_FRICTION_MULTIPLIER = .5f;

        public const float SOFTCLIP_LAMBDA_MULTIPLIER = .2f;
        public const float INERTIA_TENSOR_MULTIPLIER = 1f;

        public const float VELOCITY_MAX = 50f;
        public const float ANG_VELOCITY_MAX = 20f;

        public int solver_subtick = SOLVER_SUBTICKS;
        public int solver_iter_per_subtick = SOLVER_ITER_PER_SUBTICKS;
        public float solver_abort_veladd_sqrt = SOLVER_ABORT_VELADD_SQRT;
        public float solver_abort_angveladd_sqrt = SOLVER_ABORT_ANGVELADD_SQRT;

        public float retrograde_phys_restitution_multiplier = RETROGRADE_PHYS_RESTITUTION_MULTIPLIER;
        public float retrograde_phys_restitution_min = RETROGRADE_PHYS_RESTITUTION_MIN;
        public float retrograde_phys_friction_multiplier = RETROGRADE_PHYS_FRICTION_MULTIPLIER;

        public float softClip_lambda_multiplier = SOFTCLIP_LAMBDA_MULTIPLIER;
        public float inertia_tensor_multiplier = INERTIA_TENSOR_MULTIPLIER;

        public float rbRigidbody_velocity_max = VELOCITY_MAX;
        public float rbRigidbody_ang_velocity_max = ANG_VELOCITY_MAX;

        public Vector3 gravityAcceleration = new Vector3(0, -9.81f, 0);

        public TimeScaleMode PhysTimeScaleMode { get { return _timeScaleMode; } set { SetTimeScale(value); } }

        TimeScaleMode _timeScaleMode = TimeScaleMode.Prograde;

        List<RBRigidbody> _rigidbodies = new List<RBRigidbody>();
        List<RBCollider> _colliders = new List<RBCollider>();

        public RBTrajectory[] Trajectories_OrderByXMin { get { return _trajectories_orderByXMin; } }
        public float[] Trajectories_XMin { get { return _trajectories_xMin; } }

        RBTrajectory[] _trajectories_orderByXMin = new RBTrajectory[0];
        float[] _trajectories_xMin = new float[0];

        List<RBRigidbody> _rbAddQueue = new List<RBRigidbody>();
        List<RBRigidbody> _rbRemoveQueue = new List<RBRigidbody>();
        List<RBCollider> _colAddQueue = new List<RBCollider>();
        List<RBCollider> _colRemoveQueue = new List<RBCollider>();

        List<RBCollision> _collisionsInSolver = new List<RBCollision>();

        List<RBVirtualTransform> _vTransforms = new List<RBVirtualTransform>();
        List<RBVirtualComponent> _vComponents = new List<RBVirtualComponent>();

        StdSolverInit _stdSolverInit;
        StdSolverIteration _stdSolverIter;

        BeforeSolver _beforeSolver;
        AfterSolver _afterSolver;

        ValidatorPreBeforeSolver _validatorPreBeforeSolver;
        ValidatorBeforeSolver _validatorBeforeSolver;
        ValidatorAfterSolver _validatorAfterSolver;

        int[] _collisionIgnoreLayers = new int[32];
        RBCollisionLayerOption[] _layerOptions = new RBCollisionLayerOption[32];

        public ComputerTimeParams timeParams;
        public PhysComputerTime physComputerTime;

        SemaphoreSlim _solverSemaphore = new SemaphoreSlim(1, 1);
        SemaphoreSlim _physObjSemaphore = new SemaphoreSlim(1, 1);

        public readonly bool isPredictionComputer;
        public bool enableStats = false;

        RBPhysDiagnostics _diagnostics = new RBPhysDiagnostics();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysComputer(bool isPredictionComputer)
        {
            physComputerTime = new PhysComputerTime(this);
            timeParams = ComputerTimeParams.GetDefault();
            this.isPredictionComputer = isPredictionComputer;
            _diagnostics = new RBPhysDiagnostics();

            ReInitializeComputer();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysComputer(float deltaTime, bool isPredictionComputer)
        {
            physComputerTime = new PhysComputerTime(this);
            timeParams = new ComputerTimeParams(deltaTime, 1, false);
            this.isPredictionComputer = isPredictionComputer;
            _diagnostics = new RBPhysDiagnostics();

            ReInitializeComputer();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysComputer.SolverInfo PackSolverInfo(int subtick = -1, int iter = -1)
        {
            return new SolverInfo(this, subtick, iter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBCollision.SolverOption PackSolverOption()
        {
            var option = RBCollision.SolverOption.GetDefault();
            option.retrograde_phys_restitution_multiplier = this.retrograde_phys_restitution_multiplier;
            option.retrograde_phys_restitution_min = this.retrograde_phys_restitution_min;
            option.retrograde_phys_friction_multiplier = this.retrograde_phys_friction_multiplier;
            option.softClip_lambda_multiplier = this.softClip_lambda_multiplier;
            option.inertia_tensor_multiplier = this.inertia_tensor_multiplier;

            return option;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBRigidbody.VelocityOption PackVelocityOption()
        {
            var option = RBRigidbody.VelocityOption.GetDefault();
            option.velocity_max = VELOCITY_MAX;
            option.angularVelocity_max = ANG_VELOCITY_MAX;

            return option;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReInitializeComputer()
        {
            this.WaitSemaphore();

            physComputerTime = new PhysComputerTime(this);

            ReInitializeSolverTime();

            _collisionsInSolver.Clear();

            this.ReleaseSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTimeScale(TimeScaleMode tsm)
        {
            WaitPhysObjSemaphore();

            if ((int)_timeScaleMode + (int)tsm == 1)
            {
                foreach (var rb in _rigidbodies)
                {
                    if (rb != null) rb.InvertVelocity();
                }
            }

            _timeScaleMode = tsm;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVirtualTransform(RBVirtualTransform vt)
        {
            WaitPhysObjSemaphore();

            if (!_vTransforms.Contains(vt)) _vTransforms.Add(vt);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveVirtualTransform(RBVirtualTransform vt)
        {
            WaitPhysObjSemaphore();

            _vTransforms.Remove(vt);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVirtualComponent(RBVirtualComponent vc)
        {
            WaitPhysObjSemaphore();

            if (!_vComponents.Contains(vc)) _vComponents.Add(vc);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveVirtualComponent(RBVirtualComponent vc)
        {
            WaitPhysObjSemaphore();

            _vComponents.Remove(vc);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRigidbody(RBRigidbody rb)
        {
            WaitPhysObjSemaphore();

            if (!_rigidbodies.Contains(rb)) _rigidbodies.Add(rb);
            if (!_rbAddQueue.Contains(rb)) _rbAddQueue.Add(rb);
            _rbRemoveQueue.Remove(rb);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveRigidbody(RBRigidbody rb)
        {
            WaitPhysObjSemaphore();

            _rigidbodies.Remove(rb);
            _rbAddQueue.Remove(rb);
            if (!_rbRemoveQueue.Contains(rb)) _rbRemoveQueue.Add(rb);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCollider(RBCollider c)
        {
            WaitPhysObjSemaphore();

            if (!_colliders.Contains(c)) _colliders.Add(c);
            if (!_colAddQueue.Contains(c)) _colAddQueue.Add(c);
            _colRemoveQueue.Remove(c);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveCollider(RBCollider c)
        {
            WaitPhysObjSemaphore();

            _colliders.Remove(c);
            _colAddQueue.Remove(c);
            if (!_colRemoveQueue.Contains(c)) _colRemoveQueue.Add(c);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SwitchToCollider(RBCollider c)
        {
            WaitPhysObjSemaphore();

            if (!_colAddQueue.Contains(c)) _colAddQueue.Add(c);
            _colRemoveQueue.Remove(c);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SwitchToRigidbody(RBCollider c)
        {
            WaitPhysObjSemaphore();

            _colAddQueue.Remove(c);
            if (!_colRemoveQueue.Contains(c)) _colRemoveQueue.Add(c);

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddStdSolver(StdSolverInit stdInit, StdSolverIteration stdIter)
        {
            WaitPhysObjSemaphore();

            if (stdInit != null) _stdSolverInit += stdInit;
            if (stdIter != null) _stdSolverIter += stdIter;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveStdSolver(StdSolverInit stdInit, StdSolverIteration stdIter)
        {
            WaitPhysObjSemaphore();

            if (stdInit != null) _stdSolverInit -= stdInit;
            if (stdIter != null) _stdSolverIter -= stdIter;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddPhysObject(BeforeSolver beforeSolver, AfterSolver afterSolver)
        {
            WaitPhysObjSemaphore();

            if (beforeSolver != null) _beforeSolver += beforeSolver;
            if (afterSolver != null) _afterSolver += afterSolver;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemovePhysObject(BeforeSolver beforeSolver, AfterSolver afterSolver)
        {
            WaitPhysObjSemaphore();

            if (beforeSolver != null) _beforeSolver -= beforeSolver;
            if (afterSolver != null) _afterSolver -= afterSolver;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddPhysValidatorObject(ValidatorPreBeforeSolver preBeforeSolver, ValidatorBeforeSolver beforeSolver, ValidatorAfterSolver afterSolver)
        {
            WaitPhysObjSemaphore();

            if (preBeforeSolver != null) _validatorPreBeforeSolver += preBeforeSolver;
            if (beforeSolver != null) _validatorBeforeSolver += beforeSolver;
            if (afterSolver != null) _validatorAfterSolver += afterSolver;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemovePhysValidatorObject(ValidatorPreBeforeSolver preBeforeSolver, ValidatorBeforeSolver beforeSolver, ValidatorAfterSolver afterSolver)
        {
            WaitPhysObjSemaphore();

            if (preBeforeSolver != null) _validatorPreBeforeSolver -= preBeforeSolver;
            if (beforeSolver != null) _validatorBeforeSolver -= beforeSolver;
            if (afterSolver != null) _validatorAfterSolver -= afterSolver;

            ReleasePhysObjSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCollisionOption(int layer_a, int layer_b, RBCollisionOption option)
        {
            WaitSemaphore();

            switch (option)
            {
                case RBCollisionOption.Ignore:
                    _collisionIgnoreLayers[layer_a] |= (1 << layer_b);
                    _collisionIgnoreLayers[layer_b] |= (1 << layer_a);
                    break;

                case RBCollisionOption.Both:
                    _collisionIgnoreLayers[layer_a] &= ~(1 << layer_b);
                    _collisionIgnoreLayers[layer_b] &= ~(1 << layer_a);
                    break;
            }

            ReleaseSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCollisionLayerOption(int layer, RBCollisionLayerOption option, bool state)
        {
            WaitSemaphore();

            int p0 = (int)_layerOptions[layer];
            int sel = (int)option;

            _layerOptions[layer] = (RBCollisionLayerOption)((p0 & ~sel) | (state ? sel : 0));

            ReleaseSemaphore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIgnorePhysCastLayer(int layer)
        {
            int p = (int)_layerOptions[layer];
            return (p & 1) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsTriggerLayer(int layer)
        {
            int p = (int)_layerOptions[layer];
            return (p & 2) == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPlayerLayer(int layer)
        {
            int p = (int)_layerOptions[layer];
            return (p & 4) == 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WaitSemaphore()
        {
            _solverSemaphore.Wait();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool WaitSemaphore(int timeoutMs)
        {
            return _solverSemaphore.Wait(timeoutMs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task WaitSemaphoreAsync()
        {
            await _solverSemaphore.WaitAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> WaitSemaphoreAsync(int timeoutMs)
        {
            return await _solverSemaphore.WaitAsync(timeoutMs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleaseSemaphore()
        {
            _solverSemaphore.Release();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WaitPhysObjSemaphore()
        {
            _physObjSemaphore.Wait();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool WaitPhysObjSemaphore(int timeoutMs)
        {
            return _physObjSemaphore.Wait(timeoutMs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task WaitPhysObjSemaphoreAsync()
        {
            await _physObjSemaphore.WaitAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> WaitPhysObjSemaphoreAsync(int timeoutMs)
        {
            return await _physObjSemaphore.WaitAsync(timeoutMs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleasePhysObjSemaphore()
        {
            _physObjSemaphore.Release();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncObjectTransformsAsync(bool waitSemaphore = true)
        {
            if (waitSemaphore)
            {
                if (await WaitSemaphoreAsync(500))
                {
                    foreach (var v in _vTransforms)
                    {
                        v.SyncBaseObjectTransform();
                    }

                    ReleaseSemaphore();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                foreach (var v in _vTransforms)
                {
                    v.SyncBaseObjectTransform();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncBaseVTransformsAsync(bool waitSemaphore = true)
        {
            if (waitSemaphore)
            {
                if (await WaitSemaphoreAsync(500))
                {
                    foreach (var v in _vTransforms)
                    {
                        v.SyncBaseVTransform();
                    }

                    ReleaseSemaphore();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                foreach (var v in _vTransforms)
                {
                    v.SyncBaseVTransform();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task ApplyObjectTransformsAsync(bool waitSemaphore = true)
        {
            if (waitSemaphore)
            {
                if (await WaitSemaphoreAsync(500))
                {
                    foreach (var v in _vTransforms)
                    {
                        v.ApplyBaseObjectTransform();
                    }

                    ReleaseSemaphore();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                foreach (var v in _vTransforms)
                {
                    v.ApplyBaseObjectTransform();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task SyncBaseVComponentsAsync(bool waitSemaphore = true)
        {
            if (waitSemaphore)
            {
                if (await WaitSemaphoreAsync(500))
                {
                    foreach (var v in _vComponents)
                    {
                        v.SyncVirtualComponent();
                    }

                    ReleaseSemaphore();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                foreach (var v in _vComponents)
                {
                    v.SyncVirtualComponent();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBPhysStats GetStats()
        {
            RBPhysStats stats = _diagnostics.PackStates();
            return stats;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearStats()
        {
            _diagnostics.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task PhysicsFrameAsync()
        {
            await PhysFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        async Task PhysFrame()
        {
            if (!enableStats) _diagnostics.Clear();

            float dt = _solverDeltaTimeAsFloat;

            if (dt == 0) return;

            const int SEMAPHORE_TIMEOUT = 500;

            if (await _solverSemaphore.WaitAsync(SEMAPHORE_TIMEOUT))
            {
                try
                {
                    if (await _physObjSemaphore.WaitAsync(SEMAPHORE_TIMEOUT))
                    {
                        try
                        {
                            SyncTrajectories();

                            if (enableStats) _diagnostics.CountObjects(_rigidbodies, _colliders);
                            if (enableStats) _diagnostics.CountCallbacks(_beforeSolver?.GetInvocationList(), _afterSolver?.GetInvocationList(), _stdSolverInit?.GetInvocationList(), _stdSolverIter?.GetInvocationList(), _colliders);

                            ClearCollisions();
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            _physObjSemaphore.Release();
                        }
                    }
                    else throw new SemaphoreFullException();

                    if (_validatorPreBeforeSolver != null) _validatorPreBeforeSolver(dt, _timeScaleMode);
                    if (_validatorBeforeSolver != null) _validatorBeforeSolver(dt, _timeScaleMode);
                    if (_beforeSolver != null) _beforeSolver(dt, _timeScaleMode);
                    
                    if (await _physObjSemaphore.WaitAsync(SEMAPHORE_TIMEOUT))
                    {
                        try
                        {
                            UpdateTransform();
                            UpdateObjTrajectory();
                            SortTrajectories();

                            if (_trajectories_orderByXMin.Length > 0)
                            {
                                foreach (RBRigidbody rb in _rigidbodies)
                                {
                                    var expTraj = rb.ObjectTrajectory;
                                    if (!(expTraj.IsStaticOrSleeping || expTraj.IsLimitedSleeping) && rb.useGravity && !rb.ObjectTrajectory.IsIgnoredTrajectory)
                                    {
                                        rb.ExpVelocity += gravityAcceleration * dt;
                                    }
                                }

                                SolveConstraints(dt);
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            _physObjSemaphore.Release();
                        }
                    }
                    else throw new SemaphoreFullException();

                    if (_afterSolver != null) _afterSolver(dt, _timeScaleMode);
                    if (_validatorAfterSolver != null) _validatorAfterSolver(dt, _timeScaleMode);

                    if (await _physObjSemaphore.WaitAsync(SEMAPHORE_TIMEOUT))
                    {
                        try
                        {
                            TrySleepRigidbodies();
                            TryAwakeRigidbodies();

                            var velocityOption = PackVelocityOption();

                            ApplyPhysFrame(dt, velocityOption);
                            ClearValidators();
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            _physObjSemaphore.Release();
                        }
                    }
                    else throw new SemaphoreFullException();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _solverSemaphore.Release();
                }
            }
            else
            {
                _diagnostics.Clear();
                throw new SemaphoreFullException();
            }

            //OnClosePhysicsFrame��
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ReInitializeSolverTime()
        {
            _solverTime = 0;
            _solverDeltaTime = 0;
            _solverUnscaledTime = 0;
            _solverUnscaledDeltaTime = 0;

            SetSolverTimeVariables();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetSolverTimeVariables()
        {
            if (timeParams.enableAutoTimeIntergrading)
            {
                timeParams.fixedDeltaTime = Time.fixedDeltaTime;
                timeParams.timeScale = Time.timeScale;

                if (_solverTimeInitialized)
                {
                    _solverDeltaTime = Time.fixedDeltaTime;
                    _solverUnscaledDeltaTime = Time.fixedUnscaledDeltaTime;
                }
                else
                {
                    _solverDeltaTime = Time.fixedDeltaTime;
                    _solverUnscaledDeltaTime = Time.fixedUnscaledDeltaTime;
                }

                _solverTime = Time.timeAsDouble;
                _solverUnscaledTime = Time.unscaledTimeAsDouble;

                _solverTimeInitialized = true;
            }
            else
            {
                double delta = Math.Round(timeParams.fixedDeltaTime * timeParams.timeScale, 6);

                _solverTime += delta;
                _solverDeltaTime = delta;
                _solverUnscaledTime = timeParams.fixedDeltaTime;
                _solverUnscaledDeltaTime = timeParams.fixedDeltaTime;

                _solverTimeInitialized = true;
            }
        }

        public struct RBColliderCastHitInfo
        {
            public Vector3 position;
            public Vector3 normal;
            public float length;
            public RBCollider collider;
            public bool IsValidHit { get { return _isValidHit; } }
            public PhysCastType type;
            public RBTrajectory trajectory;
            public bool backFaceCollision;

            bool _isValidHit;

            internal RBColliderCastHitInfo(RBCollider c, RBTrajectory t, PhysCastType type)
            {
                collider = c;
                trajectory = t;
                _isValidHit = false;
                backFaceCollision = false;

                position = Vector3.zero;
                normal = Vector3.zero;
                length = -1;
                this.type = type;
            }

            internal void SetHit(Vector3 p, Vector3 nN, float dist, bool backFaceCollision = false)
            {
                position = p;
                normal = nN;
                this.length = dist;
                this.backFaceCollision = backFaceCollision;

                _isValidHit = true;
            }

            public enum PhysCastType
            {
                Raycast,
                SphereCast
            }
        }

        public struct RBColliderOverlapInfo
        {
            public Vector3 position;
            public Vector3 normal;
            public RBCollider collider;
            public bool IsValidOverlap { get { return _isValidOverlap; } }

            bool _isValidOverlap;

            internal RBColliderOverlapInfo(RBCollider c)
            {
                collider = c;
                _isValidOverlap = false;

                position = Vector3.zero;
                normal = Vector3.zero;
            }

            internal void SetOverlap(Vector3 p, Vector3 nN)
            {
                position = p;
                normal = nN;
                _isValidOverlap = true;
            }
        }

        public RBColliderCastHitInfo Raycast(Vector3 org, Vector3 dir, float d, bool ignoreBackFaceCollision = true)
        {
            return Raycast(org, dir, d, false, null, null, ignoreBackFaceCollision);
        }

        public RBColliderCastHitInfo Raycast(Vector3 org, Vector3 dir, float d, bool allowTriggerCollision, List<RBCollider> ignoreCols, List<RBTrajectory> ignoreTrajs, bool ignoreBackFaceCollision = true)
        {
            var hitList = RaycastAll(org, dir, d, allowTriggerCollision, ignoreCols, ignoreTrajs, ignoreBackFaceCollision);

            RBColliderCastHitInfo fMinOverlap = default;

            foreach (var v in hitList)
            {
                if (!fMinOverlap.IsValidHit || (v.IsValidHit && v.length < fMinOverlap.length))
                {
                    fMinOverlap = v;
                }
            }

            return fMinOverlap;
        }

        public List<RBColliderCastHitInfo> RaycastAll(Vector3 org, Vector3 dir, float d, bool allowTriggerCollision, List<RBCollider> ignoreCols, List<RBTrajectory> ignoreTrajs, bool ignoreBackFaceCollision = true)
        {
            var hitList = new List<RBColliderCastHitInfo>();
            RaycastAll(org, dir, d, allowTriggerCollision, ignoreCols, ignoreTrajs, ref hitList, ignoreBackFaceCollision);
            return hitList;
        }

        public void RaycastAll(Vector3 org, Vector3 dir, float d, bool allowTriggerCollision, List<RBCollider> ignoreCols, List<RBTrajectory> ignoreTrajs, ref List<RBColliderCastHitInfo> hitInfos, bool ignoreBackFaceCollision = true)
        {
            dir = dir.normalized;

            Vector3 pos_a = org;
            Vector3 pos_b = org + dir * d;
            Vector3 center = (pos_a + pos_b) / 2f;

            if (hitInfos == null)
            {
                hitInfos = new List<RBColliderCastHitInfo>();
            }

            hitInfos.Clear();
            var hitList = hitInfos;

            float xMin = Mathf.Min(pos_a.x, pos_b.x);
            float xMax = Mathf.Max(pos_a.x, pos_b.x);

            bool selectTrajs = ignoreTrajs != null;
            bool selectCols = ignoreCols != null;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (selectTrajs && ignoreTrajs.Contains(t))
                {
                    continue;
                }

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    if (t.trajectoryAABB.OverlapAABB(new RBColliderAABB(center, RBPhysUtil.V3Abs(pos_b - pos_a))))
                    {
                        foreach (var c in t.GetColliders())
                        {
                            if (c.gameObject.activeInHierarchy && c.enabled && !c.Trajectory.IsIgnoredTrajectory)
                            {
                                if (!(selectCols && ignoreCols.Contains(c)) && !IsIgnorePhysCastLayer(t.Layer) && (!IsTriggerLayer(t.Layer) || allowTriggerCollision))
                                {
                                    hitList.Add(new RBColliderCastHitInfo(c, t, RBColliderCastHitInfo.PhysCastType.Raycast));
                                }
                            }
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }

            for (int i = 0; i < hitList.Count; i++)
            {
                var t = hitList[i];

                switch (t.collider.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var info = RBRaycast.RaycastOBB.CalcRayCollision(t.collider.CalcOBB(), org, dir, d, ignoreBackFaceCollision);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, info.backFaceCollision);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                    case RBGeometryType.Sphere:
                        {
                            var info = RBRaycast.RaycastSphere.CalcRayCollision(t.collider.CalcSphere(), org, dir, d, ignoreBackFaceCollision);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, info.backFaceCollision);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                    case RBGeometryType.Capsule:
                        {
                            var info = RBRaycast.RaycastCaspule.CalcRayCollision(t.collider.CalcCapsule(), org, dir, d, ignoreBackFaceCollision);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, info.backFaceCollision);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                }
            }

            hitInfos = hitInfos.Where(item => item.IsValidHit).OrderBy(item => item.length).ToList();
        }

        public RBColliderCastHitInfo SphereCast(float delta, Vector3 org, Vector3 dir, float length, float radius, bool allowNegativeValue = true)
        {
            return SphereCast(delta, org, dir, length, radius, false, null, null, allowNegativeValue);
        }

        public RBColliderCastHitInfo SphereCast(float delta, Vector3 org, Vector3 dir, float length, float radius, bool allowTriggerCollision, RBCollider[] ignoreCols, RBTrajectory[] ignoreTrajs, bool allowNegativeValue = true)
        {
            return SphereCastAll(delta, org, dir, length, radius, allowTriggerCollision, ignoreCols, ignoreTrajs, allowNegativeValue).FirstOrDefault();
        }

        public List<RBColliderCastHitInfo> SphereCastAll(float delta, Vector3 org, Vector3 dir, float length, float radius, bool allowTriggerCollision, RBCollider[] ignoreCols, RBTrajectory[] ignoreTrajs, bool allowNegativeValue = true)
        {
            dir = dir.normalized;

            Vector3 pos_a = org;
            Vector3 pos_b = org + dir * length;
            Vector3 center = (pos_a + pos_b) / 2f;

            List<RBColliderCastHitInfo> hitList = new List<RBColliderCastHitInfo>();

            float xMin = Mathf.Min(pos_a.x, pos_b.x) - radius;
            float xMax = Mathf.Max(pos_a.x, pos_b.x) + radius;

            bool selectTrajs = ignoreTrajs != null;
            bool selectCols = ignoreCols != null;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (selectTrajs && ignoreTrajs.Contains(t))
                {
                    continue;
                }

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    if (t.trajectoryAABB.OverlapAABB(new RBColliderAABB(center, RBPhysUtil.V3Abs(pos_b - pos_a) + Vector3.one * radius * 2)))
                    {
                        foreach (var c in t.GetColliders())
                        {
                            if (c.gameObject.activeInHierarchy && c.enabled && !c.Trajectory.IsIgnoredTrajectory)
                            {
                                if (!(selectCols && ignoreCols.Contains(c)) && !IsIgnorePhysCastLayer(t.Layer) && (!IsTriggerLayer(t.Layer) || allowTriggerCollision))
                                {
                                    hitList.Add(new RBColliderCastHitInfo(c, t, RBColliderCastHitInfo.PhysCastType.SphereCast));
                                }
                            }
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }

            for (int i = 0; i < hitList.Count; i++)
            {
                var t = hitList[i];

                switch (t.collider.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var info = RBSphereCast.SphereCastOBB.CalcSphereCollision(t.collider.CalcOBB(), org, dir, length, radius, allowNegativeValue);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, false);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                    case RBGeometryType.Sphere:
                        {
                            var info = RBSphereCast.SphereCastSphere.CalcSphereCollision(t.collider.CalcSphere(), org, dir, length, radius, allowNegativeValue);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, false);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                    case RBGeometryType.Capsule:
                        {
                            var info = RBSphereCast.SphereCastCapsule.CalcSphereCollision(t.collider.CalcCapsule(), org, dir, length, radius, allowNegativeValue);
                            if (info.IsValidHit) t.SetHit(info.position, info.normal, info.length, false);
                            RBPhysDebugging.IsCastHitValidAssert(t);
                            hitList[i] = t;
                        }
                        break;
                }
            }

            hitList = hitList.Where(item => item.IsValidHit).OrderBy(item => item.length).ToList();
            return hitList;
        }

        public List<RBColliderOverlapInfo> BoxOverlap(RBColliderOBB obb)
        {
            List<RBColliderOverlapInfo> overlappings = new List<RBColliderOverlapInfo>();

            var fSize = obb.GetAxisSize(Vector3.right) / 2f;
            float xMin = obb.pos.x - fSize;
            float xMax = obb.pos.x + fSize;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    if (t.trajectoryAABB.OverlapAABB(new RBColliderAABB(obb.Center, obb.size)))
                    {
                        foreach (var c in t.GetColliders())
                        {
                            overlappings.Add(new RBColliderOverlapInfo(c));
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }

            for (int i = 0; i < overlappings.Count; i++)
            {
                var t = overlappings[i];

                switch (t.collider.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var p = RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollisionInfo(t.collider.CalcOBB(), obb);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pB, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Sphere:
                        {
                            var p = RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(obb, t.collider.CalcSphere());
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, -p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Capsule:
                        {
                            var p = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(obb, t.collider.CalcCapsule());
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, -p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                }
            }

            return overlappings;
        }

        public List<RBColliderOverlapInfo> SphereOverlap(RBColliderSphere sphere)
        {
            List<RBColliderOverlapInfo> overlappings = new List<RBColliderOverlapInfo>();

            float xMin = sphere.pos.x - sphere.radius;
            float xMax = sphere.pos.x + sphere.radius;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    if (t.trajectoryAABB.OverlapAABB(new RBColliderAABB(sphere.pos, new Vector3(sphere.radius, sphere.radius, sphere.radius) * 2)))
                    {
                        foreach (var c in t.GetColliders())
                        {
                            overlappings.Add(new RBColliderOverlapInfo(c));
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }

            for (int i = 0; i < overlappings.Count; i++)
            {
                var t = overlappings[i];

                switch (t.collider.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var p = RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(t.collider.CalcOBB(), sphere);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pB, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Sphere:
                        {
                            var p = RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollisionInfo(t.collider.CalcSphere(), sphere);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Capsule:
                        {
                            var p = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(sphere, t.collider.CalcCapsule());
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, -p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                }
            }

            return overlappings;
        }

        public List<RBColliderOverlapInfo> CapsuleOverlap(RBColliderCapsule capsule)
        {
            List<RBColliderOverlapInfo> overlappings = new List<RBColliderOverlapInfo>();

            var fSize = capsule.GetAxisSize(Vector3.right) / 2f;
            float xMin = capsule.pos.x - fSize;
            float xMax = capsule.pos.x + fSize;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    if (t.trajectoryAABB.OverlapAABB(new RBColliderAABB(capsule.pos, new Vector3(fSize * 2, capsule.GetAxisSize(Vector3.up), capsule.GetAxisSize(Vector3.forward)))))
                    {
                        foreach (var c in t.GetColliders())
                        {
                            overlappings.Add(new RBColliderOverlapInfo(c));
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }

            for (int i = 0; i < overlappings.Count; i++)
            {
                var t = overlappings[i];

                switch (t.collider.GeometryType)
                {
                    case RBGeometryType.OBB:
                        {
                            var p = RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(t.collider.CalcOBB(), capsule);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pB, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Sphere:
                        {
                            var p = RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(t.collider.CalcSphere(), capsule);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                    case RBGeometryType.Capsule:
                        {
                            var p = RBDetailCollision.DetailCollisionCapsuleCapsule.CalcDetailCollisionInfo(t.collider.CalcCapsule(), capsule);
                            if (p.p != Vector3.zero) t.SetOverlap(p.pA, p.p.normalized);
                            RBPhysDebugging.IsOverlapValidAssert(t);
                            overlappings[i] = t;
                        }
                        break;
                }
            }

            return overlappings;
        }

        public void AreaOverlapTrajectories(Vector3 originWs, Vector3 sizeWs, ref List<RBTrajectory> trajectories, int layer = -1)
        {
            bool ignoreLayer = layer < 0 || 32 <= layer;

            sizeWs = RBPhysUtil.V3Max(sizeWs, 0);

            var centerWs = originWs + (sizeWs / 2f);

            RBColliderAABB aabbWs = new RBColliderAABB(centerWs, sizeWs);

            float xMin = originWs.x;
            float xMax = originWs.x + sizeWs.x;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    bool layerPassed = ignoreLayer;
                    if (!ignoreLayer) layerPassed = (_collisionIgnoreLayers[layer] & (1 << t.Layer)) == 0;

                    if (t.trajectoryAABB.OverlapAABB(aabbWs) && layerPassed)
                    {
                        trajectories.Add(t);
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }
        }

        public void AreaOverlapRigidbodies(Vector3 originWs, Vector3 sizeWs, ref List<RBRigidbody> rigidbodies, int layer = -1)
        {
            bool ignoreLayer = layer < 0 || 32 <= layer;

            sizeWs = RBPhysUtil.V3Max(sizeWs, 0);

            var centerWs = originWs + (sizeWs / 2f);

            RBColliderAABB aabbWs = new RBColliderAABB(centerWs, sizeWs);

            float xMin = originWs.x;
            float xMax = originWs.x + sizeWs.x;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    bool layerPassed = ignoreLayer;
                    if (!ignoreLayer) layerPassed = (_collisionIgnoreLayers[layer] & (1 << t.Layer)) == 0;

                    if (t.trajectoryAABB.OverlapAABB(aabbWs) && layerPassed)
                    {
                        if (!t.IsStatic && t.Rigidbody) rigidbodies.Add(t.Rigidbody);
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }
        }

        public void AreaOverlapStaticColliders(Vector3 originWs, Vector3 sizeWs, ref List<RBCollider> collider, int layer = -1)
        {
            bool ignoreLayer = layer < 0 || 32 <= layer;

            sizeWs = RBPhysUtil.V3Max(sizeWs, 0);

            var centerWs = originWs + (sizeWs / 2f);

            RBColliderAABB aabbWs = new RBColliderAABB(centerWs, sizeWs);

            float xMin = originWs.x;
            float xMax = originWs.x + sizeWs.x;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                var t = _trajectories_orderByXMin[i];
                var f = _trajectories_xMin[i];

                if (xMin < t.trajectoryAABB.MaxX)
                {
                    bool layerPassed = ignoreLayer;
                    if (!ignoreLayer) layerPassed = (_collisionIgnoreLayers[layer] & (1 << t.Layer)) == 0;

                    if (t.trajectoryAABB.OverlapAABB(aabbWs) && layerPassed)
                    {
                        if (t.IsStatic)
                        {
                            collider.Add(t.Collider);
                        }
                    }
                }

                if (xMax < f)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplySolverVelocity(SolverInfo info, float sdt)
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplySolverVelocity(info, sdt, _timeScaleMode);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void PrepareSubtick()
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.PrepareSubtick();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplySubtick()
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplySubtick();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplyPhysFrame(float dt, RBRigidbody.VelocityOption velocityOption)
        {
            //FixedUpdate�I�����Ɏ��s

            // ====== �����t���[���E�C���h�E �����܂� ======

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ApplyTransform(dt, _timeScaleMode, velocityOption);
            }
        }

        void TrySleepRigidbodies()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodySleepTest");

            foreach (var rb in _rigidbodies)
            {
                rb.UpdatePhysSleepGrace();
            }

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.TryPhysSleep();
            }

            Profiler.EndSample();
        }

        void TryAwakeRigidbodies()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodyAwakeTest");
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.TryPhysAwake();
            }
            Profiler.EndSample();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateTransform()
        {
            UpdateRigidbodyTransform();
            UpdateColliderTransform();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateRigidbodyTransform()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateRigidbody");

            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform();
            }

            Profiler.EndSample();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateColliderTransform()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-UpdateCollider");

            foreach (RBCollider c in _colliders)
            {
                if (c.ParentRigidbody == null) c.UpdateTransform();
            }

            Profiler.EndSample();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearValidators()
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.ClearValidators();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearCollisions()
        {
            foreach (var rb in _rigidbodies)
            {
                if (!rb.isSleeping)
                {
                    ClearCollision(rb);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SyncTrajectories()
        {
            foreach (var traj in _trajectories_orderByXMin)
            {
                traj.SyncTrajectory();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearCollision(RBRigidbody rb)
        {
            for (int i = 0; i < rb.collidingCount; i++)
            {
                rb.colliding[i] = null;
            }

            if (Mathf.Max(2, rb.collidingCount) != rb.colliding.Length)
            {
                Array.Resize(ref rb.colliding, rb.collidingCount);
            }

            rb.collidingCount = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateObjTrajectory()
        {
            foreach (RBRigidbody rb in _rigidbodies)
            {
                rb.UpdateTransform();
            }
        }

        void SortTrajectories()
        {
            Profiler.BeginSample(name: "Physics-CollisionResolution-Sort");

            int count = _trajectories_orderByXMin.Length + _rbAddQueue.Count + _colAddQueue.Count;

            for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
            {
                _trajectories_xMin[i] = _trajectories_orderByXMin[i].trajectoryAABB.MinX;
            }

            if (_rbAddQueue.Any() || _colAddQueue.Any())
            {
                int offset = _trajectories_orderByXMin.Length;

                Array.Resize(ref _trajectories_orderByXMin, count);
                Array.Resize(ref _trajectories_xMin, count);

                for (int i = 0; i < _rbAddQueue.Count; i++)
                {
                    int p = offset + i;
                    _trajectories_xMin[p] = _rbAddQueue[i].ObjectTrajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _rbAddQueue[i].ObjectTrajectory;
                }

                for (int i = 0; i < _colAddQueue.Count; i++)
                {
                    int p = offset + _rbAddQueue.Count + i;
                    _trajectories_xMin[p] = _colAddQueue[i].Trajectory.trajectoryAABB.MinX;
                    _trajectories_orderByXMin[p] = _colAddQueue[i].Trajectory;
                }

                _rbAddQueue.Clear();
                _colAddQueue.Clear();
            }

            bool pRemove = _rbRemoveQueue.Any() || _colRemoveQueue.Any();

            {
                int rmvOffset_min = 0;

                //削除（間をつめる）
                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    bool isStatic = _trajectories_orderByXMin[i].IsStatic;

                    if (rmvOffset_min > 0)
                    {
                        _trajectories_orderByXMin[i - rmvOffset_min] = _trajectories_orderByXMin[i];
                        _trajectories_xMin[i - rmvOffset_min] = _trajectories_xMin[i];
                    }

                    if (!isStatic && _trajectories_orderByXMin[i].Rigidbody == null)
                    {
                        rmvOffset_min++;
                    }

                    if (pRemove)
                    {
                        if ((isStatic && _colRemoveQueue.Contains(_trajectories_orderByXMin[i].Collider)) || (!isStatic && _rbRemoveQueue.Contains(_trajectories_orderByXMin[i].Rigidbody)))
                        {
                            rmvOffset_min++;
                        }
                    }
                }

                Array.Resize(ref _trajectories_orderByXMin, _trajectories_orderByXMin.Length - rmvOffset_min);
                Array.Resize(ref _trajectories_xMin, _trajectories_xMin.Length - rmvOffset_min);

                _rbRemoveQueue.Clear();
                _colRemoveQueue.Clear();
            }

            _trajectories_orderByXMin = _trajectories_orderByXMin.Distinct().ToArray();

            //挿入ソート
            for (int i = 1; i < _trajectories_orderByXMin.Length; i++)
            {
                float a = _trajectories_xMin[i];
                var aTraj = _trajectories_orderByXMin[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    float p = _trajectories_xMin[j];
                    if (a < p)
                    {
                        _trajectories_xMin[j + 1] = p;
                        _trajectories_orderByXMin[j + 1] = _trajectories_orderByXMin[j];
                    }
                    else
                    {
                        _trajectories_xMin[j + 1] = a;
                        _trajectories_orderByXMin[j + 1] = aTraj;
                        break;
                    }

                    _trajectories_xMin[j] = a;
                    _trajectories_orderByXMin[j] = aTraj;
                }
            }

            Profiler.EndSample();
        }

        List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)> _detailCollisions = new List<(RBCollider col_a, RBCollider col_b, RBDetailCollision.Penetration p, RBCollision col)>();

        public void SolveConstraints(float dt)
        {
            //�Փˌ��m�i�u���[�h�t�F�[�Y�j

            List<(RBTrajectory, RBTrajectory)> collidingTrajs = new List<(RBTrajectory, RBTrajectory)>();

            Profiler.BeginSample(name: "Physics-CollisionResolution-TrajectoryAABBTest");
            {
                for (int i = 0; i < _trajectories_orderByXMin.Length; i++)
                {
                    RBTrajectory activeTraj = _trajectories_orderByXMin[i];

                    if (activeTraj.IsValidTrajectory && !activeTraj.IsIgnoredTrajectory)
                    {
                        float x_max = activeTraj.trajectoryAABB.MaxX;

                        for (int j = i + 1; j < _trajectories_orderByXMin.Length; j++)
                        {
                            RBTrajectory targetTraj = _trajectories_orderByXMin[j];

                            if ((_collisionIgnoreLayers[activeTraj.Layer] & (1 << targetTraj.Layer)) == 0)
                            {
                                if (!targetTraj.IsIgnoredTrajectory)
                                {
                                    bool isTrigger = IsTriggerLayer(activeTraj.Layer) ^ IsTriggerLayer(targetTraj.Layer);
                                    bool isAwake = !isTrigger && !(activeTraj.IsStaticOrSleeping && targetTraj.IsStaticOrSleeping);

                                    isAwake &= !(activeTraj.IsLimitedSleepingOrStatic && targetTraj.IsLimitedSleepingOrStatic);
                                    isAwake &= !(activeTraj.IsStatic && targetTraj.IsStatic);

                                    if (isAwake || isTrigger)
                                    {
                                        if (targetTraj.IsValidTrajectory)
                                        {
                                            float x_min_target = targetTraj.trajectoryAABB.MinX;
                                            float x_max_target = targetTraj.trajectoryAABB.MaxX;

                                            if (x_max < x_min_target)
                                            {
                                                break;
                                            }

                                            if (activeTraj.trajectoryAABB.OverlapAABB(targetTraj.trajectoryAABB))
                                            {
                                                collidingTrajs.Add((activeTraj, targetTraj));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Profiler.EndSample();

            //�Փˌ��m�i�i���[�t�F�[�Y�j�Ɖ��

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareDetailTest");

            _detailCollisions.Clear();

            foreach (var trajPair in collidingTrajs)
            {
                CalcAABBDetailCollision(trajPair.Item1, trajPair.Item2, ref _detailCollisions);
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-DetailTest");

            float ccdDelta = _solverDeltaTimeAsFloat;

            for (int i = 0; i < _detailCollisions.Count; i++)
            {
                var c = _detailCollisions[i];

                var p = CalcDetailCollision(c.col_a, c.col_b);
                _detailCollisions[i] = (c.col_a, c.col_b, p, null);
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-PrepareSolveCollisions");

            for (int i = 0; i < _detailCollisions.Count; i++)
            {
                var pair = _detailCollisions[i];

                if (pair.p.p != Vector3.zero)
                {
                    var traj_a = pair.col_a.Trajectory;
                    var traj_b = pair.col_b.Trajectory;

                    if (pair.col_a.ParentRigidbody != null) traj_a = pair.col_a.ParentRigidbody.ObjectTrajectory;
                    if (pair.col_b.ParentRigidbody != null) traj_b = pair.col_b.ParentRigidbody.ObjectTrajectory;

                    var rbc = new RBCollision(pair.col_a, pair.col_b, pair.p.p, traj_a.Layer, traj_b.Layer);

                    rbc.isStaticOrSleeping = traj_a.IsStaticOrSleeping && traj_b.IsStaticOrSleeping;
                    rbc.useSoftClip = rbc.collider_a.allowSoftClip && rbc.collider_b.allowSoftClip;

                    if (!traj_b.IsStatic || traj_b.IsLimitedRetrogradeVaild)
                    {
                        if (rbc.rigidbody_a != null) rbc.rigidbody_a.AddValidator(new RBCollisionValidator(traj_b));
                    }

                    if (!traj_a.IsStatic || traj_a.IsLimitedRetrogradeVaild)
                    {
                        if (rbc.rigidbody_b != null) rbc.rigidbody_b.AddValidator(new RBCollisionValidator(traj_a));
                    }

                    rbc.Update(pair.p.p, pair.p.pA, pair.p.pB);

                    _collisionsInSolver.Add(rbc);
                }
            }

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-RigidbodyPrepareSolve");

            foreach (RBCollision col in _collisionsInSolver)
            {
                if (col.rigidbody_a != null)
                {
                    if (col.rigidbody_a.isSleeping)
                    {
                        ClearCollision(col.rigidbody_a);
                    }
                    col.rigidbody_a.PhysAwake();
                    AddCollision(col.rigidbody_a, col.collider_b);
                }

                if (col.rigidbody_b != null)
                {
                    if (col.rigidbody_b.isSleeping)
                    {
                        ClearCollision(col.rigidbody_b);
                    }
                    col.rigidbody_b.PhysAwake();
                    AddCollision(col.rigidbody_b, col.collider_a);
                }

                if (IsTriggerLayer(col.layer_a) || IsTriggerLayer(col.layer_b))
                {
                    col.skipInSolver = true;
                    col.triggerCollision = true;
                }
            }
            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-SolveCollisions/StdSolver");

            //Debug.Log(cpu_std_solver_internal_sync_per_iteration);

            var solverOption = PackSolverOption();

            PrepareSubtick();

            float sdt = dt / solver_subtick;

            {
                var solverInfo = PackSolverInfo();
                if (_stdSolverInit != null) _stdSolverInit(sdt, solverInfo);
            }

            for (int i = 0; i < solver_subtick; i++)
            {
                Profiler.BeginSample(name: "SolveConstraints/Subticks/Resolve");
                Profiler.BeginSample(name: "SolveConstraints/Subticks" + i + "/Resolve");

                for (int j = 0; j < solver_iter_per_subtick; j++)
                {
                    var solverInfo = PackSolverInfo(i, j);

                    if (_stdSolverIter != null) _stdSolverIter(solverInfo);

                    for (int k = 0; k < _collisionsInSolver.Count; k++)
                    {
                        var col = _collisionsInSolver[k];

                        if (col.penetration != Vector3.zero && !col.skipInSolver)
                        {
                            col.InitVelocityConstraint(sdt, _timeScaleMode, solverOption, j == 0);
                            SolveCollisionPair(col);
                        }
                    }
                }

                Profiler.EndSample();
                Profiler.EndSample();

                Profiler.BeginSample(name: "SolveConstraints/Subticks/DetailTest");
                Profiler.BeginSample(name: "SolveConstraints/Subticks" + (i + 1) + "/DetailTest");

                ApplySolverVelocity(PackSolverInfo(i), sdt);

                if (i < solver_subtick - 1)
                {
                    for (int k = 0; k < _collisionsInSolver.Count; k++)
                    {
                        var col = _collisionsInSolver[k];

                        if (!col.triggerCollision)
                        {
                            var p = RecalcCollision(col);
                            col.Update(p.p, p.pA, p.pB);
                            col.skipInSolver = false;
                        }
                    }
                }

                Profiler.EndSample();
                Profiler.EndSample();
            }

            ApplySubtick();

            Profiler.EndSample();

            Profiler.BeginSample(name: "Physics-CollisionResolution-OnCollision");

            foreach (var rbc in _collisionsInSolver)
            {
                RBCollisionInfo info_a, info_b;

                if (rbc.triggerCollision)
                {
                    info_a = RBCollisionInfo.GetTriggerCollision(rbc.isStaticOrSleeping, -rbc.penetration, rbc.layer_b);
                    info_b = RBCollisionInfo.GetTriggerCollision(rbc.isStaticOrSleeping, rbc.penetration, rbc.layer_a);
                }
                else
                {
                    Vector3 contact = (rbc.aNearest + rbc.bNearest) / 2f;

                    Vector3 relVel = rbc.solverCache_velAdd_a - rbc.solverCache_velAdd_b;

                    info_a = new RBCollisionInfo(rbc.rigidbody_b, contact, -rbc.penetration, -relVel, rbc.ContactNormal, rbc.isStaticOrSleeping, rbc.layer_b);
                    info_b = new RBCollisionInfo(rbc.rigidbody_a, contact, rbc.penetration, relVel, -rbc.ContactNormal, rbc.isStaticOrSleeping, rbc.layer_a);
                }

                if (rbc.rigidbody_a != null) rbc.rigidbody_a.OnCollision(rbc.collider_b, info_a);
                if (rbc.collider_a != null) rbc.collider_a.OnCollision(rbc.collider_b, info_a);
                if (rbc.rigidbody_b != null) rbc.rigidbody_b.OnCollision(rbc.collider_a, info_b);
                if (rbc.collider_a != null) rbc.collider_b.OnCollision(rbc.collider_a, info_b);
            }

            Profiler.EndSample();

            _collisionsInSolver.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        RBDetailCollision.Penetration RecalcCollision(RBCollision col)
        {
            return CalcDetailCollision(col.collider_a, col.rigidbody_a, col.collider_b, col.rigidbody_b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        RBDetailCollision.Penetration CalcDetailCollision(RBCollider col_a, RBCollider col_b)
        {
            return CalcDetailCollision(col_a, col_a.ParentRigidbody, col_b, col_b.ParentRigidbody);
        }

        RBDetailCollision.Penetration CalcDetailCollision(RBCollider col_a, RBRigidbody rb_a, RBCollider col_b, RBRigidbody rb_b)
        {
            var geomType_a = col_a.GeometryType;
            var geomType_b = col_b.GeometryType;

            var offset_a = Vector3.zero;
            var offset_b = Vector3.zero;

            if (rb_a != null) offset_a = col_a.CCDOffset;
            if (rb_b != null) offset_b = col_b.CCDOffset;

            if (geomType_a == RBGeometryType.OBB && geomType_b == RBGeometryType.OBB)
            {
                //OBB-OBB
                return RBDetailCollision.DetailCollisionOBBOBB.CalcDetailCollisionInfo(col_a.CalcOBB(), col_b.CalcOBB());
            }
            else if (geomType_a == RBGeometryType.OBB && geomType_b == RBGeometryType.Sphere)
            {
                //OBB-Sphere
                if (col_b.useCCD)
                {
                    return RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfoCCD(col_a.CalcOBB(), col_b.CalcSphere(), offset_a, offset_b);
                }
                else
                {
                    return RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(col_a.CalcOBB(), col_b.CalcSphere());
                }
            }
            else if (geomType_a == RBGeometryType.Sphere && geomType_b == RBGeometryType.OBB)
            {
                //Sphere-OBB
                if (col_a.useCCD)
                {
                    return RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfoCCD(col_b.CalcOBB(), col_a.CalcSphere(), offset_b, offset_a).inverted;
                }
                else
                {
                    return RBDetailCollision.DetailCollisionOBBSphere.CalcDetailCollisionInfo(col_b.CalcOBB(), col_a.CalcSphere()).inverted;
                }
            }
            else if (geomType_a == RBGeometryType.Sphere && geomType_b == RBGeometryType.Sphere)
            {
                //Sphere-Sphere
                if (col_a.useCCD || col_b.useCCD)
                {
                    return RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollisionInfoCCD(col_a.CalcSphere(), col_b.CalcSphere(), offset_a, offset_b);
                }
                else
                {
                    return RBDetailCollision.DetailCollisionSphereSphere.CalcDetailCollisionInfo(col_a.CalcSphere(), col_b.CalcSphere());
                }
            }
            else if (geomType_a == RBGeometryType.OBB && geomType_b == RBGeometryType.Capsule)
            {
                //OBB-Capsule

                return RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(col_a.CalcOBB(), col_b.CalcCapsule()); ;
            }
            else if (geomType_a == RBGeometryType.Capsule && geomType_b == RBGeometryType.OBB)
            {
                //Capsule-OBB

                return RBDetailCollision.DetailCollisionOBBCapsule.CalcDetailCollisionInfo(col_b.CalcOBB(), col_a.CalcCapsule()).inverted;
            }
            else if (geomType_a == RBGeometryType.Sphere && geomType_b == RBGeometryType.Capsule)
            {
                //Sphere-Capsule

                if (col_a.useCCD)
                {
                    return RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfoCCD(col_a.CalcSphere(), col_b.CalcCapsule(), offset_a, offset_b);
                }
                else
                {
                    return RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(col_a.CalcSphere(), col_b.CalcCapsule());
                }
            }
            else if (geomType_a == RBGeometryType.Capsule && geomType_b == RBGeometryType.Sphere)
            {
                //Capsule-Sphere

                if (col_b.useCCD)
                {
                    return RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfoCCD(col_b.CalcSphere(), col_a.CalcCapsule(), offset_b, offset_a).inverted;
                }
                else
                {
                    return RBDetailCollision.DetailCollisionSphereCapsule.CalcDetailCollisionInfo(col_b.CalcSphere(), col_a.CalcCapsule()).inverted;
                }
            }
            else if (geomType_a == RBGeometryType.Capsule && geomType_b == RBGeometryType.Capsule)
            {
                //Capsule-Capsule

                return RBDetailCollision.DetailCollisionCapsuleCapsule.CalcDetailCollisionInfo(col_a.CalcCapsule(), col_b.CalcCapsule());
            }
            else throw new Exception();
        }

        void AddCollision(RBRigidbody rb, RBCollider collider)
        {
            if (rb.colliding.Length <= rb.collidingCount)
            {
                Array.Resize(ref rb.colliding, rb.collidingCount + 1);
            }

            rb.colliding[rb.collidingCount] = collider;
            rb.collidingCount++;
        }

        void SolveCollisionPair(RBCollision col)
        {
            if (col.penetration != Vector3.zero)
            {
                (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) = SolveCollision(col);
                //Debug.Log((velAdd_a, angVelAdd_a, velAdd_b, angVelAdd_b, velAdd_b.magnitude, angVelAdd_b.magnitude, col.rigidbody_b.ExpVelocity, col.rigidbody_b.ExpAngularVelocity));

                if (col.rigidbody_a != null)
                {
                    col.rigidbody_a.SubtickVelocity += velAdd_a;
                    col.rigidbody_a.SubtickAngularVelocity += angVelAdd_a;
                }

                if (col.rigidbody_b != null)
                {
                    col.rigidbody_b.SubtickVelocity += velAdd_b;
                    col.rigidbody_b.SubtickAngularVelocity += angVelAdd_b;
                }

                col.solverCache_velAdd_a += velAdd_a;
                col.solverCache_velAdd_b += velAdd_b;

                if (velAdd_a.sqrMagnitude < solver_abort_veladd_sqrt && angVelAdd_a.sqrMagnitude < solver_abort_angveladd_sqrt && velAdd_b.sqrMagnitude < solver_abort_veladd_sqrt && angVelAdd_b.sqrMagnitude < solver_abort_angveladd_sqrt)
                {
                    col.skipInSolver = true;
                }
            }
        }

        //void UpdateTrajectoryPair(RBCollision col, float dt)
        //{
        //    RecalculateCollision(dt, col.collider_a, col.collider_b, col.info, out Vector3 p, out Vector3 pA, out Vector3 pB);

        //    if (p != Vector3.zero)
        //    {
        //        p = col.collider_a.ExpToCurrentVector(p);
        //        pA = col.collider_a.ExpToCurrent(pA);
        //        pB = col.collider_b.ExpToCurrent(pB);
        //        col.Update(p, pA, pB);
        //        col.InitVelocityConstraint(dt, _timeScaleMode, false);
        //    }
        //    else
        //    {
        //        col.skipInSolver = true;
        //    }
        //}

        List<(RBCollider collider, RBColliderAABB aabb)> trajAABB_a_cache = new List<(RBCollider collider, RBColliderAABB aabb)>();
        List<(RBCollider collider, RBColliderAABB aabb)> trajAABB_b_cache = new List<(RBCollider collider, RBColliderAABB aabb)>();

        void CalcAABBDetailCollision(RBTrajectory traj_a, RBTrajectory traj_b, ref List<(RBCollider, RBCollider, RBDetailCollision.Penetration, RBCollision)> cols)
        {
            trajAABB_a_cache.Clear();
            trajAABB_b_cache.Clear();

            if (traj_a.IsStatic)
            {
                trajAABB_a_cache.Add((traj_a.Collider, traj_a.Collider.Trajectory.trajectoryAABB));
            }
            else
            {
                foreach (var c in traj_a.Rigidbody.GetColliders())
                {
                    trajAABB_a_cache.Add((c, c.Trajectory.trajectoryAABB));
                }
            }

            if (traj_b.IsStatic)
            {
                trajAABB_b_cache.Add((traj_b.Collider, traj_b.Collider.Trajectory.trajectoryAABB));
            }
            else
            {
                foreach (var c in traj_b.Rigidbody.GetColliders())
                {
                    trajAABB_b_cache.Add((c, c.Trajectory.trajectoryAABB));
                }
            }

            //�R���C�_���ɐڐG�𔻒�
            for (int i = 0; i < trajAABB_a_cache.Count; i++)
            {
                var collider_a = trajAABB_a_cache[i];

                if (collider_a.collider.VEnabled)
                {
                    for (int j = 0; j < trajAABB_b_cache.Count; j++)
                    {
                        var collider_b = trajAABB_b_cache[j];

                        if (collider_b.collider.VEnabled)
                        {
                            bool aabbOverlapping = collider_a.aabb.OverlapAABB(collider_b.aabb);
                            if (aabbOverlapping) cols.Add((collider_a.collider, collider_b.collider, default, null));
                        }
                    }
                }
            }
        }

        (Vector3 velAdd_a, Vector3 angVelAdd_a, Vector3 velAdd_b, Vector3 angVelAdd_b) SolveCollision(RBCollision col)
        {
            col.SolveVelocityConstraints(out Vector3 velocityAdd_a, out Vector3 angularVelocityAdd_a, out Vector3 velocityAdd_b, out Vector3 angularVelocityAdd_b, _timeScaleMode);
            return (velocityAdd_a, angularVelocityAdd_a, velocityAdd_b, angularVelocityAdd_b);
        }

        public void Dispose()
        {
            _solverTimeInitialized = false;
        }

        public class PhysComputerTime
        {
            public PhysComputerTime(RBPhysComputer computer)
            {
                this.computer = computer;
            }

            public RBPhysComputer computer;

            public bool Initialized { get { return computer._solverTimeInitialized; } }

            public float SolverSetDeltaTime { get { return computer.timeParams.fixedDeltaTime; } }
            public double SolverTime { get { return computer._solverTime; } }
            public double SolverUnscaledTime { get { return computer._solverUnscaledTime; } }
            public double SolverDeltaTime { get { return computer._solverDeltaTime; } }
            public double SolverUnscaledDeltaTime { get { return computer._solverUnscaledDeltaTime; } }
        }

        bool _solverTimeInitialized;
        double _solverTime;
        double _solverUnscaledTime;
        double _solverDeltaTime;
        double _solverUnscaledDeltaTime;
        float _solverDeltaTimeAsFloat { get { return (float)_solverDeltaTime; } }
        double _solverUnscaledDeltaTimeAsFloat { get { return (float)_solverUnscaledDeltaTime; } }
    }

    public struct ComputerTimeParams
    {
        public float fixedDeltaTime;
        public float timeScale;
        public bool enableAutoTimeIntergrading;

        public ComputerTimeParams(float fixedDeltaTime, float timeScale, bool enableAutoTimeIntergrading)
        {
            this.fixedDeltaTime = fixedDeltaTime;
            this.timeScale = timeScale;
            this.enableAutoTimeIntergrading = enableAutoTimeIntergrading;
        }

        public static ComputerTimeParams GetDefault()
        {
            var p = new ComputerTimeParams();
            p.fixedDeltaTime = .01f;
            p.timeScale = 1f;
            p.enableAutoTimeIntergrading = true;

            return p;
        }
    }

    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, true)]
    public class RBCollision
    {
        public RBCollider collider_a;
        public RBRigidbody rigidbody_a;
        public RBCollider collider_b;
        public RBRigidbody rigidbody_b;

        public int layer_a;
        public int layer_b;

        public Vector3 cg_a;
        public Vector3 cg_b;
        public Vector3 aNearest;
        public Vector3 bNearest;
        public Vector3 penetration;
        public Vector3 ContactNormal { get { return _contactNormal; } set { _contactNormal = value.normalized; } }
        public Vector3 rA;
        public Vector3 rB;

        public Vector3 solverCache_velAdd_a;
        public Vector3 solverCache_velAdd_b;

        public bool skipInSolver;

        public bool useSoftClip;
        public bool triggerCollision;
        public bool isStaticOrSleeping;

        Jacobian _jN = new Jacobian(Jacobian.Type.Normal); //Normal
        Jacobian _jT = new Jacobian(Jacobian.Type.Tangent); //Tangent
        //Jacobian _jB = new Jacobian(Jacobian.Type.Tangent); //Bi-Tangent

        public Vector3 Velocity_a { get { return GetVelocity(rigidbody_a, collider_a); } }
        public Vector3 AngularVelocity_a { get { return GetAngularVelocity(rigidbody_a, collider_a); } }
        public Vector3 ExpVelocity_a { get { return GetExpVelocity(rigidbody_a, collider_a); } }
        public Vector3 ExpAngularVelocity_a { get { return GetExpAngularVelocity(rigidbody_a, collider_a); } }
        public Vector3 SubtickVelocity_a { get { return GetSubtickVelocity(rigidbody_a, collider_a); } }
        public Vector3 SubtickAngularVelocity_a { get { return GetSubtickAngularVelocity(rigidbody_a, collider_a); } }
        public float InverseMass_a { get { return GetInvMass(rigidbody_a); } }
        public Vector3 InverseInertiaWs_a { get { return GetInvInertiaWs(rigidbody_a); } }

        public Vector3 Velocity_b { get { return GetVelocity(rigidbody_b, collider_b); } }
        public Vector3 AngularVelocity_b { get { return GetAngularVelocity(rigidbody_b, collider_b); } }
        public Vector3 ExpVelocity_b { get { return GetExpVelocity(rigidbody_b, collider_b); } }
        public Vector3 ExpAngularVelocity_b { get { return GetExpAngularVelocity(rigidbody_b, collider_b); } }
        public Vector3 SubtickVelocity_b { get { return GetSubtickVelocity(rigidbody_b, collider_b); } }
        public Vector3 SubtickAngularVelocity_b { get { return GetSubtickAngularVelocity(rigidbody_b, collider_b); } }
        public float InverseMass_b { get { return GetInvMass(rigidbody_b); } }
        public Vector3 InverseInertiaWs_b { get { return GetInvInertiaWs(rigidbody_b); } }

        public bool IsSleeping_a
        {
            get
            {
                if (rigidbody_a != null) return rigidbody_a.isSleeping;
                else return false;
            }
        }

        public bool IsSleeping_b
        {
            get
            {
                if (rigidbody_b != null) return rigidbody_b.isSleeping;
                else return false;
            }
        }

        Vector3 _contactNormal;

        public RBCollision(RBCollider col_a, RBCollider col_b, Vector3 penetration, int layer_a, int layer_b)
        {
            collider_a = col_a;
            rigidbody_a = col_a.ParentRigidbody;
            collider_b = col_b;
            rigidbody_b = col_b.ParentRigidbody;

            cg_a = col_a.GetColliderCenter();
            cg_b = col_b.GetColliderCenter();

            if (rigidbody_a != null) cg_a = rigidbody_a.CenterOfGravityWorld;
            if (rigidbody_b != null) cg_b = rigidbody_b.CenterOfGravityWorld;

            this.penetration = penetration;
            _contactNormal = penetration.normalized;

            this.layer_a = layer_a;
            this.layer_b = layer_b;
        }

        public void ClearSolverCache()
        {
            solverCache_velAdd_a = Vector3.zero;
            solverCache_velAdd_b = Vector3.zero;
        }

        public bool ObjectEquals(RBCollision col)
        {
            return ObjectEquals(col, false, out _);
        }

        public bool ObjectEquals(RBCollision col, bool containsInverted, out bool isInverted)
        {
            isInverted = false;

            if (col == null)
            {
                return true;
            }

            if (col != null)
            {
                bool b1 = collider_a = col.collider_a;
                bool b2 = collider_b = col.collider_b;
                bool b3 = rigidbody_a = col.rigidbody_a;
                bool b4 = rigidbody_b = col.rigidbody_b;

                bool bRet = (b1 && b2 && b3 && b4);

                if (!containsInverted && !bRet)
                {
                    bool bc1 = collider_a = col.collider_b;
                    bool bc2 = collider_b = col.collider_a;
                    bool bc3 = rigidbody_a = col.rigidbody_b;
                    bool bc4 = rigidbody_b = col.rigidbody_a;

                    bool bcRet = (bc1 && bc2 && bc3 && bc4);

                    isInverted = bcRet;
                    return bcRet;
                }
                else
                {
                    return bRet;
                }
            }

            return false;
        }

        public void SwapTo(RBCollider col_a, RBCollider col_b)
        {
            if ((collider_a, collider_b) == (col_b, col_a))
            {
                (collider_a, collider_b) = (col_a, col_b);
                (rigidbody_a, rigidbody_b) = (rigidbody_b, rigidbody_a);
                (cg_a, cg_b) = (cg_b, cg_a);
                (aNearest, bNearest) = (bNearest, aNearest);
                penetration = -penetration;
                _contactNormal = -_contactNormal;
                (rA, rB) = (rB, rA);
            }
        }

        public void Update(Vector3 penetration, Vector3 aNearest, Vector3 bNearest)
        {
            this.aNearest = aNearest;
            this.bNearest = bNearest;

            this.penetration = penetration;
            _contactNormal = penetration.normalized;

            cg_a = collider_a.GetColliderCenter();
            cg_b = collider_b.GetColliderCenter();

            if (rigidbody_a != null) cg_a = rigidbody_a.CenterOfGravityWorld;
            if (rigidbody_b != null) cg_b = rigidbody_b.CenterOfGravityWorld;

            rA = aNearest - cg_a;
            rB = bNearest - cg_b;

            skipInSolver = false;
            triggerCollision = false;
        }

        public void InitVelocityConstraint(float dt, TimeScaleMode tMode, SolverOption option, bool initLambda = true)
        {
            Vector3 contactNormal = ContactNormal;
            Vector3 tangent = Vector3.ProjectOnPlane(SubtickVelocity_b - SubtickVelocity_a, contactNormal).normalized;
            //Vector3.OrthoNormalize(ref contactNormal, ref tangent, ref bitangent);

            _jN.Init(this, contactNormal, dt, tMode, option, initLambda);
            _jT.Init(this, tangent, dt, tMode, option, initLambda);
        }

        public void SolveVelocityConstraints(out Vector3 vAdd_a, out Vector3 avAdd_a, out Vector3 vAdd_b, out Vector3 avAdd_b, TimeScaleMode tMode)
        {
            vAdd_a = Vector3.zero;
            avAdd_a = Vector3.zero;
            vAdd_b = Vector3.zero;
            avAdd_b = Vector3.zero;

            (vAdd_a, avAdd_a, vAdd_b, avAdd_b) = _jN.Resolve(this, vAdd_a, avAdd_a, vAdd_b, avAdd_b, tMode);
            (vAdd_a, avAdd_a, vAdd_b, avAdd_b) = _jT.Resolve(this, vAdd_a, avAdd_a, vAdd_b, avAdd_b, tMode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.Velocity;
            else if (c != null) return c.Trajectory.StaticVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetAngularVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.AngularVelocity;
            else if (c != null) return c.Trajectory.StaticAngularVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetExpVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.ExpVelocity;
            else if (c != null) return c.Trajectory.StaticVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetExpAngularVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.ExpAngularVelocity;
            else if (c != null) return c.Trajectory.StaticAngularVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetSubtickVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.SubtickVelocity;
            else if (c != null) return c.Trajectory.StaticVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetSubtickAngularVelocity(RBRigidbody rb, RBCollider c)
        {
            if (rb != null) return rb.SubtickAngularVelocity;
            else if (c != null) return c.Trajectory.StaticAngularVelocity;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetInvMass(RBRigidbody rb)
        {
            if (rb != null) return rb.InverseMass;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetInvInertiaWs(RBRigidbody rb)
        {
            if (rb != null) return rb.InverseInertiaWs;
            else return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Vector3 GetInvInertiaWs(RBRigidbody rb, float inertiaTensorMultiplier)
        {
            if (rb != null) return rb.GetInvInertiaTensor(inertiaTensorMultiplier);
            else return default;
        }

        const float COLLISION_ERROR_SLOP = 0.001f;

        public struct SolverOption
        {
            public float retrograde_phys_restitution_multiplier;
            public float retrograde_phys_restitution_min;
            public float retrograde_phys_friction_multiplier;
            public float softClip_lambda_multiplier;
            public float inertia_tensor_multiplier;

            public static SolverOption GetDefault()
            {
                var option = new SolverOption();

                option.retrograde_phys_restitution_multiplier = RBPhysComputer.RETROGRADE_PHYS_RESTITUTION_MULTIPLIER;
                option.retrograde_phys_restitution_min = RBPhysComputer.RETROGRADE_PHYS_RESTITUTION_MIN;
                option.retrograde_phys_friction_multiplier = RBPhysComputer.RETROGRADE_PHYS_FRICTION_MULTIPLIER;

                option.softClip_lambda_multiplier = RBPhysComputer.SOFTCLIP_LAMBDA_MULTIPLIER;
                option.inertia_tensor_multiplier = RBPhysComputer.INERTIA_TENSOR_MULTIPLIER;

                return option;
            }
        }

        class Jacobian
        {
            // Jv + b >= 0

            Type _type;

            Vector3 _va;
            Vector3 _wa;
            Vector3 _vb;
            Vector3 _wb;

            float _bias;
            float _restitutionBias;
            float _totalLambda;
            float _effectiveMass;

            bool _useSoftClip;

            SolverOption _solverOption;

            public enum Type
            {
                Normal,
                Tangent
            }

            public Jacobian(Type type)
            {
                _type = type;

                _va = Vector3.zero;
                _wa = Vector3.zero;
                _vb = Vector3.zero;
                _wb = Vector3.zero;

                _bias = 0;
                _restitutionBias = 0;
                _totalLambda = 0;
                _effectiveMass = 0;

                _useSoftClip = false;
            }

            public void Init(RBCollision col, Vector3 dir, float dt, TimeScaleMode tMode, SolverOption solverOption, bool initLambda = true)
            {
                _solverOption = solverOption;

                Vector3 dirN = dir;

                _va = dirN;
                _wa = Vector3.Cross(col.rA, dirN);
                _vb = -dirN;
                _wb = Vector3.Cross(col.rB, -dirN);

                if (initLambda)
                {
                    _bias = 0;
                    _restitutionBias = 0;
                    _totalLambda = 0;
                }

                const float EPSILON = .000001f;
                if (_solverOption.inertia_tensor_multiplier < EPSILON) throw new Exception();

                var invInertiaWs_a = col.GetInvInertiaWs(col.rigidbody_a, _solverOption.inertia_tensor_multiplier);
                var invInertiaWs_b = col.GetInvInertiaWs(col.rigidbody_b, _solverOption.inertia_tensor_multiplier);

                float k = 0;
                k += col.InverseMass_a;
                k += Vector3.Dot(_wa, Vector3.Scale(invInertiaWs_a, _wa));
                k += col.InverseMass_b;
                k += Vector3.Dot(_wb, Vector3.Scale(invInertiaWs_b, _wb));

                _effectiveMass = 1 / k;

                if (_type == Type.Normal)
                {
                    float beta = col.collider_a.beta * col.collider_b.beta;
                    _bias = -(beta / dt) * Mathf.Max(0, col.penetration.magnitude - COLLISION_ERROR_SLOP);

                    if (initLambda)
                    {
                        float restitution = col.collider_a.restitution * col.collider_b.restitution;

                        if (tMode.IsRetg())
                        {
                            restitution = (restitution != 0) ? 1 / restitution : 0;
                            restitution *= _solverOption.retrograde_phys_restitution_multiplier;
                            restitution = (restitution != 0) ? Mathf.Max(restitution, _solverOption.retrograde_phys_restitution_min) : 0;
                        }

                        Vector3 relVel = Vector3.zero;
                        relVel += col.ExpVelocity_a;
                        relVel += Vector3.Cross(col.ExpAngularVelocity_a, col.rA);
                        relVel -= col.ExpVelocity_b;
                        relVel -= Vector3.Cross(col.ExpAngularVelocity_b, col.rB);

                        float closingVelocity = Vector3.Dot(relVel, -dirN);
                        _restitutionBias = -restitution * closingVelocity;
                    }

                    _useSoftClip = col.useSoftClip;
                }
            }

            public (Vector3, Vector3, Vector3, Vector3) Resolve(RBCollision col, Vector3 vAdd_a, Vector3 avAdd_a, Vector3 vAdd_b, Vector3 avAdd_b, TimeScaleMode tMode)
            {
                float jv = 0;
                jv += Vector3.Dot(_va, col.SubtickVelocity_a);
                jv += Vector3.Dot(_wa, col.SubtickAngularVelocity_a);
                jv += Vector3.Dot(_vb, col.SubtickVelocity_b);
                jv += Vector3.Dot(_wb, col.SubtickAngularVelocity_b);

                float lambda = _effectiveMass * (-(jv + Mathf.Min(_bias, _restitutionBias)));
                float oldTotalLambda = _totalLambda;

                if (_useSoftClip)
                {
                    lambda *= _solverOption.softClip_lambda_multiplier;
                }

                if (_type == Type.Normal)
                {
                    _totalLambda = Mathf.Max(0.0f, _totalLambda + lambda);
                }
                else if (_type == Type.Tangent)
                {
                    float friction = col.collider_a.friction * col.collider_b.friction;
                    if (tMode.IsRetg()) friction *= _solverOption.retrograde_phys_friction_multiplier;

                    float maxFriction = friction * col._jN._totalLambda;

                    bool c_friction = col.collider_a.friction >= 1 || col.collider_b.friction >= 1;

                    if (tMode.IsProg()) _totalLambda = Mathf.Clamp(_totalLambda + lambda, -maxFriction, maxFriction);
                    else
                    {
                        if (c_friction) _totalLambda = Mathf.Clamp(_totalLambda + lambda, -maxFriction, maxFriction);
                        else _totalLambda = Mathf.Clamp(_totalLambda - lambda, -maxFriction, maxFriction);
                    }
                }

                lambda = _totalLambda - oldTotalLambda;

                const float EPSILON = .000001f;
                if (_solverOption.inertia_tensor_multiplier < EPSILON) throw new Exception();

                var invInertiaWs_a = col.GetInvInertiaWs(col.rigidbody_a, _solverOption.inertia_tensor_multiplier);
                var invInertiaWs_b = col.GetInvInertiaWs(col.rigidbody_b, _solverOption.inertia_tensor_multiplier);

                vAdd_a += col.InverseMass_a * _va * lambda;
                avAdd_a += Vector3.Scale(invInertiaWs_a, _wa) * lambda;
                vAdd_b += col.InverseMass_b * _vb * lambda;
                avAdd_b += Vector3.Scale(invInertiaWs_b, _wb) * lambda;

                return (vAdd_a, avAdd_a, vAdd_b, avAdd_b);
            }
        }
    }

    public struct RBCollisionInfo
    {
        public readonly float impulse;
        public readonly float vDiff;

        public readonly bool isTriggerCollision;
        public readonly bool isStaticOrSleeping;

        public readonly Vector3 penetration;
        public readonly Vector3 contactPoint;
        public readonly Vector3 normal;

        public readonly Vector3 velAddRelative;

        public readonly int layer_other;

        public RBCollisionInfo(RBRigidbody rbRigidbody, Vector3 contactPoint, Vector3 penetration, Vector3 velAddRelative, Vector3 normal, bool isStaticOrSleeping, int layer_other)
        {
            vDiff = Vector3.Project(velAddRelative, normal).magnitude;

            impulse = 0;
            if (rbRigidbody != null) impulse = vDiff * rbRigidbody.mass;

            isTriggerCollision = false;
            this.isStaticOrSleeping = isStaticOrSleeping;

            this.normal = normal;
            this.penetration = penetration;
            this.contactPoint = contactPoint;

            this.velAddRelative = velAddRelative;

            this.layer_other = layer_other;
        }

        public RBCollisionInfo(bool isTriggerCollision, bool isStaticOrSleeping, Vector3 penetration, int layer_other)
        {
            this.isTriggerCollision = isTriggerCollision;
            this.isStaticOrSleeping = isStaticOrSleeping;

            this.penetration = penetration;

            vDiff = 0;
            impulse = 0;

            normal = default;
            contactPoint = default;
            velAddRelative = default;

            this.layer_other = layer_other;
        }

        public static RBCollisionInfo GetTriggerCollision(bool isStaticOrSleeping, Vector3 penetration, int layer_other)
        {
            return new RBCollisionInfo(true, isStaticOrSleeping, penetration, layer_other);
        }
    }

    public struct RBColliderAABB
    {
        public bool isValidAABB { get { return _isValidAABB; } }
        public Vector3 Center { get; private set; }
        public Vector3 Size { get; private set; }
        public Vector3 Extents { get { return Size / 2f; } }

        public float MinX { get { return _minX; } }
        public float MaxX { get { return _maxX; } }
        public Vector3 Min { get { return _min; } }
        public Vector3 Max { get { return _max; } }

        float _minX;
        float _maxX;
        Vector3 _min;
        Vector3 _max;

        bool _isValidAABB;

        public RBColliderAABB(Vector3 center, Vector3 size)
        {
            _isValidAABB = true;
            this.Center = center;
            this.Size = RBPhysUtil.V3Abs(size);

            _min = Center - Size / 2;
            _max = Center + Size / 2;
            _minX = _min.x;
            _maxX = _max.x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(Vector3 point)
        {
            if (_isValidAABB)
            {
                if (!ContainsPoint(point))
                {
                    Vector3 res_min = Vector3.Min(Min, point);
                    Vector3 res_max = Vector3.Max(Max, point);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
            }
            else
            {
                Center = point;
                Size = Vector3.zero;
                _isValidAABB = true;

                _min = Center;
                _max = Center;
                _minX = _min.x;
                _maxX = _max.x;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(RBColliderAABB aabb)
        {
            if (aabb._isValidAABB)
            {
                if (_isValidAABB)
                {
                    Vector3 res_min = Vector3.Min(Min, aabb.Min);
                    Vector3 res_max = Vector3.Max(Max, aabb.Max);

                    Center = (res_min + res_max) / 2f;
                    Size = res_max - res_min;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
                else
                {
                    Center = aabb.Center;
                    Size = aabb.Size;
                    _isValidAABB = true;

                    _min = Center - Size / 2;
                    _max = Center + Size / 2;
                    _minX = _min.x;
                    _maxX = _max.x;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsPoint(Vector3 point)
        {
            return _isValidAABB && RBPhysUtil.IsV3Less(Min, point) && RBPhysUtil.IsV3Less(point, Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool OverlapAABB(RBColliderAABB ext)
        {
            if (_isValidAABB && ext._isValidAABB)
            {
                if (!RBPhysUtil.RangeOverlap(Min.x, Max.x, ext.Min.x, ext.Max.x)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.y, Max.y, ext.Min.y, ext.Max.y)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.z, Max.z, ext.Min.z, ext.Max.z)) return false;

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool OverlapAABBEpsilon(RBColliderAABB ext, float epsilon)
        {
            float e = epsilon / 2f;

            if (_isValidAABB && ext._isValidAABB)
            {
                if (!RBPhysUtil.RangeOverlap(Min.x, Max.x, ext.Min.x, ext.Max.x, e)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.y, Max.y, ext.Min.y, ext.Max.y, e)) return false;
                if (!RBPhysUtil.RangeOverlap(Min.z, Max.z, ext.Min.z, ext.Max.z, e)) return false;

                return true;
            }

            return false;
        }
    }

    public struct RBColliderOBB
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 size;
        public readonly bool isValidOBB;

        Vector3 _center;

        public Vector3 Center { get { return _center; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBColliderOBB(Vector3 pos, Quaternion rot, Vector3 size)
        {
            this.pos = pos;
            this.rot = rot;
            this.size = size;
            isValidOBB = true;
            _center = pos + rot * size / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetAxisSize(Vector3 axisN)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, size.z), axisN));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(size.x, 0, 0), axisN));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, size.y, 0), axisN));
            return fwd + right + up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisForwardN()
        {
            return rot * Vector3.forward;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisRightN()
        {
            return rot * Vector3.right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetAxisUpN()
        {
            return rot * Vector3.up;
        }
    }

    public struct RBColliderSphere
    {
        public Vector3 pos;
        public float radius;
        public readonly bool isValidSphere;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBColliderSphere(Vector3 pos, float radius)
        {
            this.pos = pos;
            this.radius = radius;
            isValidSphere = true;
        }
    }

    public struct RBColliderCapsule
    {
        public Vector3 pos;
        public Quaternion rot;
        public float radius;
        public float height;
        public readonly bool isValidCapsule;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBColliderCapsule(Vector3 pos, Quaternion rot, float radius, float height)
        {
            this.pos = pos;
            this.rot = rot;
            this.radius = radius;
            this.height = height;
            isValidCapsule = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetHeightAxisN()
        {
            return rot * Vector3.up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetAxisSize(Vector3 axisN)
        {
            return Mathf.Abs(Vector3.Dot(rot * new Vector3(0, height, 0), axisN)) + radius * 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetCylinderAxisN(Vector3 axisN)
        {
            float fwd = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, 0, radius * 2), axisN));
            float right = Mathf.Abs(Vector3.Dot(rot * new Vector3(radius * 2, 0, 0), axisN));
            float up = Mathf.Abs(Vector3.Dot(rot * new Vector3(0, height, 0), axisN));
            return fwd + right + up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (Vector3 begin, Vector3 end) GetEdge()
        {
            return (pos + rot * new Vector3(0, height / 2f, 0), pos - rot * new Vector3(0, height / 2f, 0));
        }
    }

    public enum RBGeometryType
    {
        OBB,
        Sphere,
        Capsule
    }

    public class RBTrajectory
    {
        public RBColliderAABB trajectoryAABB;
        public readonly Guid trajectoryGuid;

        public bool IsValidTrajectory { get { return _isValidTrajectory; } }
        public RBRigidbody Rigidbody { get { return _rigidbody; } }
        public bool IsStatic { get { return _isStatic; } }
        public RBCollider Collider { get { return _collider; } }

        public bool IsStaticOrSleeping
        {
            get
            {
                bool sleep = !activeStatic;
                if (Rigidbody != null) sleep = Rigidbody.IsStaticOrSleeping;

                return (sleep && !limitedSleeping) || forceSleeping || (IsStatic && !activeStatic) || IsIgnoredTrajectory;
            }
        }

        public bool IsLimitedSleeping { get { return !forceSleeping && limitedSleeping; } }
        public bool IsLimitedSleepingOrStatic { get { return (!forceSleeping && limitedSleeping) || (IsStatic && !activeStatic) || IsIgnoredTrajectory; } }
        public bool IsIgnoredTrajectory { get { return _ignoreTrajectory; } }

        public int Layer { get { return _layer; } }

        public bool forceSleeping = false;
        public bool limitedSleeping = false;
        public bool iregularTraj = false;

        public bool activeStatic = false;
        Vector3 _staticVelocity;
        Vector3 _staticAngularVelocity;

        public Vector3 StaticVelocity { get { return activeStatic ? _staticVelocity : Vector3.zero; } set { _staticVelocity = value; } }
        public Vector3 StaticAngularVelocity { get { return activeStatic ? _staticAngularVelocity : Vector3.zero; } set { _staticAngularVelocity = value; } }

        bool _ignoreTrajectory = false;
        bool _setIgnoreTrajectory = false;

        bool _isValidTrajectory;
        RBRigidbody _rigidbody;
        bool _isStatic;
        RBCollider _collider;
        List<RBCollider> _colliders;
        int _layer;

        public Guid RetrogradeKeyGuid { get { return _retrogradeKeyGuid; } }
        public int RetrogradeFrame { get { return _retrogradeFrame; } }

        public bool IsFullRetrogradeValid { get { return _retrogradeValid && _fullRetrogradeValid; } }
        public bool IsLimitedRetrogradeVaild { get { return _retrogradeValid; } }

        Guid _retrogradeKeyGuid = Guid.Empty;
        int _retrogradeFrame = 0;
        bool _retrogradeValid;
        bool _fullRetrogradeValid;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLimitedRetrograde()
        {
            Debug.Assert(_retrogradeKeyGuid == Guid.Empty);
            Debug.Assert(!_fullRetrogradeValid);

            _retrogradeKeyGuid = Guid.Empty;
            _retrogradeFrame = 0;
            _retrogradeValid = true;
            _fullRetrogradeValid = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetgRetrogradeFrame(int frame)
        {
            _retrogradeFrame = frame;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFullRetrograde(Guid guid, int frame)
        {
            _retrogradeKeyGuid = guid;
            _retrogradeFrame = frame;
            _retrogradeValid = true;
            _fullRetrogradeValid = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearRetrograde()
        {
            _retrogradeKeyGuid = Guid.Empty;
            _retrogradeFrame = 0;
            _retrogradeValid = false;
            _fullRetrogradeValid = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateRetrogradeKeyGuid(Guid guid)
        {
            return _retrogradeKeyGuid == guid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIgnoreTrajectory(bool ignore)
        {
            _setIgnoreTrajectory = ignore;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SyncTrajectory()
        {
            _ignoreTrajectory = _setIgnoreTrajectory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBTrajectory()
        {
            _isValidTrajectory = false;
            trajectoryGuid = Guid.NewGuid();

            _colliders = new List<RBCollider>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBTrajectory(RBRigidbody rigidbody, int layer)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.VEnabled)
                {
                    aabb.Encapsulate(c.Trajectory.trajectoryAABB);
                }
            }

            trajectoryAABB = aabb;
            _rigidbody = rigidbody;
            _collider = null;
            _isStatic = false;
            _isValidTrajectory = true;

            _colliders = new List<RBCollider>();
            foreach (var c in rigidbody.GetColliders())
            {
                _colliders.Add(c);
            }

            _layer = layer;

            trajectoryGuid = Guid.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBTrajectory(RBCollider collider, int layer)
        {
            trajectoryAABB = collider.CalcAABB(collider.VTransform.WsPosition, collider.VTransform.WsRotation);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;

            _colliders = new List<RBCollider> { collider };

            _layer = layer;

            trajectoryGuid = Guid.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(RBRigidbody rigidbody, int layer)
        {
            RBColliderAABB aabb = new RBColliderAABB();

            foreach (RBCollider c in rigidbody.GetColliders())
            {
                if (c.VEnabled)
                {
                    aabb.Encapsulate(c.Trajectory.trajectoryAABB);
                }
            }

            trajectoryAABB = aabb;
            _rigidbody = rigidbody;
            _collider = null;
            _isStatic = false;
            _isValidTrajectory = true;

            _colliders = new List<RBCollider>();
            foreach (var c in rigidbody.GetColliders())
            {
                _colliders.Add(c);
            }

            _layer = layer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(RBCollider collider, Vector3 pos, Quaternion rot, int layer)
        {
            trajectoryAABB = collider.CalcAABB(pos, rot);
            _rigidbody = null;
            _collider = collider;
            _isStatic = true;
            _isValidTrajectory = true;
            _layer = layer;

            _colliders.Clear();
            _colliders.Add(collider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TryPhysAwake()
        {
            if (_rigidbody != null)
            {
                _rigidbody.PhysAwake();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RBCollider> GetColliders()
        {
            foreach (var c in _colliders)
            {
                yield return c;
            }
        }
    }

    public enum TimeScaleMode
    {
        Prograde = 0,
        Retrograde = 1
    }

    public static class TimeScaleModeExtentions
    {
        public static bool IsProg(this TimeScaleMode tsm)
        {
            return tsm == TimeScaleMode.Prograde;
        }

        public static bool IsRetg(this TimeScaleMode tsm)
        {
            return tsm == TimeScaleMode.Retrograde;
        }
    }
}