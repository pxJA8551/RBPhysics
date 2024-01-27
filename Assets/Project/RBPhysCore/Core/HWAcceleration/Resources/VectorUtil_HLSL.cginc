
float3 ProjectOnPlane(float3 v, float3 planeNormal)
{
    float sqrtF = dot(planeNormal, planeNormal);
    float f = dot(v, planeNormal);
    return v - planeNormal * f / sqrtF;
}

float3 ProjectPointToLine(float3 p, float3 begin, float3 dirN)
{
    return begin + dirN * dot(p - begin, dirN);
}

float3 ProjectPointToEdge(float3 p, float3 begin, float3 end)
{
    float dn = length(end - begin);
    float3 dirN = (end - begin) / dn;
    return begin + dirN * clamp(dot(p - begin, dirN), 0, dn);
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
    float3 prjP = p;
    
    if (dot(cross(prjP - a, b - prjP), n) >= 0 && dot(cross(prjP - b, c - prjP), n) >= 0 && dot(cross(prjP - c, d - prjP), n) >= 0 && dot(cross(prjP - d, a - prjP), n) >= 0)
    {
        return true;
    }
    else
    {
        if (dot(cross(prjP - a, b - prjP), n) <= 0 && dot(cross(prjP - b, c - prjP), n) <= 0 && dot(cross(prjP - c, d - prjP), n) <= 0 && dot(cross(prjP - d, a - prjP), n) <= 0)
        {
            return true;
        }
    }
    
    return false;
}

float3 ProjectPointToRect(float3 p, float3 a, float3 b, float3 c, float3 d, float3 cx, float3 n)
{
    float3 prjP = ProjectPointToPlane(p, n, cx);
    
    if (IsInRect(prjP, a, b, c, d, n))
    {
        return prjP;
    }
    else
    {
        float3 prjA = ProjectPointToEdge(prjP, a, b);
        float3 prjB = ProjectPointToEdge(prjP, b, c);
        float3 prjC = ProjectPointToEdge(prjP, c, d);
        float3 prjD = ProjectPointToEdge(prjP, d, a);
        
        float dt = length(prjA - prjP);

        float dMin = dt;
        float3 prjR = prjA;
        
        dt = length(prjB - prjP);
        if (dt < dMin)
        {
            dMin = dt;
            prjR = prjB;
        }
        
        dt = length(prjC - prjP);
        if (dt < dMin)
        {
            dMin = dt;
            prjR = prjC;
        }
        
        dt = length(prjD - prjP);
        if (dt < dMin)
        {
            dMin = dt;
            prjR = prjD;
        }
        
        return prjR;
    }
}

void ProjectLineToLine(inout float3 beginA, inout float3 endA, float3 beginB, float3 endB)
{
    float ebA = length(endA - beginA);
    float3 prjDirN = ebA > 0 ? (endA - beginA) / ebA : 0;
    
    float3 avg = (endA + beginA) / 2;
    
    float db = clamp(dot(prjDirN, beginB - avg), -ebA / 2, ebA / 2);
    float de = clamp(dot(-prjDirN, endB - avg), -ebA / 2, ebA / 2);
    
    beginA = avg + prjDirN * db;
    endA = avg + -prjDirN * de;
}

float3 ReverseProject(float d, float3 prjDirN, float3 revPrjDirN)
{
    float div = dot(prjDirN, revPrjDirN);
    return div != 0 ? revPrjDirN * (d / div) : d;
}

void ReverseProjectLineToLine(inout float3 beginA, inout float3 endA, float3 beginB, float3 endB)
{
    float ebA = length(endA - beginA);
    float3 prjDirN = ebA > 0 ? (endA - beginA) / ebA : 0;
    
    float3 bDirN = normalize(endB - beginB);
    
    float3 avg = (endA + beginA) / 2;
    float3 prjAvg = ProjectPointToLine(avg, beginB, normalize(endB - beginB));
    
    float3 pbB = beginB - prjAvg;
    float3 peB = endB - prjAvg;
    
    float db = clamp(dot(prjDirN, ReverseProject(length(pbB) * sign(dot(pbB, bDirN)), bDirN, prjDirN)), -ebA / 2, ebA / 2);
    float de = clamp(dot(prjDirN, ReverseProject(length(peB) * sign(dot(peB, bDirN)), bDirN, prjDirN)), -ebA / 2, ebA / 2);
    
    beginA = avg + prjDirN * db;
    endA = avg + prjDirN * de;
}

void CalcNearest(float3 beginA, float3 endA, float3 beginB, float3 endB, out float3 nearestA, out float3 nearestB)
{
    float ebA = length(endA - beginA);
    float ebB = length(endB - beginB);
    float3 dirAN = ebA > 0 ? (endA - beginA) / ebA : 0;
    float3 dirBN = ebB > 0 ? (endB - beginB) / ebB : 0;
    
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
    float3 dirAN = ebA > 0 ? (endA - beginA) / ebA : 0;
    float3 dirBN = ebB > 0 ? (endB - beginB) / ebB : 0;
    
    float dotAB = dot(dirAN, dirBN);
    float div = 1 - dotAB * dotAB;
    
    float3 aToB = beginB - beginA;
    
    float r1 = (dot(aToB, dirAN) - dotAB * dot(aToB, dirBN)) / div;
    float r2 = (dotAB * dot(aToB, dirAN) - dot(aToB, dirBN)) / div;

    return beginA + clamp(r1, 0, ebA) * dirAN;
}

void CalcNearestLine(float3 beginA, float3 endA, float3 beginB, float3 endB, out float3 nearestA, out float3 nearestB)
{
    float3 dirAN = normalize(endA - beginA);
    float3 dirBN = normalize(endB - beginB);
    
    float dotAB = dot(dirAN, dirBN);
    float div = 1 - dotAB * dotAB;
    
    float3 aToB = beginB - beginA;
    
    float r1 = (dot(aToB, dirAN) - dotAB * dot(aToB, dirBN)) / div;
    float r2 = (dotAB * dot(aToB, dirAN) - dot(aToB, dirBN)) / div;

    nearestA = beginA + r1 * dirAN;
    nearestB = beginB + r2 * dirBN;
}

float3 CalcNearestLine(float3 beginA, float3 endA, float3 beginB, float3 endB)
{
    float3 dirAN = normalize(endA - beginA);
    float3 dirBN = normalize(endB - beginB);
    
    float dotAB = dot(dirAN, dirBN);
    float div = 1 - dotAB * dotAB;
    
    float3 aToB = beginB - beginA;
    
    float r1 = (dot(aToB, dirAN) - dotAB * dot(aToB, dirBN)) / div;
    float r2 = (dotAB * dot(aToB, dirAN) - dot(aToB, dirBN)) / div;

    return beginA + r1 * dirAN;
}

float3 Scale(float3 a, float3 b)
{
    return float3(a.x * b.x, a.y * b.y, a.z * b.z);
}

float3 Epsilon(float3 v, float eps)
{
    return v - (v % eps);
}