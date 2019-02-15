using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// порой свойство зависит от других свойств и как тогда их рендерить - хуй его знает

public static class Css
{
    public static enum CssResolverType
    {
        NUMBER__OR__CSS_OBJECT,
        NUMBER10__OR__CSS_OBJECT,   // число в диаппазоне [0;1]
        PIXELS__OR__CSS_OBJECT,
    }

    public static readonly Dictionary<string, CssResolverType> Propertys = new Dictionary<string, CssResolver>()
    {
        { "blur", PIXELS__OR__CSS_OBJECT },
        { "pixeling", PIXELS__OR__CSS_OBJECT },
        { "opacity", NUMBER10__OR__CSS_OBJECT }
    };


    // Вернуть первичное свойство по ключу и значению
    CssFirstProperty Resolve( string key, string value )
    {
        
    }

    // вернуть вторичные свойства по всем первичным
    List<CssSecondProperty> Resolve( List<CssFirstProperty> properties, List<CssFirstProperty> inheritProperties )
    {
        
    }
}