using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;

public class mud_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject target;
    [SerializeField] GameObject bullet;

    Vector2 direction;
    float speed;

    Random rng = new Random();

    bool playerDetected = false;
    bool walking;
    bool attacking = false;
    bool hurting = false;

    float attackTimer = 0;
    float hurtTimer = 0;

    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(playerDetected);
        EnemyBehaviour();    
    }

    public override void EnemyBehaviour()
    {
        if(hurting)
        {
            hurtTimer = Time.time + 0.5f;
            Hurt();
        }
        else if(attacking)
        {
            Attack();
        }
        else
        {
            if (!playerDetected)
            {
                Idle();
            }
            else
            {
                attacking = true;
                attackTimer = Time.time + 0.7f;
            }
        }
        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Idle()
    {
        //spela animation
    }
    void Attack()
    {
        if(Time.time > attackTimer)
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
        else if (Time.time > attackTimer)
        {
            attacking = false;
        }
    }
    // lägg till en metod som bestämmer när mud enemy ska flytta sig och vart
    void Hurt()
    {
        EnemyHealthPoints--;
        if(Time.time>hurtTimer)
        {
            hurting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            playerDetected = true;
            target = trig.gameObject;
        }
        if (trig.gameObject.tag == "Player_Bullet")
        {
            hurting = true;
        }
    }
}
