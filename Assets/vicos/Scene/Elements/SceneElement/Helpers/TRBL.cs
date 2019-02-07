namespace SceneElements
{
	public class TRBL 
	{
		public float Top    { get; set; }
		public float Right  { get; set; }
		public float Bottom { get; set; }
		public float Left   { get; set; }

		public TRBL( float _top, float _right, float _bottom, float _left )
		{
			Top    = _top;
			Right  = _right;
			Bottom = _bottom;
			Left   = _left;
		}

		public override string ToString()
		{
			return $"Top: {Top}, Right: {Right}, Bottom: {Bottom}, Left: {Left}";
		}
	}
}
