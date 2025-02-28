using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfTurnActions : MonoBehaviour
{
    private static int currentPlayer;
    private static int[] playerPositions;

    //public TextMeshProUGUI decisionOutput;
    //public static TextMeshProUGUI staticDecisionOutput;

    void Start() {
        //decisionOutput.enabled = false;
        //staticDecisionOutput = decisionOutput;
    }

    //void Update() {
        //decisionOutput.text = staticDecisionOutput.text;
    //}

    public static void decideAction() {

        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        //staticDecisionOutput.enabled = true;

        string currentTileData = GameController.boardData[playerPositions[currentPlayer], 2];  

        if (currentTileData != ""){

            //staticDecisionOutput.text = "You landed on " + GameController.boardData[playerPositions[currentPlayer], 1] + " This costs " + GameController.boardData[playerPositions[currentPlayer], 5] + "<br><br>Press b to buy or a to auction.";
            PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();
            pop.popUpCard(currentTileData);
        }
    }
}
