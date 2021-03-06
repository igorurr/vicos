﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosi.UI.Scene.Elements.TextElement
{
	public class TextContainer : Element
	{
		[SerializeField] private List<TextLine> 	a_Lines;
		[SerializeField] private List<TextElement>  a_Elements;

		public void ResizeList( TextLine _startLine )
		{
			bool wasFound = false;
			float marginFromStart = 0;
			
			foreach (var line in a_Lines)
			{
				if ( !wasFound )
				{
					if ( line == _startLine )
						wasFound = true;
					
					marginFromStart += line.Height;
					continue;
				}

				//line.MarginTop = marginFromStart;
						
				marginFromStart += line.Height;
			}
		}
		
		public static TextContainer Create( 
			Element   		_parent,
			List<TextElement>	_elements
		)
		{
			TextContainer ret = Element.Create( _parent ) as TextContainer;
			
			/*ret.a_Text           = _Text;
			ret.a_Font           = _Font;
			ret.a_FontStyle      = _FontStyle;
			ret.a_LineSpacing    = _LineSpacing;
			ret.a_ShaderMaterial = _ShaderMaterial;*/

			return ret;
		}
		
	}
}