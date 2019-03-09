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
}

/*
    текущая точка имеет дробные координаты
    точке [0;0] соответствует левый нижний угол, точке [1;1] правый верхний
    позиции всех точек в этом квадрате лежат в диаппазоне от 0 до 1 в 2 измерениях
*/

// эта функция вызывается из шейдера, её аргументы лучше не трогай, код можешь переписать весь
float4 CalculateColorFromPointRadialGradient( 
    float2  _d, // текущая точка изображения float2( [0;1], [0;1] )
    float   _gradientParams[64],    // из этого массива мы берём исходные данные радиального градиента, центр, радиусы элипса и прочее
    int     _gradientCountPoints,   // сколько точек у градиента (самой цветолинии), забей
    float   _gradientPoints[1024],  // сюда отдаются данные цветолинии градиента, тоже забей
    float   _gradientPointsPositions[512], // забей
    float   _gradientTypePoints[256] // забей
)
{
    float2 b = float2( _gradientParams[0], _gradientParams[1] );    // центр элипса радиального градиента
    float2 c = float2( _gradientParams[2], _gradientParams[3] );    // центр радиального градиента (точка в которую сходятся все проекции цветолиний)
    float2 r = float2( _gradientParams[4], _gradientParams[5] );    // радиус элипса градиента, с элипса описываемого этим радиусом и центром в точке b исходят проекции цветолиний
    float2 e;
    
    if ( isx( c, _d ) ) {
    
        float  k     = (_d.y - c.y) / (_d.x - c.x);
        float  b1    = _d.y - _d.x* k;
        float  t     = b1 - b.y;
        float  sqA   = r.y * r.y + r.x * r.x * k * k;
        float  sqB   = 2 * ( r.x * r.x * k * t - r.y * r.y * b.x );
        float  sqC   = r.y * r.y * b.x * b.x + r.x * r.x * t * t - r.y * r.y * r.x * r.x;
        
        float sqD = sqrt( sqB * sqB - 4 * sqA * sqC );
        float x1 = ( -sqB + sqD ) / ( 2 * sqA );
        float x2 = ( -sqB - sqD ) / ( 2 * sqA );
        
        float x;
        float y;
    
        if( -_d.x + c.x + c.y < _d.y )
        {
            x = x1;
            y = k * x + b1;
        }
        else
        {
            x = x2;
            y = k * x + b1;
        }
        
        e = float2(x,y);
    }
    else {
    
        float k   = (_d.x - c.x) / (_d.y - c.y);
        float b1  = _d.x - _d.y * k;
        float t   = b1 - b.x;
        float sqA = r.x * r.x + r.y * r.y * k * k;
        float sqB = 2 * ( r.y * r.y * k * t - r.x * r.x * b.y );
        float sqC = r.x * r.x * b.y * b.y + r.y * r.y * t * t - r.y * r.y * r.x * r.x;
        
        float sqD = sqrt( sqB * sqB - 4 * sqA * sqC );
        float y1 = ( -sqB + sqD ) / ( 2 * sqA );
        float y2 = ( -sqB - sqD ) / ( 2 * sqA );
        
        float x;
        float y;
    
        if( -_d.x + c.x + c.y < _d.y )
        {
            y = y1;
            x = k * y + b1;
        }
        else
        {
            y = y2;
            x = k * y + b1;
        }
        
        e = float2(x,y);
    }
    
    float l = lexp( e, c, _d );
    
    return CalculateColorOnLine( 
        l, 
        _gradientCountPoints,
        _gradientPoints,
        _gradientPointsPositions,
        _gradientTypePoints
    );
}





#endif // UI_GRADIENT_RADIAL