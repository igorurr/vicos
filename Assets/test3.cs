

using UnityEngine;
using UnityEngine.UI;

public class test3 : MonoBehaviour 
{
	public GameObject target;
	
	private Material      material;	
	private ComputeBuffer buffer;
	private Vector4[]     element;
	private string        label;
	private Renderer      render;
	
	void Load ()
	{
		buffer   = new ComputeBuffer(1, 16, ComputeBufferType.Default);
		element  = new Vector4[1];
		label    = string.Empty;
	}
	
	void LoadShtoto()
	{
		render   = target.GetComponent<Renderer>(); 
		material = render.material;
	}
	
	void LoadText()
	{
		material = target.GetComponent<Text>().material;
	}
	
	void Start () 
	{
		Load();
		LoadText();
	}
	
	void Update () 
	{
		Graphics.ClearRandomWriteTargets();
		material.SetPass(0);
		material.SetBuffer("buffer", buffer);
		Graphics.SetRandomWriteTarget(1, buffer, false);
		buffer.GetData(element);
		label = ( element != null ) ? element[0].ToString("F3") : string.Empty;
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 62;
		GUI.Label(new Rect(50, 50, 400, 100), label, style);
	}
	
	void OnDestroy()
	{
		buffer.Dispose();
	}
}