using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] counterPrefabs;
    public GameObject[] playerCounters;
    public static Vector3[] startPositions;
    public static bool[] playersInJail;
    public static int[] turnsInJail;
    public static int  currentPlayer;
    public static int humanPlayers;
    public static bool turnComplete = true;
    public static string[,] boardData;
    public static string[,] cardData;
    public static int[]  playerPositions;
    public static bool turnActionsTrigger = false;
    public GameObject[] playerUIImages;
    public static int[] counterChoices;
    
    void Start() {
        setStartPostitions();
        
       
        playersInJail = new bool[6];
        playerCounters = new GameObject[humanPlayers];
        
        currentPlayer = 0;
        for (int i = 0; i < humanPlayers; i++) {
            playerCounters[i] = (GameObject)Instantiate(counterPrefabs[counterChoices[i] - 1], startPositions[i], Quaternion.identity) as GameObject;
        }
            for (int j = 0; j < playerUIImages.Length; j++) {
            playerUIImages[j].SetActive(j < humanPlayers);
        }

        Banking.setUpBank();
    }

    void Update() {
        if ((Input.GetKeyDown(KeyCode.Space)) && turnComplete) {	
			CounterMovement.counterRB = playerCounters[currentPlayer].GetComponent<Rigidbody>();
		}
    }

    void setStartPostitions() {
        startPositions = new Vector3[6];
        startPositions[1] = new Vector3(15.5f, 0.2f, -19f);
        startPositions[2] = new Vector3(15.5f, 0.2f, -18f);
        startPositions[3] = new Vector3(15.5f, 0.2f, -17f);
        startPositions[4] = new Vector3(16.75f, 0.2f, -19f);
        startPositions[5] = new Vector3(16.75f, 0.2f, -18f);
        startPositions[0] = new Vector3(16.75f, 0.2f, -17f);
    }

    public static void buyProperty(int tileNumber, int player, int amount) {
        boardData[tileNumber, 13] = player.ToString();
        Banking.playerToBankTransfer(player, amount);
    }

    //Setters

    public static void setTurnComplete(bool givenBool) {
        turnComplete = givenBool;
    }

    //Getters

    public static int getHumanPlayers() {
        return humanPlayers;
    }

    public static string[,] getBoardData() {
        return boardData;
    }

    public static int getCurrentPlayer() {
        return currentPlayer;
    }

}
