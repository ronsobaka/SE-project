using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] playerCounters;
    public static Vector3[] startPositions;
    public static bool[] playersInJail;
    public static int[] turnsInJail;
    public static int  currentPlayer;
    public GameObject counterPrefab;
    public static int humanPlayers = 2;
    public static bool turnComplete = true;
    

    void Start() {
        setStartPostitions();
        playersInJail = new bool[6];
        playerCounters = new GameObject[humanPlayers];
        
        currentPlayer = 0;
        for (int i = 0; i < humanPlayers; i++) {
            playerCounters[i] = (GameObject)Instantiate(counterPrefab, startPositions[i], Quaternion.identity) as GameObject;
        }
    }

    void Update() {
        if ((Input.GetKeyDown(KeyCode.Space)) && turnComplete) {	
			CounterMovement.counterRB = playerCounters[currentPlayer].GetComponent<Rigidbody>();
		}
    }


    void setStartPostitions() {
        startPositions = new Vector3[6];
        startPositions[1] = new Vector3(15.5f, 0.9f, -19f);
        startPositions[2] = new Vector3(15.5f, 0.9f, -18f);
        startPositions[3] = new Vector3(15.5f, 0.9f, -17f);
        startPositions[4] = new Vector3(16.75f, 0.9f, -19f);
        startPositions[5] = new Vector3(16.75f, 0.9f, -18f);
        startPositions[0] = new Vector3(16.75f, 0.9f, -17f);
    }
}
