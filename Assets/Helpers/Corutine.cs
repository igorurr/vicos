using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corutine {
    // наследоваться напрямую от Coroutine запрещено
	
	#region constants
	
	
	
	#endregion
		
    #region atributes
	
    IEnumerator 	a_ienumerator;
    Coroutine 		a_coroutine;
		
    Action 			a_onStart;
    Action 			a_onStop;
    Action 			a_onFinish;
	
	float			a_Delay;
		
    #endregion
		
    #region propertys
	
		
		
    #endregion
		
    #region public methods
	
	public static Corutine Run(
		IEnumerator 	_ienumerator,
		Action 			_onStart 		= null,
		Action 			_onStop 		= null,
		Action 			_onFinish 		= null,
		float			_delay			= 0
	)
	{
		Corutine ret = new Corutine( _ienumerator, _onStart, _onStop, _onFinish, _delay );
		ret.Start();
		return ret;
	}
	
    public void Stop()
    {
	    if ( a_coroutine == null )
		    return;
	    
	    CorutinesManager.StopCorutine(a_coroutine);
			
        a_onStop?.Invoke();
    }
	
    #endregion
		
    #region service methods
	
	void Start()
	{
		a_coroutine = CorutinesManager.StartCorutine( GetRoutine() );
	}
		
	protected Corutine(
		IEnumerator _ienumerator, 
		Action      _onStart  		= null, 
		Action      _onStop   		= null, 
		Action      _onFinish 		= null,
		float		_delay	  		= 0
	)
	{
		a_ienumerator 	= _ienumerator;
			
		a_onStart  		= _onStart;
		a_onStop   		= _onStop;
		a_onFinish 		= _onFinish;
		
		a_Delay	  		= _delay;
	}

	IEnumerator GetRoutine()
	{
		if ( a_Delay > 0 )
			yield return new WaitForSeconds( a_Delay );
		
		a_onStart?.Invoke();
		
		yield return a_ienumerator;
		
		a_onFinish?.Invoke();
	}

	#endregion
	
	#region Public Coroutins Examples

	public static Corutine Tick( 
		Action _action,
		float  _time 	 = 1, 
		Action _onStart  = null, 
		Action _onStop   = null, 
		Action _onFinish = null,
		float  _delay    = 1
	)
	{
		return Run( _Tick( _action, _time ), _onStart, _onStop, _onFinish, _delay );
	}

	public static Corutine Wait( 
		Action _action, 
		Action _onStart  = null, 
		Action _onStop   = null, 
		Action _onFinish = null,
		float  _delay    = 1
	)
	{
		return Run(
			new WaitWhile( () =>
			{
				_action();
				return false;
			} ),
			_onStart,
			_onStop, 
			_onFinish, 
			_delay
		);
	}

	public static Corutine While( 
		Func<bool> _condition, 
		Action     _onStart  = null, 
		Action     _onStop   = null, 
		Action     _onFinish = null,
		float      _delay    = 0
	)
	{
		return Run( new WaitWhile( _condition ), _onStart, _onStop, _onFinish, _delay );
	}

	public static Corutine EveryFrame( 
		Action	   _action, 
		Action     _onStart  = null, 
		Action     _onStop   = null, 
		Action     _onFinish = null,
		float      _delay    = 0
	)
	{
		Func<bool> delega = () =>
		{
			_action();
			return true;
		};
		return Run( new WaitWhile( delega ), _onStart, _onStop, _onFinish, _delay );
	}

	#endregion

	private static IEnumerator _Tick( Action _condition, float _time )
	{
		while (true)
		{
			_condition();
			yield return new WaitForSeconds( _time );
		}
	}

	#region Service Coroutins Examples
	
	#endregion
}
