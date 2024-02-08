using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script_Right : MonoBehaviour
{
    [Header("Decks")]
    [SerializeField] CardDeck deck_tier1;
    [SerializeField] CardDeck deck_tier2;
    [SerializeField] CardDeck deck_tier3;


    [Header("Misc")]
    [SerializeField] public Card_Information_Script card_right;
    public TMP_Text nameText;
    public Image artworkImage;

    void Start()
    {
        card_right = deck_tier1.cards[Random.Range(0, deck_tier1.cards.Count)]; // väljer ett random kort från deck_tier1

        nameText.text = card_right.name;
        artworkImage.sprite = card_right.artwork;
    }
}
