using UnityEngine;

public abstract class v2<Tval>
{
	public abstract Tval x { get; set; }
	public abstract Tval y { get; set; }

	
	
	
	public Tval width
	{
		get { return x; }
		set { x = value; }
	}
	
	public Tval height 
	{
		get { return y; }
		set { y = value; }
	}
 
	
	
	
	public v2( Tval _x, Tval _y )
	{
		x = _x;
		y = _y;
	}
 
	
	
	
	public override string ToString()
	{
		return $"x: {x}, y: {y}";
	}
}