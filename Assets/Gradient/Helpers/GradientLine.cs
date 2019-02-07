using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class GradientLine : GradientBase<GradientLinePoint>
{
	protected GradientLine ( params GradientLinePoint[] _points )
	:base( _points )
	{
	}
}
