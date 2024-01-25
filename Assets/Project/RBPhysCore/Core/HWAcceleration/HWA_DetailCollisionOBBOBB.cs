using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace RBPhys.HWAcceleration
{
    public static partial class DetailCollision
    {
        public class HWA_DetailCollisionOBBOBB : IDisposable
        {
            const string FILE_NAME_OF_CS = "DetailCollisionOBBOBB_HLSL";

            static ComputeShader _computeShader;
            static int _kernelIndex_hwa_detectCollision;
            static int _nameId_obb_centers;
            static int _nameId_obb_rotations;
            static int _nameId_obb_sizes;
            static int _nameId_pair_cgs;
            static int _nameId_threads_w;
            static int _nameId_ret_obb_penetrations;
            static int _nameId_ret_obb_contacts;

            RBHWABuffer<Vector3> _obb_centers;
            RBHWABuffer<RBMatrix3x3> _obb_rotations;
            RBHWABuffer<Vector3> _obb_sizes;
            RBHWABuffer<Vector3> _pair_cgs;
            RBHWABuffer<Vector3> _ret_obb_penetrations;
            RBHWABuffer<Vector3> _ret_obb_contacts;
            int _bufferObbPairCount;

            Vector3[] _obb_centers_array;
            RBMatrix3x3[] _obb_rotations_array;
            Vector3[] _obb_sizes_array;
            Vector3[] _pair_cgs_array;
            Vector3[] _ret_obb_penetrations_array;
            Vector3[] _ret_obb_contacts_array;
            int _arrayObbPairCount;

            int _minBufferObbPairCount;

            public HWA_DetailCollisionOBBOBB(int minBufferObbPairCount = 15)
            {
                bool succeeded = LoadCS();
                if (!succeeded)
                {
                    Debug.LogWarning("Loading HWA resources failed.");
                }

                AllocateBuffers(minBufferObbPairCount);
                ResizeArrays(minBufferObbPairCount);
                _minBufferObbPairCount = minBufferObbPairCount;
            }

            bool LoadCS()
            {
                _computeShader = Resources.Load(FILE_NAME_OF_CS) as ComputeShader;

                _kernelIndex_hwa_detectCollision = _computeShader.FindKernel("HWA_DetectCollision");
                _nameId_obb_centers = Shader.PropertyToID("dc_obb_centers");
                _nameId_obb_rotations = Shader.PropertyToID("dc_obb_rotations");
                _nameId_obb_sizes = Shader.PropertyToID("dc_obb_sizes");
                _nameId_pair_cgs = Shader.PropertyToID("dc_pair_cgs");
                _nameId_threads_w = Shader.PropertyToID("dc_threads_w");
                _nameId_ret_obb_penetrations = Shader.PropertyToID("dc_Penetrations");
                _nameId_ret_obb_contacts = Shader.PropertyToID("dc_Contacts");

                return _computeShader != null;
            }

            void TryAllocateBuffers(int obbPairCount)
            {
                if (_bufferObbPairCount != obbPairCount)
                {
                    TryDisposeBuffers();
                    AllocateBuffers(obbPairCount);
                }
            }

            public void SetMinOBBPairCount(int targetBufferObbPairCount)
            {
                _minBufferObbPairCount = targetBufferObbPairCount;
            }

            void AllocateBuffers(int obbPairCount)
            {
                _obb_centers = new RBHWABuffer<Vector3>(obbPairCount * 2);
                _obb_rotations = new RBHWABuffer<RBMatrix3x3>(obbPairCount * 2);
                _obb_sizes = new RBHWABuffer<Vector3>(obbPairCount * 2);
                _pair_cgs = new RBHWABuffer<Vector3>(obbPairCount);
                _ret_obb_penetrations = new RBHWABuffer<Vector3>(obbPairCount);
                _ret_obb_contacts = new RBHWABuffer<Vector3>(obbPairCount * 2);

                _bufferObbPairCount = obbPairCount;
            }

            void TryDisposeBuffers()
            {
                _obb_centers?.Dispose();
                _obb_rotations?.Dispose();
                _obb_sizes?.Dispose();
                _pair_cgs?.Dispose();
                _ret_obb_penetrations?.Dispose();
                _ret_obb_contacts?.Dispose();
            }

            void ResizeBuffers(int obbPairCount)
            {
                if (obbPairCount > _bufferObbPairCount)
                {
                    TryAllocateBuffers(obbPairCount);
                }
                else
                {
                    TryAllocateBuffers(Mathf.Max(obbPairCount, _minBufferObbPairCount));
                }
            }

            void TryResizeArrays(int obbPairCount)
            {
                if (_arrayObbPairCount != obbPairCount)
                {
                    ResizeArrays(obbPairCount);
                }
            }

            void ResizeArrays(int obbPairCount)
            {
                _obb_centers_array = new Vector3[obbPairCount * 2];
                _obb_rotations_array = new RBMatrix3x3[obbPairCount * 2];
                _obb_sizes_array = new Vector3[obbPairCount * 2];
                _ret_obb_penetrations_array = new Vector3[obbPairCount];
                _ret_obb_contacts_array = new Vector3[obbPairCount * 2];
                _arrayObbPairCount = obbPairCount;
            }

            public void HWA_ComputeDetailCollision(List<(RBCollider obb_a, RBCollider obb_b)> cols, ref List<(Vector3 penetration, Vector3 nearestA, Vector3 nearestB)> pList)
            {
                int obbCount = cols.Count;

                if (obbCount > 0)
                {
                    ResizeBuffers(obbCount);
                    TryResizeArrays(_bufferObbPairCount);

                    SetBufferDatas(cols);
                    DetailCollision();
                    GetBufferDatas();

                    pList.Clear();

                    for (int i = 0; i < obbCount; i++)
                    {
                        pList.Add((_ret_obb_penetrations_array[i], _ret_obb_contacts_array[i * 2], _ret_obb_contacts_array[i * 2 + 1]));
                    }
                }
            }

            void SetBufferDatas(List<(RBCollider obb_a, RBCollider obb_b)> cols)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    var pair = cols[i];

                    int id_a = i * 2;
                    int id_b = i * 2 + 1;

                    var obb_a = pair.obb_a.CalcOBB();
                    var obb_b = pair.obb_b.CalcOBB();

                    _obb_centers_array[id_a] = obb_a.Center;
                    _obb_centers_array[id_b] = obb_b.Center;
                    _obb_rotations_array[id_a] = obb_a.RotMatrix.Transposed();
                    _obb_rotations_array[id_b] = obb_b.RotMatrix.Transposed();
                    _obb_sizes_array[id_a] = obb_a.size;
                    _obb_sizes_array[id_b] = obb_b.size;
                }

                _obb_sizes.SetData(_obb_sizes_array);
                _obb_rotations.SetData(_obb_rotations_array);
                _obb_centers.SetData(_obb_centers_array);
            }

            void GetBufferDatas()
            {
                _ret_obb_penetrations.GetData(_ret_obb_penetrations_array);
                _ret_obb_contacts.GetData(_ret_obb_contacts_array);
            }

            void DetailCollision()
            {
                ComputeShader c = _computeShader;

                int pairCount = _bufferObbPairCount;
                int threadGroupsX = Mathf.CeilToInt(pairCount / 1024f);
                int threadGroupsY = Mathf.Max(Mathf.FloorToInt(pairCount / 1024f), 1);
                int threads_w = threadGroupsX * 32;

                int kernelIndex = _kernelIndex_hwa_detectCollision;
                c.SetInt(_nameId_threads_w, threads_w);
                c.SetBuffer(kernelIndex, _nameId_obb_centers, _obb_centers.GetGraphicsBuffer());
                c.SetBuffer(kernelIndex, _nameId_obb_rotations, _obb_rotations.GetGraphicsBuffer());
                c.SetBuffer(kernelIndex, _nameId_obb_sizes, _obb_sizes.GetGraphicsBuffer());
                c.SetBuffer(kernelIndex, _nameId_pair_cgs, _pair_cgs.GetGraphicsBuffer());
                c.SetBuffer(kernelIndex, _nameId_ret_obb_penetrations, _ret_obb_penetrations.GetGraphicsBuffer());
                c.SetBuffer(kernelIndex, _nameId_ret_obb_contacts, _ret_obb_contacts.GetGraphicsBuffer());

                c.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);
                GL.Flush();
            }

            public void Dispose()
            {
                TryDisposeBuffers();
            }
        }
    }
}