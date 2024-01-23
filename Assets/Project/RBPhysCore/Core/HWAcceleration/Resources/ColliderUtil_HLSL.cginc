#include "MathUtil_HLSL.cginc"

inline float GetOBBAxisSize(float3 fwdSize, float3 rightSize, float3 upSize, float3 axisN)
{
    return abs(dot(fwdSize, axisN)) + abs(dot(rightSize, axisN)) + abs(dot(upSize, axisN));
}

inline float3 GetOBBAxisOffset(float3 fwdSize, float3 rightSize, float3 upSize, float3 axisN)
{
    return fwdSize * Sign101(dot(fwdSize, axisN)) + rightSize * Sign101(dot(rightSize, axisN)) + upSize * Sign101(dot(upSize, axisN));
}