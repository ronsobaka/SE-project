using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Auction : MonoBehaviour {

    public GameObject auctionBox;
    public Animator auctionAnimator;
    private static int highestBid;
    private static int highestBidder;
    private static int numberofPlayers;
    private static List<int> biddersRemaining;
    private static int currentBidder;
    private static int nextBidder;
    private static TextMeshProUGUI bidAndBidderText;

    
    public void startAuction() {

        auctionBox.SetActive(true);
        auctionAnimator.SetTrigger("open");
        
        //initialize variables

        highestBid = 0;
        highestBidder = GameController.getCurrentPlayer();
        numberofPlayers = GameController.getHumanPlayers();
        currentBidder = 1;
        nextBidder = 2;

        biddersRemaining = new List<int>();
        for (int i = 1; i <= numberofPlayers; i++) {
            biddersRemaining.Add(i);
        }

        TextMeshProUGUI title = GameObject.FindGameObjectWithTag("AuctionTitle").GetComponent<TextMeshProUGUI>();
        title.text = "Auction for " + GameController.getBoardData()[EndOfTurnActions.getCurrentPosition(), 1];

        bidAndBidderText = GameObject.FindGameObjectWithTag("AuctionBidAndBidder").GetComponent<TextMeshProUGUI>();
        updateUI();
    }

    public static void makeBid(int bidAmount) {
        highestBid += bidAmount;
        getNextBidder();
        updateUI();
    }

    public static void removeBidder() {
        if (currentBidder = biddersRemaining.Count) {
            currentBidder = 0;
        }
        biddersRemaining.RemoveAt(currentBidder);

        foreach (int number in biddersRemaining)
        {
            Debug.Log(number);  // Print each element
        }

        if (biddersRemaining.Count == 1) {
            endBidding();
        }

        if (nextBidder >= (biddersRemaining.Count)){
            nextBidder = 1;
        } else {
            nextBidder++;
        }

        updateUI();

        if (currentBidder >= (biddersRemaining.Count)) {
            currentBidder = 1;
        } else  {
            currentBidder++;
        }
    }

    private static void getNextBidder() {
        if (currentBidder >= (biddersRemaining.Count)) {
            currentBidder = 1;
        } else  {
            currentBidder++;
        }
        
        if (nextBidder >= (biddersRemaining.Count)){
            nextBidder = 1;
        } else {
            nextBidder++;
        }

    }

    private static void endBidding(){

    }

    private static void updateUI() {
        bidAndBidderText.text = "£" + highestBid + "\n\n\nPlayer " + biddersRemaining[currentBidder - 1] + "\n\n\nPlayer " + biddersRemaining[nextBidder - 1];
    }

}
