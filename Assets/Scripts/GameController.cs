using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] playerCounters;
    public static Vector3[] startPositions;
    public static bool[] playersInJail;
    public static int[] turnsInJail;
    public static int  currentPlayer;
    public GameObject counterPrefab;
    public static int humanPlayers;
    public static bool turnComplete = true;
    public static int[] playerBalances;
    public static int bankBalance; 
    public static string[,] boardData;
    public static string[,] cardData;
    public static int[]  playerPositions;
    public static bool turnActionsTrigger = false;
    

    void Start() {
        setStartPostitions();
        setupBalances();
        shuffleCards();
        
       
        playersInJail = new bool[6];
        playerCounters = new GameObject[humanPlayers];
        
        currentPlayer = 0;
        for (int i = 0; i < humanPlayers; i++) {
            playerCounters[i] = (GameObject)Instantiate(counterPrefab, startPositions[i], Quaternion.identity) as GameObject;
        }
    }

    void shuffleCards() {

        System.Random rng = new System.Random();
        
        // Iterate through the cardData in reverse order
        for (int i = 17; i > 0; i--)
        {
            // Random index to swap with
            int j = rng.Next(0, i + 1);

            string temp = cardData[i,0];
            string temp2 = cardData[i,1];
            cardData[i,0] = cardData[j,0];
            cardData[i,1] = cardData[j,1];
            cardData[j,0] = temp;
            cardData[j,1] = temp2;
        }


        for (int i = cardData.GetLength(0) - 1; i > 17; i--)
        {
            // Random index to swap with
            int j = rng.Next(0, i + 1);

            string temp = cardData[i,0];
            string temp2 = cardData[i,1];
            cardData[i,0] = cardData[j,0];
            cardData[i,1] = cardData[j,1];
            cardData[j,0] = temp;
            cardData[j,1] = temp2;
        }
    
    }
    void Update() {
        if ((Input.GetKeyDown(KeyCode.Space)) && turnComplete) {	
			CounterMovement.counterRB = playerCounters[currentPlayer].GetComponent<Rigidbody>();
		}
    }

    void setupBalances() {
        bankBalance = 50000;
        playerBalances = new int[humanPlayers];
        for (int i = 0; i < humanPlayers; i++) {    
            playerBalances[i] = 1500;
        }
        bankBalance -= (humanPlayers * 1500);
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

    public void setTurnComplete(bool givenBool) {
        turnComplete = givenBool;
    }
}
