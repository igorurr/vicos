using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CssFirstProperty
{
    public bool Inherited { get; }

    public string Value { get; protected set; }

    public CssValue ResolvedValue { get; protected set; }

    protected CssContainer a_Container;

    // предназначен только для обновления зависимых свойств
    public event System.Action OnUpdate;

    public CssFirstProperty( string _value, CssContainer _container )
    {
        a_Container = _container;
        Update( _value );
    }

    public void Update( string _value )
    {
        if( !ResolveValue( _value ) )
            return;

        Value = _value;

        OnUpdate?.Invoke();
    }

    // если во свойство некорректно - вернуть false
    public abstract bool ResolveValue( string _value );
}