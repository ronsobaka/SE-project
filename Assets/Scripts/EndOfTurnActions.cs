using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfTurnActions : MonoBehaviour
{
    private static int currentPlayer;
    private static int[] playerPositions;


    public static void decideAction() {

        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        string currentTileColour = GameController.boardData[playerPositions[currentPlayer], 2];  

        if (currentTileColour != ""){
            PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();
            pop.popUpCard(currentTileColour);
        } else {
            GameController.turnComplete = true;
        }
    }
}
