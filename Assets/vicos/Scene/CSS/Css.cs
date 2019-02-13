using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Css
{
    public static enum CssResolverType
    {
        
    }

    private static readonly Dictionary<CssResolverType, CssResolver> Resolvers = new Dictionary<CssResolverType, CssResolver>()
    {
    }

    public static readonly Dictionary<string, CssResolverType> Propertys = new Dictionary<string, CssResolver>()
    {
        "blur": ,
        "pixeling",
        "blur"
    }










    private dobj a_Data;
    private ModelNode a_Root;

    public static void Dispatch( ModelAction _action )
    {
        a_Instance._Dispatch( _action );
    }

    private void _Dispatch( ModelAction _action )
    {
        // обходим dobj нашего акшона, проверяем есть ли в модели узлы, соответствующие текущим поддеревьям
        // нужна проверка в каждом поддереве что хешсуммы dodj совпадают, оптимизация короч

        // тут и a_Data должен быть null
        if( a_Root == null )
        {

        }
        else
        {
            Merge( a_Data, _action.Data, a_Root );
        }
    }

    private void Merge( dobj _state, dobj _newData, ModelNode _subTree )
    {
        // идём по ключам стейта и newData, т.к. они отсортированы, одновременно
        // для поддеревьев, у которых одинаковые хешсуммы ничего не делаем,
        // у которых различаются - рекурсивно вызываем Merge,
        // добавляем отсутствующие вершины, удаляем удалённые

        int i=0, j=0;
        while( i < _state.Count || j < _newData.Count )
        {

        }

        // потом алгоритм напишешь
    }
}