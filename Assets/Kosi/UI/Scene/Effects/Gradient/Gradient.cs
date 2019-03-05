using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kosi.UI.Scene.Elements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kosi.UI.Scene.Effects.Gradient
{
	public class Gradient : Effect
	{
		#region fields

		[SerializeField] private GradientType GradientType;

		[SerializeField] private Points GradientPoints;

		[SerializeField] private Radial GradientRadial;

		[SerializeField] private Linear GradientLinear;
		
		#endregion

		#region property

		protected override Shader p_Shader
		{
			get
			{
				if ( GradientType == GradientType.LINEAR )
					return Resources.Load<Shader>("Kosi.UI.Scene.Effects.Gradient/Shaders/Linear");

				if ( GradientType == GradientType.RADIAL )
					return Resources.Load<Shader>("Kosi.UI.Scene.Effects.Gradient/Shaders/Radial");

				return Resources.Load<Shader>("Kosi.UI.Scene.Effects.Gradient/Shaders/Points");
			}
		}

		#endregion


		protected override void WriteMaterialData()
		{
			if ( GradientType == GradientType.LINEAR )
				GradientLinear.WriteData( Material );

			else if ( GradientType == GradientType.RADIAL )
				GradientRadial.WriteData( Material );

			else
				GradientPoints.WriteData( Material );
		}
	}
}