using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class BlockBreak : MonoBehaviour
{
	// Use this for initialization
	void Start () 
	{
		Invoke ("blockAdd", 1.0f);

	}


	void Update () {

	}

	void OnCollisionEnter(Collision coll)
	{
		
		if (coll.gameObject.CompareTag ("Pong"))
		{
			GameControl.gc.removeBlock();
			Destroy (gameObject);
		}
	
	}
	public void blockAdd()
	{
		GameControl.gc.blockAmount += 1;
	}
}
