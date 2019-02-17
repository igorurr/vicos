using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutinesManager : MonoBehaviour
{

    private static CorutinesManager Instance;

    public static Coroutine StartCorutine( IEnumerator _coroutine )
    {
        return Instance.StartCoroutine( _coroutine );
    }

    public static void StopCorutine( Coroutine _coroutine )
    {
        Instance.StopCoroutine( _coroutine );
    }

    private void Awake()
    {
        Instance = this;
    }
}
