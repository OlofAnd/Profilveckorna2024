using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixAbility_Script : MonoBehaviour
{
    [SerializeField] GameController_Script gameCont_Script;
    [SerializeField] Player_Script player_Script;
    [SerializeField] Player_Movement_Script player_Movement_Script;

    [SerializeField] SpriteRenderer weaponSprRen;
    Animator ani;
    public bool isPheonixActive = false;
    public bool pheonixActivatedThisRound = false;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (!player_Script.isAlive && !pheonixActivatedThisRound && isPheonixActive)
        {
            player_Script.isAlive = true;
            pheonixActivatedThisRound = true;

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

            ani.SetBool("Revive", true);
            Invoke("ReviveAni", 5f);
        }
        if (gameCont_Script.enemiesAlive <= 0)
        {
            pheonixActivatedThisRound = false;
        }
    }
    void ReviveAni()
    {
        player_Script.currentHealth = player_Script.maxHealth / 2;
        gameCont_Script.remainingTime = 10;
        ani.SetBool("Revive", false);
        ani.SetBool("isIdle", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
     
        weaponSprRen.enabled = true;

        player_Script.isAlive = true;
        player_Script.deathAnimation = 0;
        player_Movement_Script.state = Player_Movement_Script.State.Normal;
    }
}
