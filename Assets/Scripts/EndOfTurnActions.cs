using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfTurnActions : MonoBehaviour {
    private static int currentPlayer;
    private static int[] playerPositions;
    public Button buyButton;
    public Button auctionButton;
    public static string[,] boardData;
    public static int currentPosition;


    public static void decideAction() {

        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        currentPosition = playerPositions[currentPlayer];
        boardData = GameController.getBoardData();

        string currentTileGroup = boardData[currentPosition, 2];


        if (currentTileGroup != "") {

            if (boardData[currentPosition, 13] != null) {   //Check if property is already owned
                
                int tileOwner = int.Parse(boardData[currentPosition, 13]);
                int rentAmount = int.Parse(boardData[currentPosition, 6]);
                Banking.playerToPlayerTransfer(currentPlayer, tileOwner, rentAmount);

            } else {   //not owned so can buy so show user options

                PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();
                pop.popUpCard(currentTileGroup);

            }

            
            
        } else {
            GameController.turnComplete = true; //Temporary needs changeing for other tiles
        }
    }

    public void boughtProperty() {
        Banking.playetToBankTransfer(currentPlayer, int.Parse(boardData[currentPosition, 5]));
        boardData[currentPosition, 13] = currentPlayer.ToString();
        GameController.turnComplete = true;
    }
}
