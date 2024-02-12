using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] GameController_Script game_controller_script;

    public bool isAlive = true;

    public int playerDamage = 5;


    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Score")]
    public int score = 0;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth <= 0)
            isAlive = false;
        if (game_controller_script.remainingTime <= 0)
            isAlive = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy_bullet") || other.gameObject.CompareTag("Explosion"))
        {
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                var enemyDamageFindProperty = script.GetType().GetProperty("Damage");
                if (enemyDamageFindProperty != null)
                {
                    float damageValue = (float)enemyDamageFindProperty.GetValue(script);
                    currentHealth -= (int)damageValue;
                    break;
                }
            }
        }
    }


}
