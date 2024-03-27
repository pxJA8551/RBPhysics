using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    // (PhysX ver3.4) https://github.com/NVIDIAGameWorks/PhysX-3.4/blob/master/PhysX_3.4/Source/PhysXExtensions/src/ExtRigidBodyExt.cpp
    // ŽQÆ: ExtRigidBodyExt.cpp
    //       ExtInertiaTensor.h

    public class RBInertiaTensor
    {
        RBMatrix3x3 _inertiaTensor;
        Vector3 _cg;
        float _mass;

        public RBMatrix3x3 InertiaTensor { get { return _inertiaTensor; } set { _inertiaTensor = value; } }
        public Vector3 CenterOfGravity { get { return _cg; } set { _cg = value; } }
        public float Mass { get { return _mass; } set { _mass = value; } }

        public static RBInertiaTensor CreateIdentity()
        {
            RBInertiaTensor t = new RBInertiaTensor();
            return t;
        }

        public void SetInertiaOBB(RBColliderOBB obb, Vector3 pos, Quaternion rot)
        {
            Vector3 extents = obb.size / 2f;

            float cMass = 8f * RBPhysUtil.V3Volume(extents);
            float s = (1f / 3f) * cMass;

            float x = extents.x * extents.x;
            float y = extents.y * extents.y;
            float z = extents.z * extents.z;

            SetDiagonal(cMass, new Vector3(y + z, z + x, x + y) * s);

            ApplyTransform(pos, rot);
        }

        public void SetInertiaSphere(RBColliderSphere sphere, Vector3 pos, Quaternion rot)
        {
            float r = sphere.radius;
            float mr = ((4f / 3f) * Mathf.PI * r * r * r);
            float ms = mr * r * r * (2f / 5f);
            SetDiagonal(mr, new Vector3(ms, ms, ms));

            ApplyTransform(pos, rot);
        }

        public void SetInertiaCapsule(RBColliderCapsule capsule, Vector3 pos, Quaternion rot)
        {
            float r = capsule.radius;
            float h = capsule.height / 2f;
            float m = ((4f / 3f) * Mathf.PI * r * r * r) + (Mathf.PI * r * r * 2f * h);

            float t = Mathf.PI * r * r;
            float i1 = t * ((r * r * r * 8f / 15f) + (h * r * r));
            float i2 = t * ((r * r * r * 8f / 15f) + (h * r * r * 3f / 2f) + (h * h * r * 4f / 3f) + (h * h * h * 2f / 3f));

            SetDiagonal(m, new Vector3(i2, i1, i2));
            ApplyTransform(pos, rot);
        }

        public void ApplyTransform(Vector3 pos, Quaternion rot)
        {
            RBMatrix3x3 rotM = new RBMatrix3x3(rot);

            _inertiaTensor = rotM * _inertiaTensor * rotM.Transposed();
            _cg = rot * _cg;
            
            Vector3 c0 = new Vector3(0, _cg.z, -_cg.y);
            Vector3 c1 = new Vector3(-_cg.z, 0, _cg.x);
            Vector3 c2 = new Vector3(_cg.y, -_cg.x, 0);

            RBMatrix3x3 m = RBMatrix3x3.CreateFromCols(c0, c1, c2);

            if (pos != Vector3.zero)
            {

                Vector3 sum = _cg + pos;

                if (sum == Vector3.zero)
                {
                    _inertiaTensor += (m * m) * _mass;
                }
                else
                {
                    Vector3 rc0 = new Vector3(0, sum.z, -sum.y);
                    Vector3 rc1 = new Vector3(-sum.z, 0, sum.x);
                    Vector3 rc2 = new Vector3(sum.y, -sum.x, 0);

                    RBMatrix3x3 mr = RBMatrix3x3.CreateFromCols(rc0, rc1, rc2);

                    _inertiaTensor += (m * m - mr * mr) * _mass;
                }

                _cg += pos;
            }
        }

        void SetDiagonal(float mass, Vector3 dv)
        {
            _mass = mass;
            _inertiaTensor = RBMatrix3x3.CreateDiagonal(dv);
            _cg = Vector3.zero;
        }

        public void ScaleDensity(float densityScale)
        {
            _inertiaTensor *= densityScale;
            _mass *= densityScale;
        }

        public void Merge(RBInertiaTensor t)
        {
            if (t.Mass > 0)
            {
                float mass = _mass + t._mass;
                Vector3 cg = (_cg * _mass + t._cg * t._mass) / mass;

                _cg = cg;
                _mass = mass;
                _inertiaTensor += t._inertiaTensor;
            }
        }
    }
}