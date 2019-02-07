using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Effects.Gradient
{
	public class GradientElement : MonoBehaviour
	{
		#region Debug

		[SerializeField] private bool 		sfDebug;
		[SerializeField] private float 		sfFrequencyRender;
		private 				 float 		a_TimeAfterLastRender;

		void DebugRenderInit()
		{
			sfFrequencyRender = 5;
			sfDebug = true;
			
			a_TimeAfterLastRender = 0;
		}

		void DebugRender()
		{
			if ( !sfDebug )
				return;

			a_TimeAfterLastRender += Time.deltaTime;

			if ( a_TimeAfterLastRender > 1/sfFrequencyRender )
			{
				a_TimeAfterLastRender = 0;
				Render();
			}
		}

		#endregion
		
		#region fields

		private Image a_Image;

		[SerializeField] private GradientType sfGradientType;

		[SerializeField] private GradientPoints sfGradientPoints;

		[SerializeField] private GradientRadial sfGradientRadial;

		[SerializeField] private GradientLinear sfGradientLinear;
		
		#endregion

		#region property

		private Material Material
		{
			get
			{
				return a_Image.material;
			}
			set
			{
				a_Image.material = value;
			}
		}

		#endregion


		void WriteData()
		{
			if ( sfGradientType == GradientType.LINEAR )
				sfGradientLinear.WriteData( Material );

			else if ( sfGradientType == GradientType.RADIAL )
				sfGradientRadial.WriteData( Material );

			else
				sfGradientPoints.WriteData( Material );

			Material.SetInt("_TypeGradient", (int) sfGradientType);
		}

		void Awake()
		{
			InitData();
			
			a_Image = gameObject.AddComponent(typeof(Image)) as Image;
			
			if ( sfGradientType == GradientType.LINEAR )
				Material = Resources.Load<Material>("Materials/GradientLinearMaterial");

			else if ( sfGradientType == GradientType.RADIAL )
				Material = Resources.Load<Material>("Materials/GradientRadialMaterial");
		}

		void InitData()
		{
			{
				sfGradientType = GradientType.LINEAR;

				sfGradientLinear = new GradientLinear(
					new Vector2( 0f, 0f ),
					new Vector2( 1f, 1f ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT, 0f, Color.black ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT, 0f, Color.green ),
					new GradientLinePoint( GradientPointType.LINE_MIDDLE_POINT, 0.3f ),
					new GradientLinePoint( GradientPointType.LINE_MIDDLE_POINT, 0.6f ),
					new GradientLinePoint( GradientPointType.LINE_MIDDLE_POINT, 0.9f ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  1f, Color.red ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT, 1f, Color.black )
				);

				/*sfGradientLinear = new GradientLinear(
					new Vector2( 0.2f, 0.2f ),
					new Vector2( 0.8f, 0.8f ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.2f, Color.white ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.3f, Color.red ),
					new GradientLinePoint( GradientPointType.LINE_MIDDLE_POINT, 0.6f ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.7f, Color.blue ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.8f, Color.black )
				);*/

				/*sfGradientLinear = new GradientLinear(
					new Vector2( 0.2f, 0.2f ),
					new Vector2( 0.8f, 0.8f ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.2f, Color.red ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.4f, Color.yellow ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.6f, Color.cyan ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0.8f, Color.blue )
				);*/
			}

			{
				/*sfGradientType = GradientType.RADIAL;

				sfGradientRadial = new GradientRadial(
					new Vector2( 0.5f, 0.5f ),
					new Vector2( 0.5f, 0.5f ),
					0.4f,
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0,    Color.black ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  0,    Color.red ),
					new GradientLinePoint( GradientPointType.LINE_COLOR_POINT,  1f,   Color.white )
				);*/

			}

			{

			}
		}
		
		void Start(){
			Render();

			DebugRenderInit();
		}

		void Update()
		{
			DebugRender();
		}

		void Render()
		{
			WriteData();
		}

		void CreateFromCss()
		{

		}
	}
}