using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CssContainer
{
    private dobj a_Values;

    private SceneElement a_SceneElement;

    // тут хранятся только свойства текущего контейнера
    private List<CssProperty> a_Properties;

    // тут хранятся все наследуемые от всех родителей свойства, меняются при смене родителя
    private List<CssProperty> a_InheritProperties;

    public void ChangeParent( CssContainer _parent )
    {
        
    }

    public void Update( dobj _values )
    {
        // все доби теперь упорядочены, есть 4 случая:
        // 0. и то и то пусто - ничего не делаем
        // 1. текущие вальюсы пустые - просто создаём новые
        // 2. новые вальюсы пустые - удаляем существующие
        // 3. и то и то не пусто:
        //  3.1: до конца добби не дошли ни в новых ни в текущих вальюсах - идём по обоим добби в алфавитном порядке и сравниваем свойства, удаляем несуществующие, добавляем новые, меняем меняющиеся
        //  3.2: прошли все существующие свойства и остались новые: добавляем всех
        //  3.3: прошли все новые свойства - удаляем существующие
    }
}