using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Newtonsoft.Json.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils {

    #region ListComparer

    public class ListComparer<T>
    {
        // xеш строки
        private static int RowHash(object _newo)
        {
            return _newo.GetHashCode();
        }
        
        private static int GetHash( List<T> _list )
        {
            // сумма xешей всеx строк списка
            int hashSumRowsHashes                    = 0;
            
            // поразрядная сумма xешей всеx строк списка
            List<int> hashBitSumRowsHashesList       = new List<int>( sizeof(int) );

            foreach (T el in _list)
            {
                int      hashEl      = RowHash( el );
                BitArray elBitHash = new BitArray(new int[] { hashEl });
    
                for ( int i = 0; i < hashBitSumRowsHashesList.Count; i++ )
                    hashBitSumRowsHashesList[i] += elBitHash[i] ? 1 : 0;
    
                hashSumRowsHashes += hashEl;
            }

            int hashBitSumRowsHashes = 0;
            for ( int i = 0; i < hashBitSumRowsHashesList.Count; i++ )
                hashBitSumRowsHashes += i*hashBitSumRowsHashesList[i].CycleShiftLeft( i );
            
            return hashSumRowsHashes ^ hashBitSumRowsHashes;
        }
    
        public static bool Compare( List<T> _list1, List<T> _list2 )
        {
            if ( _list1.Count == 0 && _list2.Count == 0 )
                return true;

            if ( _list1.Count == 0 || _list2.Count == 0 || _list1.Count != _list2.Count )
                return false;
    
            return GetHash( _list1 ) == GetHash( _list2 );
        }
    }

    #endregion
	
    #region unity vectors
    
    /*public static Vector3 SumPoints<T>(this HashSet<T> _set)
    where T: Vector3, Vector2, Vector3int, Vector2int
    {
        Vector3 res = Vector3.zero;
        if ( _set.Count == 0 )
            return res;

        foreach (var el in _set)
        {
            res += el;
        }
        
        return res;
    }*/

    public static Vector3 ComponentMultiply(this Vector3 a, Vector3 b)
    {
        return new Vector3( a.x*b.x, a.y*b.y, a.z*b.z );
    }

    public static Vector2 ComponentMultiply(this Vector2 a, Vector2 b)
    {
        return new Vector3( a.x*b.x, a.y*b.y );
    }

    // public static U Reduce<T,U>(this List<T> _list, Func<T,U> _assumator)    не работает
    // {
    //     U ret = new U();
    //     foreach ( T val in _list )
    //         ret += _assumator(val);
    // }

    public static Vector2 Sign(this Vector2 a)
    {
        return new Vector2( 
            a.x < 0 ? -1 : 1,
            a.y < 0 ? -1 : 1
        );
    }

    public static Vector2 Abs(this Vector2 a)
    {
        return new Vector3( Mathf.Abs(a.x), Mathf.Abs(a.y) );
    }

    public static Vector2 Clamp01(this Vector2 a)
    {
        return new Vector2( Mathf.Clamp01(a.x), Mathf.Clamp01(a.y) );
    }
    
    #endregion
    
    #region c# list dictionary

    /*public static T GetRandom<T>(this List<T> _list)
    {
        return _list[Random.Range(0, _list.Count())];
    }*/

    public static bool Equals<V>(
        List<V>          _list1, 
        List<V>          _list2
    )
    {
        if ( _list1 == null && _list2 == null )
            return true;
        
        if ( _list1 == null || _list2 == null )
            return false;
        
        if ( _list1.Count == 0 && _list2.Count == 0 )
            return true;
        
        if ( _list1.Count == 0 || _list2.Count == 0 || _list2.Count != _list1.Count )
            return false;

        IEnumerable<V> residual = _list1.Except(_list2);

        return residual.Any();
    }

    public static List<V> Copy<V>(
        this List<V> _list
    )
    {
        return _list.GetRange(0, _list.Count);
    }

    #endregion
    
    #region dictionary

    /*public static bool TryFind<K,V>( 
        this Dictionary<K,V>              dic,
        Func<KeyValuePair<K,V>, bool>     pred,
        out KeyValuePair<K,V>             res)
    {
        res = null;
        
        if ( dic == null || dic.Count == 0 )
            return false;

        foreach (KeyValuePair<K,V> kv in dic)
        {
            if ( pred(kv) )
            {
                res = kv;
                return true;
            }
        }

        return false;
    }*/

    public static Dictionary<TKey, TValue> Merge<TKey, TValue> (
        Dictionary<TKey, TValue>          _first, 
        params Dictionary<TKey, TValue>[] _dictionaries
    )
    {
        List<Dictionary<TKey, TValue>> dictionaries = _dictionaries.ToList();
        dictionaries.Insert( 0, _first );
        
        Dictionary<TKey, TValue> result = dictionaries
             .SelectMany(dict => dict)
             .ToDictionary(pair => pair.Key, pair => pair.Value);

        return result;
    }

    public static List<TValue> Merge<TValue> (
        this List<TValue>          _first, 
        params List<TValue>[] _lists
    )
    {
        List<List<TValue>> lists = _lists.ToList();
        lists.Insert( 0, _first );
        
        List<TValue> result = lists
                              .SelectMany(list => list)
                              .ToList();

        return result;
    }

    #endregion
    
    #region int

    public static int div( this int a, int b )
    {
        return a >= 0 ? a / b : -1 - ( (-1 - a) / b );
    }

    public static int mod( this int a, int b )
    {
        return a >= 0 ? a % b : b - (b - a - 1) % b - 1;
    }

    public static int CycleShiftLeft( this int a, int b )
    {
        int bn = (b % (sizeof(int)*8));
        return ( a << bn ) | ( a >> ( sizeof(int)*8 - bn ) );
    }

    #endregion
    
    #region float
    
    public static bool BelongsToRange( float a, float b, float c )
    {
        return c >= a && a <= b;
    }

    #endregion
    
    #region string

    public static void ForEvery(
        this string  _str, 
        Action<int,char>    _action
    )
    {
        if ( _str.Length == 0 )
            return;
        
        for ( int i = 0; i < _str.Length; i++ )
            _action( i, _str[i] );
    }

    public static byte[] ToBytes( this string _str )
    {
        return Encoding.UTF8.GetBytes(_str);
    }

    public static byte[] FromChar16StringToBytes( this string _str )
    {
        byte[] ret = new byte[ _str.Length / 2 ];

        Func<char, int> Get16BitVal = _c => 'a' <= _c && 'z' >= _c ? _c-'a'+10 : _c-'0';
        
        // подразумевается что длина строки кратна 2
        // и на вxод подаются символы 0123456789abcdef
        for ( int i = 0; 2*i < _str.Length; i++ )
        {
            char c1 = _str[2*i];
            char c2 = _str[2*i+1];

            ret[i] = (byte)( Get16BitVal(c1)*16 + Get16BitVal(c2) );
        }

        return ret;
    }

    public static string GetString( this byte[] _str )
    {
        return Encoding.UTF8.GetString(_str);
    }
    
    public static int? TryGetInt( this string str )
    {
        int count;
        try
        {
            count = Convert.ToInt32( str );
        }
        catch (Exception e)
        {
            return null;
        }

        return count;
    }

    #endregion
    
    #region DateTime

    public static string SecondsToNormalTime( int _seconds )
    {
        int hours   = _seconds / 60 / 60;
        int minutes = ( _seconds % (60 * 60) ) / 60;
        int seconds = _seconds % 60;

        Func<int, string> SubtimeToString = ( val ) =>
        {
            string ret = val.ToString();
            if ( ret.Length == 1 )
                ret = "0" + ret;

            return ret;
        };
		
        if ( hours > 0 )
            return $"{SubtimeToString(hours)}:{SubtimeToString(minutes)}:{SubtimeToString(seconds)}";
        else if ( minutes > 0 )
            return $"{SubtimeToString(minutes)}:{SubtimeToString(seconds)}";
        else
            return $"00:{(seconds)}";
    }

    public static int GetTimestamp( this DateTime _dt )
    {
        return (int) (_dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    public static DateTime DateTimeFromTimestamp( int _dt )
    {
        System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return dtDateTime.AddSeconds( _dt );
    }

    #endregion
    
    #region []

    public static T? Find<T>( this T[] _array, Func<T, bool> _predicate )
        where T: struct
    {
        if ( _array.Length == 0 )
            return null;

        foreach (T var in _array)
            if ( _predicate(var) )
                return var;

        return null;
    }
    
    public static void Swap<T>(this T[] _array, int _index1, int _index2)
    {
        T temp = _array[_index1];
        _array[_index1] = _array[_index2];
        _array[_index2] = temp;
    }
    
    public static T[] Concat<T>( this T[] _a1, T[] _a2 )
    {
        var temp = new T[_a1.Length + _a2.Length];
        _a1.CopyTo(temp, 0);
        _a2.CopyTo(temp, _a1.Length);
        return temp;
    }
    
    static event Action ddd;

    // Преобразует байты в 16ричное представление и выдаёт строку
    public static string ToChar16String( this byte[] _bytes )
    {
        return BitConverter.ToString(_bytes).Replace("-", string.Empty).ToLower();
    }
    
    #endregion
    
    /*#region JTokens
    
    public static T TryGetSingleVariable<T>( this JObject _jo, string _nodeName, T _def )
    {
        bool joIsset =  _jo != null;

        if ( joIsset )
        {
            JToken tryGetVal = _jo.GetValue(_nodeName);
            return tryGetVal == null ? _def : tryGetVal.Value<T>() ;
        }

        return _def;
    }

    public static T TryGetSingleVariableEnum<T>( this JObject _jo, string _nodeName, T _def )
    {
        return (T)Enum.ToObject(
            typeof(T), 
            TryGetSingleVariable<int>( _jo, _nodeName, Convert.ToInt32(_def) )
        );
    }

    public static JObject AttemptJObjectParse( string str )
    {
        try
        {
            return JObject.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogError("xа, маслины ловит");
            return JObject.Parse("{}");
        }
    }

    #endregion*/
    
    #region action

   /*public static Action SubscribeOnce( this Action _event, Action _action )
    {
        if ( _action == null )
            return _event;
                
        Action a = null;
        a = () =>
        {
            _action();
            _event -= a;
        };
        _event += a;

        return _event;
    }
    
    public static Action<T1,T2> SubscribeOnce<T1,T2>( this Action<T1,T2> _event, Action<T1,T2> _action )
    {
        if ( _action == null )
            return _event;
                
        Action<T1, T2> a = null;
        a = (v1, v2) =>
        {
            _action(v1, v2);
            _event -= a;
        };
        _event += a;

        return _event;
    }
    
    public static Action<T1,T2,T3> SubscribeOnce<T1,T2,T3>( this Action<T1,T2,T3> _event, Action<T1,T2,T3> _action )
    {
        if ( _action == null )
            return _event;
                
        Action<T1, T2, T3> a = null;
        a = (v1, v2, v3) =>
        {
            _action(v1, v2, v3);
            _event -= a;
        };
        _event += a;

        return _event;
    }
    
    button.Click += Delegates.AutoUnsubscribe<EventHandler>((sender, args) =>
    {
        // One-time code here
    }, handler => button.Click -= handler);*/
    
    #endregion
    
    #region enum

    public static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }
    
    /*public static T GetString<T>( this T enumVal )
        where T: Enum
    {
        
    }*/

    // не работает
    /*public static void ForEvery<T>( this T _enum, Action<T> _action )
        where T : Enum
    {
        foreach (T el in (T[]) Enum.GetValues(typeof(T)))
            _action( el );
    }

    public static void ForEvery( this PurchasesStorage.UnityPurchaseManager.TypeConsumable _enum, Action<PurchasesStorage.UnityPurchaseManager.TypeConsumable> _action )
    {
        foreach (PurchasesStorage.UnityPurchaseManager.TypeConsumable el in (PurchasesStorage.UnityPurchaseManager.TypeConsumable[]) Enum.GetValues(typeof(PurchasesStorage.UnityPurchaseManager.TypeConsumable)))
            _action( el );
    }*/

    #endregion
}