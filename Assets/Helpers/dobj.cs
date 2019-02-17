using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


public class tdobj<T> : dobj
{
    public tdobj( params object[] _args )
    {
        if( _args.Length % 2 != 0 )
            throw new Exception("Каждому ключу должно соответствовать значение");
        
        InitHash();

        for ( int i=0; i < _args.Length; i+=2 )
        {
            if( !(_args[i+1] is T) )
                throw new Exception("Неверный тип объекта");
            
            this[ (string)_args[i] ] = _args[i+1];
        }
    }
}

public class dobj : Dictionary<string, object>
{    
    public dobj() : base()
    {
        InitHash();
    } 
    
    public dobj( params object[] _args )
        : base()
    {
        if( _args.Length % 2 != 0 )
            throw new Exception("Каждому ключу должно соответствовать значение");
        
        InitHash();

        for ( int i=0; i < _args.Length; i+=2 )
        {
            this[ (string)_args[i] ] = _args[i+1];
        }
    }
    
    public object this [string _key]
    {
        get
        {
            object ret = null;
            TryGetValue( _key, out ret );
            return ret;
        }
        set
        {
            if ( value == null )
            {
                Del( _key );
                return;
            }
            
            if( ContainsKey(_key) )
                OnRemoveElement( _key, base[_key] );

            base[_key] = value;
            
            OnAddElement( _key, value );
        }
    }

    public dobj Merge ( params dobj[] _dobjs )
    {
        dobj ret = new dobj();
        foreach (dobj dobj in _dobjs)
        {
            foreach (KeyValuePair<string,object> kv in dobj)
            {
                string strVal = (kv.Value as string);
                if( strVal != null && strVal == String.Empty )
                    continue;

                ret[ kv.Key ] = kv.Value;
            } 
        }

        return ret;
    }

    public void Del( string _key )
    {
        object val = null;
        if ( !TryGetValue( _key, out val ) )
            return;

        Remove( _key );
        
        OnRemoveElement( _key, val );
    }

    #region Equals

    // xеш списка
    private int a_Hash;

    // сумма xешей всеx строк списка
    private int a_HashSumRowsHashes;
    
    // поразрядная сумма xешей всеx строк списка
    private List<int> a_HashBitSumRowsHashesList;

    // xеш строки
    private int RowHash( string _key, object _val )
    {
        return _key.GetHashCode() ^ ( _val.GetHashCode() + _key.GetHashCode() );
    }

    protected void InitHash()
    {
        a_Hash                     = 0;
        a_HashSumRowsHashes        = 0;
        a_HashBitSumRowsHashesList = new List<int>( sizeof(int)*8 );
        
        if( Count > 0 )
            foreach (var el in this)
                OnAddElement( el.Key, el.Value );
    }

    private void OnAddElement( string _key, object _val )
    {
        int      hashNewkv    = RowHash( _key, _val );
        BitArray newkvBitHash = new BitArray(new int[] { hashNewkv });

        int hashBitSumRowsHashes = 0;
        for ( int i = 0; i < a_HashBitSumRowsHashesList.Count; i++ )
        {
            a_HashBitSumRowsHashesList[i] += newkvBitHash[i] ? 1 : 0;
            hashBitSumRowsHashes          += i*a_HashBitSumRowsHashesList[i].CycleShiftLeft( i );
        }

        a_HashSumRowsHashes += hashNewkv;

        a_Hash = a_HashSumRowsHashes ^ hashBitSumRowsHashes;
    }

    private void OnRemoveElement( string _key, object _val )
    {
        int      hashRewkv    = RowHash( _key, _val );
        BitArray rewkvBitHash = new BitArray(new int[] { hashRewkv });

        int hashBitSumRowsHashes = 0;
        for ( int i = 0; i < a_HashBitSumRowsHashesList.Count; i++ )
        {
            a_HashBitSumRowsHashesList[i] -= rewkvBitHash[i] ? 1 : 0;
            hashBitSumRowsHashes          += i*a_HashBitSumRowsHashesList[i].CycleShiftLeft( i );
        }

        a_HashSumRowsHashes -= hashRewkv;

        a_Hash = a_HashSumRowsHashes ^ hashBitSumRowsHashes;
    }

    public override bool Equals(object obj)
    {
        if ( !(obj is dobj) )
            return false;

        return (obj as dobj).GetHashCode() == GetHashCode();
    }

    public override int GetHashCode()
    {
        return a_Hash;
    }

    #endregion
    
    #region testing
    
   /* static readonly Random rndGen = new Random();
    static string GetRandomPassword(int pwdLength)
    {
        string ch  = "qwertyuiopasdfghjklzxcvbnm0123456789";
        char[] pwd = new char[pwdLength];
        for (int i = 0; i < pwd.Length; i++)
            pwd[i] = ch[rndGen.Next(ch.Length)];     
        return new string(pwd);
    }

    static void Testing()
    {
        List<Tuple<int, dobj>> aaaa  = new List<Tuple<int, dobj>>();
        List<Tuple<int, dobj>> aaaa2 = new List<Tuple<int, dobj>>();

        for (int i = 1; i < 10000; i++)
        {
            dobj a = new dobj(
                "key", GetRandomPassword(20),
                "fen", 0,
                "rt", 2,
                "ol", 9,
                "se", 18,
                "d", 985
            );
            aaaa.Add( new Tuple<int, dobj>( a.GetHashCode(), a ) );
        }

        aaaa.Sort( ( t1, t2) => t1.Item1 > t2.Item1 ? 1 : t1.Item1 == t2.Item1 ? 0 : -1 );
        for ( int i = 1; i < aaaa.Count; i++ )
            if ( aaaa[i].Item1 == aaaa[ i - 1].Item1 )
            {
                aaaa2.Add( aaaa[i - 1] );
                aaaa2.Add( aaaa[i] );
            }

        Debug.Log( aaaa2.Count );
    }*/

    #endregion
}