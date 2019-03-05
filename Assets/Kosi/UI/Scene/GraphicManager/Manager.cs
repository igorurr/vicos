using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Kosi.UI.Scene.Graphic
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
            Material          _material,
            ref RenderTexture _texture,
            v2i               _wh
        )
        {
            Instance._CreateEffectTexture( _material, ref _texture, _wh );
        }

        private void _CreateEffectTexture(
            Material          _material,
            ref RenderTexture _texture,
            v2i               _wh
        )
        {
            // по дефолту текстура закрашена хз во что
            Texture input = new Texture2D( _wh.width, _wh.height, TextureFormat.ARGB32, false );
            
            Graphics.Blit( input, _texture, _material );
        }
    }
}