using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kosi.UI.Scene.Effects.Gradient
{
	[Serializable]
	public class Points : Base<PointPoints>
	{
		public Points ( params PointPoints[] _points )
			: base( _points )
		{
		}
	}
}