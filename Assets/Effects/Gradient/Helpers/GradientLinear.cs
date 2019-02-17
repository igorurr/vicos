using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Effects.Gradient
{

    [Serializable]
    public class GradientLinear : GradientLine
    {
        // начальная позиция градиента
        [SerializeField] private Vector2 sfStart;

        // конечная позиция градиента
        [SerializeField] private Vector2 sfEnd;

        private Tuple<float, float, float> GetShaderPreprocessData( Vector2 _a, Vector2 _b )
        {
            float x = ( _b.x - _a.x );
            float y = ( _b.y - _a.y );

            float isx = Math.Abs(x) > Math.Abs(y) ? 1 : -1;

            float k = isx > 0 ? (y / x) : (x / y);

            float p =
                isx > 0
                    ? (_a.y - _a.x * k)
                    : (_a.x - _a.y * k);

            Debug.Log($"{x}, {y}, {isx > 0}, {k}, {p}");

            return new Tuple<float, float, float>( isx, k, p );
        }

        public GradientLinear (
            Vector2                    _start,
            Vector2                    _end,
            params GradientLinePoint[] _points
        )
            : base( _points )
        {
            sfStart = _start;
            sfEnd   = _end;
        }

        public override void WriteData( Material _material )
        {
            Tuple<float, float, float> preprocessData = GetShaderPreprocessData( sfStart, sfEnd );

            _material.SetFloatArray( "_GradientParams", new []
            {
                // подробные описания всех обозначений смотреть в документации
                // цифрами указаны позиции в массиве _GradientParams в шейдере
                sfStart.x, sfStart.y, // 0, 1 : точка а (ax, ay)
                sfEnd.x, sfEnd.y,     // 2, 3 : точка b (bx, by)
                preprocessData.Item1, // 4: isx
                preprocessData.Item2, // 5: k
                preprocessData.Item3  // 6: p
            } );

            base.WriteData( _material );
        }
    }
}