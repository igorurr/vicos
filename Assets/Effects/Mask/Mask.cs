using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Effects.Mask
{
	public class MaskElement : MonoBehaviour
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
			/*sfGradientPoints.WriteData( Material );

			Material.SetInt("_TypeGradient", (int) sfGradientType);*/
		}

		void Awake()
		{
			//a_Image = gameObject.AddComponent(typeof(Image)) as Image;
			a_Image = GetComponent<Image>();
			
			Material = Instantiate( Resources.Load<Material>("Materials/MaskMaterial") );
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