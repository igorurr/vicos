using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kosi.UI.Scene.Effects
{
    public abstract class Effect : StereoBehaviour
    {
        #region Debug

        [FormerlySerializedAs("sfDebug")] [SerializeField] private bool  DebugEnable;
        [FormerlySerializedAs("sfFrequencyRender")] [SerializeField] private float FrequencyRender;
        private                  float a_TimeAfterLastRender;

        #if DEV
        void DebugRenderInit()
        {
            FrequencyRender = 5;
            DebugEnable     = true;
			
            a_TimeAfterLastRender = 0;
        }

        void DebugRender()
        {
            if ( !DebugEnable )
                return;

            a_TimeAfterLastRender += Time.deltaTime;

            if ( a_TimeAfterLastRender > 1/FrequencyRender )
            {
                a_TimeAfterLastRender = 0;
                Render();
            }
        }
        #endif

        #endregion
        
        #region Atributes

        protected Elements.Image a_ImageElement;

        public Material Material { get; protected set; }
        
        public RenderTexture Texture { get; protected set; }
        
        #endregion
        
        #region Properties

        protected UnityEngine.UI.Image p_UnityImage
        {
            get { return a_ImageElement.UnityImage; }
        }

        protected abstract Shader p_Shader { get; }
        
        #endregion

        protected override void OnAwake()
        {
            base.OnAwake();
            
            a_ImageElement = gameObject.GetComponent(typeof(Elements.Image)) as Elements.Image;
        }
        
        protected void Render()
        {
            v2i wh        = a_ImageElement.WidthHeight.Floor();
            Rect rect     = new Rect( 0, 0, wh.width, wh.height );
            Vector2 pivot = new Vector2( 0.5f, 0.5f );
            
            RenderTexture rt = new RenderTexture( wh.width, wh.height, 16 );
            
            Material = new Material( p_Shader );
            WriteMaterialData();
            
            Graphic.Manager.CreateEffectTexture( Material, ref rt, wh );
            
            Texture2D tex = new Texture2D( wh.width, wh.height );
            
            // видимо ReadPixels считывает из RenderTexture.active
            RenderTexture.active = rt;
            tex.ReadPixels( rect, 0, 0 );
            tex.Apply();
            RenderTexture.active = null;

            Sprite sprite = Sprite.Create( tex, rect, pivot );
            p_UnityImage.sprite = sprite;
        }

        protected virtual void WriteMaterialData() { }
		
        protected override void OnStart() {
            base.OnStart();
            
            Render();

            #if DEV
            DebugRenderInit();
            #endif

            a_ImageElement.OnResize += ImageElement_OnResize;
        }

        protected override void OnUpdate() 
        {
            base.OnUpdate();

            #if DEV
            DebugRender();
            #endif
        }

        protected override void DoDestroy() 
        {
            base.DoDestroy();
            
            a_ImageElement.OnResize -= ImageElement_OnResize;
        }

        private void ImageElement_OnResize( v2f _oldSize )
        {
            Render();
        }
    }
}