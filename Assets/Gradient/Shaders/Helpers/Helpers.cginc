#ifndef UI_GRADIENT_HELPERS
#define UI_GRADIENT_HELPERS




// обратная функция для lerp:
// res = lerp( a, b, t )
// t = lexp( a, b, res )
float lexp( float a, float b, float res ) {
    return ( res - a ) / ( b - a );
}

float sqr( float x ) {
    return x * x;
}


#endif // UI_GRADIENT_HELPERS