using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneElements;


public abstract class CssSecondProperty
{
    public CssValue ResolvedValue { get; protected set; }

    protected CssContainer a_Container;

    protected SceneElement a_SceneElement;

    public CssSecondProperty( 
        CssValue            _value,
        CssContainer        _container, 
        SceneElement        _sceneElement
    )
    {
        ResolvedValue   = _value;
        a_Container     = _container;
        a_SceneElement  = _sceneElement;

        RerenderSceneElement();
    }

    public void Update( CssValue _newValue )
    {
        if( !ShouldRerender( _newValue ) )
            return;

        ResolvedValue = _newValue;

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
}