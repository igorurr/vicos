#ifndef UI_GRADIENT_KERNEL
#define UI_GRADIENT_KERNEL

#include "./Debug.cginc"

#include "./Constants.cginc"

#include "./LinearGradient.cginc"
#include "./PointsGradient.cginc"
#include "./RadialGradient.cginc"





float4 CalculateColorFromPoint( 
    float2  _curPos,
    int     _typeGradient,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    /*if( CheckDebug( 
            _curPos,
            _typeGradient,
            _gradientParams,
            _gradientCountPoints,
            _gradientPoints,
            _gradientPointsPositions,
            _gradientTypePoints
     ) )
        return ColorDebug(
            _curPos,
            _typeGradient,
            _gradientParams,
            _gradientCountPoints,
            _gradientPoints,
            _gradientPointsPositions,
            _gradientTypePoints
        );*/

    if( _typeGradient == GRADIENT_TYPE__LINEAR )
        return CalculateColorFromPointLinearGradient(
            _curPos,
            _gradientParams,
            _gradientCountPoints,
            _gradientPoints,
            _gradientPointsPositions,
            _gradientTypePoints
        );
        
    else if( _typeGradient == GRADIENT_TYPE__RADIAL )
        return CalculateColorFromPointRadialGradient(
            _curPos,
            _gradientParams,
            _gradientCountPoints,
            _gradientPoints,
            _gradientPointsPositions,
            _gradientTypePoints
        );
        
    else
        return CalculateColorFromPointPointsGradient(
            _curPos,
            _gradientParams,
            _gradientCountPoints,
            _gradientPoints,
            _gradientPointsPositions,
            _gradientTypePoints
        );
}




#endif // UI_GRADIENT_KERNEL