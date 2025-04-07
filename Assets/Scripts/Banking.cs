using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banking : GameController {

    private static int[] playerBalances;
    private static int bankBalance;
   

    void Start() {

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
    }

    //public static void playetToBankTransfer()
}
