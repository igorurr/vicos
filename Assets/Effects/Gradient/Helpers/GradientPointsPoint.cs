using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GradientPointsPoint : GradientPointBase
{
    [SerializeField] private Vector2 sfPosition;
    [SerializeField] private float sfStrong;

    public GradientPointsPoint( 
        Vector2  _position,
        float    _strong,
        Color    _color
    )
    {
        sfPosition = _position.Clamp01();
        sfStrong   = Mathf.Clamp01( _strong );
        sfColor    = _color;
    }

    public override List<float> ToData()
    {
        return new List<float>
        {
            sfPosition.x,   sfPosition.y,                             // 0, 1       - позиция
            sfStrong,                                                 // 2          - вес точки
            sfColor.r,      sfColor.g,     sfColor.b,     sfColor.a   // 3, 4, 5, 6 - цвет
        };
    }

    public override int GetSize()
    {
        return (int) GradientPointDataSize.POINTS_POINT;
    }

    public override GradientPointType GetTypeGradient()
    {
        return GradientPointType.POINTS_POINT;
    }
}
