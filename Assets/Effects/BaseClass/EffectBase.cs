using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Effects
{
    public abstract class Effect
    {
        #region Atributes

        protected abstract Shader a_Shader { get; }

        public Material Material { get; protected set; }
        
        public RenderTexture Texture { get; protected set; }
        
        #endregion

        public static void CreateEffectTexture( Effect _effect, ref RenderTexture _texture )
        {
            
        }
    }
}