
float3 ProjectOnPlane(float3 v, float3 planeNormal)
{
    float sqrtF = dot(planeNormal, planeNormal);
    float f = dot(v, planeNormal);
    return v - planeNormal * f / sqrtF;
}

float3 ProjectPointOnPlane(float3 p, float3 plane, float3 center)
{
    return center + ProjectOnPlane(p - center, plane);
}