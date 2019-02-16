using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// new Vector4( x, y, z, w );

namespace SceneElements
{
	public class SceneElement : StereoBehaviour 
	{
		#region Atributes

		#endregion
		
		#region Propertys

		[SerializeField] public virtual Vector2 AnchorsWidthHeight
		{
			get
			{
				Vector2 parentWH = Parent.WidthHeight;
				
				return new Vector2( 
					parentWH.x  * ( 1 - Anchors.Left - Anchors.Right  ),
					parentWH.y  * ( 1 - Anchors.Top  - Anchors.Bottom )
				);
			}
		}

		// css: Top Right Bottom Left
		[SerializeField] public virtual Anchors Anchors
		{
			get
			{
				Vector2 anchorMax = RectTransform.anchorMax;
				Vector2 anchorMin = RectTransform.anchorMin;
				
				return new Anchors( 1-anchorMax.y, 1-anchorMax.x, anchorMin.y, anchorMin.x );
			}
			set
			{
				Vector2 oldWH = WidthHeight;
				
				RectTransform.anchorMax = new Vector2( 1-value.Right,   1-value.Top );
				RectTransform.anchorMin = new Vector2( value.Left,      value.Bottom );

				WidthHeight = oldWH;
			}
		}

		[SerializeField] public virtual Margin Margin
		{
			get
			{
				Vector2 omax = RectTransform.offsetMax;
				Vector2 omin = RectTransform.offsetMin;
				
				return new Margin( -omax.y, -omax.x, omin.y, omin.x );
			}
			set
			{
				RectTransform.offsetMax = new Vector2( -value.Right,   -value.Top   );
				RectTransform.offsetMin = new Vector2( value.Left,     value.Bottom );
			}
		}
		
		protected SceneElement Parent { get; private set; }

		[SerializeField] protected virtual Vector2 Position
		{
			get { return RectTransform.localPosition; }
			set { RectTransform.localPosition = value; }
		}

		protected RectTransform RectTransform { get; private set; }

		[SerializeField] protected virtual float Rotation
		{
			get { return RectTransform.localRotation.eulerAngles.z; }
			set { RectTransform.localRotation = Quaternion.Euler(0,0,value); }
		}

		[SerializeField] public virtual Vector2 WidthHeight
		{
			get
			{
				Rect rtr = RectTransform.rect;
				
				return new Vector2( rtr.width, rtr.height );
			}
			set
			{
				RectTransform.sizeDelta = value - AnchorsWidthHeight;
			}
		}
		
		[SerializeField] public virtual float Width
		{
			get
			{
				return WidthHeight.x;
			}
			set
			{
				Vector2 cur = WidthHeight;
				cur.x       = value;
				WidthHeight = cur;
			}
		}
		
		[SerializeField] public virtual float Height
		{
			get
			{
				return WidthHeight.y;
			}
			set
			{
				Vector2 cur = WidthHeight;
				cur.y       = value;
				WidthHeight = cur;
			}
		}
		
		public float MarginTop
		{
			get
			{
				return Margin.Top;
			}
			set
			{
				Margin cur = Margin;
				cur.Top = value;
				Margin  = cur;
			}
		}
		
		public float MarginRight
		{
			get
			{
				return Margin.Right;
			}
			set
			{
				Margin cur = Margin;
				cur.Right = value;
				Margin    = cur;
			}
		}
		
		public float MarginBottom
		{
			get
			{
				return Margin.Bottom;
			}
			set
			{
				Margin cur = Margin;
				cur.Bottom = value;
				Margin     = cur;
			}
		}
		
		public float MarginLeft
		{
			get
			{
				return Margin.Left;
			}
			set
			{
				Margin cur = Margin;
				cur.Left = value;
				Margin   = cur;
			}
		}

		#endregion
		
		#region Public Methods

		public static SceneElement Create( SceneElement _parent = null, string _name = "" )
		{
			SceneElement newSceneElement = Create(
				_parent == null ? null : _parent.transform.parent, 
				_name
			);
			newSceneElement.Parent = newSceneElement.transform.parent.GetComponent<SceneElement>();
			
			return newSceneElement;
		}

		public static SceneElement Create( Transform _parent = null, string _name = "" )
		{
			string name = _name == "" ? "scene element" : _name;
			
			GameObject newObject = new GameObject(name, typeof(RectTransform));
			
			if( _parent != null )
				newObject.transform.SetParent( _parent );
			
			SceneElement newSceneElement = newObject.AddComponent(typeof(SceneElement)) as SceneElement;
			
			newSceneElement.Anchors = new Anchors( 0, 1, 1, 0 );
			newSceneElement.WidthHeight = new Vector2( 0, 0 );

			newSceneElement.Parent = null;

			return newSceneElement;
		}
		
		public static bool operator == ( SceneElement a, SceneElement b )
		{
			return a.transform == b.transform;
		}
		
		public static bool operator != ( SceneElement a, SceneElement b )
		{
			return a.transform != b.transform;
		}

		// Update уже занято Unity
		public void Refresh()
		{
			
		}

		// Destroy уже занято Unity
		public void Delete()
		{
			Destroy();
		}
		
		#endregion
		
		#region Service Methods

		/*protected virtual void Initialize()
		{ }

		void InitializeElement()
		{
			RectTransform = GetComponent<RectTransform>();
			
			/*Vector3 curpos = RectTransform.localPosition;
			RectTransform.localPosition = new Vector3( curpos.x, curpos.y, 0 );
			
			RectTransform.localScale = Vector3.one;
		}*/

		#endregion
	}
}
