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
    public Button dropOut;
    private static int highestBid;
    private static int highestBidder;
    public static int numberofPlayers;
    private static List<int> biddersRemaining;
    private static int currentBidder;
    private static int nextBidder;
    private static TextMeshProUGUI bidAndBidderText;
    public TextMeshProUGUI surroundingText;
    public TextMeshProUGUI title;
    public TextMeshProUGUI errorText;

    
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

    public void makeBid(int bidAmount) {

        if (Banking.checkBalance(currentBidder, (highestBid + bidAmount))) {
            highestBid += bidAmount;
            getNextBidder();
        } else {
            errorText.text = "You cannot afford this bid.";
            removeBidder();
        }
    }

    IEnumerator errorMessageBuffer() {
        yield return new WaitForSeconds(2f);
        errorText.text = "";
    }

    public void removeBidder() {
        if (biddersRemaining.Count <= 1) {
            return;
        }

        biddersRemaining.RemoveAt(nextBidder);

        if (nextBidder < currentBidder) {
            currentBidder--;
        }

        if (currentBidder >= biddersRemaining.Count) {
            currentBidder = 0;
        }

        if (currentBidder + 1 >= biddersRemaining.Count) {
            nextBidder = 0;
        } else {
            nextBidder = currentBidder + 1;
        }

        if (biddersRemaining.Count == 1) {
            endBidding();
            return;
        }

        updateUI();
    }

    private void getNextBidder() {
        
        highestBidder = biddersRemaining[currentBidder];
        currentBidder++;

        if (currentBidder >= biddersRemaining.Count) {
            currentBidder = 0;
        }

        if (biddersRemaining.Count == 1) {
            GameObject.FindObjectOfType<Auction>().endBidding();

            return;
        }

        nextBidder = (currentBidder + 1) % biddersRemaining.Count;

        updateUI();

    }

    private void endBidding(){

        setButtonActivation(false);

        EndOfTurnActions.auctionBuyProperty(biddersRemaining[0] - 1);

        surroundingText.text = "player " + biddersRemaining[0].ToString() + " You have won the bidding!!\nYou now own " + title.text + " for £" + highestBid;
        bidAndBidderText.text = "";

        StartCoroutine(auctionTimeout());
    }

    private void setButtonActivation(bool trueFalse) {
        pound1.enabled = trueFalse;
        pound10.enabled = trueFalse;
        pound100.enabled = trueFalse;
        dropOut.enabled = trueFalse;
    }

    IEnumerator auctionTimeout() {

		yield return new WaitForSeconds(2.5f);
		auctionAnimator.SetTrigger("close");
        GameController.setTurnComplete(true);
	}


    private static void updateUI() {

        bidAndBidderText.text = "£" + highestBid + "\n\n\nPlayer " + biddersRemaining[currentBidder] + "\n\n\nPlayer " + biddersRemaining[nextBidder];
    
    }

}
