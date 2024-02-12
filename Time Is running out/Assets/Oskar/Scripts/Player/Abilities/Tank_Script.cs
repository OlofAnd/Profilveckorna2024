using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Script : MonoBehaviour
{
    [SerializeField] Player_Script player_Script;
    [SerializeField] GameController_Script gameController_Script;


    public bool isTankActive;


    void Start()
    {

    }

    void Update()
    {
        if (isTankActive)
        {
            player_Script.tankMaxHealth = (int)gameController_Script.remainingTime + player_Script.maxHealth;
        }
    }
}
