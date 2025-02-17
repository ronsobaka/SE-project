using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceResult : MonoBehaviour
{
    public TextMeshProUGUI DiceResultText;             

    // Update is called once per frame
    void Update()
    {
        if (DiceCheckZone.diceTotal == 11){
            DiceResultText.text = "You rolled an " + (DiceCheckZone.diceTotal);
        } else {
            DiceResultText.text = "You rolled a " + (DiceCheckZone.diceTotal);
        }
    }
}
