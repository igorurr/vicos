using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kosi.UI.Scene.Effects.Gradient
{
    public enum GradientType
    {
        POINT  = 1,
        RADIAL = 2,
        LINEAR = 3
    }

    public enum GradientPointType
    {
        POINTS_POINT = 1,
    
        LINE_COLOR_POINT  = 2,
        LINE_MIDDLE_POINT = 3
    }

    public enum GradientPointDataSize
    {
        POINTS_POINT = 7,
    
        LINE_COLOR_POINT  = 6,
        LINE_MIDDLE_POINT = 2
    }
}