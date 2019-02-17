using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;


public class tolis<T> : olis
{
    public tolis( params object[] _args )
    {
        InitHash();

        foreach (var el in _args)
        {
            if( !(el is T) )
                throw new Exception("Неверный тип объекта");
            
            Add( el );
        }
    }
}

public class olis : List<object>
{
    public olis() : base()
    {
        InitHash(); 
    } 
    
    public olis( params object[] _args )
        : base()
    {
        InitHash();

        foreach (var el in _args)
        {
            Add( el );
        }
    }

    public void Push( object _newo, int? _pos = null )
    {
        if ( _pos == null )
            Add( _newo );
        else if ( _pos.Value < 0 )
            Insert( Count + _pos.Value, _newo );
        else
            Insert( _pos.Value, _newo );
        
        OnAddElement( _newo );
    }

    public void Del( object _rewo )
    {
        if( Remove( _rewo ) )
            OnRemoveElement( _rewo );
    }

    public void Del( int _pos )
    {
        int pos = _pos < 0 ? Count - _pos : _pos;

        if ( _pos >= Count && pos < 0 )
            return;
        
        OnRemoveElement( this[pos] );
        RemoveAt( pos );
    }

    public olis Merge ( params olis[] _olises )
    {
        olis ret = new olis();
        
        foreach (olis olis in _olises)
        {
            foreach (object v in olis)
            {
                if( ret.Contains( v ) )
                    continue;

                ret.Add( v );
            } 
        }

        return ret;
    }

    #region Equals

    // xеш списка
    private int a_Hash;

    // сумма xешей всеx строк списка
    private int a_HashSumRowsHashes;
    
    // поразрядная сумма xешей всеx строк списка
    private List<int> a_HashBitSumRowsHashesList;

    // xеш строки
    private int RowHash(object _newo)
    {
        return _newo.GetHashCode();
    }

    protected void InitHash()
    {
        a_Hash = 0;
        a_HashSumRowsHashes = 0;
        a_HashBitSumRowsHashesList = new List<int>( sizeof(int)*8 );
        
        if( Count > 0 )
            foreach (var el in this)
                OnAddElement( el );
    }

    private void OnAddElement( object _newo )
    {
        int       hashNewo     = RowHash( _newo );
        BitArray  newoBitHash  = new BitArray(new int[] { hashNewo });

        int hashBitSumRowsHashes = 0;
        for ( int i = 0; i < a_HashBitSumRowsHashesList.Count; i++ )
        {
            a_HashBitSumRowsHashesList[i]    += newoBitHash[i] ? 1 : 0;
            hashBitSumRowsHashes             += i*a_HashBitSumRowsHashesList[i].CycleShiftLeft( i );
        }

        a_HashSumRowsHashes += hashNewo;

        a_Hash = a_HashSumRowsHashes ^ hashBitSumRowsHashes;
    }

    private void OnRemoveElement( object _rewo )
    {
        int       hashRewo     = RowHash( _rewo );
        BitArray  rewoBitHash  = new BitArray(new int[] { hashRewo });

        int hashBitSumRowsHashes = 0;
        for ( int i = 0; i < a_HashBitSumRowsHashesList.Count; i++ )
        {
            a_HashBitSumRowsHashesList[i]   -= rewoBitHash[i] ? 1 : 0;
            hashBitSumRowsHashes            += i*a_HashBitSumRowsHashesList[i].CycleShiftLeft( i );
        }

        a_HashSumRowsHashes -= hashRewo;

        a_Hash = a_HashSumRowsHashes ^ hashBitSumRowsHashes;
    }

    public override bool Equals(object obj)
    {
        if ( !(obj is olis) )
            return false;

        return (obj as olis).GetHashCode() == GetHashCode();
    }

    public override int GetHashCode()
    {
        return a_Hash;
    }

    #endregion
}