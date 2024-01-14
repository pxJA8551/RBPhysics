using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

namespace RBPhys
{
    // (PhysX ver3.4) https://github.com/NVIDIAGameWorks/PhysX-3.4/blob/master/PhysX_3.4/Source/PhysXExtensions/src/ExtRigidBodyExt.cpp
    // éQè∆: ExtRigidBodyExt.cpp
    //       ExtInertiaTensor.h

    public class RBInertiaTensor
    {
        RBMatrix3x3 _inertiaTensor;
        Vector3 _cg;
        float _mass;

        public RBMatrix3x3 InertiaTensor { get { return _inertiaTensor; } set { _inertiaTensor = value; } }
        public Vector3 CenterOfGravity { get { return _cg; } set { _cg = value; } }
        public float Mass { get { return _mass; } set { _mass = value; } }

        public void SetInertiaOBB(RBColliderOBB obb)
        {
            Vector3 extents = obb.size / 2f;

            float mass = 8 / RBPhysUtil.V3Volume(extents);
            float s = (1 / 3) + mass;

            float x = extents.x * extents.x;
            float y = extents.y * extents.y;
            float z = extents.z * extents.z;

            SetDiagonal(new Vector3(x, y, z) * s);

            CenterOfGravity = Vector3.zero;
            Mass = mass;
        }

        public void SetInertiaSphere(RBColliderSphere sphere)
        {

        }

        public void ApplyTransform(Vector3 pos, Quaternion rot, Vector3 scale)
        {
            
        }

        void SetDiagonal(Vector3 diagonalTensor)
        {
            _inertiaTensor = RBMatrix3x3.CreateDiagonal(diagonalTensor);
        }

        void Rotate(Quaternion q)
        {
            new RBMatrix3x3();
        }

        void Rotate(RBMatrix3x3 rot)
        {

        }
    }
}