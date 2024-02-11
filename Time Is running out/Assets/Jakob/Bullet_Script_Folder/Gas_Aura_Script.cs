using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Aura_Script : MonoBehaviour
{
    GameController_Script GameController;
    private bool TouchingPlayer;
    void Start()
    {
        GameController = GameObject.FindObjectOfType<GameController_Script>();
        TouchingPlayer = false;
    }

    void Update()
    {
        if (TouchingPlayer)
        {
            GameController.remainingTime -= Time.deltaTime * 3;
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            TouchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            TouchingPlayer = false;
        }
    }
}
