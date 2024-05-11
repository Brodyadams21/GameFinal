using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class BallMovemen : MonoBehaviour
{
	Rigidbody rigidBody;

	public float speedMultiplier = 1.1f; //how much the speed increases every second

	public Vector3 physicsSpinMultiplier; //how much spin the ball experiences physically
	public Vector3 visualSpinMultiplier; //how much spin appears on the ball
	public Vector3 spinDecay;

	public Vector3 spin; //the spin of the ball

	public float minVelocity;
	public float maxVelocity;

    public float horizontalSpeedConstraint = 5;
    public float verticalCorrection = 1;
    public float randomSpeedAdd = 2;
    public float horizontalSpeedMin = 3;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		spin = Vector3.zero;

		rigidBody.AddForce(new Vector3(0f,2f,0f));
		rigidBody.velocity = new Vector3 (0f, -8f, 0f);
		rigidBody.AddTorque(new Vector3 (100f, 0, 0), ForceMode.Force);

	}

	//[Command]
	public void CmdSpin(Vector3 spin){
		this.spin = spin;
	}
	

	void Update () {
		spin = new Vector3 (spin.x * (1 - (1 - spinDecay.x) * Time.deltaTime), spin.y * (1 - (1 - spinDecay.y) * Time.deltaTime), spin.z * (1 - (1 - spinDecay.z) * Time.deltaTime));
		rigidBody.angularVelocity = new Vector3 (spin.x * visualSpinMultiplier.x, spin.y * visualSpinMultiplier.y, spin.z * visualSpinMultiplier.z);

		Vector3 vel = rigidBody.velocity;
		float tempMultiplier = (speedMultiplier - 1f) * Time.deltaTime + 1;
		rigidBody.velocity = new Vector3 (vel.x + spin.x * physicsSpinMultiplier.x, vel.y + spin.y * physicsSpinMultiplier.y, vel.z  * tempMultiplier); 

		if (Mathf.Abs(rigidBody.velocity.z) > maxVelocity) {
			vel = rigidBody.velocity;
			rigidBody.velocity = new Vector3 (vel.x, vel.y, Mathf.Lerp (vel.z, maxVelocity * Mathf.Sign(vel.z), 0.6f));
		}
		if (Mathf.Abs(rigidBody.velocity.z) < minVelocity) {
			vel = rigidBody.velocity;
			rigidBody.velocity = new Vector3 (vel.x, vel.y, Mathf.Lerp (vel.z, minVelocity * Mathf.Sign(vel.z), 0.6f));
		}
        /////////
        if (rigidBody.velocity.z >= horizontalSpeedConstraint)
        {
            vel = rigidBody.velocity;
            if (vel.y < 0 && vel.y > -10)
            {
			    rigidBody.velocity = new Vector3 (vel.x, vel.y - verticalCorrection, vel.z - 1);
            }

            else if (vel.y > 0 && vel.y < 10)
            {
			    rigidBody.velocity = new Vector3 (vel.x, vel.y + verticalCorrection, vel.z - 1);
            }

            else
			    rigidBody.velocity = new Vector3 (vel.x, vel.y, vel.z - 1);

        }
        if (rigidBody.velocity.z <= -horizontalSpeedConstraint)
        {
            vel = rigidBody.velocity;
            if (vel.y < 0 && vel.y > -10)
            {
			    rigidBody.velocity = new Vector3 (vel.x, vel.y - verticalCorrection, vel.z + 1);
            }

            else if (vel.y > 0 && vel.y < 10)
            {
			    rigidBody.velocity = new Vector3 (vel.x, vel.y + verticalCorrection, vel.z + 1);
            }

            else
			    rigidBody.velocity = new Vector3 (vel.x, vel.y, vel.z + 1);

        }
        ////////
        if (rigidBody.velocity.x >= horizontalSpeedConstraint)
        {
            vel = rigidBody.velocity;
            if (vel.y < 0 && vel.y > -10)
            {
			    rigidBody.velocity = new Vector3 (vel.x - 1, vel.y - verticalCorrection, vel.z);
            }

            else if (vel.y > 0 && vel.y < 10)
            {
			    rigidBody.velocity = new Vector3 (vel.x - 1, vel.y + verticalCorrection, vel.z);
            }

            else
			    rigidBody.velocity = new Vector3 (vel.x - 1, vel.y, vel.z);

        }
        if (rigidBody.velocity.x <= -horizontalSpeedConstraint)
        {
            vel = rigidBody.velocity;
            if (vel.y < 0 && vel.y > -10)
            {
			    rigidBody.velocity = new Vector3 (vel.x + 1, vel.y - verticalCorrection, vel.z);
            }

            else if (vel.y < 0 && vel.y < 10)
            {
			    rigidBody.velocity = new Vector3 (vel.x + 1, vel.y + verticalCorrection, vel.z);
            }

            else
			    rigidBody.velocity = new Vector3 (vel.x + 1, vel.y, vel.z);
        }

        //Check if horizontal velocity is 0, if so add some
        if (Mathf.Abs(rigidBody.velocity.x) <= horizontalSpeedMin)
        {
            vel = rigidBody.velocity;
            int ran = Random.Range(-1, 1);
            rigidBody.velocity = new Vector3 (vel.x + (ran * randomSpeedAdd), vel.y, vel.z);

        }

        if (Mathf.Abs(rigidBody.velocity.z) <= horizontalSpeedMin)
        {
            vel = rigidBody.velocity;
            int ran = Random.Range(-1, 1);
            rigidBody.velocity = new Vector3 (vel.x, vel.y, vel.z + (ran * randomSpeedAdd));

        }
	}


	public void Reset(){
		rigidBody.velocity = Vector3.zero;
		spin = Vector3.zero;

	}

	void OnCollisionEnter(Collision coll)
    {
		if (coll.gameObject.CompareTag ("Paddle")) 
        {
            GameControl.gc.addScore(10);

			//spin = coll.gameObject.GetComponent<Controller>().velocity;
			//coll.gameObject.GetComponent<BallScript> ().CmdSpin (velocity);
			//Debug.LogError (audio == null);
			//audio.pitch = audio.pitch + 0.03f;
			//audio.Play ();
		}

	}
}
