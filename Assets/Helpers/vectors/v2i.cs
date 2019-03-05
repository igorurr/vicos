
using System;
using UnityEngine;

public class v2i : v2<int>
{
	private Vector2Int a_Vector;
	
	public override int x
	{
		get { return a_Vector.x; }
		set { a_Vector.x = value; }
	}
	
	public override int y 
	{
		get { return a_Vector.y; }
		set { a_Vector.y = value; }
	}

	public v2i Abs()
	{
		return new v2i( Mathf.Abs(x), Mathf.Abs(y) );
	}
	
	public v2i( int _x, int _y )
		:base( _x, _y )
	{}
}