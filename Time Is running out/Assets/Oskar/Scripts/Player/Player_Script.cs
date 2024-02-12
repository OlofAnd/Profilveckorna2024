using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] GameController_Script game_controller_script;
    [SerializeField] Tank_Script tankScript;

    public bool isAlive = true;

    public int playerDamage = 5;


    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public int tankMaxHealth;

    [Header("Score")]
    public int score = 0;


    void Start()
    {
        currentHealth = maxHealth;
        tankMaxHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth > maxHealth && !tankScript.isTankActive)
            currentHealth = maxHealth;
        else if (currentHealth > tankMaxHealth)
            currentHealth = tankMaxHealth;


        if (currentHealth <= 0 || game_controller_script.remainingTime <= 0)
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
