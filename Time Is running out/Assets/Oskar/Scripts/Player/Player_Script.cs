using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] Enemy_Test_Script enemy_test_script;

    public bool isAlive = true;
    [Header("Health")]
    [SerializeField] int maxHealth = 4;
    [SerializeField] public int currentHealth;


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
        if (currentHealth > 0 && other.gameObject.CompareTag("Enemy"))
        {
            //Gör så den tar skada av hur mycket damage enemyn gör
            currentHealth -= enemy_test_script.damage;
            Debug.Log("Skadad");
        }
    }
}
