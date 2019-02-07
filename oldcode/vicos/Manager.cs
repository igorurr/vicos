

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vicos
{
    public class Manager
    {
        private static Manager Instance;
        
        private IVicos a_VicosManager;

        // это дерево описывает то что сейчас на сцене
        private DomNode a_CurState;
        
        // на это дерево состояния указывают VINode(s]
        private DomNode a_NewState;
        
        private bool    a_RenderIsRuning;
        
        private bool    a_WasChangesState;

        #region Public Methods
        
        // Запустить викос и начать рендерить
        public static void Create( IVicos vicosManager )
        {
            Instance = new Manager( vicosManager );
        }
        
        public static void Destroy()
        {
            Instance._Destroy();
        }

        public static void RenderChanges()
        {
            Instance._RenderChanges();
        }

        /*public static void WasChangesState() 
            => Instance.a_WasChangesState = true;*/

        #endregion

        #region Private Methods

        private Manager( IVicos vicosManager )
        {
            a_VicosManager = vicosManager;
            a_RenderIsRuning = false;
            
            Init();
        }
        
        private void _Destroy()
        {
            RenderStop();
            a_NewState.Remove();
        }
        
        private void Init()
        {
            a_CurState = null;
            a_NewState = new DomNode( new App() );
            //WasChangesState();
            RenderStart();
        }

        private void RenderStart()
        {
            a_RenderIsRuning = true;
        }

        private void RenderStop()
        {
            a_RenderIsRuning = false;
        }

        private void _RenderChanges()
        {
            if ( !a_RenderIsRuning || !a_WasChangesState )
                return;

            a_WasChangesState = false;
            
            a_CurState = a_NewState;
        }

        private void GetDiff()
        {
            
        }

        #endregion
    }
}