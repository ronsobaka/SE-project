using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfTurnActions : MonoBehaviour
{
    public TextMeshProUGUI DecisionText;
    private int currentPlayer;
    private int[] playerPositions;

    void Start() {
        DecisionText.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public static void decideAction() {
        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        DecisionText.GetComponent<TextMeshProUGUI>().enabled = true;  

        if (GameController.boardData[playerPositions[currentPlayer], 2] != ""){

                      
            
            DecisionText.text = "You landed on " + GameController.boardData[playerPositions[currentPlayer], 1] + " This costs " + GameController.boardData[playerPositions[currentPlayer], 5] + "<br><br>Press b to buy or a to auction.";
        } 
    }
}
