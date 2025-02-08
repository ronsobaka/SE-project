using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
Vector3 diceVelocity;
public static int diceLandedNumber = 0;

	// Update is called once per frame
	void FixedUpdate () {
		diceVelocity = DicePhysics.diceVelocity;
	}

	void OnTriggerStay(Collider col)
	{
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
		{
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
		}
	}
}

