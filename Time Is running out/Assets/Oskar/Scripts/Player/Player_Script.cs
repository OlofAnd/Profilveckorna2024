using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Player_Script : MonoBehaviour
{
    [SerializeField] GameController_Script game_controller_script;
    [SerializeField] Tank_Script tankScript;

    SpriteRenderer sprRen;
    [SerializeField] SpriteRenderer weaponSprRen;

    public bool isAlive = true;

    public int playerDamage = 5;
    public bool playerIFrames = false;

    int deathAnimation = 0;

    Animator ani;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public int tankMaxHealth;

    [Header("Score")]
    public int score = 0;


    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        tankMaxHealth = maxHealth;
        sprRen = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

    }

    void Update()
    {
        if (!isAlive && deathAnimation == 0)
        {
            deathAnimation++;
            ani.SetBool("isIdle", false);
            ani.SetBool("isRunning", false);
            ani.SetTrigger("isDead");
            weaponSprRen.enabled = false;
        }

        if (!game_controller_script)
        {
            if (currentHealth > maxHealth && !tankScript.isTankActive)
                currentHealth = maxHealth;
            else if (currentHealth > tankMaxHealth)
                currentHealth = tankMaxHealth;
        }


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
                if (enemyDamageFindProperty != null && !playerIFrames)
                {
                    float damageValue = (float)enemyDamageFindProperty.GetValue(script);
                    currentHealth -= (int)damageValue;
                    Hurt();
                    break;
                }
            }
        }

    }




    private void Hurt()
    {
        playerIFrames = true;
        HurtAnimationRed();
        Invoke("HurtAnimationWhite", 0.1f);
        Invoke("HurtAnimationRed", 0.2f);
        Invoke("HurtAnimationWhite", 0.3f);
        Invoke("TurnOffPlayerIFrames", 0.4f);
    }
    private void HurtAnimationRed()
    {
        sprRen.color = Color.red;
    }
    private void HurtAnimationWhite()
    {
        sprRen.color = Color.white;
    }
    private void TurnOffPlayerIFrames()
    {
        playerIFrames = false;
    }


}
