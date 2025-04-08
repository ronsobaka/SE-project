using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Auction : MonoBehaviour {

    public GameObject auctionBox;
    public Animator auctionAnimator;

    
    public void startAuction() {
        auctionBox.SetActive(true);
        auctionAnimator.SetTrigger("open");
        
    }

}
