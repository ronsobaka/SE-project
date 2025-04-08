using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banking : GameController {

    private static int[] playerBalances;
    private static int bankBalance;
    public TextMeshProUGUI[] moneyTexts;
   
    public static void setUpBank() {
        int humanPlayers = getHumanPlayers();

        bankBalance = 50000;

        playerBalances = new int[humanPlayers];
        for (int i = 0; i < humanPlayers; i++) {    
            playerBalances[i] = 1500;
        }

        bankBalance -= (humanPlayers * 1500);
    }

    //Takes from player 1 and gives to player 2
    public static void playerToPlayerTransfer(int player1, int player2, int amount) {
        playerBalances[player1] -= amount;
        playerBalances[player2] += amount;

        logBalances();
    }

    public static void playetToBankTransfer(int player, int amount) {
        bankBalance += amount;
        playerBalances[player] -= amount;
        logBalances();
    }

    public static void logBalances() {
        for (int i = 0; i < playerBalances.Length - 1; i++) {
            Debug.Log("Player: " + i + " has " + playerBalances[i]);
        }
    }

    
    public static int GetPlayerBalance(int playerIndex)
    {
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

}
