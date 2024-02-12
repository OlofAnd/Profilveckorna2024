using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_card_select_script : MonoBehaviour
{
    Card_Information_Script cardInfo;
    [SerializeField] Card_Display_Script_Left cardDisplay;

    [SerializeField] GameController_Script gameController;

    public void RightButtonPressed()
    {
        cardInfo = GetComponent<Card_Display_Script_Left>().card_left;

      
        if (cardInfo.name == "Time 1")
            cardInfo.tier1Time = true;
        else if(cardInfo.name=="Time 2")
            cardInfo.tier2Time= true;
        else if(cardInfo.name=="Time 3")
            cardInfo.tier3Time=true;


        if (cardInfo.tier1Time)
        {
            gameController.remainingTime += 30f;//kanske?
            Debug.Log(gameController.remainingTime);
            cardInfo.tier1Time = false;
        }
        else if (cardInfo.tier2Time)
        {
            gameController.remainingTime += 60f;//??
            Debug.Log(gameController.remainingTime);
            cardInfo.tier2Time = false;
        }
        else if(cardInfo.tier3Time)
        {
            gameController.remainingTime += 90f;//???
            Debug.Log(gameController.remainingTime);
            cardInfo.tier3Time = false;
        }
    }
}
