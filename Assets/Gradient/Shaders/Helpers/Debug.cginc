#ifndef DEBUG
#define DEBUG




bool IsLineSegment( float2 cur, float2 a, float2 b )
{
    float val = ( a.y - b.y ) * cur.x + ( b.x - a.x ) * cur.y + ( a.x*b.y - b.x*a.y );
    
    float l;
    if ( ( b.y - a.y ) > ( b.x - a.x ) )
        l = ( cur.y - a.y ) / ( b.y - a.y );
    else
        l = ( cur.x - a.x ) / ( b.x - a.x );
    
    return ( 
        val < 0.007 && val > -0.007 &&
        l > 0 && l < 1
    );
}

bool IsPoint( float2 cur, float2 a )
{
    return ( ( cur.x - a.x ) * ( cur.x - a.x ) + ( cur.y - a.y ) * ( cur.y - a.y ) < 0.0008 );
}




bool CheckDebug( 
    float2  _curPos,
    int     _typeGradient,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    
    float2 a = float2( _gradientParams[0], _gradientParams[1] );
    float2 b = float2( _gradientParams[2], _gradientParams[3] );

    return ( 
        IsLineSegment( 
            _curPos,
            a,
            b
        )
        ||
        IsPoint( 
            _curPos,
            a
        )
        ||
        IsPoint( 
            _curPos,
            b
        )
    );
}

float4 ColorDebug( 
    float2  _curPos,
    int     _typeGradient,
    float   _gradientParams[64],
    int     _gradientCountPoints,
    float   _gradientPoints[1024],
    float   _gradientPointsPositions[512],
    float   _gradientTypePoints[256]
)
{
    float2 a = float2( _gradientParams[0], _gradientParams[1] );
    float2 b = float2( _gradientParams[2], _gradientParams[3] );
        
    if ( 
        IsLineSegment( 
            _curPos,
            a,
            b
        ))
        return float4(0,0,0,1);
    
    if ( 
        IsPoint( 
            _curPos,
            a
        )
        ||
        IsPoint( 
            _curPos,
            b
        )
    )
        return float4(0.4,0.4,0.4,1);
        
    return float4(0,0,0,0);
}




#endif // DEBUG