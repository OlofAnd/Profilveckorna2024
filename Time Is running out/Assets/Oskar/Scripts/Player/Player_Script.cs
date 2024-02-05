using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    [Header("Health Bar")]
    [SerializeField] int maxHealth = 4;
    [SerializeField] public int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentHealth > 0 && other.gameObject.CompareTag("Enemy"))
        {
            //Gör så den tar skada av hur mycket damage enemyn gör
            currentHealth--;
            Debug.Log("Skadad");
        }
    }
}
