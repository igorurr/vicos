using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GradientRadial : GradientLine {

	// позиция центра
	[SerializeField] private Vector2 sfPosition;
	
	// позиция крайней точки
	[SerializeField] private Vector2 sfOffset;
	[SerializeField] private Vector2 sfFakeOffset;
	
	// двумерный радиус
	[SerializeField] private float sfRadius;
	
	public GradientRadial (
		Vector2                     _position,
		Vector2                     _offset,
		float                       _radius,
		params GradientLinePoint[] _points
	)
		:base( _points )
	{
		sfPosition = _position;
		sfFakeOffset = _offset.Clamp01();
		sfRadius = _radius;
	}

	public override void WriteData( Material _material )
	{
		_material.SetFloatArray( "_GradientParams", new []
		{
			// подробные описания всех обозначений смотреть в документации
			// цифрами указаны позиции в массиве _GradientParams в шейдере
			sfPosition.x, sfPosition.y, // 0, 1 : точка b (bx, by)
			sfFakeOffset.x, sfFakeOffset.y,     // 2, 3 : точка c (cx, cy)
			sfRadius,     				// 4    : радиус, переделать на элипсоидный
		} );
		
		base.WriteData( _material );
	}
}
