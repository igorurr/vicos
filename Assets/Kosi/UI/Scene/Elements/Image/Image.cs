using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// new Vector4( x, y, z, w );

namespace Kosi.UI.Scene.Elements
{
	public class Image : LowLevelElement 
	{
		#region Atributes
		
		public UnityEngine.UI.RawImage UnityImage { get; private set; }

		#endregion
		
		#region Propertys

		public Material Material
		{
			get { return UnityImage.material; }
		}

		#endregion
		
		#region Public Methods

		protected override void OnAwake()
		{
			base.OnAwake();

			UnityImage = gameObject.AddComponent<UnityEngine.UI.RawImage>();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			//this.WidthHeight += new v2f( 1, 0 );
		}

		#endregion
	}
}
