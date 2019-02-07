#ifndef UI_GRADIENT_LINE
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
#define UI_GRADIENT_LINE

#include "UnityCG.cginc"
			
#include "./Constants.cginc"
			
#include "./Helpers.cginc"




// возвращает позицию на точку в _gradientPoints
int GetNextColorPoint(
    float  _curColorPoint,
    int    _gradientCountPoints,
    float  _gradientPoints[1024],
    float  _gradientPointsPositions[512],
    float  _gradientTypePoints[256]
)
{
    int nextPos = _curColorPoint + 1;
        
    for( ; nextPos < _gradientCountPoints; nextPos++ ) {
        if( _gradientTypePoints[ nextPos ] == GRADIENT_POINT_TYPE__LINE_COLOR_POINT )
            break;
    }
    
    if ( nextPos >= _gradientCountPoints )
        return _curColorPoint;
        
    return nextPos;
}

// возвращает локальную позицию точки
float GetPointLocalPosition(
    float  _gradientPoints[1024],
    float  _gradientPointsPosition,
    int    _gradientTypePoint
)
{
    return _gradientPoints[ _gradientPointsPosition + 1 ];
}

// возвращает цвет точки
float4 GetPointColor(
    float  _gradientPoints[1024],
    float  _gradientPointsPosition,
    int    _gradientTypePoint
)
{
    if( _gradientTypePoint == GRADIENT_TYPE__POINT )
        return float4( 
            _gradientPoints[ _gradientPointsPosition + 3 ],
            _gradientPoints[ _gradientPointsPosition + 4 ],
            _gradientPoints[ _gradientPointsPosition + 5 ],
            _gradientPoints[ _gradientPointsPosition + 6 ]
        );
    else
        return float4( 
            _gradientPoints[ _gradientPointsPosition + 2 ],
            _gradientPoints[ _gradientPointsPosition + 3 ],
            _gradientPoints[ _gradientPointsPosition + 4 ],
            _gradientPoints[ _gradientPointsPosition + 5 ]
        );
}

// закрашивает точку в цвет заданной ColorPoint
float4 GetColorInterpolatePoints(
    float  _l,
    int    _colorPoint1,
    int    _colorPoint2,
    int    _middlePoint,
    int    _countMiddlePoints,
    float  _gradientPoints[1024],
    float  _gradientPointsPositions[512],
    float  _gradientTypePoints[256]
)
{
    float startMiddlePointlocalPosition = GetPointLocalPosition( 
        _gradientPoints,
        _gradientPointsPositions[ _middlePoint ],
        _gradientTypePoints[ _middlePoint ]
    );
    
    float endMiddlePointlocalPosition = GetPointLocalPosition( 
        _gradientPoints,
        _gradientPointsPositions[ _middlePoint + 1 ],
        _gradientTypePoints[ _middlePoint + 1 ]
    );
    
    float t1 = lexp(
        startMiddlePointlocalPosition,
        endMiddlePointlocalPosition,
        _l
    );
    
    float startMiddlePointInterpolateColorValue = float( _middlePoint - _colorPoint1 ) / _countMiddlePoints;
    float endMiddlePointInterpolateColorValue = float( _middlePoint - _colorPoint1 + 1 ) / _countMiddlePoints;
    
    float t2 = lerp(
        startMiddlePointInterpolateColorValue,
        endMiddlePointInterpolateColorValue,
        t1
    );
    
    float4 c1 = GetPointColor(
        _gradientPoints,
        _gradientPointsPositions[ _colorPoint1 ],
        _gradientTypePoints[ _colorPoint1 ]
    );
    
    float4 c2 = GetPointColor(
        _gradientPoints,
        _gradientPointsPositions[ _colorPoint2 ],
        _gradientTypePoints[ _colorPoint2 ]
    );
    
    return lerp(
        c1,
        c2,
        t2
    );
}




float4 CalculateColorOnLine(
    float   _l,
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    if( _l <= 0 )
        return GetPointColor( 
            _gradientPoints, 
            0, 
            _gradientTypePoints[ 0 ]
        );
        
    if( _l >= 1 )
        return GetPointColor( 
            _gradientPoints,
            _gradientPointsPositions[ _gradientCountPoints-1 ],
            _gradientTypePoints[ _gradientCountPoints-1 ]
        );

    // MIDDLE_POINT могут между точками как быть, так и не быть
    // когда их нет, текущая MIDDLE_POINT это текущая COLOR_POINT,
    // а следующая MIDDLE_POINT это следующая COLOR_POINT
    
    // Изначально все 3 значения равно 0
    // начинается поиск: (итерация) берём текущую COLOR_POINT, если значение её позиции >= _l,
    // останавливаем поиск, текущая точка является nextColorPoint, предыдущая curColorPoint, 
    // иначе в curColorPoint присваеваем текущее значение, продолжаем поиск
    // если были просмотрены все точки, curColorPoint и nextColorPoint присваивается позиция последней COLOR_POINT
    
    // поиск cur/next middlePoint производится аналогично
    // nextMiddlePoint = curMiddlePoint + 1 всегда
    
    // во всех 3 переменных хранится номера точек, аналог _gradientPointsPositions, независимо от типа самих точек
    
    int curColorPoint = -1;
    int nextColorPoint = 0;
    
    int curMiddlePoint = 0;
    
    while ( curColorPoint < _gradientCountPoints ) {
        int next = GetNextColorPoint( 
            curColorPoint,
            _gradientCountPoints,
            _gradientPoints, 
            _gradientPointsPositions, 
            _gradientTypePoints
        );
        
        float nextPosition = GetPointLocalPosition( 
            _gradientPoints, 
            _gradientPointsPositions[next],
            _gradientTypePoints[next]
        );
        
        // точка next лежит дальше текущей, делаем её nextColorPoint и выходим
        // или если больше точек нет и следующая точка это текущая
        if( nextPosition >= _l || next == curColorPoint ) {
            nextColorPoint = next;
            break;
        }
        
        curColorPoint = next;
    }
    
    // такое случится, если позиция 1 точки > 0, _l < позиции 1 точки
    if( curColorPoint < 0 )
        return GetPointColor(
            _gradientPoints,
            _gradientPointsPositions[ 0 ],
            _gradientTypePoints[ 0 ]
        );
    
    // такое случится, если позиция последней точки < 1, _l > позиции последней точки
    if( nextColorPoint == curColorPoint )
        return GetPointColor(
            _gradientPoints,
            _gradientPointsPositions[ curColorPoint ],
            _gradientTypePoints[ curColorPoint ]
        );
    
    int countMiddlePoints = nextColorPoint - curColorPoint; // +1
    
    for ( 
        curMiddlePoint = curColorPoint; 
        curMiddlePoint + 1 < nextColorPoint; 
        curMiddlePoint++
    ) {
        int next = curMiddlePoint + 1;
        
        float nextPosition = GetPointLocalPosition( 
            _gradientPoints, 
            _gradientPointsPositions[next],
            _gradientTypePoints[next]
        );
        
        // точка next лежит дальше текущей, выходим
        // или если больше точек нет и следующая точка это текущая
        if( nextPosition >= _l ) {
            break;
        }
    }
    
    return GetColorInterpolatePoints(
        _l,
        curColorPoint,
        nextColorPoint,
        curMiddlePoint,
        countMiddlePoints,
        _gradientPoints,
        _gradientPointsPositions,
        _gradientTypePoints
    );
}





#endif // UI_GRADIENT_LINE