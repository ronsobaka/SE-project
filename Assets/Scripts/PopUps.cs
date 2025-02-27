using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUps : MonoBehaviour  {
    
    public GameObject popUpBox;
    public Animator animator;

    public void popUpCard() {
        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
    }
}
