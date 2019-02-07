#ifndef UI_GRADIENT_RADIAL
#define UI_GRADIENT_RADIAL

#include "./Constants.cginc"

#include "./Helpers.cginc"

#include "./LineGradient.cginc"







bool isx(
    float2  _c,
    float2  _d
){
    float x = abs( _c.x - _d.x );
    float y = abs( _c.y - _d.y );
    
    return x > y;
    
    return float2( x, y );
}



float CalculateT1isx(
    float2  _c,
    float2  _d,
    float2  _b
){
    return ( _c.x * _d.y - _d.x * _c.y ) / ( _c.x - _d.x ) - _b.y;
}

float CalculateT1isy(
    float2  _c,
    float2  _d,
    float2  _b
){
    return ( _c.x * _d.y - _d.x * _c.y ) / ( _d.y - _c.y ) - _b.x;
}



float CalculateT2isx(
    float2  _c,
    float2  _d
){
    return ( _d.y - _c.y ) / ( _d.x - _c.x );
}

float CalculateT2isy(
    float2  _c,
    float2  _d
){
    return ( _d.x - _c.x ) / ( _d.y - _c.y );
}



float CalculateDisx(
    float   _t1,
    float   _t2,
    float   _r,
    float2  _b
){
    return sqr( 2 * _t1 * _t2 - 2 * _b.x ) - 4 * ( 1 + _t2 * _t2 ) * ( _b.x * _b.x - _r * _r + _t1 * _t1 );
}

float CalculateDisy(
    float   _t1,
    float   _t2,
    float   _r,
    float2  _b
){
    return sqr( 2 * _t1 * _t2 - 2 * _b.y ) - 4 * ( 1 + _t2 * _t2 ) * ( _b.y * _b.y - _r * _r + _t1 * _t1 );
}



float2 CalculateEisx(
    float   _t1,
    float   _t2,
    float   _D,
    float2  _b
){
    float x = ( 2 * _b.x - 2 * _t2 * _t1 + sqrt(_D) ) / ( 1 + _t2 * _t2 );
    
    float y = x * _t2 + _t1 + _b.y;
    
    return float2( x, y );
}

float2 CalculateEisy(
    float   _t1,
    float   _t2,
    float   _D,
    float2  _b
){
    float y = ( 2 * _b.y - 2 * _t2 * _t1 + sqrt(_D) ) / ( 1 + _t2 * _t2 );
    
    float x = y * _t2 + _t1 + _b.x;
    
    return float2( x, y );
}



float4 CalculateColorFromPointRadialGradient( 
    float2  _d,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    float2 b = float2( _gradientParams[0], _gradientParams[1] );
    float2 c = float2( _gradientParams[2], _gradientParams[3] );
    float2 r = _gradientParams[4];
    
    float  l;
    
    // проверка, какой знаменатель больше
    // вычисления пойдут по той ветви где знаменатель по модулю больше
    if ( isx( c, _d ) ) {
        float  t1 = CalculateT1isx( c, _d, b );
        float  t2 = CalculateT2isx( c, _d );
        float  D = CalculateDisx( t1, t2, r, b );
        float2 e = CalculateEisx( t1, t2, D, b );
        l = lexp( e.x, c.x, _d.x );
    }
    else {
        float  t1 = CalculateT1isy( c, _d, b );
        float  t2 = CalculateT2isy( c, _d );
        float  D = CalculateDisy( t1, t2, r, b );
        float2 e = CalculateEisy( t1, t2, D, b );
        l = lexp( e.y, c.y, _d.y );
    }
    
    return CalculateColorOnLine( 
        l, 
        _gradientCountPoints,
        _gradientPoints,
        _gradientPointsPositions,
        _gradientTypePoints
    );
}





#endif // UI_GRADIENT_RADIAL