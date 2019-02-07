using System.Linq;
using UnityEngine;

public static class MyRandom
{
    public static float FloatValue
    {
        get { return Random.value; }
    }
    public static int IntValue
    {
        get
        {
            float aaaa = ( Random.value * 2 - 1);
            float aadaa = aaaa * int.MaxValue;
            int aaduaa = (int)aadaa;
            return aaduaa;
        }
    }
    
    public static float Range( int _a, int _b )
    {
        return Random.Range(_a, _b);
    }
    
    public static float FloatRange( Vector2 _ab )
    {
        return Random.Range( _ab.x, _ab.y );
    }
    
    public static int IntRange( Vector2Int _ab )
    {
        return IntRange( _ab.x, _ab.y );
    }
    
    public static int IntRange( int _a, int _b )
    {
        int ret = Random.Range( _a, _b + 1 );
        return ret == _b+1 ? _b : ret ;
    }
    
    public static int IntBefore( int _a )
    {
        return IntRange(0,_a);
    }
    
    public static int ProbabilityDistribution3( Vector3 _v )
    {
        float randomVal = FloatValue;
        
        if ( randomVal < _v.x )
            return 0;
        
        if ( randomVal < _v.x + _v.y )
            return 1;
        
        return 2;
    }
    
    public static string RandomString( int _length )
    {
        System.Random random = new System.Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(
            Enumerable.Repeat(chars, _length).Select(s => s[random.Next(s.Length)]).ToArray()
        );
    }
    
    #region another

    public static int IntRandomBeforeAnother( int last, int another )
    {
        while ( true )
        {
            int ret = IntBefore( last );
            if ( ret != another )
                return ret;
        }
    }

    #endregion
}