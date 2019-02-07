namespace SceneElements
{
    public class Margin: TRBL 
    {
        public Margin(float _top, float _right, float _bottom, float _left)
            : base(_top, _right, _bottom, _left)
        {
        }
        
        public Margin(float _vertical, float _horisontal)
            : base(_vertical, _horisontal, _vertical, _horisontal)
        {
        }
        
        public Margin(float _const)
            : base(_const, _const, _const, _const)
        {
        }
    }
}
