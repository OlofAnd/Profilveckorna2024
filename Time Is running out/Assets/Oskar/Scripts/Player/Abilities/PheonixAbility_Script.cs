using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixAbility_Script : MonoBehaviour
{
    [SerializeField] Card_Information_Script cardInfor_Script;
    [SerializeField] GameController_Script gameCont_Script;
    [SerializeField] Player_Script player_Script;

    public bool isPheonixActive = false;
    public bool pheonixActivatedThisRound = false;

    void Start()
    {

    }
    
    void Update()
    {
        //isPheonixActive = cardInfor_Script.phoenix;
        if (!player_Script.isAlive && !pheonixActivatedThisRound && isPheonixActive)
        {
            player_Script.isAlive = true;
            pheonixActivatedThisRound = true;
            player_Script.currentHealth = player_Script.maxHealth / 2;
            gameCont_Script.remainingTime = 10;
        }
        if (gameCont_Script.enemiesAlive <= 0)
        {
            pheonixActivatedThisRound = false;
        }
    }
}
