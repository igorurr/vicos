using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kosi.UI.Scene.Effects.Gradient
{
	[Serializable]
	public abstract class Line : Base<PointLine>
	{
		protected Line ( params PointLine[] _points )
			: base( _points )
		{
		}
	}
}