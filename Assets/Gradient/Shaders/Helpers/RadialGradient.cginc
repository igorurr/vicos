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



float3 CalcT1T2T3isx(
    float2 _b,
    float2 _c,
    float2 _d
) {
    float firstPath = ( _c.x * _d.y - _d.x * _c.y );
    
    float secondPath = _c.y - _d.y;
    
    float delim = _c.x - _d.x;

    float t1 = firstPath / delim;
    
    float t2 = secondPath / delim;

    float t3 = t1 - _b.y;

    return float3( t1, t2, t3 );
}

float3 CalcT1T2T3isy(
    float2 _b,
    float2 _c,
    float2 _d
) {
    float firstPath = ( _c.x * _d.y - _d.x * _c.y );
    
    float secondPath = _d.x - _c.x;
    
    float delim = _d.y - _c.y;

    float t1 = firstPath / delim;
    
    float t2 = secondPath / delim;

    float t3 = t1 - _b.x;

    return float3( t1, t2, t3 );
}



float3 CalcSQABCisx(
    float2 _b,
    float2 _r,
    float  _t2,
    float  _t3
) {
    float sqa = sqr( _r.y ) + sqr( _r.x * _t2 );
    
    float sqb = 2 * _b.x * sqr( _r.y ) - 2 * sqr( _r.x ) * _t2 * _t3;

    float sqc = sqr( _r.y * _b.x ) + sqr( _r.x * _t3 ) - sqr( _r.x * _r.y );

    return float3( sqa, sqb, sqc );
}

float3 CalcSQABCisy(
    float2 _b,
    float2 _r,
    float  _t2,
    float  _t3
) {
    float sqa = sqr( _r.x ) + sqr( _r.y * _t2 );
    
    float sqb = 2 * _b.y * sqr( _r.x ) - 2 * sqr( _r.y ) * _t2 * _t3;

    float sqc = sqr( _r.x * _b.y ) + sqr( _r.y * _t3 ) - sqr( _r.x * _r.y );

    return float3( sqa, sqb, sqc );
}

// передаются параметры квадратного уравнения: a, -b, c
// ВАЖНО: b передаётся именно со знаком -
// возвращается один корень со знаком - или + , этот момент пока не ясен
float CalcSqareEquation(
    float3 _sqABC
) {
    float sqrtD = sqrt( sqr( _sqABC.y ) - 4 * _sqABC.x * _sqABC.z );
    
    float sqb = 2 * _b.y * sqr( _r.x ) - 2 * sqr( _r.y ) * _t2 * _t3;

    float sqc = sqr( _r.x * _b.y ) + sqr( _r.y * _t3 ) - sqr( _r.x * _r.y );

    return ( _sqABC.b + sqrtD ) / ( 2 * _sqABC.x );
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
    float2 r = float2( _gradientParams[4], _gradientParams[5] );
    
    float  l;
    
    // проверка, какой знаменатель больше
    // вычисления пойдут по той ветви где знаменатель по модулю больше
    if ( isx( c, _d ) ) {
        float3  t123 = CalcT1T2T3isx( b, c, d );

        float3  sqABC = CalcSQABCisx( b, r, t123.y, t123.z );

        // вычисляем координату x точки e
        float  ex = CalcSqareEquation( sqABC );

        l = lexp( ex, c.x, _d.x );
    }
    else {
        float3  t123 = CalcT1T2T3isy( b, c, d );

        float3  sqABC = CalcSQABCisy( b, r, t123.y, t123.z );

        // вычисляем координату y точки e
        float  ey = CalcSqareEquation( sqABC );

        l = lexp( ey, c.y, _d.y );
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