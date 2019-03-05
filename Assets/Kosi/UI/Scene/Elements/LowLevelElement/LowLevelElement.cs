using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// new Vector4( x, y, z, w );

namespace Kosi.UI.Scene.Elements
{
	public class LowLevelElement : StereoBehaviour 
	{
		#region Atributes

		#endregion
		
		#region Propertys

		public v2f AnchorsWidthHeight
		{
			get
			{
				v2f parentWH = Parent.WidthHeight;
				
				return new v2f( 
					parentWH.x  * ( 1 - Anchors.Left - Anchors.Right  ),
					parentWH.y  * ( 1 - Anchors.Top  - Anchors.Bottom )
				);
			}
		}

		public v4f Anchors
		{
			get
			{
				Vector2 anchorMax = RectTransform.anchorMax;
				Vector2 anchorMin = RectTransform.anchorMin;
				
				return new v4f( 1-anchorMax.y, 1-anchorMax.x, anchorMin.y, anchorMin.x );
			}
			set
			{
				v2f oldWH = WidthHeight;
				
				RectTransform.anchorMax = new Vector2( 1-value.Right,   1-value.Top );
				RectTransform.anchorMin = new Vector2( value.Left,      value.Bottom );

				WidthHeight = oldWH;
			}
		}

		public v4f Margin
		{
			get
			{
				Vector2 omax = RectTransform.offsetMax;
				Vector2 omin = RectTransform.offsetMin;
				
				return new v4f( -omax.y, -omax.x, omin.y, omin.x );
			}
			set
			{
				RectTransform.offsetMax = new Vector2( -value.Right,   -value.Top   );
				RectTransform.offsetMin = new Vector2( value.Left,     value.Bottom );
			}
		}
		
		protected LowLevelElement Parent { get; private set; }

		protected v2f Position
		{
			get { return RectTransform.localPosition.Tov2f(); }
			set { RectTransform.localPosition = value.ToVector2(); }
		}

		protected RectTransform RectTransform { get; private set; }

		protected float Rotation
		{
			get { return RectTransform.localRotation.eulerAngles.z; }
			set { RectTransform.localRotation = Quaternion.Euler(0,0,value); }
		}

		public v2f WidthHeight
		{
			get
			{
				Rect rtr = RectTransform.rect;
				
				return new v2f( rtr.width, rtr.height );
			}
			set
			{
				RectTransform.sizeDelta = ( value - AnchorsWidthHeight ).ToVector2();
			}
		}

		#endregion
		
		#region Events

		public event Action<v2f> OnResize;

		#endregion
		
		#region Public Methods

		public static Element Create( Element _parent = null, string _name = "" )
		{
			Element newElement = Create(
				_parent == null ? null : _parent.transform.parent, 
				_name
			);
			newElement.Parent = newElement.transform.parent.GetComponent<Element>();
			
			return newElement;
		}

		public static Element Create( Transform _parent = null, string _name = "" )
		{
			string name 		 = _name == "" ? "scene element" : _name;
			GameObject newObject = new GameObject(name, typeof(RectTransform));
			
			if( _parent != null )
				newObject.transform.SetParent( _parent );
			
			Element newElement 			= newObject.AddComponent(typeof(Element)) as Element;
			
			newElement.RectTransform 	= newObject.GetComponent<RectTransform>();
			newElement.Parent 			= newObject.transform.parent.GetComponent<LowLevelElement>();

			if ( newElement.Parent != null )
			{
				newElement.RectTransform.localPosition  = newElement.Parent.RectTransform.localPosition;
				newElement.RectTransform.rotation 		= newElement.Parent.RectTransform.rotation;
				newElement.RectTransform.localScale 	= newElement.Parent.RectTransform.localScale;
			}
			else
			{
				newElement.RectTransform.localPosition  = Vector3.zero;
				newElement.RectTransform.rotation 		= Quaternion.identity;
				newElement.RectTransform.localScale 	= Vector3.one;
			}
			
			newElement.Anchors     = new v4f( 0, 1, 1, 0 );
			newElement.WidthHeight = new v2f( 0, 0 );

			return newElement;
		}
		
		public static bool operator == ( LowLevelElement a, LowLevelElement b )
		{
			if ( (object) a == null && (object) b == null )
				return true;
			
			if ( (object) a == null || (object) b == null )
				return false;
			
			return a.transform == b.transform;
		}
		
		public static bool operator != ( LowLevelElement a, LowLevelElement b )
		{
			return !( a == b );
		}

		// Update уже занято Unity
		public void Refresh()
		{
			
		}

		// Destroy уже занято Unity
		public void Delete()
		{
			Destroy( gameObject );
		}
		
		#endregion
		
		#region Protected Methods

		protected virtual void DoResize( v2f _oldSize ) { }
		
		#endregion
		
		#region Private Methods

		protected override void OnAwake()
		{
			base.OnAwake();
			
			Init();
		}

		protected override void OnStart()
		{
			base.OnUpdate();
			
			InitCheckUpdate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			
			CheckUpdate();
		}

		protected virtual void Init()
		{
			if( RectTransform == null )
				RectTransform = GetComponent<RectTransform>();
			
			if( Parent == null )
				Parent = GetComponent<LowLevelElement>();
		}

		#endregion
		
		#region Check Update

		private v2f oldWH;

		void InitCheckUpdate()
		{
			oldWH = WidthHeight;
		}

		// так как размеры элемента могут измениться из-за ресайза родителя, надо тречить,
		// проверки одних текущих свойств недостаточно
		void CheckUpdate()
		{
			if ( WidthHeight != oldWH )
			{
				OnResize?.Invoke( oldWH );
				DoResize( oldWH );
			}
			
			oldWH = WidthHeight;
		}

		#endregion
	}
}
