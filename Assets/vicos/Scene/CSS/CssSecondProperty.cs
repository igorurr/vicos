using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CssSecondProperty
{
    public CssValue ResolvedValue { get; protected set; }

    protected CssContainer a_Container;

    protected SceneElement a_SceneElement;

    public void CssSecondProperty( 
        List<CssProperty>   _firstInherit,
        List<CssProperty>   _firstCurrent,
        CssContainer        _container, 
        SceneElement        _sceneElement
    )
    {
        ResolvedValue   = _value;
        a_Container     = _container;
        a_SceneElement  = _sceneElement;

        Update( _firstInherit, _firstCurrent );
    }

    public void Update( 
        List<CssProperty>   _firstInherit,
        List<CssProperty>   _firstCurrent
    )
    {
        CssValue newValue = ResolveValue( _firstInherit, _firstCurrent );

        if( !ShouldRerender( newValue ) )
            return;

        ResolvedValue = newValue;

        RerenderSceneElement();
    }

    // берёт значение и меняет a_SceneElement
    public abstract void RerenderSceneElement();

    // принимает новое значение и проверяет надо ли его обновлять
    public virtual bool ShouldRerender( CssValue _newValue )
    {
        // тут будет оптимизация, проверки на простое равенство значений порой не достаточно
        return true;
    }

    // ВАЖНО: функция является простым преобразователем свойств без сложной логики
    public abstract CssValue ResolveValue(
        List<CssProperty>   _firstInherit,
        List<CssProperty>   _firstCurrent
    );
}