using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
	// Use this for initialization
	public string nextLevelToLoad;

	void Start () 
	{

	}


	void Update ()
	{

	}

	void OnTriggerEnter(Collider coll)
	{
		
		if (coll.gameObject.CompareTag ("Pong"))
		{
			SceneManager.LoadScene(nextLevelToLoad);
		}
	
	}
}
