using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Effects.Gradient
{
	[Serializable]
	public abstract class GradientLine : GradientBase<GradientLinePoint>
	{
		protected GradientLine ( params GradientLinePoint[] _points )
			: base( _points )
		{
		}
	}
}