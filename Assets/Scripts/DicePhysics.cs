using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePhysics : MonoBehaviour {

	static Rigidbody rb;
	public static Vector3 diceVelocity;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;

		if (Input.GetKeyDown (KeyCode.Space)) {
			float dirX = Random.Range (0, 100);
			float dirY = Random.Range (0, 100);
			float dirZ = Random.Range (0, 100);
			transform.rotation = Quaternion.identity;
			rb.AddForce (transform.up * 200);
            rb.AddForce (transform.forward * 100);
			rb.AddTorque (dirX, dirY, dirZ);
		}
	}
}
