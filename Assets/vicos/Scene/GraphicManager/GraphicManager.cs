using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scene.Graphic
{
    public class Manager : MonoBehaviour
    {
        #region Instance
        
        private static Manager Instance;

        void CreateInstance()
        {
            Instance = this;
        }
        
        #endregion
        
        #region Atributes
        
        #endregion

        void Awake()
        {
            CreateInstance();
        }

        public static void CreateEffectTexture(
            Material             _material,
            ref RenderTexture    _texture,
            int                  _width,
            int                  _height
        )
        {
            // по дефолту текстура закрашена хз во что
            Texture input = new Texture2D( _width, _height, TextureFormat.ARGB32, false );
            
            Graphics.Blit( input, _texture, _material );
        }
    }
}