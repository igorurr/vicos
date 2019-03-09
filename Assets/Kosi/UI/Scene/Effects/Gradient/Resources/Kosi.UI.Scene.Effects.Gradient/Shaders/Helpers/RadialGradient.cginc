#ifndef UI_GRADIENT_RADIAL
#define UI_GRADIENT_RADIAL

#include "./Constants.cginc"

#include "./Helpers.cginc"

#include "./LineGradient.cginc"








int FloatCompare( float a, float b, eps=0.000000001 )
{
    if( b - a > eps )
        return 1;

    if( a - b > eps )
        return -1;

    return 0;
}





// wolfram alpha
float2 CalcE(
    float2 _b,
    float2 _c,
    float2 _d,
    float2 _r
)
{
    float A = _d.y - _c.y;
    float B = _c.x - _d.x;
    float C = -( _d.x * A  + _d.y * B );

    float t5  = B * B * _r.y * _r.y;
    float t6  = A * A  + _r.x * _r.x;
    float t4  = t5  + t6;
    float t7  = _r.x * _r.y;
    float t8  = - _b.x * _b.x * A * A  - 2 * _b.x * C * A  - C * C;
    float t9  = - B * ( 2 _b.x * _b.y * A  - _b.y * _b.y * B  - 2 * _b.y * C );
    float t10 = _b.y * _r.x * _r.x;
    float t11 = A * _r.x * _r.x;

    // 1 случай
    if( FloatCompare( B, 0 ) != 0 )
    {
        float x11 = _b.x * t5  - t10 * B * A  - C * t11;
        float y11 = _b.x * t5 * A  - t10 * B * A  - C * t5;

        float sqrtD1 = B * t7 * sqrt( t8 + t9 + t4 );

        // возможно lexp вообще не надо делать и всегда надо показывать только один корень, закрась картинку в зелёный или красный в зависимости от выбранного корня
        float x1 = ( sqrtD1 + x11 ) / t4;
        float x2 = ( -sqrtD1 + x11 ) / t4;

        float y1 = ( - A * sqrtD1 + y11 ) / ( B * t4 );
        float y2 = ( A * sqrtD1 + y11 ) / ( B * t4 );

        float e1 = float2( x1, y1 );

        // код дублируется чтобы удобнее было сделать проверку, после неё убери лишнее
        if ( lexp( _c, _d, e1 ) >= 0 )
            return e1;
        else
            return float2( x2, y2 );
    }

    // 2 случай
    else
    {
        float sqrtD2 = t7 * sqrt( t8 + t6 );

        float x = - C / A;

        float y1 = - sqrtD2 / t11 + _b.y;
        float y2 = sqrtD2 / t11 + _b.y;

        float e1 = float2( x1, y1 );

        if ( lexp( _c, _d, e1 ) >= 0 )
            return e1;
        else
            return float2( x2, y2 );
    }
}




float4 CalculateColorFromPointRadialGradient( 
    float2  _d, // текущая точка изображения float2( [0;1], [0;1] )
    float   _gradientParams[64],    // из этого массива мы берём исходные данные радиального градиента, центр, радиусы элипса и прочее
    int     _gradientCountPoints,   // сколько точек у градиента (самой цветолинии)
    float   _gradientPoints[1024],  // сюда отдаются данные цветолинии градиента
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256] 
)
{
    float2 b = float2( _gradientParams[0], _gradientParams[1] );    // центр элипса радиального градиента
    float2 c = float2( _gradientParams[2], _gradientParams[3] );    // центр радиального градиента (точка в которую сходятся все проекции цветолиний)
    float2 r = float2( _gradientParams[4], _gradientParams[5] );    // радиус элипса градиента, с элипса описываемого этим радиусом и центром в точке b исходят проекции цветолиний
    
    float l = 1;

    if( FloatCompare( b.x, c.x ) != 0 || FloatCompare( b.y, c.y ) != 0 )
    {
        float2 e = CalcE( b, c, _d, r );
        l = lexp( _c, e, d );
    }
    
    // вычисление цвета текущей точки по позиции на проекции цветолинии градиента
    return CalculateColorOnLine( 
        l, 
        _gradientCountPoints,
        _gradientPoints,
        _gradientPointsPositions,
        _gradientTypePoints
    );
}



















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