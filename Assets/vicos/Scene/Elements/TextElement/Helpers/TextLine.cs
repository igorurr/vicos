using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SceneElements.TextContainer
{
    public class TextLine : SceneElement
    {
        [SerializeField] private List<TextLineItem> a_LineItems;

        private TextLineItem a_MaxHeightChildItem;

        // a_MaxHeightChildItem, newMaxHeight 
        void FindMaxHeight()
        {
            if( a_LineItems.Count == 0 )
                NewMaxHeightZerro();

            TextLineItem maxHeightItem = null;
            
            foreach (TextLineItem item in a_LineItems)
            {
                if ( maxHeightItem == null || item.Height > maxHeightItem.Height )
                    maxHeightItem = item;
            }

            NewMaxHeight( maxHeightItem );
        }

        void NewMaxHeight( TextLineItem _item )
        {
            Height               = _item.Height;
            a_MaxHeightChildItem = _item;
        }

        void NewMaxHeightZerro()
        {
            Height               = 0;
            a_MaxHeightChildItem = null;
        }

        void CheckNewMaxHeight( TextLineItem _item, float _oldValue )
        {
            if ( 
                   a_MaxHeightChildItem == null
                || ( _item != a_MaxHeightChildItem && a_MaxHeightChildItem.Height < _item.Height )
                || ( _item == a_MaxHeightChildItem && a_MaxHeightChildItem.Height > _oldValue )
            )
            {
                NewMaxHeight( _item );
            }
            else if ( _item == a_MaxHeightChildItem && a_MaxHeightChildItem.Height < _oldValue )
            {
                FindMaxHeight();
            }
        }
        
        
    }
}