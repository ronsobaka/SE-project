using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Auction : MonoBehaviour {

    public GameObject auctionBox;
    public Animator auctionAnimator;
    public Button pound1;
    public Button pound10;
    public Button pound100;
    private static int highestBid;
    private static int highestBidder;
    public static int numberofPlayers;
    private static List<int> biddersRemaining;
    private static int currentBidder;
    private static int nextBidder;
    private static TextMeshProUGUI bidAndBidderText;
    public TextMeshProUGUI surroundingText;
    public TextMeshProUGUI title;

    
    public void startAuction() {

        auctionBox.SetActive(true);
        auctionAnimator.SetTrigger("open");
        setButtonActivation(true);
        
        //initialize variables

        highestBid = 0;
        highestBidder = GameController.getCurrentPlayer();
        numberofPlayers = GameController.getHumanPlayers();

        currentBidder = highestBidder - 1;

        if (currentBidder == numberofPlayers - 2){
            nextBidder = 0;
        } else {
            nextBidder = currentBidder + 1;
        }
        

        biddersRemaining = new List<int>();
        for (int i = 1; i <= numberofPlayers; i++) {
            biddersRemaining.Add(i);
        }

        
        title.text = "Auction for " + GameController.getBoardData()[EndOfTurnActions.getCurrentPosition(), 1];

        bidAndBidderText = GameObject.FindGameObjectWithTag("AuctionBidAndBidder").GetComponent<TextMeshProUGUI>();
        updateUI();
    }

    public static void makeBid(int bidAmount) {
        highestBid += bidAmount;
        getNextBidder();
    }

    public void removeBidder() {
        biddersRemaining.RemoveAt(currentBidder);
    }

    private static void getNextBidder() {

        if (biddersRemaining.Count == 1) {
            endBidding();
            return;
        }
        
        if (biddersRemaining.Contains(currentBidder + 1)) {
        } else {

        }

        

        

    }

    private void endBidding(){
        Debug.Log("Got here with no errors");
        setButtonActivation(false);

        surroundingText.text = "player " + biddersRemaining[0].ToString() + " You have won the bidding!!\nYou now own " + title.text + " for £" + highestBid;
        bidAndBidderText.text = "";

        StartCoroutine(auctionTimeout());
    }

    private void setButtonActivation(bool trueFalse) {
        pound1.enabled = trueFalse;
        pound10.enabled = trueFalse;
        pound100.enabled = trueFalse;
    }

    IEnumerator auctionTimeout() {

		yield return new WaitForSeconds(5f);
		auctionAnimator.SetTrigger("close");
        GameController.setTurnComplete(true);
	}


    private static void updateUI() {
        Debug.Log(currentBidder + " " + nextBidder);

        foreach(int i in biddersRemaining) {
            Debug.Log(i);
        }
        
        bidAndBidderText.text = "£" + highestBid + "\n\n\nPlayer " + biddersRemaining[currentBidder] + "\n\n\nPlayer " + biddersRemaining[nextBidder];
    }

}
