namespace SceneElements
{
    public class Anchors: TRBL 
    {
        public Anchors(float _top, float _right, float _bottom, float _left)
            : base(_top, _right, _bottom, _left)
        {
        }
        
        public Anchors(float _vertical, float _horisontal)
            : base(_vertical, _horisontal, _vertical, _horisontal)
        {
        }
        
        public Anchors(float _const)
            : base(_const, _const, _const, _const)
        {
        }
    }
}
