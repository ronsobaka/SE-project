using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMovement : MonoBehaviour
{
	private static float delay = 0.25f;
	public static Rigidbody counterRB;
	private float moveSpeed = 5;
	private Vector3 endPosition;
	private Vector3 moveDistance;
	private int moves;
	public static bool moveCounterTrigger = false;
	private bool firstGo;
	
	private int NumberDoublesRolled;
	
	void Start() {
		GameController.playerPositions = new int[6];
		firstGo = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (moveCounterTrigger) {
			endPosition = counterRB.transform.position;
			moves = GameController.playerPositions[GameController.currentPlayer];
			StartCoroutine(MoveCounterCoroutine(DiceRoll.diceTotal));
			moveCounterTrigger = false;
			
		}
	}

	

	public IEnumerator MoveCounterCoroutine(int movesToMake) {

		bool forwards = !(movesToMake < 0);

    	for (int i = 0; i < movesToMake; i++) {

        	UpdateMoveDistance();
            endPosition += moveDistance;

			if (forwards) {
				moves++;
			} else {
				moves--;
			}
			
			
			

            // Rotate the counter every 10 moves
            if ((forwards) && (moves % 10) == 0) {
                counterRB.transform.Rotate(Vector3.up * 90);
            } else {
				counterRB.transform.Rotate(Vector3.up * 270);
			}

            // Move the counter to the new position
            while (counterRB.transform.position != endPosition) {
                moveOneStep();
                yield return null; // Wait for the next frame
            }

            // Wait for a specified delay before the next move
            yield return new WaitForSeconds(delay);
			//If lands on go to jail square
			if ((moves == 30) && (i == (movesToMake - 1))) {

				StartCoroutine(sendPlayerToJail());
				
			} else if ((moves == 40) && (forwards)) {
				moves = 0;
			} else if ((moves == 0) && (!forwards)) {
				moves = 0;
			}
			
		}			
		
		//Updates Gamecontroller of the player position
		GameController.playerPositions[GameController.currentPlayer] = moves;

		//Double Event 
		if (DiceRoll.doubleRolled) {
			NumberDoublesRolled++;
			if (NumberDoublesRolled == 3) {
				StartCoroutine(sendPlayerToJail());
			}
		} else {;
			NumberDoublesRolled = 0;
		}
		EndOfTurnActions.decideAction();
		GameController.currentPlayer++;

		if (GameController.currentPlayer == (GameController.humanPlayers)){
			GameController.currentPlayer = 0;
			firstGo = false;
		}
		
	}


	IEnumerator sendPlayerToJail() {
		moves = 10;
		GameController.playerPositions[GameController.currentPlayer] = 10;
		GameController.playersInJail[GameController.currentPlayer] = true;
		counterRB.transform.Rotate(Vector3.up * 180);
		UpdateMoveDistance();

		while (counterRB.transform.position != endPosition) {
			moveOneStep();
			yield return null; // Wait for the next frame
		}
	}

	public void UpdateMoveDistance() {

		if ((moves == 0) && (!firstGo)) {
			Banking.bankToPlayerTransfer(GameController.currentPlayer, 200);
		} else if (moves == 20) {
			Banking.takeFreeParking(GameController.getCurrentPlayer());
		}

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

				GameController.turnsInJail[GameController.currentPlayer] += 1;
				
				if(GameController.turnsInJail[GameController.currentPlayer] == 2) {
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
			GameController.playerPositions[GameController.currentPlayer] = 0;
			
		}
	}
	void moveOneStep() {
		
		if (counterRB.transform.position != endPosition) {
			counterRB.transform.position = Vector3.MoveTowards(counterRB.transform.position, endPosition, moveSpeed * Time.deltaTime);
		}
	}
}


