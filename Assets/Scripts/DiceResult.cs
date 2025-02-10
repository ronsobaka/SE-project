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
        DiceResultText.text = "You rolled a " + DiceCheckZone.diceLandedNumber;
    }
}
