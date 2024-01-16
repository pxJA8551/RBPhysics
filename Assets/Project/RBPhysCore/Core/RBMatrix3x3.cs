using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Animations;

namespace RBPhys
{
    public struct RBMatrix3x3
    {
        //I(Row)(Column)
        float I00;
        float I01;
        float I02;
        float I10;
        float I11;
        float I12;
        float I20;
        float I21;
        float I22;

        public Vector3 C0 { get { return new Vector3(I00, I10, I20); } set { (I00, I10, I20) = (value.x, value.y, value.z); } }
        public Vector3 C1 { get { return new Vector3(I01, I11, I21); } set { (I01, I11, I21) = (value.x, value.y, value.z); } }
        public Vector3 C2 { get { return new Vector3(I02, I12, I22); } set { (I02, I12, I22) = (value.x, value.y, value.z); } }

        public Vector3 R0 { get { return new Vector3(I00, I01, I02); } set { (I00, I01, I02) = (value.x, value.y, value.z); } }
        public Vector3 R1 { get { return new Vector3(I10, I11, I12); } set { (I10, I11, I12) = (value.x, value.y, value.z); } }
        public Vector3 R2 { get { return new Vector3(I20, I21, I22); } set { (I20, I21, I22) = (value.x, value.y, value.z); } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(float[] values)
        {
            I00 = values[0];
            I01 = values[1];
            I02 = values[2];
            I10 = values[3];
            I11 = values[4];
            I12 = values[5];
            I20 = values[6];
            I21 = values[7];
            I22 = values[8];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(float i00, float i01, float i02, float i10, float i11, float i12, float i20, float i21, float i22)
        {
            I00 = i00;
            I01 = i01;
            I02 = i02;
            I10 = i10;
            I11 = i11;
            I12 = i12;
            I20 = i20;
            I21 = i21;
            I22 = i22;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(Vector3 c0, Vector3 c1, Vector3 c2)
        {
            I00 = c0.x;
            I10 = c0.y;
            I20 = c0.z;
            I01 = c1.x;
            I11 = c1.y;
            I21 = c1.z;
            I02 = c2.x;
            I12 = c2.y;
            I22 = c2.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(Quaternion q)
        {
            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            float x2 = x + x;
            float y2 = y + y;
            float z2 = z + z;

            float xx = x2 * x;
            float yy = y2 * y;
            float zz = z2 * z;

            float xy = x2 * y;
            float xz = x2 * z;
            float xw = x2 * w;
            
            float yz = y2 * z;
            float yw = y2 * w;
            float zw = z2 * w;

            Vector3 c0 = new Vector3(1 - yy - zz, xy + zw, xz - yw);
            Vector3 c1 = new Vector3(xy - zw, 1 - xx - zz, yz + xw);
            Vector3 c2 = new Vector3(xz + yw, yz - xw, 1 - xx - yy);

            this = new RBMatrix3x3(c0, c1, c2);
        }

        public Vector3 this[int indexColumn]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return indexColumn switch
                {
                    0 => C0,
                    1 => C1,
                    2 => C2,
                    _ => throw new NotImplementedException()
                };
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (indexColumn)
                {
                    case 0:
                        C0 = value;
                        break;
                    case 1:
                        C1 = value;
                        break;
                    case 2:
                        C2 = value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator +(RBMatrix3x3 a, RBMatrix3x3 b)
        {
            return new RBMatrix3x3(a.C0 + b.C0, a.C1 + b.C1, a.C2 + b.C2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator -(RBMatrix3x3 a, RBMatrix3x3 b)
        {
            return new RBMatrix3x3(a.C0 + b.C0, a.C1 + b.C1, a.C2 + b.C2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(float s, RBMatrix3x3 m)
        {
            return new RBMatrix3x3(s * m.C0, s * m.C1, s * m.C2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(RBMatrix3x3 m, float s)
        {
            return s * m;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(RBMatrix3x3 m, Vector3 v)
        {
            float x = Vector3.Dot(m.R0, v);
            float y = Vector3.Dot(m.R1, v);
            float z = Vector3.Dot(m.R2, v);

            return new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 v, RBMatrix3x3 m)
        {
            float x = Vector3.Dot(v, m.C0);
            float y = Vector3.Dot(v, m.C1);
            float z = Vector3.Dot(v, m.C2);

            return new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(RBMatrix3x3 a, RBMatrix3x3 b)
        {
            Vector3 c0 = a * b.C0;
            Vector3 c1 = a * b.C1;
            Vector3 c2 = a * b.C2;

            return CreateFromCols(c0, c1, c2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Mul(Vector3 a, RBMatrix3x3 m, Vector3 b)
        {
            return Vector3.Dot(a * m, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3 Inverted()
        {
            float det = 0;
            det += I00 * I11 * I22;
            det += I01 * I12 * I20;
            det += I10 * I21 * I02;
            det -= I02 * I11 * I20;
            det -= I01 * I10 * I22;
            det -= I12 * I21 * I00;

            float detInv = 1 / det;

            float[] values = new float[9];
            values[0] = (I11 * I22 - I21 * I12) * detInv;
            values[1] = (I12 * I20 - I10 * I22) * detInv;
            values[2] = (I10 * I21 - I20 * I11) * detInv;
            values[3] = (I02 * I21 - I01 * I22) * detInv;
            values[4] = (I00 * I22 - I02 * I20) * detInv;
            values[5] = (I20 * I01 - I00 * I21) * detInv;
            values[6] = (I01 * I12 - I02 * I11) * detInv;
            values[7] = (I10 * I02 - I00 * I12) * detInv;
            values[8] = (I00 * I11 - I10 * I01) * detInv;

            return new RBMatrix3x3(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3 Transposed()
        {
            return new RBMatrix3x3(I00, I10, I20, I01, I11, I21, I02, I12, I22);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 CreateFromRows(Vector3 r0, Vector3 r1, Vector3 r2)
        {
            RBMatrix3x3 m = new RBMatrix3x3();
            m.C0 = new Vector3(r0[0], r1[0], r2[0]);
            m.C1 = new Vector3(r0[1], r1[1], r2[1]);
            m.C2 = new Vector3(r0[2], r1[2], r2[2]);
            return m;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 CreateFromCols(Vector3 c0, Vector3 c1, Vector3 c2)
        {
            return new RBMatrix3x3(c0, c1, c2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 CreateDiagonal(Vector3 d)
        {
            var r = new RBMatrix3x3();
            r.C0 = new Vector3(d.x, 0, 0);
            r.C1 = new Vector3(0, d.y, 0);
            r.C2 = new Vector3(0, 0, d.z);
            return r;
        }

        //éQè∆: https://github.com/NVIDIAGameWorks/PhysX-3.4/blob/master/PxShared/src/foundation/src/PsMathUtils.cpp
        //    : PxVec3 physx::PxDiagonalize(const PxMat33& m, PxQuat& massFrame)

        const int DIAGONALIZE_MAX_ITERATION = 24;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Diagonalize(RBMatrix3x3 matrix, out Quaternion rotation)
        {
            rotation = Quaternion.identity;

            Quaternion q = Quaternion.identity;

            RBMatrix3x3 d = new RBMatrix3x3();

            for (int i = 0; i < DIAGONALIZE_MAX_ITERATION; i++)
            {
                RBMatrix3x3 axis = new RBMatrix3x3(q);
                d = axis.Transposed() * matrix * axis;

                float d0 = Mathf.Abs(d[1][2]);
                float d1 = Mathf.Abs(d[0][2]);
                float d2 = Mathf.Abs(d[0][1]);
                int a = d0 > d1 && d0 > d2 ? 0 : d1 > d2 ? 1 : 2;

                int a1 = (a + 1) % 3;
                int a2 = (a1 + 1) % 3;

                if (d[a1][a2] == 0.0f || Mathf.Abs(d[a1][a1] - d[a2][a2]) > 2e6f * Mathf.Abs(2 * d[a1][a2]))
                {
                    break;
                }

                float w = (d[a1][a1] - d[a2][a2]) / (2 * d[a1][a2]);
                float absw = Mathf.Abs(w);

                //?????????????????????????

                Quaternion r;
                if (absw > 1000)
                {
                    r = IndexedRotation(a, 1f / (4 * w), 1f);
                }
                else
                {
                    float t = 1 / (absw + Mathf.Sqrt(w * w + 1));
                    float h = 1 / Mathf.Sqrt(t * t + 1);

                    r = IndexedRotation(a, Mathf.Sqrt((1 - h) / 2f) * Mathf.Sign(w), Mathf.Sqrt((1 + h) / 2f));
                }

                q = (q * r).normalized;
            }

            rotation = q;
            return new Vector3(d.C0.x, d.C1.y, d.C2.z);
        }

        //éQè∆: PxQuat indexedRotation(PxU32 axis, PxReal s, PxReal c)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion IndexedRotation(int axis, float s, float c)
        {
            float[] v = new float[3];
            v[axis] = s;

            return new Quaternion(v[0], v[1], v[2], c);
        }
    }
}
