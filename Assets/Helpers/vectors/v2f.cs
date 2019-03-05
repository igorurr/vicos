
using UnityEngine;
using UnityEngine.Analytics;

public class v2f : v2<float>
{
	private Vector2 a_Vector;
	
	public override float x
	{
		get { return a_Vector.x; }
		set { a_Vector.x = value; }
	}
	
	public override float y 
	{
		get { return a_Vector.y; }
		set { a_Vector.y = value; }
	}

	public Vector2 ToVector2()
	{
		return a_Vector;
	}

	public v2i Floor()
	{
		return new v2i( (int)x, (int)y );
	}

	public static bool operator == ( v2f a, v2f b )
	{
		return a.x.Equals( b.x ) && a.y.Equals( b.y );
	}

	public static bool operator != ( v2f a, v2f b )
	{
		return !( a == b );
	}

	public static v2f operator + ( v2f a, v2f b )
	{
		return new v2f( a.x + a.y, b.x + b.y );
	}

	public static v2f operator - ( v2f a, v2f b )
	{
		return new v2f( a.x - a.y, b.x - b.y );
	}
	
	public v2f( float _x, float _y )
		:base( _x, _y )
	{}
}