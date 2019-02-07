using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshRenderUnityUI : MonoBehaviour
{

	[SerializeField] private SpriteRenderer SpriteRenderer;
	[SerializeField] private Image Image;
	
	// Use this for initialization
	private void Awake()
	{
		Image.sprite = SpriteRenderer.sprite;
		//SpriteRenderer.enabled = false;
		Image.color = Color.white;
	}
	
	// Use this for initialization
	void Start ()
	{
		TextMesh tm;
		MeshRenderer mr;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
