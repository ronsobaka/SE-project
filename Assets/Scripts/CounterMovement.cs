using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMovement : MonoBehaviour
{
	public GameObject counter;
	private static float delay = 0.5f;
	private Rigidbody counterRB;
	private float moveSpeed = 3f;
	private Vector3 moveDistance;
	private Vector3 endPosition;
	private int moves = 0;
	public static bool moveCounterTrigger = false;
	
	
	void Start() {
		counterRB = counter.GetComponent<Rigidbody>();
		endPosition = counterRB.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (counterRB.transform.position != endPosition){
			moveCounter();
		}

		if (moveCounterTrigger) {
			Debug.Log("YOu fuckin ediot");
			StartCoroutine(MoveCounterCoroutine(DicePhysics.diceTotal));
			moveCounterTrigger = false;
		}
	}

	

	IEnumerator MoveCounterCoroutine(int movesToMake) {
    	for (int i = 0; i < movesToMake; i++) {
        	UpdateMoveDistance();
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

	void UpdateMoveDistance() {
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


