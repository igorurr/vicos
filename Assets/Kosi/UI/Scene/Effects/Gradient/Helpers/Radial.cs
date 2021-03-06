﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosi.UI.Scene.Effects.Gradient
{
	[Serializable]
	public class Radial : Line
	{
		// x, y
		[SerializeField] private Vector2 sfRealOffset;

		// позиция центра
		[SerializeField] private Vector2 sfPosition;

		// позиция крайней точки
		// r, alpha
		[SerializeField] private Vector2 sfOffset;

		// двумерный радиус
		[SerializeField] private Vector2 sfRadius;

		public Radial (
			Vector2                    _position,
			Vector2                    _offset,
			Vector2                    _radius,
			params PointLine[] _points
		)
			: base( _points )
		{
			sfPosition   = _position;
			sfRadius     = _radius;
		}

		public override void WriteData( Material _material )
		{
			_material.SetFloatArray( "_GradientParams", new []
			{
				// подробные описания всех обозначений смотреть в документации
				// цифрами указаны позиции в массиве _GradientParams в шейдере
				sfPosition.x, 	sfPosition.y, // 0, 1 : точка b (bx, by)
				sfRealOffset.x, sfRealOffset.y, // 2, 3 : точка c (cx, cy)
				sfRadius.x,		sfRadius.y,   // 4, 5 : радиус r (ra, rb)
			} );

			base.WriteData( _material );
		}
	}
}