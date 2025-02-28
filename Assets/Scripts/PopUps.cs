using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour  {
    
    public GameObject popUpBox;
    public Animator animator;
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
            { "Deep blue", cardSprites[7] }
        };
    }



    public void popUpCard(string propertyType) {

        Debug.Log(propertyType);

        if (cardDictionary.ContainsKey(propertyType)) {
            cardImage.sprite = cardDictionary[propertyType];
        } else {
            Debug.LogWarning("Property type not found in dictionary: " + propertyType);
        }

        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
    }
}
