using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class RBRaycast
{
    public struct RBRaycastHit
    {
        public Vector3 point;
        public Vector3 normal;
        public float dist;
    }
}