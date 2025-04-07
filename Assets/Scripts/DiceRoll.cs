using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DiceRoll : MonoBehaviour {
	public GameObject dice2;
	public GameObject dice1;
	private Rigidbody dice1RigidBody;
	private Rigidbody dice2RigidBody;
	private Vector3 dice1Velocity;
	private Vector3 dice2Velocity;
	private bool diceDetectTrigger;
	public static int diceTotal;	
	public static bool doubleRolled = false;

	// Use this for initialization
	void Start () {

		//get and set dice to static
		dice1RigidBody = dice1.GetComponent<Rigidbody> ();
		dice1RigidBody.useGravity = false;
		dice2RigidBody = dice2.GetComponent<Rigidbody> ();
		dice2RigidBody.useGravity = false;

		diceDetectTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		dice1Velocity = dice1RigidBody.velocity;
		dice2Velocity = dice2RigidBody.velocity;

		//gives both dice a random force and torque

		if (Input.GetKeyDown(KeyCode.Space) && GameController.turnComplete) {
			rollDice();
		}
		
		//conditions for both dice being still and wanting to check

		if (dice1Velocity.x == 0f && dice1Velocity.y == 0f && dice1Velocity.z == 0f && dice2Velocity.x == 0f && dice2Velocity.y == 0f && dice2Velocity.z == 0f && diceDetectTrigger) {
			checkDice();
		}

		if (Input.GetKeyDown (KeyCode.Space) && GameController.turnComplete) {	
			StartCoroutine(buffer());
		}
		
	}

	IEnumerator buffer() {
		yield return new WaitForSeconds(0.1f);
		diceDetectTrigger = true;
		GameController.turnComplete = false;
	}

	void rollDice() {
		dice1RigidBody.useGravity = true;
		float dirX = Random.Range (0, 100);
		float dirY = Random.Range (0, 100);
		float dirZ = Random.Range (0, 100);
		dice1RigidBody.AddForce (transform.up * 200);
		dice1RigidBody.AddForce (transform.forward * 100);
		dice1RigidBody.AddTorque (dirX, dirY, dirZ);
		dice2RigidBody.useGravity = true;
		dirX = Random.Range (0, 100);
		dirY = Random.Range (0, 100);
		dirZ = Random.Range (0, 100);
		dice2RigidBody.AddForce (transform.up * 200);
		dice2RigidBody.AddForce (transform.forward * 100);
		dice2RigidBody.AddTorque (dirX, dirY, dirZ);
	}


	void checkDice() {	
		int dice1Result = 0;
		float dice1MaxDot = -1f;

		int dice2Result = 0;
		float dice2MaxDot = -1f;
		
		doubleRolled = false;

		//Check dice1
		
		
		if(Vector3.Dot(dice1.transform.up, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(dice1.transform.up, Vector3.up);
			dice1Result = 2;
		}
		
		if(Vector3.Dot(-dice1.transform.up, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(-dice1.transform.up, Vector3.up);
			dice1Result = 5;
		}
		
		if(Vector3.Dot(dice1.transform.forward, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(dice1.transform.forward, Vector3.up);
			dice1Result = 1;
		}
		
		if(Vector3.Dot(-dice1.transform.forward, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(-dice1.transform.forward, Vector3.up);
			dice1Result = 6;
		}
		
		if(Vector3.Dot(dice1.transform.right, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(dice1.transform.right, Vector3.up);
			dice1Result = 4;
		}
		
		if(Vector3.Dot(-dice1.transform.right, Vector3.up) > dice1MaxDot) {
			dice1MaxDot = Vector3.Dot(-dice1.transform.right, Vector3.up);
			dice1Result = 3;
		}
		//Check dice2

		

		if(Vector3.Dot(dice2.transform.up, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(dice2.transform.up, Vector3.up);
			dice2Result = 2;
		}
		
		if(Vector3.Dot(-dice2.transform.up, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(-dice2.transform.up, Vector3.up);
			dice2Result = 5;
		}
		
		if(Vector3.Dot(dice2.transform.forward, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(dice2.transform.forward, Vector3.up);
			dice2Result = 1;
		}
		
		if(Vector3.Dot(-dice2.transform.forward, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(-dice2.transform.forward, Vector3.up);
			dice2Result = 6;
		}
		
		if(Vector3.Dot(dice2.transform.right, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(dice2.transform.right, Vector3.up);
			dice2Result = 4;
		}
		
		if(Vector3.Dot(-dice2.transform.right, Vector3.up) > dice2MaxDot) {
			dice2MaxDot = Vector3.Dot(-dice2.transform.right, Vector3.up);
			dice2Result = 3;
		}

		if (dice1Result == dice2Result) {
			doubleRolled = true;
		}
		diceTotal = dice1Result + dice2Result;
		diceTotal = 1;
		diceDetectTrigger = false;


		CounterMovement.moveCounterTrigger = true;
	}
}
