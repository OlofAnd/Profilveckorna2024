using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script_Left : MonoBehaviour
{
    [SerializeField] GameController_Script gameController;

    [Header("Ability decks")]
    [SerializeField] CardDeck deck_tier1;
    [SerializeField] CardDeck deck_tier2;
    [SerializeField] CardDeck deck_tier3;

    [Header("Time decks")]
    [SerializeField] CardDeck deck_time_tier1;
    [SerializeField] CardDeck deck_time_tier2;
    [SerializeField] CardDeck deck_time_tier3;

    [Header("Misc")]
    [SerializeField] public Card_Information_Script card_left;
    public TMP_Text nameText;
    public Image artWorkImage;

    void Start()
    {
        //card_left = deck_time_tier1.cards[Random.Range(0,deck_time_tier1.cards.Count)]; // v�ljer ett random kort fr�n deck_time_tier1

        nameText.text = card_left.name; // �ndra s� att nameText �r ett image
        artWorkImage.sprite = card_left.artwork;
    }
    private void Awake()
    {
        RandomizeCard();
    }
    public void RandomizeCard()
    {
        if (gameController.remainingTime <= 120f && gameController.remainingTime >= 60f)
        {
            card_left = deck_time_tier1.cards[Random.Range(0, deck_time_tier1.cards.Count)];
            Debug.Log("1");
        }
        if (gameController.remainingTime <= 60f && gameController.remainingTime >= 30f)
        {
            card_left = deck_time_tier2.cards[Random.Range(0, deck_time_tier2.cards.Count)];
            Debug.Log("2");
        }
        if (gameController.remainingTime <= 30f)
        {
            card_left = deck_time_tier3.cards[Random.Range(0, deck_time_tier3.cards.Count)];
            Debug.Log("3");
        }
    }
}