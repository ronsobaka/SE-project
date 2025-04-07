using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auction : MonoBehaviour {

    public GameObject auctionBox;
    public Animator auctionAnimator;
    
    public void startAuction() {
        if (auctionBox == null) {
            Debug.Log("huh");
        }
    }

}
