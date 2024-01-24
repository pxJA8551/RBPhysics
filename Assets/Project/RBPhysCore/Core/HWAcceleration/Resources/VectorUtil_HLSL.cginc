
float3 ProjectOnPlane(float3 v, float3 planeNormal)
{
    float sqrtF = dot(planeNormal, planeNormal);
    float f = dot(v, planeNormal);
    return v - planeNormal * f / sqrtF;
}

float3 ProjectPointToLine(float3 p, float3 begin, float3 end)
{
    return begin + dot(p - begin, normalize(begin - end));
}

float3 ProjectPointToPlane(float3 p, float3 plane, float3 center)
{
    return center + ProjectOnPlane(p - center, plane);
}

float3 ProjectPointOnPlaneToPlane(float3 p, float3 planeNormalN, float3 center)
{
    float3 d = center - p;
    float f = dot(-d, planeNormalN);
    return f > 0 ? p : center + (-d - planeNormalN * f);
}

bool IsInRect(float3 p, float3 a, float3 b, float3 c, float3 d, float3 n)
{
    bool da = dot(cross(p, a), n) > 0;
    
    if (da)
    {
        return dot(cross(p, b), n) > 0 && dot(cross(p, c), n) > 0 && dot(cross(p, d), n) > 0;
    }
    else
    {
        return dot(cross(p, a), n) < 0 && dot(cross(p, b), n) < 0 && dot(cross(p, c), n) < 0 && dot(cross(p, d), n) < 0;
    }
}

void ProjectLineToLine(inout float3 beginA, inout float3 endA, float3 beginB, float3 endB)
{
    float ebA = length(endA - beginA);
    float3 prjDirN = (endA - beginA) / ebA;
    
    float avg = (endA + beginA) / 2;
    
    float db = clamp(dot(prjDirN, beginB - avg), -ebA / 2, ebA / 2);
    float de = clamp(dot(prjDirN, endB - avg), -ebA / 2, ebA / 2);
    
    beginA = avg + prjDirN * min(db, de);
    endA = avg + prjDirN * max(db, de);
}

void ProjectLineToLineD(inout float3 beginA, inout float3 endA, float3 beginB, float3 endB)
{
    float ebA = length(endA - beginA);
    float3 prjDirN = (endA - beginA) / ebA;
    
    float avg = (endA + beginA) / 2;
    
    float db = clamp(dot(prjDirN, beginB - avg), -ebA / 2, ebA / 2);
    float de = clamp(dot(prjDirN, endB - avg), -ebA / 2, ebA / 2);
    
    if (db != de)
    {
        beginA = avg + prjDirN * min(db, de);
        endA = avg + prjDirN * max(db, de);
    }
}

void CalcNearest(float3 beginA, float3 endA, float3 beginB, float3 endB, out float3 nearestA, float3 nearestB)
{
    float ebA = length(endA - beginA);
    float ebB = length(endB - beginB);
    float3 dirAN = (endA - beginA) / ebA;
    float3 dirBN = (endB - beginB) / ebB;
    
    float dotAB = dot(dirAN, dirBN);
    float div = 1 - dotAB * dotAB;
    
    float3 aToB = beginB - beginA;
    
    float r1 = (dot(aToB, dirAN) - dotAB * dot(aToB, dirBN)) / div;
    float r2 = (dotAB * dot(aToB, dirAN) - dot(aToB, dirBN)) / div;

    nearestA = beginA + clamp(r1, 0, ebA) * dirAN;
    nearestB = beginB + clamp(r2, 0, ebB) * dirBN;
}

float3 CalcNearest(float3 beginA, float3 endA, float3 beginB, float3 endB)
{
    float ebA = length(endA - beginA);
    float ebB = length(endB - beginB);
    float3 dirAN = (endA - beginA) / ebA;
    float3 dirBN = (endB - beginB) / ebB;
    
    float dotAB = dot(dirAN, dirBN);
    float div = 1 - dotAB * dotAB;
    
    float3 aToB = beginB - beginA;
    
    float r1 = (dot(aToB, dirAN) - dotAB * dot(aToB, dirBN)) / div;
    float r2 = (dotAB * dot(aToB, dirAN) - dot(aToB, dirBN)) / div;

    return beginA + clamp(r1, 0, ebA) * dirAN;
}

void CalcConvexTriangleFan(float3 points[16])
{
    [unroll(16)]
    for (int i = 0; i < 16; i++)
    {
        
    }
}