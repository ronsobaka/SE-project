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
    public static GameObject player;
    public static GameObject[] playerObjects;

    public static void decideAction() {

        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        currentPosition = playerPositions[currentPlayer];
        boardData = GameController.boardData;

        playerObjects = new GameObject[GameController.humanPlayers + 1];
        
        for (int i = 0; i < playerObjects.Length - 1; i++) {
            player = GameObject.FindGameObjectWithTag($"Player{i+1}");
            playerObjects[i] = player;
        }

        PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();

        string currentTileGroup = boardData[currentPosition, 2];


        if (currentTileGroup != "") {

            if (boardData[currentPosition, 13] != null) {   //Check if property is already owned
                
                if ((currentPosition == 5) || (currentPosition == 15) || (currentPosition == 25) || (currentPosition == 35)){
                    
                    //Add station rents here (below is temporary)

                    int tileOwner = int.Parse(boardData[currentPosition, 13]);
                    Banking.playerToPlayerTransfer(currentPlayer, tileOwner, 50);
                    
                } else {
                    int tileOwner = int.Parse(boardData[currentPosition, 13]);
                    int rentAmount = int.Parse(boardData[currentPosition, 6]);
                    Banking.playerToPlayerTransfer(currentPlayer, tileOwner, rentAmount);
                    
                }

                GameController.setTurnComplete(true);
                

            } else {   //not owned so can buy so show user options

                pop.popUpCard(currentTileGroup);
            }

        } else if ((currentPosition == 4) || (currentPosition == 38)) {
            
            pop.popUpCard("tax");

            if (currentPosition == 4) {
                Banking.addMoneyToFreeParking(currentPlayer, 200);
            } else {
                Banking.addMoneyToFreeParking(currentPlayer, 100);
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
        string propertyGroup;

        for (int i = 0; i < playerObjects.Length - 1; i++) {
            if (i == currentPlayer) {
                propertyGroup = boardData[currentPosition, 2];
            } else {
                propertyGroup = "Gray";
            }
            PopUps.setSprite(propertyGroup, playerObjects[i].transform.Find(propertyName).GetComponent<Image>());
        }
        

    }
}
