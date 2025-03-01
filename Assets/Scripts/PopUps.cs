using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour  {
    
    public GameObject popUpBox;
    public Button buyButton;
    public Animator animator;
    public GameObject textParentComponent;
    public Image cardImage;
    public Sprite[] cardSprites; 
    private Dictionary<string, Sprite> cardDictionary;
    

    public void Start() {
        
        cardDictionary = new Dictionary<string, Sprite> {
            { "Brown", cardSprites[0] },
            { "Blue", cardSprites[1] },
            { "Purple", cardSprites[2] },
            { "Orange", cardSprites[3] },
            { "Red", cardSprites[4] },
            { "Yellow", cardSprites[5] },
            { "Green", cardSprites[6] },
            { "Deep blue", cardSprites[7] },
            { "Station", cardSprites[8] },
            { "Tesla Utility", cardSprites[9] },
            { "Edison Utility", cardSprites[10] }
        };
    }



    public void popUpCard(string propertyType) {

        if (cardDictionary.ContainsKey(propertyType)) {
            cardImage.sprite = cardDictionary[propertyType];
            setPropertyCardText();
        } else {      
            Debug.LogWarning("Property type not found in dictionary: " + propertyType);
        }
        buyButton.enabled = true;
        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
    }

    void setPropertyCardText() {
        string[,] boardData = GameController.boardData;
        int tileNumber = GameController.playerPositions[GameController.currentPlayer];

        TextMeshProUGUI propertyName = GameObject.FindGameObjectWithTag("PropertyName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI propertyCost = GameObject.FindGameObjectWithTag("PropertyCost").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rentPrices = GameObject.FindGameObjectWithTag("RentPrices").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI constructionCost = GameObject.FindGameObjectWithTag("ConstructionCosts").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI stationAndUtilityPrices = GameObject.FindGameObjectWithTag("Station&Utility").GetComponent<TextMeshProUGUI>();

        propertyName.text = boardData[tileNumber, 1];
        propertyCost.text = "Price: £" + boardData[tileNumber, 5];
        //propertyCost.Transform.position.y = 35;
        rentPrices.text = "";
        constructionCost.text = "";

        if (boardData[tileNumber,2] == "Station"){

            stationAndUtilityPrices.text = "If 1 station is owned rent is £25\nIf 2 stations are owned rent is £50\nIf 3 stations are owned rent is £100\nIf 4 stations are owned rent is £200";
            //propertyCost.transform.position.y = 10;


        } else {
            rentPrices.text = "Unimproved rent: £" + boardData[tileNumber, 6] + "\n1 House rent: £" + boardData[tileNumber, 7] + "\n2 House rent: £" + boardData[tileNumber, 8] + "\n3 House rent: £" + boardData[tileNumber, 9] + "\n4 House rent: £" + boardData[tileNumber, 10]+ "\nHotel rent: £" + boardData[tileNumber, 11];
            constructionCost.text = "House cost £" + boardData[tileNumber, 12] + "\nHotel cost £" + boardData[tileNumber, 12] + " + 4 houses";  
        }

        
    }

    public void endTurn() {
        GameController.turnComplete = true;
    }
}
