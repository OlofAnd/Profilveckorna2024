using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] Enemy_Test_Script enemy_test_script;
    [SerializeField] GameController_Script game_controller_script;

    public bool isAlive = true;


    [Header("Health")]
    [SerializeField] int maxHealth = 4;
    [SerializeField] public int currentHealth;

    [Header("Score")]
    public int score = 0;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (currentHealth <= 0)
        {
            isAlive = false;
            Debug.Log("Player ded");
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (currentHealth > 0 && other.gameObject.CompareTag("Enemy"))
        //{
        //    //Gör så den tar skada av hur mycket damage enemyn gör
        //    currentHealth -= enemy_test_script.damage;
        //    Debug.Log("Skadad");
        //}
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log(game_controller_script.enemiesAlive);
            score += enemy_test_script.scoreGive;
        }
    }
}
