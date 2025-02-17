using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePhysics : MonoBehaviour {
	public GameObject dice2;
	public GameObject dice1;
	static Rigidbody dice1RB;
	static Rigidbody dice2RB;
	public static Vector3 dice1Velocity;
	public static Vector3 dice2Velocity;

	// Use this for initialization
	void Start () {
		dice1RB = dice1.GetComponent<Rigidbody> ();
		dice1RB.useGravity = false;
		dice2RB = dice2.GetComponent<Rigidbody> ();
		dice2RB.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		dice1Velocity = dice1RB.velocity;
		dice2Velocity = dice2RB.velocity;

		if (Input.GetKeyDown (KeyCode.Space)) {
			dice1RB.useGravity = true;
			float dirX = Random.Range (0, 100);
			float dirY = Random.Range (0, 100);
			float dirZ = Random.Range (0, 100);
			dice1RB.AddForce (transform.up * 200);
            dice1RB.AddForce (transform.forward * 100);
			dice1RB.AddTorque (dirX, dirY, dirZ);

			dice2RB.useGravity = true;
			dirX = Random.Range (0, 100);
			dirY = Random.Range (0, 100);
			dirZ = Random.Range (0, 100);
			dice2RB.AddForce (transform.up * 200);
            dice2RB.AddForce (transform.forward * 100);
			dice2RB.AddTorque (dirX, dirY, dirZ);
		}

		
	}
}
