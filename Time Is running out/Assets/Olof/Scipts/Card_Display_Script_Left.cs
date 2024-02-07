using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script_Left : MonoBehaviour
{

    [SerializeField] CardDeck deck_tier1;
    [SerializeField] public Card_Information_Script card_left;

    public TMP_Text nameText;
    public Image artWorkImage;

    void Start()
    {
        card_left = deck_tier1.cards[Random.Range(0,deck_tier1.cards.Count)];
        nameText.text = card_left.name; // �ndra s� att nameText �r ett image
        artWorkImage.sprite = card_left.artwork;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        //Card_Information_Script
    //    }
    //}
}