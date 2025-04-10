using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banking : MonoBehaviour {

    private static int[] playerBalances;
    private static int bankBalance;
    private static int freeParkingBalance;
   
   public TextMeshProUGUI[] moneyTexts;

    public static void setUpBank() {

        int humanPlayers = GameController.getHumanPlayers();

        bankBalance = 50000;

        playerBalances = new int[humanPlayers];
        for (int i = 0; i < humanPlayers; i++) {    
            playerBalances[i] = 1500;
        }

        bankBalance -= (humanPlayers * 1500);

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
        
    }

    //Takes from player 1 and gives to player 2
    public static void playerToPlayerTransfer(int player1, int player2, int amount) {
        playerBalances[player1] -= amount;
        playerBalances[player2] += amount;

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
    }

    public static void playerToBankTransfer(int player, int amount) {
        bankBalance += amount;
        playerBalances[player] -= amount;

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
    }

    public static void bankToPlayerTransfer(int player, int amount) {
        bankBalance -= amount;
        playerBalances[player] += amount;

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
    }

    public static int GetPlayerBalance(int playerIndex){
        return playerBalances[playerIndex];
    }

    private void UpdateMoneyUI() {

        for (int j = 0; j < moneyTexts.Length; j++)
        {
            if (j < playerBalances.Length && moneyTexts[j] != null)
            {
                moneyTexts[j].text = "£" + playerBalances[j].ToString();
            }
        }
    }

    public static bool checkBalance(int player, int amount) {
        return (playerBalances[player] >= amount);
    }

    public static void addMoneyToFreeParking(int player, int amount) {
        playerBalances[player] -= amount;
        freeParkingBalance += amount;

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
    }

    public static void takeFreeParking(int player) {
        playerBalances[player] += freeParkingBalance;
        freeParkingBalance = 0;

        Banking bankingInstance = FindObjectOfType<Banking>();
        if (bankingInstance != null) {
            bankingInstance.UpdateMoneyUI();
        }
    }

}
