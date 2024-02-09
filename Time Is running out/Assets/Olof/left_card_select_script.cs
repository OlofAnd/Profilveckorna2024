using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_card_select_script : MonoBehaviour
{
    Card_Information_Script cardInfo;
    [SerializeField] Card_Display_Script_Right cardDisplay;
    [SerializeField] Player_Script playerScript;

    public void LeftButtonPressed()
    {
        cardInfo = GetComponent<Card_Display_Script_Right>().card_right;

       //if()

        if (cardInfo.healToMax)
        {
            playerScript.currentHealth = 10;
            Debug.Log(playerScript.currentHealth);
            playerScript.currentHealth = playerScript.maxHealth;
            Debug.Log(playerScript.currentHealth);
            cardInfo.healToMax = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        cardInfo = GetComponent<Card_Display_Script_Right>().card_right;
        Debug.Log(cardInfo.healToMax);
    }
}
