using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour {
    
    public GameObject propertyPopUpBox;
    public GameObject opportunityPopUpBox;
    public GameObject potLuckPopUpBox;
    public Button buyButton;
    public Animator propertyAnimator;
    public Animator opportunityAnimator;
    public Animator potLuckAnimator;
    public Image cardImage;
    public Sprite[] cardSprites; 
    public static Dictionary<string, Sprite> cardDictionary;
    private int opportunityCardNumber;
    private int potLuckCardNumber;

    public void Start() {
        potLuckCardNumber = 0;
        opportunityCardNumber = 17;
        
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


        if (propertyType == "Pot Luck"){

            potLuckPopUpBox.SetActive(true);
            potLuckAnimator.SetTrigger("pop");
            setPotLuckCardText();
            StartCoroutine(potLuckCardTimeout());
            
        } else if (propertyType == "Opportunity Knocks") {

            opportunityPopUpBox.SetActive(true);
            opportunityAnimator.SetTrigger("pop");
            setOpportunityCardText();
            StartCoroutine(opportunityCardTimeout());
        
        } else if (cardDictionary.ContainsKey(propertyType)) {

            cardImage.sprite = cardDictionary[propertyType];
            propertyPopUpBox.SetActive(true);
            setPropertyCardText();
            buyButton.enabled = true;
            propertyAnimator.SetTrigger("pop");

        } else {      
            Debug.LogWarning("Property type not found in dictionary: " + propertyType);
        }
    }

    IEnumerator potLuckCardTimeout() {

		yield return new WaitForSeconds(5f);
		potLuckAnimator.SetTrigger("close");
        GameController.turnComplete = true;
	}

    IEnumerator opportunityCardTimeout() {

		yield return new WaitForSeconds(5f);
		opportunityAnimator.SetTrigger("close");
        GameController.turnComplete = true;
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
       
        rentPrices.text = "";
        constructionCost.text = "";
        stationAndUtilityPrices.text = "";

        if (boardData[tileNumber,2] == "Station"){

            stationAndUtilityPrices.text = "If 1 station is owned, rent is £25\nIf 2 stations are owned, rent is £50\nIf 3 stations are owned, rent is £100\nIf 4 stations are owned, rent is £200";
            propertyCost.rectTransform.localPosition = new Vector3(0, 5, 0);

        } else if ((boardData[tileNumber, 2] == "Tesla Utility") || (boardData[tileNumber, 2] == "Edison Utility")) {

            stationAndUtilityPrices.text = "If 1 utility owned, rent is 4 times the dice result\n If 2 utilites are owned, rent is 10 times the dice result";
            propertyCost.rectTransform.localPosition = new Vector3(0, 5, 0);

        } else {

            rentPrices.text = "Unimproved rent: £" + boardData[tileNumber, 6] + "\n1 House rent: £" + boardData[tileNumber, 7] + "\n2 House rent: £" + boardData[tileNumber, 8] + "\n3 House rent: £" + boardData[tileNumber, 9] + "\n4 House rent: £" + boardData[tileNumber, 10]+ "\nHotel rent: £" + boardData[tileNumber, 11];
            constructionCost.text = "House cost £" + boardData[tileNumber, 12] + "\nHotel cost £" + boardData[tileNumber, 12] + " + 4 houses";  
            propertyCost.rectTransform.localPosition = new Vector3(0, 35, 0);
            
        }
    }

    void setOpportunityCardText() {
        string[,] cardData = GameController.cardData;

        
        TextMeshProUGUI OpportunityText = GameObject.FindGameObjectWithTag("OpportunityText").GetComponent<TextMeshProUGUI>();

        OpportunityText.text = cardData[opportunityCardNumber, 0];

        opportunityCardNumber++;
        if (opportunityCardNumber == 33) {
            opportunityCardNumber = 17;
        }
    }
    
    void setPotLuckCardText() {
        string[,] cardData = GameController.cardData;
        
        TextMeshProUGUI PotLuckText = GameObject.FindGameObjectWithTag("PotLuckText").GetComponent<TextMeshProUGUI>();
        PotLuckText.text = cardData[potLuckCardNumber, 0];

        potLuckCardNumber++;
        if (potLuckCardNumber == 17) {
            potLuckCardNumber = 0;
        }

    }

    public static void setSprite(string group, Image img) {
        img.sprite = cardDictionary[group];
        Debug.Log("Set sprite");
    }
}