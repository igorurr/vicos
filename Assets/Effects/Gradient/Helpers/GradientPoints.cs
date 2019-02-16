using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GradientPoints : GradientBase<GradientPointsPoint>
{
	public GradientPoints ( params GradientPointsPoint[] _points )
		:base( _points )
	{
	}
}