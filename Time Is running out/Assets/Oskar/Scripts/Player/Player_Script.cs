using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] GameController_Script game_controller_script;

    public bool isAlive = true;


    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] public float currentHealth;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy_bullet"))
        {
            Debug.Log("Skadad");
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                Debug.Log("Inne i foreach");
                var enemyDamageFindProperty = script.GetType().GetProperty("Damage");
                Debug.Log(enemyDamageFindProperty);
                if (enemyDamageFindProperty != null)
                {
                    Debug.Log("Hittade damage: " + enemyDamageFindProperty);
                    float damageValue = (float)enemyDamageFindProperty.GetValue(script);
                    currentHealth -= damageValue;
                    break;
                }
            }
        }
    }


}
