using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    [Header("Health Bar")]
    [SerializeField] int maxHealth = 4;
    public int currentHealth;


    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentHealth > 0)
        {

        }
    }
}
