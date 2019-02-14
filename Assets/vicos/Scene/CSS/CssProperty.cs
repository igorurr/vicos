using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CssProperty
{
    public string Value { get; protected set; }

    public CssValue ResolvedValue { get; protected set; }

    protected CssContainer a_ContainerProperties;

    public event System.Action OnUpdate;

    public void CssProperty( string _value, SceneElement _sceneElement )
    {
        a_SceneElement = _sceneElement;
        Update( _value );
    }

    public void Update( 
        List<CssProperty>   _inherit,
        List<CssProperty>   _current,
        SceneElement        _sceneElement,
        string              _value
    )
    {
        if( !ShouldRerender( _inherit, _current, _sceneElement, _value ) )
            return;

        ResolvedValue = ResolveValue( _value );
        Value = _value;

        RerenderSceneElement( _inherit, _current, _sceneElement );

        OnUpdate?.Invoke();
    }

    public abstract void RerenderSceneElement(
        List<CssProperty>   _inherit,
        List<CssProperty>   _current,
        SceneElement        _sceneElement
    );

    public virtual bool ShouldRerender(
        List<CssProperty>   _inherit,
        List<CssProperty>   _current,
        SceneElement        _sceneElement,
        string              _value
    )
    {
        // тут будет оптимизация, проверки на простое равенство значений порой не достаточно
        return true;
    }

    public abstract CssValue ResolveValue( string _value );
}