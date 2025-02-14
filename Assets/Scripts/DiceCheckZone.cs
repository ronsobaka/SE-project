using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
	public GameObject counter;
	Vector3 diceVelocity;
	public static int diceLandedNumber = 0;
	public static float delay = 1f;
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
			for(int i = 0; i < diceLandedNumber; i++) {
				UpdatedMoveDistance();
				endPosition += moveDistance;
				moves++;
				
				if ((moves % 10) == 0){
					counterRB.transform.Rotate(Vector3.up * 90);
				}
				if (moves > 40) {
					moves = 0;
					endPosition = counterRB.transform.position;
				}
			}
			triggerActive = false;
		}
	}


	void UpdatedMoveDistance() {
		if ( moves == 1 ) {
			moveDistance = new Vector3(-4.3f,0,0);
		}else {
			moveDistance = new Vector3(-3.1f,0,0);
		}
	}
	void moveCounter() {
		
		if (counterRB.transform.position != endPosition) {
			counterRB.transform.position = Vector3.MoveTowards(counterRB.transform.position, endPosition, moveSpeed * Time.deltaTime);
		}
	}
}


