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
    public Button auctionButton;
    public Animator propertyAnimator;
    public Animator opportunityAnimator;
    public Animator potLuckAnimator;
    public Image cardImage;
    public Sprite[] cardSprites; 
    public static Dictionary<string, Sprite> cardDictionary;
    private int opportunityCardNumber;
    private int potLuckCardNumber;
    private int currentPlayer;
    private int currentPosition;
    public CounterMovement counterMovement;

    public void Start() {
        counterMovement = GetComponent<CounterMovement>();
        potLuckCardNumber = 0;
        opportunityCardNumber = 23;
        
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
            { "Edison Utility", cardSprites[10] },
            { "Gray", cardSprites[11] },
            { "tax", cardSprites[12] }
        };

    }



    public void popUpCard(string propertyType) {

        buyButton.gameObject.SetActive(true);
        auctionButton.gameObject.SetActive(true);

        currentPlayer = GameController.getCurrentPlayer();
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

        } else if (propertyType == "tax") {


            cardImage.sprite = cardDictionary[propertyType];
            propertyPopUpBox.SetActive(true);

            int currentPos = EndOfTurnActions.getCurrentPosition();
            TextMeshProUGUI propertyName = GameObject.FindGameObjectWithTag("PropertyName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI propertyCost = GameObject.FindGameObjectWithTag("PropertyCost").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rentPrices = GameObject.FindGameObjectWithTag("RentPrices").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI constructionCost = GameObject.FindGameObjectWithTag("ConstructionCosts").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stationAndUtilityPrices = GameObject.FindGameObjectWithTag("Station&Utility").GetComponent<TextMeshProUGUI>();

            rentPrices.text = "";
            constructionCost.text = "";
            stationAndUtilityPrices.text =  "";

            buyButton.gameObject.SetActive(false);
            auctionButton.gameObject.SetActive(false);

            if (currentPos == 4) {
                propertyName.text = "Income Tax";
                propertyCost.text  = "You Have to pay £200";
                Banking.playerToBankTransfer(currentPlayer, 200);
                
            } else {
                propertyName.text = "Super Tax";
                propertyCost.text  = "You Have to pay £100";
                Banking.playerToBankTransfer(currentPlayer, 100);
            }

            propertyAnimator.SetTrigger("pop");
            StartCoroutine(taxCardTimeout());
        
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

    IEnumerator taxCardTimeout() {

		yield return new WaitForSeconds(2.5f);
		propertyAnimator.SetTrigger("close");
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

        

        Debug.Log(opportunityCardNumber);

        string action = cardData[opportunityCardNumber, 1];
        currentPosition = EndOfTurnActions.getCurrentPosition();

        if (action.StartsWith("Bank pays")) {

            int startIndex = action.IndexOf("£");
            startIndex++;
            int endIndex = action.IndexOf(" ", startIndex);
            int amount = int.Parse(action.Substring(startIndex, endIndex - startIndex));

            Debug.Log(amount);
            Banking.bankToPlayerTransfer(currentPlayer, amount);

        } else if (opportunityCardNumber == 19) {
            Debug.Log("go");
            //go to turing heights current player should go all the way to moves = 40
            int movesToTuringHeights = 40 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToTuringHeights < 0) movesToTuringHeights += 40;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToTuringHeights));

        } else if (opportunityCardNumber == 20) {
            //go to Xin Gardens
            int movesToXinGardens = 24 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToXinGardens < 0) movesToXinGardens += 24;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToXinGardens));

        } else if (opportunityCardNumber == 21) {
            //put £15 on free parking

            Banking.addMoneyToFreeParking(currentPlayer, 15);

        } else if (opportunityCardNumber == 23) {
            //go to Hove station
            
            int movesToHoveStation = 15 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToHoveStation < 0) movesToHoveStation += 15;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToHoveStation));


        } else if (opportunityCardNumber == 26) {
            // go to Go

            int movesToGo = 40 - GameController.playerPositions[GameController.currentPlayer];

            if (movesToGo < 0) {
                movesToGo += 40;
            }

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToGo));

        } else if ((opportunityCardNumber == 27) || (opportunityCardNumber == 25)) {
            //housing

            Debug.Log("housing");

        } else if (opportunityCardNumber == 28) {
            //go back 3 spaces

            //StartCoroutine(counterMovement.MoveCounterCoroutine(-3));

        } else if (opportunityCardNumber == 29) {
            //go to SkyWalker Drive.

            int movesToSkyWalkerDrive = 25 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToSkyWalkerDrive < 0) movesToSkyWalkerDrive += 25;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToSkyWalkerDrive));

            
        } else if (opportunityCardNumber == 30) {
            //go to jail

            int movesToJail = 20 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToJail < 0) movesToJail += 20;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToJail));

        } else if (opportunityCardNumber == 31) {
            //put £20 on free parking

            Banking.addMoneyToFreeParking(currentPlayer, 20);
        } else if (opportunityCardNumber == 32) {
            //get out of jail

        } else if (action.StartsWith("Player pays")) {

            Debug.Log("Player pays");

            int startIndex = action.IndexOf("£");
            startIndex++;
            int endIndex = action.IndexOf(" ", startIndex);
            int amount = int.Parse(action.Substring(startIndex, endIndex - startIndex));
            Banking.playerToBankTransfer(currentPlayer, amount);

        }

        opportunityCardNumber++;
        if (opportunityCardNumber == 33) {
            opportunityCardNumber = 17;
        }
    }
    
    void setPotLuckCardText() {
        string[,] cardData = GameController.cardData;
        
        TextMeshProUGUI PotLuckText = GameObject.FindGameObjectWithTag("PotLuckText").GetComponent<TextMeshProUGUI>();
        PotLuckText.text = cardData[potLuckCardNumber, 0];

        

        string action = cardData[potLuckCardNumber, 1];
        currentPosition = EndOfTurnActions.getCurrentPosition();

        if (action.StartsWith("Bank pays player")) {
            int startIndex = action.IndexOf("£");
            startIndex++;
            int endIndex = action.IndexOf(" ", startIndex);
            int amount = int.Parse(action.Substring(startIndex, endIndex - startIndex));
            Banking.bankToPlayerTransfer(currentPlayer, amount);

        } else if (potLuckCardNumber == 3){
            //move to old creek

            int movesToOldCreek = 41 - GameController.playerPositions[GameController.currentPlayer];
            if (movesToOldCreek < 0) movesToOldCreek += 41;

            StartCoroutine(counterMovement.MoveCounterCoroutine(movesToOldCreek));
        } else if (potLuckCardNumber == 7) {
            //move forwards to go

            int movesToGo = 40 - GameController.playerPositions[GameController.currentPlayer];

            if (movesToGo < 0) {
                movesToGo += 40;
            }
        } else if (potLuckCardNumber == 10) {
            // option to take opportunity knocks card or put £10 on free parking
        } else if (potLuckCardNumber == 15) {
            //receive 10 from each of the other players

            for (int i = 0; i < GameController.humanPlayers - 1; i++) {
                if (i != currentPlayer) {
                    Banking.playerToPlayerTransfer(i, currentPlayer, 10);
                }
            }

        } else if (potLuckCardNumber == 16) {
            //get out of jail free

        } else if (action.StartsWith("Player pays")) {

            int startIndex = action.IndexOf("£");
            startIndex++;
            int endIndex = action.IndexOf(" ", startIndex);
            int amount = int.Parse(action.Substring(startIndex, endIndex - startIndex));
            Banking.playerToBankTransfer(currentPlayer, amount);

        }

        potLuckCardNumber++;
        if (potLuckCardNumber == 17) {
            potLuckCardNumber = 0;
        }

    }

    public static void setSprite(string group, Image img) {
        img.sprite = cardDictionary[group];
    }
}