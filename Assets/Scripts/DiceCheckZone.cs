using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
	public GameObject dice1;
	public GameObject dice2;
	public GameObject counter;
	private Vector3 dice1Velocity;
	private Vector3 dice2Velocity;
	public static int dice1Result;
	public static int dice2Result;
	public static int diceTotal;
	public static float delay = 0.5f;
	private Rigidbody counterRB;
	public float moveSpeed = 3f;
	private Vector3 moveDistance;
	private Vector3 endPosition;
	private int moves = 0;
	private bool triggerActive;
	
	void Start() {
		counterRB = counter.GetComponent<Rigidbody>();
		endPosition = counterRB.transform.position;
		triggerActive = false;
	}

	// Update is called once per frame
	void Update () {
		dice1Velocity = DicePhysics.dice1Velocity;
		dice2Velocity = DicePhysics.dice2Velocity;
		if (Input.GetKeyDown (KeyCode.Space)) {	
			StartCoroutine(buffer());
		}
		if (counterRB.transform.position != endPosition){
			moveCounter();
		}

		if (dice1Velocity.x == 0f && dice1Velocity.y == 0f && dice1Velocity.z == 0f && dice2Velocity.x == 0f && dice2Velocity.y == 0f && dice2Velocity.z == 0f && triggerActive) {
			checkDice();
		}
	}

	void checkDice() {	
		dice1Result = 0;

		//Check dice1
		float dice1MaxDot = -1f;
		
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

		float dice2MaxDot = -1f;

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


		diceTotal = dice1Result + dice2Result;
		triggerActive = false;
		StartCoroutine(MoveCounterCoroutine(diceTotal));
	}

	IEnumerator buffer() {
		yield return new WaitForSeconds(0.1f);
		triggerActive = true;
	}

	IEnumerator MoveCounterCoroutine(int movesToMake) {
    	for (int i = 0; i < movesToMake; i++) {
        	UpdatedMoveDistance();
            endPosition += moveDistance;
            moves++;

            // Rotate the counter every 10 moves
            if ((moves % 10) == 0) {
                counterRB.transform.Rotate(Vector3.up * 90);
            }

            // Move the counter to the new position
            while (counterRB.transform.position != endPosition) {
                moveCounter();
                yield return null; // Wait for the next frame
            }

            // Wait for a specified delay before the next move
            yield return new WaitForSeconds(delay);

			if (moves == 40) {
			moves = 0;
			endPosition = counterRB.transform.position;
		}
		}
	}

	void UpdatedMoveDistance() {
		if ( moves == 0 ) {
			moveDistance = new Vector3(-4.3f,0,0);
		}else if (moves >= 1 && moves <= 9) {
			moveDistance = new Vector3(-3.1f,0,0);
		}else if (moves == 10) {
			moveDistance = new Vector3 (0,0,4.3f);
		}else if (moves >= 11 && moves <= 19) {
			moveDistance = new Vector3(0,0,3.1f);
		}else if ( moves == 20) {
			moveDistance = new Vector3 (4.3f,0,0);
		}else if (moves >= 21 && moves <= 29) {
			moveDistance = new Vector3(3.1f,0,0);
		}else if ( moves == 30) {
			moveDistance = new Vector3 (0,0,-4.3f);
		}else if (moves >= 31 && moves <= 39) {
			moveDistance = new Vector3(0,0,-3.1f);
		}
	}
	void moveCounter() {
		
		if (counterRB.transform.position != endPosition) {
			counterRB.transform.position = Vector3.MoveTowards(counterRB.transform.position, endPosition, moveSpeed * Time.deltaTime);
		}
	}
}


