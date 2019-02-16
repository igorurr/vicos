using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneElements;

/*
    свойства первичны и вторичны, первичные свойства могут зависеть друг от друга, вторичные не могут, первичные свойства могут наследоваться, вторичные не могут
    вторичные свойства - то что применяется к каждому компоненту за которым завязан контейнер, первичные свойства задаёт пользователь
    вторичные свойства определяются из первичных посредством обработки всех первичных (текущих и наследуемых) свойств компонента

    не всегда надо наследовать свойства от своих родителей, как например *тут должен быть пример* в css,
    хотя на данном этапе разработки это заигнорено, как только такая необходимость встанет - надо будет реализовать проверку надо ли наследовать и где её делать - хз

    наследуем свойства только от прямого потомка
*/

public class CssContainer
{
    private SceneElement a_SceneElement;

    // тут хранятся вторичные свойства текущего контейнера
    private List<CssSecondProperty> a_SecondProperties;

    // тут хранятся первичные свойства только текущего контейнера
    public List<CssFirstProperty> Properties { get; private set; }

    // предок текущего контейнера от которого наследуются свойства
    private CssContainer a_Parent;

    // тут хранятся все наследуемые первичные от всех родителей свойства, меняются при смене родителя
    public List<CssFirstProperty> InheritProperties { get; private set; }

    // предназначен только для обновления наследуемых свойств от родителей
    // в качестве единственного аргумента указывается контейнер который обновился
    public event Action OnUpdate;

    // либо все потомки переедут в другой контейнер и отпишутся от текущего на этом этапе
    // либо все потомки помрут вместе с их родителем
    //public event Action OnDestroy;

    public CssContainer( SceneElement _sceneElement, CssContainer _parent = null, dobj _values = null )
        :this( _sceneElement, _values, _parent )
    {}

    public CssContainer( SceneElement _sceneElement, dobj _values = null, CssContainer _parent = null )
    {
        a_SceneElement      = _sceneElement;
        a_SecondProperties  = new List<CssSecondProperty>();
        Properties          = new List<CssFirstProperty>();
        InheritProperties   = new List<CssFirstProperty>();
        
        InitParent( _parent, false );
        
        if( _values != null )
        {
            Update( _values );
        }
    }

    public void ChangeParent( CssContainer _newParent )
    {
        if( a_Parent != null )
        {
            a_Parent.OnUpdate -= CssContainer_OnUpdate;

            InheritProperties.Clear();
        }

        InitParent( _newParent );
    }

    private void InitParent( CssContainer _newParent, bool needUpdateSecond = true )
    {
        a_Parent = _newParent;

        if( _newParent != null )
        {
            a_Parent.OnUpdate += CssContainer_OnUpdate;

            InheritProperties = a_Parent.InheritProperties.Copy();
        }

        UpdateInherit( needUpdateSecond );
    }

    public void Update( dobj _values, bool needUpdateSecond = true )
    {
        {
            // свойство - значение
            string key = "";
            string value = "";
            CssFirstProperty property = Css.Resolve( key, value );
        }

        // все доби теперь упорядочены, есть 4 случая:
        // 0. и то и то пусто - ничего не делаем
        // 1. текущие вальюсы пустые - просто создаём новые
        // 2. новые вальюсы пустые - удаляем существующие
        // 3. и то и то не пусто:
        //  3.1: до конца добби не дошли ни в новых ни в текущих вальюсах - идём по обоим добби в алфавитном порядке и сравниваем свойства, удаляем несуществующие, добавляем новые, меняем меняющиеся
        //  3.2: прошли все существующие свойства и остались новые: добавляем всех
        //  3.3: прошли все новые свойства - удаляем существующие

        if( needUpdateSecond )
            UpdateSecond();
    }

    private void UpdateInherit( bool needUpdateSecond = true )
    {
        // получаем свойства, которые унаследовал предок
        List<CssFirstProperty> newPropertys = a_Parent.InheritProperties.Copy();

        // получить свойства, которые мы можем унаследовать от своего предка

        // все доби теперь упорядочены, есть 4 случая:
        // 0. и то и то пусто - ничего не делаем
        // 1. текущие вальюсы пустые - просто создаём новые
        // 2. новые вальюсы пустые - удаляем существующие
        // 3. и то и то не пусто:
        //  3.1: до конца добби не дошли ни в новых ни в текущих вальюсах - идём по обоим добби в алфавитном порядке и сравниваем свойства, удаляем несуществующие, добавляем новые, меняем меняющиеся
        //  3.2: прошли все существующие свойства и остались новые: добавляем всех
        //  3.3: прошли все новые свойства - удаляем существующие

        if( needUpdateSecond )
            UpdateSecond();
    }

    private void UpdateSecond()
    {
        List<CssSecondProperty> newPropertys = Css.Resolve( Properties, InheritProperties );

        // все доби теперь упорядочены, есть 4 случая:
        // 0. и то и то пусто - ничего не делаем
        // 1. текущие вальюсы пустые - просто создаём новые
        // 2. новые вальюсы пустые - удаляем существующие
        // 3. и то и то не пусто:
        //  3.1: до конца добби не дошли ни в новых ни в текущих вальюсах - идём по обоим добби в алфавитном порядке и сравниваем свойства, удаляем несуществующие, добавляем новые, меняем меняющиеся
        //  3.2: прошли все существующие свойства и остались новые: добавляем всех
        //  3.3: прошли все новые свойства - удаляем существующие

        OnUpdate?.Invoke();
    }

    private void CssContainer_OnUpdate()
    {
        UpdateInherit();
    }
}