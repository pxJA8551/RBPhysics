#define COLLISION_SOLVER_HW_ACCELERATION

#if COLLISION_SOLVER_HW_ACCELERATION

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Profiling;

namespace RBPhys.HWAcceleration
{
    public class HWA_SolveCollision : IDisposable
    {
        const int SOLVER_ITERATION = 15;

        const string FILE_NAME_OF_CS = "SolveCollision_HLSL";

        static ComputeShader _computeShader;
        static int _kernelIndex_hwa_solveCollision;
        static int _nameId_threads_w;
        static int _nameId_threadGroups_w;
        static int _nameId_cols;
        static int _nameId_jacobians;
        static int _nameId_ret_vels;
        static int _nameId_cols_count;

        static int _kernelIndex_hwa_updateBufferVelocity;
        static int _nameId_vels_count;
        static int _nameId_cols_group_count;
        static int _nameId_vels_offset;
        static int _nameId_cols_group_offset;

        RBHWABuffer<RBCollisionHWA_Layout> _cols;
        RBHWABuffer<RBCollisionHWAJacobian_Layout> _jacobians;
        int _bufferColsCount;

        List<RBRigidbody> _vels_rb_list = new List<RBRigidbody>();
        RBHWABuffer<Vector3> _ret_vels;
        Vector3[] _ret_vels_array = new Vector3[0];

        RBCollisionHWA_Layout[] _cols_array;
        RBCollisionHWAJacobian_Layout[] _jacobians_array;
        int _arrayColsCount;

        int _minBufferColsBuffer;
        int _minRigidbodyBuffer;

        public HWA_SolveCollision(int minBufferColsCount = 32, int minRigidbodyBufferCount = 64)
        {
            bool succeeded = LoadCS();

            if (!succeeded)
            {
                Debug.LogWarning("Loading HWA Resources failed.");
            }

            _minBufferColsBuffer = minBufferColsCount;
            _minRigidbodyBuffer = minRigidbodyBufferCount;

            AllocateBuffer(_minBufferColsBuffer);
        }

        bool LoadCS()
        {
            _computeShader = Resources.Load(FILE_NAME_OF_CS) as ComputeShader;
            _kernelIndex_hwa_solveCollision = _computeShader.FindKernel("HWA_SolveCollision");
            _nameId_threads_w = Shader.PropertyToID("sc_threads_w");
            _nameId_cols = Shader.PropertyToID("sc_cols");
            _nameId_jacobians = Shader.PropertyToID("sc_jacobians");
            _nameId_ret_vels = Shader.PropertyToID("sc_vels");
            _nameId_cols_count = Shader.PropertyToID("sc_cols_count");

            _kernelIndex_hwa_updateBufferVelocity = _computeShader.FindKernel("HWA_UpdateBufferVelocity");
            _nameId_threadGroups_w = Shader.PropertyToID("sc_threadGroups_w");
            _nameId_vels_count = Shader.PropertyToID("sc_vels_count");
            _nameId_cols_group_count = Shader.PropertyToID("sc_cols_group_count");
            _nameId_vels_offset = Shader.PropertyToID("sc_vels_offset");
            _nameId_cols_group_offset = Shader.PropertyToID("sc_cols_group_offset");

            return _computeShader != null;
        }

        public void SetMinColsCount(int minColsCount)
        {
            _minBufferColsBuffer = minColsCount;
        }
        
        public void SetMinRigidbodyCount(int minRigidbodyCount)
        {
            _minRigidbodyBuffer = minRigidbodyCount;
        }

        void TryAllocateBuffer(int colsCount)
        {
            if (_bufferColsCount != colsCount)
            {
                TryDisposeBuffers();
                AllocateBuffer(colsCount);
            }
        }

        void AllocateBuffer(int colsCount)
        {
            _cols = new RBHWABuffer<RBCollisionHWA_Layout>(colsCount);
            _jacobians = new RBHWABuffer<RBCollisionHWAJacobian_Layout>(colsCount * 3);

            _bufferColsCount = colsCount;
        }

        void TryDisposeBuffers()
        {
            _cols?.Dispose();
            _jacobians?.Dispose();
        }

