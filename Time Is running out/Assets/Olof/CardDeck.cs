using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class CardDeck : ScriptableObject
{
    public List<Card_Information_Script> cards = new List<Card_Information_Script>();   
}
