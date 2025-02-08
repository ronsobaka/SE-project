using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMove : MonoBehaviour
{
    public float moveSpeed = 3.5f;  // Movement speed via velocity
    public float moveDelay = 4f; // Delay
    private bool isMoving = false;
    private Rigidbody rb;  // Rigidbody for velocity-based movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component
    }

    void Update()
    {
        // Check if the dice has landed and the object is not already moving
        if (DiceCheckZone.diceLandedNumber > 0 && !isMoving)
        {
            // Moving the object
            StartCoroutine(MoveObjectForward(DiceCheckZone.diceLandedNumber));
        }
    }

    IEnumerator MoveObjectForward(int steps)
    {
        isMoving = true;

        // Delay to make sure dice isn't moving immediately
        yield return new WaitForSeconds(moveDelay);

        Vector3 moveDirection = transform.forward * moveSpeed;

        // Apply velocity to Rigidbody for the movement
        rb.velocity = moveDirection;

        
        yield return new WaitForSeconds(1.0f);  // Adjust this for how long you want it to move

        // Stop the Rigidbody velocity after movement
        rb.velocity = Vector3.zero;

        // Reset dice value so the movement only happens once
        DiceCheckZone.diceLandedNumber = 0;

        isMoving = false;
    }
}