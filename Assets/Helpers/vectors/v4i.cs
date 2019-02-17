
using UnityEngine;

public class v4i : v4<int>
{
	// такого пока не завезли
	//private Vector4Int a_Vector;

	private int a_X;
	private int a_Y;
	private int a_Z;
	private int a_W;
	
	public override int x
	{
		get { return a_X; }
		set { a_X = value; }
	}
	
	public override int y 
	{
		get { return a_Y; }
		set { a_Y = value; }
	}
	
	public override int z
	{
		get { return a_Z; }
		set { a_Z = value; }
	}
	
	public override int w
	{
		get { return a_W; }
		set { a_W = value; }
	}
	
	public v4i( int _x, int _y, int _z, int _w )
		:base( _x, _y, _z, _w )
	{}
}