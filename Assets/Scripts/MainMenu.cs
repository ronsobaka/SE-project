using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame( int numberOfPlayers)
    {
        GameController.humanPlayers = numberOfPlayers;
        
        SceneManager.LoadSceneAsync(1);
    }

}
