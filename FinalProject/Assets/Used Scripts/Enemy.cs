using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : MonoBehaviour
{
	Rigidbody rigidBody;

	public float initSpeed = 3;
	public int lifeAmount = 1;
	public int scoreAmount = 1;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

		//rigidBody.AddForce(new Vector3(0f,initSpeed,0f));
		rigidBody.velocity = new Vector3 (0f, -initSpeed, 0f);
	}


	void Update () {

	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.CompareTag ("Ground") || coll.gameObject.CompareTag ("Paddle")) 
		{
			GameControl.gc.enemyCollision(lifeAmount);
			Destroy (gameObject);
		}

		else if (coll.gameObject.CompareTag ("Walls"))
		{
			Destroy (gameObject);
		}
		
		else if (coll.gameObject.CompareTag ("Pong"))
		{
			GameControl.gc.addScore(scoreAmount);
			Destroy (gameObject);
		}
		
	
	}
}
