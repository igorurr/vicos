

using System;
using System.Collections.Generic;
using UnityEngine;

namespace vicos.unity
{
    public class Box : Component
    {
        public Box( dobj props, params Component[] childs )
            : base( props, childs )
        {}

        internal override Component[] Render()
        {

            //Debug.Log("Render Box");
            return Childs;
        }
        
       /* public override void RenderInScene()
        {
            Debug.Log("Box");
            //TODO:
        }

        protected override void OnWillMount()
        {
            Debug.Log("OnWillMount Box");
        }

        protected override void OnDidMount()
        {
            Debug.Log("OnDidMount Box");
        }

        protected override bool ShouldUpdate()
        {
            Debug.Log("ShouldUpdate Box");
            return base.ShouldUpdate();
        }

        protected override void OnUpdate()
        {
            Debug.Log("OnUpdate Box");
            base.OnUpdate();
        }

        protected override void OnUnmount()
        {
            Debug.Log("OnUnmount Box");
            base.OnUnmount();
        }*/
    }
}