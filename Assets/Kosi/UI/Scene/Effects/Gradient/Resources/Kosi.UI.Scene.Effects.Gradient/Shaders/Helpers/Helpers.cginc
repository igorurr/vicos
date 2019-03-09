#ifndef UI_GRADIENT_HELPERS
#define UI_GRADIENT_HELPERS




// обратная функция для lerp:
// res = lerp( a, b, t )
// t = lexp( a, b, res )
float lexp( float a, float b, float res ) {
    return ( res - a ) / ( b - a );
}

float lexp( float2 a, float2 b, float2 res ) {
    if( abs( a.x - b.x ) > abs( a.y - b.y ) )
        return ( res.x - a.x ) / ( b.x - a.x );
    else
        return ( res.y - a.y ) / ( b.y - a.y );
}

float sqr( float x ) {
    return x * x;
}




#endif // UI_GRADIENT_HELPERS