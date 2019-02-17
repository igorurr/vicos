#ifndef UI_GRADIENT_LINEAR
#define UI_GRADIENT_LINEAR

#include "./Constants.cginc"

#include "./Helpers.cginc"

#include "./LineGradient.cginc"





float2 CalculateFisx(
    float  _k,
    float  _p,
    float2 _c
){
    float fx = ( _k *_c.y - _k * _p + _c.x ) / ( 1 + _k * _k );
    
    float fy = _k * fx + _p;
    
    return float2( fx, fy );
}

float2 CalculateFisy(
    float  _k,
    float  _p,
    float2 _c
){
    float fy = ( _k *_c.x - _k * _p + _c.y ) / ( 1 + _k * _k );
    
    float fx = _k * fy + _p;
    
    return float2( fx, fy );
}




float4 CalculateColorFromPointLinearGradient( 
    float2  _curPos,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    float2 a = float2( _gradientParams[0], _gradientParams[1] );
    float2 b = float2( _gradientParams[2], _gradientParams[3] );
    
    bool   isx = _gradientParams[4] > 0;
    float  k = _gradientParams[5];
    float  p = _gradientParams[6];
    
    float2 f;
    float l;
    
    if( isx ) {
        f = CalculateFisx( k, p, _curPos );
        l = lexp( a.x, b.x, f.x );
    }
    else {
        f = CalculateFisy( k, p, _curPos );
        l = lexp( a.y, b.y, f.y );
    }
    
    return CalculateColorOnLine( 
        l, 
        _gradientCountPoints,
        _gradientPoints,
        _gradientPointsPositions,
        _gradientTypePoints
    );
}





#endif // UI_GRADIENT_LINEAR