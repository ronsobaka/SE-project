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
    public static GameObject[] playerObjects;


    public static void initializeVariables() {
        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        currentPosition = playerPositions[currentPlayer];
        boardData = GameController.getBoardData();
        
        for (int i = 0; i < GameController.humanPlayers; i++){
            playerObjects[i] = GameObject.FindGameObjectWithTag($"Player{i}");
        }
        
        
    }

    public static void decideAction() {

        

        string currentTileGroup = boardData[currentPosition, 2];


        if (currentTileGroup != "") {

            if (boardData[currentPosition, 13] != null) {   //Check if property is already owned
                
                int tileOwner = int.Parse(boardData[currentPosition, 13]);
                int rentAmount = int.Parse(boardData[currentPosition, 6]);
                Banking.playerToPlayerTransfer(currentPlayer, tileOwner, rentAmount);
                GameController.setTurnComplete(true);

            } else {   //not owned so can buy so show user options

                PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();
                pop.popUpCard(currentTileGroup);
            }

            
            
        } else {
            GameController.turnComplete = true; //Temporary needs changeing for other tiles
        }
    }

    public static void buyProperty() {
        Banking.playerToBankTransfer(currentPlayer, int.Parse(boardData[currentPosition, 5]));
        boardData[currentPosition, 13] = currentPlayer.ToString();
        updateUICards();
        GameController.setTurnComplete(true);
    }

    public static void auctionBuyProperty(int player) {
        int temp = currentPlayer;
        currentPlayer = player;
        buyProperty();
        currentPlayer = temp;
    }

    public static int getCurrentPosition() {
        return currentPosition;
    }

    public void setCurrentPlayer(int gPlayer) {
        currentPlayer = gPlayer;
    }

    public static void updateUICards() {
        string propertyName = boardData[currentPosition, 1];
        string propertyGroup = boardData[currentPosition, 2];
        //PopUps.setSprite(propertyGroup, player1.transform.Find(propertyName).GetComponent<Image>());

    }
}
