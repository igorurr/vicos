using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosi.UI.Scene.Effects.Gradient
{
	public abstract class PointBase
	{

		[SerializeField] protected Color sfColor;

		public abstract List<float> ToData();

		// сколько ячеек массива будет занимать данная точка
		public abstract int GetSize();

		public abstract GradientPointType GetTypeGradient();
	}
}