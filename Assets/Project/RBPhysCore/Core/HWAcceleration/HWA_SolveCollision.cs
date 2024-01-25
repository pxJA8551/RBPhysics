#undef COLLISION_SOLVER_HW_ACCELERATION
#define COLLISION_SOLVER_HW_ACCELERATION

#if COLLISION_SOLVER_HW_ACCELERATION

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Profiling;
using System.Threading.Tasks;

namespace RBPhys.HWAcceleration
{
    public class HWA_SolveCollision : IDisposable
    {
        const int SOLVER_ITERATION = 15;

        const string FILE_NAME_OF_CS = "SolveCollision_HLSL";
        static ComputeShader _computeShader;
        static int _kernelIndex_hwa_solveCollision;
        static int _nameId_threads_w;
        static int _nameId_cols;
        static int _nameId_jacobians;
        static int _nameId_ret_vels;

        RBHWABuffer<RBCollisionHWA_Layout> _cols;
        RBHWABuffer<RBCollisionHWAJacobian_Layout> _jacobians;
        RBHWABuffer<Vector3> _ret_vels;
        int _bufferColsCount;

        RBCollisionHWA_Layout[] _cols_array;
        RBCollisionHWAJacobian_Layout[] _jacobians_array;
        Vector3[] _ret_vels_array;
        int _arrayColsCount;

        int _minBufferColsBuffer;

        public HWA_SolveCollision(int minBufferColsCount)
        {
            bool succeeded = LoadCS();
            if (!succeeded)
            {
                Debug.LogWarning("Loading HWA Resources failed.");
            }

            _minBufferColsBuffer = minBufferColsCount;
        }

        bool LoadCS()
        {
            _computeShader = Resources.Load(FILE_NAME_OF_CS) as ComputeShader;
            _kernelIndex_hwa_solveCollision = _computeShader.FindKernel("HWA_SolveCollision");
            _nameId_threads_w = Shader.PropertyToID("sc_threads_w");
            _nameId_cols = Shader.PropertyToID("sc_cols");
            _nameId_jacobians = Shader.PropertyToID("sc_jacobians");
            _nameId_ret_vels = Shader.PropertyToID("sc_Vels");

            return _computeShader != null;
        }

        public void SetMinColsCount(int minColsCount)
        {
            _minBufferColsBuffer = minColsCount;
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
            _ret_vels = new RBHWABuffer<Vector3>(colsCount * 4);

            _bufferColsCount = colsCount;
        }

        void TryDisposeBuffers()
        {
            _cols?.Dispose();
            _jacobians?.Dispose();
            _ret_vels?.Dispose();
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
            _ret_vels_array = new Vector3[colsCount * 4];

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

                SetProperties(threads_w);

                for (int i = 0; i < SOLVER_ITERATION; i++)
                {
                    Profiler.BeginSample(name: String.Format("SolveCollisions({0}/{1})", i, SOLVER_ITERATION));

                    SetBufferDatas(cols);
                    Profiler.BeginSample(name: String.Format("SolveCollisionsHWA({0}/{1})", i, SOLVER_ITERATION));
                    SolveCollision(threadGroupsX, threadGroupsY);
                    Profiler.EndSample();
                    GetBufferDatas();
                    UpdateVelocity(cols);
                    Profiler.EndSample();
                }
            }
        }

        void SetBufferDatas(List<RBCollision> cols)
        {
            for (int i = 0; i < cols.Count; i++)
            {
                cols[i].UpdateHWA();
                _cols_array[i] = cols[i].HWAData.col;
                _jacobians_array[i * 3] = cols[i].HWAData.jN;
                _jacobians_array[i * 3 + 1] = cols[i].HWAData.jT;
                _jacobians_array[i * 3 + 2] = cols[i].HWAData.jB;
            }

            _cols.SetData(_cols_array);
            _jacobians.SetData(_jacobians_array);
        }

        void GetBufferDatas()
        {
            _ret_vels.GetData(_ret_vels_array);
        }

        void UpdateVelocity(List<RBCollision> cols)
        {
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].rigidbody_a != null)
                {
                    cols[i].rigidbody_a.ExpVelocity += _ret_vels_array[i * 4];
                    cols[i].rigidbody_a.ExpAngularVelocity += _ret_vels_array[i * 4 + 1];
                }

                if (cols[i].rigidbody_b != null)
                {
                    cols[i].rigidbody_b.ExpVelocity += _ret_vels_array[i * 4 + 2];
                    cols[i].rigidbody_b.ExpAngularVelocity += _ret_vels_array[i * 4 + 3];
                }

                cols[i].UpdateHWA();
            }
        }

        void SetProperties(int threadGroupsX)
        {
            ComputeShader c = _computeShader;

            int kernelIndex = _kernelIndex_hwa_solveCollision;
            c.SetInt(_nameId_threads_w, threadGroupsX * 32);
            c.SetBuffer(kernelIndex, _nameId_cols, _cols.GetGraphicsBuffer());
            c.SetBuffer(kernelIndex, _nameId_jacobians, _jacobians.GetGraphicsBuffer());
            c.SetBuffer(kernelIndex, _nameId_ret_vels, _ret_vels.GetGraphicsBuffer());
        }

        void SolveCollision(int threadX, int threadY)
        {
            ComputeShader c = _computeShader;
            c.Dispatch(_kernelIndex_hwa_solveCollision, threadX, threadY, 1);
            GL.Flush();
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

            public Vector3 expVel_a;
            public Vector3 expAngVel_a;
            public Vector3 expVel_b;
            public Vector3 expAngVel_b;

            public float inverseMass_a;
            public Vector3 inverseInertiaWs_a;
            public float inverseMass_b;
            public Vector3 inverseInertiaWs_b;

            public float friction;

            public RBCollisionHWA_Layout(RBCollision rbc)
            {
                penetration = rbc.penetration;

                expVel_a = rbc.ExpVelocity_a;
                expAngVel_a = rbc.ExpAngularVelocity_a;
                expVel_b = rbc.ExpVelocity_b;
                expAngVel_b = rbc.ExpAngularVelocity_b;

                inverseMass_a = rbc.InverseMass_a;
                inverseInertiaWs_a = rbc.InverseInertiaWs_a;
                inverseMass_b = rbc.InverseMass_b;
                inverseInertiaWs_b = rbc.InverseInertiaWs_b;

                friction = rbc.collider_a.friction * rbc.collider_b.friction;
            }

            public void Update(RBCollision rbc)
            {
                penetration = rbc.penetration;

                expVel_a = rbc.ExpVelocity_a;
                expAngVel_a = rbc.ExpAngularVelocity_a;

                expVel_b = rbc.ExpVelocity_b;
                expAngVel_b = rbc.ExpAngularVelocity_b;

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