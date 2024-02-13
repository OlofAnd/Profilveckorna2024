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
    public Image cardBackgroundImage;
    public TMP_Text nameText;
    public Image artworkImage;

    void Start()
    {
        cardBackgroundImage.sprite = card_right.cardBackground;
        artworkImage.sprite = card_right.artwork;
        nameText.text = card_right.name;
    }
    private void Awake()
    {
        RandomizeCard();
    }
    public void RandomizeCard()
    {

        if (gameController.remainingTime >= 60f)
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
        nameText.text = card_right.name;
        artworkImage.sprite = card_right.artwork;
    }
}
