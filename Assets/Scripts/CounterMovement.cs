using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMovement : MonoBehaviour
{
	private static float delay = 0.1f;
	public static Rigidbody counterRB;
	private float moveSpeed = 10f;
	private Vector3 endPosition;
	private Vector3 moveDistance;
	private int moves;
	public static bool moveCounterTrigger = false;
	public static int[]  playerPositions;
	
	void Start() {
		playerPositions = new int[6];
	}
	
	// Update is called once per frame
	void Update () {

		if (moveCounterTrigger) {
			endPosition = counterRB.transform.position;
			moves = playerPositions[GameController.currentPlayer];
			StartCoroutine(MoveCounterCoroutine(DiceRoll.diceTotal));
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
                moveOneStep();
                yield return null; // Wait for the next frame
            }

            // Wait for a specified delay before the next move
            yield return new WaitForSeconds(delay);

			
		}
		if (moves == 40) {
			playerPositions[GameController.currentPlayer] = 0;
		} else {
			playerPositions[GameController.currentPlayer] += movesToMake;
		}
		for (int j = 0; j < 6; j++) {
			Debug.Log(playerPositions[j]);
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
	void moveOneStep() {
		
		if (counterRB.transform.position != endPosition) {
			counterRB.transform.position = Vector3.MoveTowards(counterRB.transform.position, endPosition, moveSpeed * Time.deltaTime);
		}
	}
}


