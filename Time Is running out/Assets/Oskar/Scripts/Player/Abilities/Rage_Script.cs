using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage_Script : MonoBehaviour
{
    [SerializeField] Player_Script player_Script;
    [SerializeField] Player_Movement_Script playerMove_Script;

    public bool isRageActive;

    private bool rageTier1Activated;
    private bool rageTier2Activated;
    private bool rageTier3Activated;

    void Start()
    {

    }
    void Update()
    {
        if (player_Script.currentHealth <= player_Script.maxHealth / 10 && !rageTier1Activated)
        {
            rageTier1Activated = true;
            playerMove_Script.movementSpeed *= 3;
            player_Script.playerDamage *= 6;
        }
        else if (player_Script.currentHealth <= player_Script.maxHealth / 4 && !rageTier2Activated)
        {
            rageTier2Activated = true;
            playerMove_Script.movementSpeed *= 2f;
            player_Script.playerDamage *= 4;
        }
        else if (player_Script.currentHealth <= player_Script.maxHealth / 2 && !rageTier3Activated)
        {
            rageTier3Activated = true;
            playerMove_Script.movementSpeed *= 1.5f;
            player_Script.playerDamage *= 2;
        }
    }
}
