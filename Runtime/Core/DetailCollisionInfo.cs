namespace RBPhys
{
    public static partial class RBDetailCollision
    {
        public struct DetailCollisionInfo
        {
            public OBB_OBB_PenetrationType obb_obb_penetration;
        }

        public enum OBB_OBB_PenetrationType
        {
            None = -1,
            ARight = 0,
            AUp = 1,
            AFwd = 2,
            BRight = 3,
            BUp = 4,
            BFwd = 5,
            Cross_ARight_BRight = 6,
            Cross_AUp_BRight = 7,
            Cross_AFwd_BRight = 8,
            Cross_ARight_BUp = 9,
            Cross_AUp_BUp = 10,
            Cross_AFwd_BUp = 11,
            Cross_ARight_BFwd = 12,
            Cross_AUp_BFwd = 13,
            Cross_AFwd_BFwd = 14,
        }
    }
}