using UnityEngine;

public abstract class v4<Tval>
{
	public abstract Tval x { get; set; }
	public abstract Tval y { get; set; }
	public abstract Tval z { get; set; }
	public abstract Tval w { get; set; }

	
	
	
	public Tval r
	{
		get { return x; }
		set { x = value; }
	}
	
	public Tval g
	{
		get { return y; }
		set { y = value; }
	}
	
	public Tval b
	{
		get { return z; }
		set { z = value; }
	}
	
	public Tval a
	{
		get { return w; }
		set { w = value; }
	}

	
	
	
	public Tval Top
	{
		get { return x; }
		set { x = value; }
	}
	
	public Tval Right 
	{
		get { return y; }
		set { y = value; }
	}
	
	public Tval Bottom
	{
		get { return z; }
		set { z = value; }
	}
	
	public Tval Left
	{
		get { return w; }
		set { w = value; }
	}
 
	
	
	
 	public v4( Tval _x, Tval _y, Tval _z, Tval _w )
 	{
 		x = _x;
 		y = _y;
 		z = _z;
 		w = _w;
 	}
 
	
	
	
 	public override string ToString()
 	{
 		return $"x: {x}, y: {y}, z: {z}, w: {w}";
 	}
 }