#ifndef UI_GRADIENT_POINTS
// Upgrade NOTE: excluded shader from DX11 because it uses wrong array syntax (type[size] name)
#pragma exclude_renderers d3d11
#define UI_GRADIENT_POINTS

#include "./Constants.cginc"




// возвращает вес общей точки для текущей точки
float2 GetPositionPoint(
    float  _numPoint,
    float  _gradientPoints[1024]
) {
    int startPos = GRADIENT_POINT_DATA_SIZE__POINTS_POINT * _numPoint;
    
    return float2( _gradientPoints[ startPos ], _gradientPoints[ startPos + 1 ] );
}

// возвращает вес общей точки для текущей точки
float4 GetColorPoint(
    float  _numPoint,
    float  _gradientPoints[1024]
) {
    int startPos = GRADIENT_POINT_DATA_SIZE__POINTS_POINT * _numPoint;
    
    return float4( 
        _gradientPoints[ startPos + 3 ], 
        _gradientPoints[ startPos + 4 ], 
        _gradientPoints[ startPos + 5 ], 
        _gradientPoints[ startPos + 6 ]
    );
}

// возвращает вес общей точки для текущей точки
float GetLocalWeightPoint(
    float  _numPoint,
    float  _gradientPoints[1024]
) {
    int startPos = GRADIENT_POINT_DATA_SIZE__POINTS_POINT * _numPoint;
    
    return _gradientPoints[ startPos + 2 ];
}



// возвращает часть цвета от общей точки с её весом для текущей точки
float4 ColorPathFromPoint(
    float _numPoint,
    float _weight,
    float _gradientPoints[1024]
) {
    return _weight * GetColorPoint( _numPoint, _gradientPoints );
}

// возвращает вес общей точки для текущей точки
float GetWeightPoint(
    float2 _curPoint,
    float  _numPoint,
    float  _gradientPoints[1024]
) {
    float  localWeightGeneralPoint = GetLocalWeightPoint( _numPoint, _gradientPoints );
    float2 positionGeneralPoint = GetPositionPoint( _numPoint, _gradientPoints );
    
    // dot - скалярное произведение
    // так как нам требуется 1/расстояние^2, просто найдём скалярное произведение
    // и поделим на него, вместо того чтобы искать дистанцию, так меньше вычислений
    float2 distVec = _curPoint - positionGeneralPoint;
    float  distSqr = dot( distVec, distVec );
    
    return localWeightGeneralPoint / distSqr;
}



float4 CalculateColorFromPoint( 
    float2  _curPos,
    //float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024]
    //float   _gradientPointsPositions[512],
    //float   _gradientTypePoints[256]
)
{
    float4 res = float4( 0, 0, 0, 0 );
    float resWeight = 0;
    
    for( int i = 0; i < _gradientCountPoints; i++ ) {
        float weight = GetWeightPoint( _curPos, i, _gradientPoints );
        
        res += ColorPathFromPoint( i, weight, _gradientPoints );
        resWeight += weight;
    }
    
    return res / resWeight;
}





#endif // UI_GRADIENT_POINTS