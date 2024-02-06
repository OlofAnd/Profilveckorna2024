using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script : MonoBehaviour
{

    public Card_Information_Script card;

    public TMP_Text nameText;
    public Image artWorkImage;

    void Start()
    {
        nameText.text = card.name; // ändra så att nameText är ett image
        artWorkImage.sprite = card.artwork;
    }   
}