        void ResizeBuffers(int colsCount)
        {
            if (colsCount > _bufferColsCount)
            {
                TryAllocateBuffer(colsCount);
            }
            else
            {
                TryAllocateBuffer(Mathf.Max(colsCount, _minBufferColsBuffer));
            }
        }

        void TryResizeArrays(int colsCount)
        {
            if (_arrayColsCount != colsCount)
            {
                ResizeArrays(colsCount);
            }
        }

        void ResizeArrays(int colsCount)
        {
            _cols_array = new RBCollisionHWA_Layout[colsCount];
            _jacobians_array = new RBCollisionHWAJacobian_Layout[colsCount * 3];

            _arrayColsCount = colsCount;
        }

        public void HWA_ComputeSolveCollision(List<RBCollision> cols)
        {
            int colsCount = cols.Count;

            if (colsCount > 0)
            {
                ResizeBuffers(colsCount);
                TryResizeArrays(_bufferColsCount);

                int pairCount = colsCount;
                int threadGroupsX = Mathf.CeilToInt(pairCount / 1024f);
                int threadGroupsY = Mathf.Max(Mathf.FloorToInt(pairCount / 1024f), 1);
                int threads_w = threadGroupsX * 32;

                SetBufferDatas(cols);
                SetProperties(threads_w);

                Profiler.BeginSample(name: "SolveCollisionsHWA");

                for (int i = 0; i < SOLVER_ITERATION; i++)
                {
                    SolveCollision(threadGroupsX, threadGroupsY);
                    UpdateBufferVelocity();
                }

                GetBufferDatas();
                Profiler.EndSample();
                SetVelocity();
            }
        }

        void SetBufferDatas(List<RBCollision> cols)
        {
            TryResizeVels(cols.Count * 2);
            _vels_rb_list.Clear();

            for (int i = 0; i < _ret_vels_array.Length; i++)
            {
                _ret_vels_array[i] = Vector3.zero;
            }

            for (int i = 0; i < _arrayColsCount; i++)
            {
                if (i < cols.Count)
                {
                    cols[i].UpdateHWA();

                    _cols_array[i] = cols[i].HWAData.col;
                    _jacobians_array[i * 3] = cols[i].HWAData.jN;
                    _jacobians_array[i * 3 + 1] = cols[i].HWAData.jT;
                    _jacobians_array[i * 3 + 2] = cols[i].HWAData.jB;

                    if (cols[i].rigidbody_a != null)
                    {
                        int velId_a = _vels_rb_list.IndexOf(cols[i].rigidbody_a);

                        if (velId_a == -1)
                        {
                            velId_a = _vels_rb_list.Count;

                            _vels_rb_list.Add(cols[i].rigidbody_a);
                            _ret_vels_array[velId_a * 2] = cols[i].rigidbody_a.ExpVelocity;
                            _ret_vels_array[velId_a * 2 + 1] = cols[i].rigidbody_a.ExpAngularVelocity;
                        }

                        _cols_array[i].velId_a = velId_a;
                    }
                    else
                    {
                        _cols_array[i].velId_a = -1;
                    }

                    if (cols[i].rigidbody_b != null)
                    {
                        int velId_b = _vels_rb_list.IndexOf(cols[i].rigidbody_b);

                        if (velId_b == -1)
                        {
                            velId_b = _vels_rb_list.Count;

                            _vels_rb_list.Add(cols[i].rigidbody_b);
                            _ret_vels_array[velId_b * 2] = cols[i].rigidbody_b.ExpVelocity;
                            _ret_vels_array[velId_b * 2 + 1] = cols[i].rigidbody_b.ExpAngularVelocity;
                        }

                        _cols_array[i].velId_b = velId_b;
                    }
                    else
                    {
                        _cols_array[i].velId_b = -1;
                    }
                }
                else
                {
                    _cols_array[i] = default;
                    _jacobians_array[i * 3] = default;
                    _jacobians_array[i * 3 + 1] = default;
                    _jacobians_array[i * 3 + 2] = default;
                }
            }

            TryResizeVels(_vels_rb_list.Count);

            _cols.SetData(_cols_array);
            _jacobians.SetData(_jacobians_array);
            _ret_vels.SetData(_ret_vels_array);
        }

