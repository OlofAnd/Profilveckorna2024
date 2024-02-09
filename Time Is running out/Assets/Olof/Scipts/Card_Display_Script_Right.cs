using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script_Right : MonoBehaviour
{
    [SerializeField] GameController_Script gameController;

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
        nameText.text = card_right.name;
        artworkImage.sprite = card_right.artwork;
    }
    private void Awake()
    {
        if (gameController.remainingTime <= 120f && gameController.remainingTime >= 60f)
        {
            card_right = deck_tier1.cards[Random.Range(0, deck_tier1.cards.Count)];
        }
        if (gameController.remainingTime <= 60f && gameController.remainingTime > 30f)
        {
            card_right = deck_tier2.cards[Random.Range(0, deck_tier2.cards.Count)];
        }
        if (gameController.remainingTime <= 30f)
        {
            card_right = deck_tier3.cards[Random.Range(0, deck_tier3.cards.Count)];
        }
    }
}
