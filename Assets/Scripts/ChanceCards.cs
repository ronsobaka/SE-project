using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChanceCards : MonoBehaviour
{

    // public Sprite[] potLuckCardSprites;
    // public Sprite[] opportunityKnocksCardSprites;
    public Image potLuckCardImage;
    public Image opportunityKnocksCardImage;

    public void ShowChanceCard(string cardType) {
        
        Debug.Log(cardType);
        string cardText = "";
        // Sprite cardSprite = null;

        if (cardType == "Pot Luck") {
            int index = Random.Range(0,17);   //Chooses random line from 1 to 17
            cardText = CSVReader.potLuckData[index];  //Getting card text from that line
            // cardSprite = potLuckCardSprites[index];   // Getting sprite for the line

            // potLuckCardImage.sprite = cardSprite;

        } else if (cardType == "Opportunity Knocks") {
            int index = Random.Range(0, 17);
            cardText = CSVReader.opportunityKnocksData[index];
            // cardSprite = opportunityKnocksCardSprites[index];

            // opportunityKnocksCardImage.sprite = cardSprite;
        }


        DisplayCardText(cardText);
    }

    void DisplayCardText(string cardText) {
        TextMeshProUGUI chanceText = GameObject.FindGameObjectWithTag("CardText").GetComponent<TextMeshProUGUI>();

    }
}
