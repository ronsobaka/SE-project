using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfTurnActions : MonoBehaviour
{
    private static int currentPlayer;
    private static int[] playerPositions;
    public Button buyButton;
    public Button auctionButton;


    public static void decideAction() {

        GameController.turnActionsTrigger = false;
        playerPositions = GameController.playerPositions;
        currentPlayer = GameController.currentPlayer;
        string currentTileGroup = GameController.boardData[playerPositions[currentPlayer], 2];  

        if (currentTileGroup != ""){
            // if (GameController.boardData[playerPositions[currentPlayer], 13] != ""){
                //payRent();
            // }
            PopUps pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUps>();
            pop.popUpCard(currentTileGroup);
            
            
        } else {
            GameController.turnComplete = true; //Temporary needs changeing for other tiles
        }
    }

    public void boughtProperty() {
        GameController.playerBalances[currentPlayer] -= int.Parse(GameController.boardData[playerPositions[currentPlayer], 5]);
        Debug.Log(GameController.playerBalances[currentPlayer]);
        GameController.boardData[playerPositions[currentPlayer], 13] = currentPlayer.ToString();
        GameController.turnComplete = true;
    }

    /*public static void payRent() {
        Debug.Log("called");
    }*/
}
