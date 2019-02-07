#ifndef UI_GRADIENT_POINTS
#define UI_GRADIENT_POINTS

#include "./Constants.cginc"





float4 CalculateColorFromPointPointsGradient( 
    float2  _curPos,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    return float4(0,0,1,1);
}





#endif // UI_GRADIENT_POINTS