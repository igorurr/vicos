using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StereoBehaviour : MonoBehaviour
{
	#region const
	
	#endregion
	
	#region MonoBehaviour
	
	void Awake ()
	{
		OnAwake();
	}

	private void Start()
	{
		OnStart();
	}
	
	void OnDestroy()
	{
		DoDestroy();
	}
	
	
	void OnApplicationPause( bool _isPaused )
	{
		if( _isPaused )
			OnPaused();
		else
			OnUnpaused();
	}

	

	#if UNITY_EDITOR
    
	void OnApplicationQuit()
	{
		OnQuit();
	}
    
	#elif UNITY_ANDROID
        // тут должна быть функция во время выxода
    #elif UNITY_IOS
		// тут должна быть функция во время выxода
	#endif

	
	#endregion

	#region Public Methods

	
	protected virtual void OnAwake() {}
	
	protected virtual void OnStart() {}
	
	protected virtual void DoDestroy() {}
	
	protected virtual void OnPaused() {}
	
	protected virtual void OnUnpaused() {}
	
	protected virtual void OnQuit() {}
	

	#endregion
}
