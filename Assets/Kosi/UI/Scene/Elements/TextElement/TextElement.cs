using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosi.UI.Scene.Elements.TextElement
{
	public class TextElement : Element
	{
		[SerializeField] private string a_Text;
		[SerializeField] private Font a_Font;
		[SerializeField] private float a_Size;
		[SerializeField] private FontStyle a_FontStyle;
		[SerializeField] private float a_LineSpacing;
		//[SerializeField] private ShaderMaterial a_ShaderMaterial;
		
		[SerializeField] private Text a_UnityUITextComponent;
		
		public string Text
		{
			get
			{
				return a_Text;
			}
			set
			{
				a_Text = value;
			}
		}
		
		public Font Font
		{
			get
			{
				return a_Font;
			}
			set
			{
				a_Font = value;
			}
		}
		
		public float Size
		{
			get
			{
				return a_Size;
			}
			set
			{
				a_Size = value;
			}
		}
		
		public FontStyle FontStyle
		{
			get
			{
				return a_FontStyle;
			}
			set
			{
				a_FontStyle = value;
			}
		}
		
		public float LineSpacing
		{
			get
			{
				return a_LineSpacing;
			}
			set
			{
				a_LineSpacing = value;
			}
		}

		/*public ShaderMaterial ShaderMaterial
		{
			get
			{
				return a_ShaderMaterial;
			}
			set
			{
				a_ShaderMaterial = value;
			}
		}*/

		public static TextElement Create(  
			string    		_Text,
			Font      		_Font,
			float     		_Size,
			FontStyle		_FontStyle,
			float     		_LineSpacing,
			//ShaderMaterial  _ShaderMaterial,
			TextContainer   _parent = null
		)
		{
			TextElement newTextElement = Element.Create( _parent ) as TextElement;
			
			newTextElement.a_Text = 			_Text;
			newTextElement.a_Font = 			_Font;
			newTextElement.a_Size = 			_Size;
			newTextElement.a_FontStyle = 		_FontStyle;
			newTextElement.a_LineSpacing = 		_LineSpacing;
			//newTextElement.a_ShaderMaterial =   _ShaderMaterial;
			
			newTextElement.a_UnityUITextComponent = newTextElement.gameObject.AddComponent(typeof(Text)) as Text;
			
			return newTextElement;
		}
	}
}