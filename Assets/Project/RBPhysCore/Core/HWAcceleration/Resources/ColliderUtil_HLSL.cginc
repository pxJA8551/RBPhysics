
inline float GetOBBAxisSize(float3 fwdSize, float3 rightSize, float3 upSize, float3 axisN)
{
    return abs(dot(fwdSize, axisN)) + abs(dot(rightSize, axisN)) + abs(dot(upSize, axisN));
}