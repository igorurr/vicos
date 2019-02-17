using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Effects.Gradient
{
    [Serializable]
    public abstract class GradientBase<T>
        where T : GradientPointBase
    {
        [SerializeField] protected List<T> sfPoints;

        public GradientBase ( params T[] _points )
        {
            sfPoints = _points.ToList();
        }

        public virtual void WriteData( Material _material )
        {
            _material.SetInt( "_GradientCountPoints", sfPoints.Count );

            List<float> gradientPoints          = new List<float>();
            List<float> gradientPointsPositions = new List<float>();
            List<float> gradientTypePoints      = new List<float>();

            if ( sfPoints.Count > 0 )
            {
                int curPos = 0;

                foreach ( T point in sfPoints )
                {
                    gradientPoints = gradientPoints.Merge( point.ToData() );

                    gradientPointsPositions.Add( curPos );
                    curPos += point.GetSize();

                    gradientTypePoints.Add( (float) point.GetTypeGradient() );
                }
            }

            _material.SetFloatArray( "_GradientPoints",          gradientPoints.ToArray() );
            _material.SetFloatArray( "_GradientPointsPositions", gradientPointsPositions.ToArray() );
            _material.SetFloatArray( "_GradientTypePoints",      gradientTypePoints.ToArray() );
        }

        public virtual void WriteData2( Material _material )
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();

            properties.SetInt( "_GradientCountPoints", sfPoints.Count );

            List<float> gradientPoints          = new List<float>();
            List<float> gradientPointsPositions = new List<float>();
            List<float> gradientTypePoints      = new List<float>();

            if ( sfPoints.Count > 0 )
            {
                int curPos = 0;

                foreach ( T point in sfPoints )
                {
                    gradientPoints = gradientPoints.Merge( point.ToData() );

                    gradientPointsPositions.Add( curPos );
                    curPos += point.GetSize();

                    gradientTypePoints.Add( (float) point.GetTypeGradient() );
                }
            }

            properties.SetFloatArray( "_GradientPoints",          gradientPoints.ToArray() );
            properties.SetFloatArray( "_GradientPointsPositions", gradientPointsPositions.ToArray() );
            properties.SetFloatArray( "_GradientTypePoints",      gradientTypePoints.ToArray() );

            //_material.re
            //_material.SetP
            //_material.SetFloatArray( "_GradientTypePoints", gradientTypePoints.ToArray() );
        }
    }
}