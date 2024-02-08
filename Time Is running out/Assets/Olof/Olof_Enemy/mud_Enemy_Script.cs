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
    //float speed;

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
        Damage = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        EnemyBehaviour();
    }

    public override void EnemyBehaviour()
    {
        if (hurting)
        {
            hurtTimer = Time.time + 0.5f;
            Hurt();
        }
        else if (attacking)
        {
            Attack();
        }
        else
        {
            if (!playerDetected)
            {
                Idle();

            }
            // ändra första siffran för hur lång vision den har av spelaren
            else if (Vector2.Distance(rb.position, target.transform.position) >= 7 || Vector2.Distance(rb.position, target.transform.position) < 4)
            {
                transform.position = NextLocation();
            }
            else if (!attacking)
            {
                //speed = 0f;
                attacking = true;
                attackTimer = Time.time + 1f;
            }
            //else
            //{
            //    Idle();
            //    Debug.Log(playerDetected);
            //}
        }
        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Idle()
    {
        //spela animation
        playerDetected = false;
    }
    void Attack()
    {
        if (Time.time > attackTimer)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            attacking = false;
        }
    }
    // lägg till en metod som bestämmer när mud enemy ska flytta sig och vart
    void Hurt()
    {
        EnemyHealthPoints--;
        if (Time.time > hurtTimer)
        {
            hurting = false;
        }
    }
    Vector2 NextLocation()
    {

        float angle = rng.Next(0, 360);
        angle = angle * math.PI / 180;
        return target.transform.position + (Vector3)(new Vector2(math.cos(angle), math.sin(angle)) * 5); //ändra den sista siffran för att bestämma hur långt den ska flytta
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
