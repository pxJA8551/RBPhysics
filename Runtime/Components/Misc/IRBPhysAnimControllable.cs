namespace RBPhys
{
    public interface IRBPhysAnimControllable
    {
        public void SetAnimSpeed(float speed);
        public void StopAnim();

        public float AnimLength { get; }
        public float AnimTime { get; }
        public float AnimSpeed { get; }
    }
}