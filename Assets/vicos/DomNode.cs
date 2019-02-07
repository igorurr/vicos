using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace vicos
{
    public abstract class DomNode : MonoBehaviour
    {
        [SerializeField] Component         Component;
        
        [SerializeField] VirtualDomNode    CurState;
        VirtualDomNode                     NewState;
        
        internal static void Create( Transform _parent )
        {
            //_parent.
        }
        
        internal void Update()
        {
        }

        internal void RemoveAllChilds()
        {
            
        }
    }
}