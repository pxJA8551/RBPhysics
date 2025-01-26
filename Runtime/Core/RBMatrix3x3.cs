using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBPhys
{
    public struct RBMatrix3x3
    {
        //https://github.com/sharpdx/SharpDX/blob/master/Source/SharpDX.Mathematics/Matrix3x3.cs

        //I(Row)(Column)
        public float m00;
        public float m01;
        public float m02;
        public float m10;
        public float m11;
        public float m12;
        public float m20;
        public float m21;
        public float m22;

        public Vector3 C0 { get { return new Vector3(m00, m10, m20); } set { (m00, m10, m20) = (value.x, value.y, value.z); } }
        public Vector3 C1 { get { return new Vector3(m01, m11, m21); } set { (m01, m11, m21) = (value.x, value.y, value.z); } }
        public Vector3 C2 { get { return new Vector3(m02, m12, m22); } set { (m02, m12, m22) = (value.x, value.y, value.z); } }

        public Vector3 R0 { get { return new Vector3(m00, m01, m02); } set { (m00, m01, m02) = (value.x, value.y, value.z); } }
        public Vector3 R1 { get { return new Vector3(m10, m11, m12); } set { (m10, m11, m12) = (value.x, value.y, value.z); } }
        public Vector3 R2 { get { return new Vector3(m20, m21, m22); } set { (m20, m21, m22) = (value.x, value.y, value.z); } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(float[] values)
        {
            m00 = values[0];
            m01 = values[1];
            m02 = values[2];
            m10 = values[3];
            m11 = values[4];
            m12 = values[5];
            m20 = values[6];
            m21 = values[7];
            m22 = values[8];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3(Vector3 c0, Vector3 c1, Vector3 c2)
        {
            m00 = c0.x;
            m10 = c0.y;
            m20 = c0.z;
            m01 = c1.x;
            m11 = c1.y;
            m21 = c1.z;
            m02 = c2.x;
            m12 = c2.y;
            m22 = c2.z;
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
            return new RBMatrix3x3(a.C0 - b.C0, a.C1 - b.C1, a.C2 - b.C2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static RBMatrix3x3 Multiply(RBMatrix3x3 mat, float v)
        {
            var ret = new RBMatrix3x3();
            ret.m00 = mat.m00 * v;
            ret.m01 = mat.m01 * v;
            ret.m02 = mat.m02 * v;
            ret.m10 = mat.m10 * v;
            ret.m11 = mat.m11 * v;
            ret.m12 = mat.m12 * v;
            ret.m20 = mat.m20 * v;
            ret.m21 = mat.m21 * v;
            ret.m22 = mat.m22 * v;

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(RBMatrix3x3 left, float right)
        {
            return Multiply(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(float right, RBMatrix3x3 left)
        {
            return Multiply(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RBMatrix3x3 operator *(RBMatrix3x3 left, RBMatrix3x3 right)
        {
            RBMatrix3x3 m = new RBMatrix3x3();
            m.m00 = (left.m00 * right.m00) + (left.m01 * right.m10) + (left.m02 * right.m20);
            m.m01 = (left.m00 * right.m01) + (left.m01 * right.m11) + (left.m02 * right.m21);
            m.m02 = (left.m00 * right.m02) + (left.m01 * right.m12) + (left.m02 * right.m22);
            m.m10 = (left.m10 * right.m00) + (left.m11 * right.m10) + (left.m12 * right.m20);
            m.m11 = (left.m10 * right.m01) + (left.m11 * right.m11) + (left.m12 * right.m21);
            m.m12 = (left.m10 * right.m02) + (left.m11 * right.m12) + (left.m12 * right.m22);
            m.m20 = (left.m20 * right.m00) + (left.m21 * right.m10) + (left.m22 * right.m20);
            m.m21 = (left.m20 * right.m01) + (left.m21 * right.m11) + (left.m22 * right.m21);
            m.m22 = (left.m20 * right.m02) + (left.m21 * right.m12) + (left.m22 * right.m22);

            return m;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3 Inverted()
        {
            float det = 0;
            det += m00 * m11 * m22;
            det += m01 * m12 * m20;
            det += m10 * m21 * m02;
            det -= m02 * m11 * m20;
            det -= m01 * m10 * m22;
            det -= m12 * m21 * m00;

            float detInv = 1 / det;

            float[] values = new float[9];
            values[0] = (m11 * m22 - m21 * m12) * detInv;
            values[1] = (m12 * m20 - m10 * m22) * detInv;
            values[2] = (m10 * m21 - m20 * m11) * detInv;
            values[3] = (m02 * m21 - m01 * m22) * detInv;
            values[4] = (m00 * m22 - m02 * m20) * detInv;
            values[5] = (m20 * m01 - m00 * m21) * detInv;
            values[6] = (m01 * m12 - m02 * m11) * detInv;
            values[7] = (m10 * m02 - m00 * m12) * detInv;
            values[8] = (m00 * m11 - m10 * m01) * detInv;

            return new RBMatrix3x3(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RBMatrix3x3 Transposed()
        {
            return new RBMatrix3x3(m00, m10, m20, m01, m11, m21, m02, m12, m22);
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
            Quaternion q = Quaternion.identity;

            RBMatrix3x3 d = new RBMatrix3x3();

            //?????????????????????????

            for (int i = 0; i < DIAGONALIZE_MAX_ITERATION; i++)
            {
                RBMatrix3x3 axis = new RBMatrix3x3(q);
                d = axis.Transposed() * matrix * axis;

                float d0 = Mathf.Abs(d[1][2]);
                float d1 = Mathf.Abs(d[0][2]);
                float d2 = Mathf.Abs(d[0][1]);
                int a = (d0 > d1 && d0 > d2) ? 0 : (d1 > d2 ? 1 : 2);

                int a1 = (a + 1) % 3;
                int a2 = (a1 + 1) % 3;

                if (d[a1][a2] == 0.0f || Mathf.Abs(d[a1][a1] - d[a2][a2]) > 2e6f * Mathf.Abs(2 * d[a1][a2]))
                {
                    break;
                }

                float w = (d[a1][a1] - d[a2][a2]) / (2 * d[a1][a2]);
                float absw = Mathf.Abs(w);

                Quaternion r;
                if (absw > 1000)
                {
                    r = IndexedRotation(a, 1f / (4 * w), 1f);
                }
                else
                {
                    float t = 1f / (absw + Mathf.Sqrt(w * w + 1));
                    float h = 1f / Mathf.Sqrt(t * t + 1);

                    r = IndexedRotation(a, Mathf.Sqrt((1 - h) / 2f) * RBPhysUtil.F32Sign11(w), Mathf.Sqrt((1 + h) / 2f));
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
