using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// порой свойство зависит от других свойств и как тогда их рендерить - хуй его знает

namespace Kosi.UI.Scene.CSS
{
    public static class Css
    {
        private enum ResolverType
        {
            NUMBER__OR__CSS_OBJECT,
            NUMBER10__OR__CSS_OBJECT, // число в диаппазоне [0;1]
            PIXELS__OR__CSS_OBJECT,
        }

        private static readonly Dictionary<string, ResolverType> Propertys = new Dictionary<string, ResolverType>()
        {
            { "blur",         ResolverType.PIXELS__OR__CSS_OBJECT },
            { "pixeling",     ResolverType.PIXELS__OR__CSS_OBJECT },
            { "opacity",      ResolverType.NUMBER10__OR__CSS_OBJECT }
        };


        // Вернуть первичное свойство по ключу и значению
        public static CssFirstProperty Resolve( string key, string value )
        {
            return null;
        }

        // вернуть вторичные свойства по всем первичным
        public static List<CssSecondProperty> Resolve( List<CssFirstProperty> properties,
                                                       List<CssFirstProperty> inheritProperties )
        {
            return null;
        }
    }
}