        void TryResizeVels(int count)
        {
            if (count > _minRigidbodyBuffer * 2)
            {
                Array.Resize(ref _ret_vels_array, count * 2);

                _ret_vels?.Dispose();
                _ret_vels = new RBHWABuffer<Vector3>(count * 2);
            }
            else if (_ret_vels_array.Length != _minRigidbodyBuffer * 2 || _ret_vels == null)
            {
                Array.Resize(ref _ret_vels_array, _minRigidbodyBuffer * 2);

                _ret_vels?.Dispose();
                _ret_vels = new RBHWABuffer<Vector3>(_minRigidbodyBuffer * 2);
            }
        }

        void GetBufferDatas()
        {
            _ret_vels.GetData(_ret_vels_array);
        }

        void SetVelocity()
        {
            for (int i = 0; i < _vels_rb_list.Count; i++)
            {
                var rb = _vels_rb_list[i];
                rb.ExpVelocity = _ret_vels_array[i * 2];
                rb.ExpAngularVelocity = _ret_vels_array[i * 2 + 1];
            }
        }

        void SetProperties(int threadGroupsX)
        {
            ComputeShader c = _computeShader;

            int kernelIndex = _kernelIndex_hwa_solveCollision;
            c.SetInt(_nameId_threads_w, threadGroupsX * 32);
            c.SetInt(_nameId_cols_count, _bufferColsCount);

            c.SetBuffer(kernelIndex, _nameId_cols, _cols.GetGraphicsBuffer());
            c.SetBuffer(kernelIndex, _nameId_jacobians, _jacobians.GetGraphicsBuffer());
            c.SetBuffer(kernelIndex, _nameId_ret_vels, _ret_vels.GetGraphicsBuffer());

            c.SetBuffer(_kernelIndex_hwa_updateBufferVelocity, _nameId_cols, _cols.GetGraphicsBuffer());
            c.SetBuffer(_kernelIndex_hwa_updateBufferVelocity, _nameId_ret_vels, _ret_vels.GetGraphicsBuffer());

            GL.Flush();
        }

        void SolveCollision(int threadX, int threadY)
        {
            ComputeShader c = _computeShader;
            c.Dispatch(_kernelIndex_hwa_solveCollision, threadX, threadY, 1);
            GL.Flush();
        }

        void UpdateBufferVelocity()
        {
            ComputeShader c = _computeShader;
            
            for (int i = 0; i < Mathf.CeilToInt(_cols.Count / 1024f); i++) 
            {
                for (int j = 0; j < Mathf.CeilToInt(_ret_vels.Count / 2048f); j++) 
                {
                    float pairSqrt = Mathf.Sqrt(_ret_vels.Count / 2f);

                    int threadGroupsX = Mathf.CeilToInt(pairSqrt);
                    int threadGroupsY = Mathf.CeilToInt(pairSqrt);

                    c.SetInt(_nameId_threadGroups_w, threadGroupsX);

                    c.SetInt(_nameId_cols_group_offset, i * 1024);
                    c.SetInt(_nameId_cols_group_count, Mathf.Min(_cols.Count - 1024 * i, 1024));
                    c.SetInt(_nameId_vels_offset, j * 1024);
                    c.SetInt(_nameId_vels_count, Mathf.Min(_ret_vels.Count - 2048 * j, 2048) / 2);

                    c.Dispatch(_kernelIndex_hwa_updateBufferVelocity, threadGroupsX, threadGroupsY, 1);
                    GL.Flush();
                }
            }
        }

        public void Dispose()
        {
            TryDisposeBuffers();
        }

        const float COLLISION_ERROR_SLOP = 0.002f;

        public struct RBCollisionHWA
        {
            public RBCollisionHWA_Layout col;
            public RBCollisionHWAJacobian_Layout jN;
            public RBCollisionHWAJacobian_Layout jT;
            public RBCollisionHWAJacobian_Layout jB;

            public RBCollisionHWA(RBCollision rbc)
            {
                col = new RBCollisionHWA_Layout(rbc);
                jN = new RBCollisionHWAJacobian_Layout(true);
                jT = new RBCollisionHWAJacobian_Layout(false);
                jB = new RBCollisionHWAJacobian_Layout(false);
            }

