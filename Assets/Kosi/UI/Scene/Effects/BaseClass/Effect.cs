using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
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
            FrequencyRender = 5f;
            DebugEnable     = false;
			
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

        protected RenderTexture a_RenderTexture;

        public Material Material { get; protected set; }
        
        public RenderTexture Texture { get; protected set; }
        
        #endregion
        
        #region Properties

        protected UnityEngine.UI.RawImage p_UnityImage
        {
            get { return a_ImageElement.UnityImage; }
        }

        protected abstract Shader p_Shader { get; }
        
        #endregion

        private void Init()
        {
            v2i wh = a_ImageElement.WidthHeight.Floor();
            
            Material = new Material( p_Shader );
            
            a_RenderTexture = new RenderTexture( wh.width, wh.height, 16 );

            p_UnityImage.texture = a_RenderTexture;
        }
        
        protected void Render()
        {
            WriteMaterialData();
            
            v2i wh = a_ImageElement.WidthHeight.Floor().Abs();
            
            Graphic.Manager.CreateEffectTexture( Material, ref a_RenderTexture, wh );
        }

        protected virtual void WriteMaterialData() { }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            a_ImageElement = gameObject.GetComponent(typeof(Elements.Image)) as Elements.Image;
        }
		
        protected override void OnStart() {
            base.OnStart();
            
            Init();
            
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