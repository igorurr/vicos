
using UnityEngine;

public class v4f : v4<float>
{
	private Vector4 a_Vector;
	
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
	
	public override float z
	{
		get { return a_Vector.z; }
		set { a_Vector.z = value; }
	}
	
	public override float w
	{
		get { return a_Vector.w; }
		set { a_Vector.w = value; }
	}
	
	public v4f( float _x, float _y, float _z, float _w )
		:base( _x, _y, _z, _w )
	{}
}