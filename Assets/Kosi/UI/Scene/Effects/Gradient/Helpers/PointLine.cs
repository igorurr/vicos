using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosi.UI.Scene.Effects.Gradient
{

	[Serializable]
	public class PointLine : PointBase
	{

		[SerializeField] private GradientPointType sfType;
		[SerializeField] private float             sfPosition;

		public PointLine (
			GradientPointType _type,
			float             _position
		)
		{
			sfType     = _type;
			sfPosition = Mathf.Clamp01( _position );
		}

		public PointLine (
			GradientPointType _type,
			float             _position,
			Color             _color
		)
		{
			sfType     = _type;
			sfPosition = Mathf.Clamp01( _position );
			sfColor    = _color;
		}

		public override List<float> ToData()
		{
			if ( sfType == GradientPointType.LINE_COLOR_POINT )
				return new List<float>
				{
					(float) sfType,                            // 0
					sfPosition,                                // 1
					sfColor.r, sfColor.g, sfColor.b, sfColor.a // 2, 3, 4, 5
				};
			else
				return new List<float>
				{
					(float) sfType, // 0
					sfPosition      // 1
				};
		}

		public override int GetSize()
		{
			if ( sfType == GradientPointType.LINE_COLOR_POINT )
				return (int) GradientPointDataSize.LINE_COLOR_POINT;
			else
				return (int) GradientPointDataSize.LINE_MIDDLE_POINT;
		}

		public override GradientPointType GetTypeGradient()
		{
			return sfType;
		}
	}
}