using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {

    private int numberOfPlayers;
    private int currentChoser;
    private int[] choices;
    public TextMeshProUGUI choiceText;
    public Button playButton;

    public void Start() {
        playButton.gameObject.SetActive(true);
    }

    public void PlayGame() {
        GameController.humanPlayers = numberOfPlayers;
        GameController.counterChoices = choices;
        SceneManager.LoadSceneAsync(1);
    }

    public void setNumberOfPlayers(int gNumber) {
        numberOfPlayers = gNumber;
        choices = new int[numberOfPlayers];
        currentChoser = 0;
        choiceText.text = (currentChoser + 1).ToString();
    }

    public void setPlayerCounter(int choice) {
        choices[currentChoser] = choice;
        currentChoser++;
        choiceText.text = (currentChoser + 1).ToString();
        if (currentChoser == numberOfPlayers) {
            PlayGame();
        }
    }
}
