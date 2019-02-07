

using System;
using System.Collections.Generic;
using UnityEngine;

namespace vicos.unity
{
    public class Text : Component
    {
        public Text( dobj props, string child )
            : base( props )
        {
            //TODO: шото тут надо сделать чтобы текст инициализировать
        }

        internal override Component[] Render()
        {

            //Debug.Log("Render Text");
            return null;
        }
        
        /*public override void RenderInScene()
        {
            Debug.Log("Text");
            //TODO:
        }

        protected override void OnWillMount()
        {
            Debug.Log("OnWillMount Text");
        }

        protected override void OnDidMount()
        {
            Debug.Log("OnDidMount Text");
        }

        protected override bool ShouldUpdate()
        {
            Debug.Log("ShouldUpdate Text");
            return base.ShouldUpdate();
        }

        protected override void OnUpdate()
        {
            Debug.Log("OnUpdate Text");
            base.OnUpdate();
        }

        protected override void OnUnmount()
        {
            Debug.Log("OnUnmount Text");
            base.OnUnmount();
        }*/
    }
}