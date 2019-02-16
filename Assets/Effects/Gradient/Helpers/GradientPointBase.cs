using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GradientPointBase 
{
	
	[SerializeField] protected Color sfColor;
	
	public abstract List<float> ToData();
	
	// сколько ячеек массива будет занимать данная точка
	public abstract int GetSize();
	
	public abstract GradientPointType GetTypeGradient();
}
