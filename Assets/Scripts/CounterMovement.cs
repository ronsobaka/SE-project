using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMovement : MonoBehaviour
{
	private static float delay = 0.00001f;
	public static Rigidbody counterRB;
	private float moveSpeed = 20f;
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
			Debug.Log(GameController.currentPlayer);
			Debug.Log(playerPositions[GameController.currentPlayer]);
			

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

			if ((moves == 30) && (i == (movesToMake - 1))) {
				Debug.Log("huh");
				moves = 10;
				playerPositions[GameController.currentPlayer] = 10;
				GameController.playersInJail[GameController.currentPlayer] = true;
				counterRB.transform.Rotate(Vector3.up * 180);
				UpdateMoveDistance();

				while (counterRB.transform.position != endPosition) {
                	moveOneStep();
                	yield return null; // Wait for the next frame
            	}
			}
			
		}			
		
		playerPositions[GameController.currentPlayer] = moves;	
		GameController.turnComplete = true;
	}

	void UpdateMoveDistance() {
		if (moves >= 0 && moves <= 8) {

			moveDistance = new Vector3(-3.2f,0,0);

		}else if (moves == 9) {

			moveDistance = new Vector3(0,0,0);

			if (GameController.currentPlayer == 1) {
				endPosition = new Vector3(-19.5f, 0.9f, -15f);
			} else if (GameController.currentPlayer == 2) {
				endPosition = new Vector3(-19.5f, 0.9f, -16.5f);
			} else if (GameController.currentPlayer == 3) {
				endPosition = new Vector3(-19.5f, 0.9f, -18f);
			} else if (GameController.currentPlayer == 4) {
				endPosition = new Vector3(-19.5f, 0.9f, -19f);
			} else if (GameController.currentPlayer == 5) {
				endPosition = new Vector3(-17.5f, 0.9f, -19f);
			} else if (GameController.currentPlayer == 0) {
				endPosition = new Vector3(-15.5f, 0.9f, -19f);
			}
		}else if (moves == 10) {

			moveDistance = new Vector3(0,0,0);

			if (GameController.playersInJail[GameController.currentPlayer]){

				if (GameController.currentPlayer == 1) {
					endPosition = new Vector3(-17f, 0.9f, -17.5f);
				}else if (GameController.currentPlayer == 2) {
					endPosition = new Vector3(-17f, 0.9f, -16.5f);
				} else if (GameController.currentPlayer == 3) {
					endPosition = new Vector3(-17f, 0.9f, -15.5f);
				} else if (GameController.currentPlayer == 4) {
					endPosition = new Vector3(-16f, 0.9f, -17.5f);
				} else if (GameController.currentPlayer == 5) {
					endPosition = new Vector3(-16f, 0.9f, -16.5f);
				} else if (GameController.currentPlayer == 0) {
					endPosition = new Vector3(-16f, 0.9f, -15.5f);
				}

			} else {

				if (GameController.currentPlayer == 1) {
					moveDistance = new Vector3(0,0,3.1f);
				}else if (GameController.currentPlayer == 2) {
					moveDistance = new Vector3(0,0,3.1f);
				} else if (GameController.currentPlayer == 3) {
					endPosition = new Vector3(-18.5f, 0.9f, -11.9f);
				} else if (GameController.currentPlayer == 4) {
					endPosition = new Vector3(-18.5f, 0.9f, -13.4f);
				} else if (GameController.currentPlayer == 5) {
					endPosition = new Vector3(-17.5f, 0.9f, -11.9f);
				} else if (GameController.currentPlayer == 0) {
					endPosition = new Vector3(-17.5f, 0.9f, -13.4f);
				}

			}
			
		}else if (moves >= 11 && moves <= 18) {
			moveDistance = new Vector3(0,0,3.1f);
		}else if (moves == 19) {
			moveDistance = new Vector3(0,0,6.5f);
		}else if ((moves == 20) && ((GameController.currentPlayer == 1) || (GameController.currentPlayer == 2))) {
			moveDistance = new Vector3(0,0,0);
			if (GameController.currentPlayer == 1) {
				endPosition = new Vector3(-12f, 0.9f, 16.4f);
			} else {
				endPosition = new Vector3(-13f, 0.9f, 16.4f);
			}
		}else if (moves == 20) {
			moveDistance = new Vector3 (5.5f,0,0);
		}else if (moves >= 21 && moves <= 29) {
			moveDistance = new Vector3(3.1f,0,0);
		}else if ( moves == 30) {
			moveDistance = new Vector3 (0,0,-4.3f);
		}else if (moves >= 31 && moves <= 39) {
			moveDistance = new Vector3(0,0,-3.1f);
		} else if (moves == 40) {
			moveDistance = new Vector3(0,0,0);
			endPosition = GameController.startPositions[GameController.currentPlayer];
			playerPositions[GameController.currentPlayer] = 0;
			moves = 0;
		}
	}
	void moveOneStep() {
		
		if (counterRB.transform.position != endPosition) {
			counterRB.transform.position = Vector3.MoveTowards(counterRB.transform.position, endPosition, moveSpeed * Time.deltaTime);
		}
	}
}