            public void Init(RBCollision rbc, float dt)
            {
                Vector3 contactNormal = rbc.ContactNormal;
                Vector3 tangent = Vector3.zero;
                Vector3 bitangent = Vector3.zero;

                Vector3.OrthoNormalize(ref contactNormal, ref tangent, ref bitangent);

                jN.Init(contactNormal, rbc, dt);
                jT.Init(tangent, rbc, dt);
                jB.Init(bitangent, rbc, dt);
            }

            public void Update(RBCollision rbc)
            {
                col.Update(rbc);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RBCollisionHWA_Layout
        {
            public Vector3 penetration;

            public int velId_a;
            public int velId_b;

            // HWA offset
            Vector3 a;
            Vector3 b;
            Vector3 c;
            Vector3 d;

            public float inverseMass_a;
            public Vector3 inverseInertiaWs_a;
            public float inverseMass_b;
            public Vector3 inverseInertiaWs_b;

            public float friction;

            public RBCollisionHWA_Layout(RBCollision rbc)
            {
                penetration = rbc.penetration;

                velId_a = -1;
                velId_b = -1;

                a = b = c = d = Vector3.zero;

                inverseMass_a = rbc.InverseMass_a;
                inverseInertiaWs_a = rbc.InverseInertiaWs_a;
                inverseMass_b = rbc.InverseMass_b;
                inverseInertiaWs_b = rbc.InverseInertiaWs_b;

                friction = rbc.collider_a.friction * rbc.collider_b.friction;
            }

            public void Update(RBCollision rbc)
            {
                penetration = rbc.penetration;

                velId_a = -1;
                velId_b = -1;

                inverseMass_a = rbc.InverseMass_a;
                inverseInertiaWs_a = rbc.InverseInertiaWs_a;
                inverseMass_b = rbc.InverseMass_b;
                inverseInertiaWs_b = rbc.InverseInertiaWs_b;

                friction = rbc.collider_a.friction * rbc.collider_b.friction;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RBCollisionHWAJacobian_Layout
        {
            public Vector3 va;
            public Vector3 wa;
            public Vector3 vb;
            public Vector3 wb;
            public int type; // 0=Normal 1=Tangent
            public float totalLambda;
            public float totalLambdaInFrame;
            public float effectiveMass;
            public float bias;

            public RBCollisionHWAJacobian_Layout(bool isNormal)
            {
                va = Vector3.zero;
                wa = Vector3.zero;
                vb = Vector3.zero;
                wb = Vector3.zero;

                type = isNormal ? 0 : 1;
                totalLambda = 0;
                totalLambdaInFrame = 0;
                effectiveMass = 0;
                bias = 0;
            }

            public void Init(Vector3 dirN, RBCollision rbc, float dt)
            {
                va = dirN;
                wa = Vector3.Cross(rbc.rA, dirN);
                vb = -dirN;
                wb = Vector3.Cross(rbc.rB, -dirN);

                bias = 0;

                if (type == 0)
                {
                    float beta = rbc.collider_a.beta * rbc.collider_b.beta;
                    float restitution = rbc.collider_a.restitution * rbc.collider_b.restitution;
                    Vector3 relVel = Vector3.zero;
                    relVel += rbc.ExpVelocity_a;
                    relVel += Vector3.Cross(rbc.ExpAngularVelocity_a, rbc.rA);
                    relVel -= rbc.ExpVelocity_b;
                    relVel -= Vector3.Cross(rbc.ExpAngularVelocity_b, rbc.rB);

                    float closingVelocity = Vector3.Dot(relVel, dirN);
                    bias = -(beta / dt) * Mathf.Max(0, rbc.penetration.magnitude - COLLISION_ERROR_SLOP) + restitution * closingVelocity;
                }

                float k = 0;
                k += rbc.InverseMass_a;
                k += Vector3.Dot(wa, Vector3.Scale(rbc.InverseInertiaWs_a, wa)); 
                k += rbc.InverseMass_b;
                k += Vector3.Dot(wb, Vector3.Scale(rbc.InverseInertiaWs_b, wb));

                effectiveMass = 1 / k;
                totalLambdaInFrame = totalLambda;
                totalLambda = 0;
            }
        }
    }
}

#endif