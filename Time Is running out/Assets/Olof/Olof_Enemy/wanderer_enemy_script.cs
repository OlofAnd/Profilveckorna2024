using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = System.Random;

public class wanderer_enemy_script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject target;

    Vector2 direction;
    [SerializeField] float speed;
    float angle;

    Random rng = new Random();

    bool playerDetected = false;
    bool hurting = false;

    float idleTimer = 0;
    float hurtTimer = 0;

    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 0f;

        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        EnemyBehaviour();
    }
    public override void EnemyBehaviour()
    {
        // alla behaviour här a.k.a hur enemy ska hantera dess olika behaviour

        rb.velocity = direction * speed;
        if (hurting)
        {
            hurtTimer = Time.time + 0.5f;
            hurt();
        }
        else
        {
            if (!playerDetected)
            {
                Idle();
            }
            else if (Vector2.Distance(rb.position, target.transform.position) <= 7)
            {
                Walk();
            }
            else
            {
                Idle();
            }
        }
        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }


    void Idle()
    {
        if (idleTimer < Time.time)
        {
            angle = rng.Next(0, 360);
            angle = angle * math.PI / 180;
            idleTimer = Time.time + rng.Next(2, 3);
        }

        direction = new Vector2(math.cos(angle), math.sin(angle));
        if (idleTimer > Time.time + 1)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
        }
    }
    void Walk()
    {
        direction = -((Vector2)(target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(target.transform.position) - rb.position);
        speed = 3f;
    }
    void hurt()
    {
        EnemyHealthPoints--;
        if (Time.time > hurtTimer)
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
