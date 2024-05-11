using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class GroundCollision : MonoBehaviour
{
	public float initSpeed = 3;


	// Use this for initialization
	void Start () {

	}


	void Update () {

	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.CompareTag ("Player")) {

		}

	}
}
