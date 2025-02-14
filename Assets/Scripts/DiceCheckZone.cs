using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
	public GameObject counter;
	Vector3 diceVelocity;
	public static int diceLandedNumber = 0;
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
		diceVelocity = DicePhysics.diceVelocity;
		if (Input.GetKeyDown (KeyCode.Space)) {	
			StartCoroutine(buffer());
		}
		if (counterRB.transform.position != endPosition){
			moveCounter();
		}
	}

	IEnumerator buffer() {
		yield return new WaitForSeconds(0.1f);
		triggerActive = true;
	}

	void OnTriggerStay(Collider col)
	{
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && triggerActive) {
			switch (col.gameObject.name) {
			case "Side1":
				diceLandedNumber = 6;
				break;
			case "Side2":
				diceLandedNumber = 5;
				break;
			case "Side3":
				diceLandedNumber = 4;
				break;
			case "Side4":
				diceLandedNumber = 3;
				break;
			case "Side5":
				diceLandedNumber = 2;
				break;
			case "Side6":
				diceLandedNumber = 1;
				break;
			
			}
			StartCoroutine(MoveCounterCoroutine(diceLandedNumber));
			triggerActive = false;
		}
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
		Debug.Log(moves);
    }

	void UpdatedMoveDistance() {
		if ( moves == 0 ) {
			moveDistance = new Vector3(-4.3f,0,0);
		}else if (moves >= 2 && moves <= 9) {
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


