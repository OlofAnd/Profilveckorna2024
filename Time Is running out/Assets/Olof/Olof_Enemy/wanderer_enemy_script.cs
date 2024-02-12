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

    // gpt code
    [SerializeField] float stopCooldown = 1.0f;   // Adjust the cooldown time as needed
    float stopCooldownTimer = 0f;
    [SerializeField] float runDuration = 2.0f;   // Adjust the run duration as needed
    float runEndTime = 5f;

    [SerializeField] float wanderRadius = 5f;

    void Start()
    {
        EnemyHealthPoints = 10f;
        Damage = 0f;

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
                // gpt code
                if (Time.time < runEndTime)
                {
                    Walk();
                    //Wander();
                }
                else
                {
                    Wander();
                    runEndTime = Time.time + runDuration; // set the end time for the run
                }
                if (Time.time > stopCooldownTimer)
                {
                    StopMovement();
                    stopCooldownTimer = Time.time + stopCooldown; // set cooldown timer
                }
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

    // gpt code
    void StopMovement()
    {
        direction = Vector2.zero;
        speed = 0f;
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
        if(target!=null)
        {
            direction = -((Vector2)(target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(target.transform.position) - rb.position);
            speed = 3f;
        }
        else
        {
            // Handle the case when the target is null, for example, by stopping movement
            StopMovement();
        }
    }
    void Wander()
    {
        // generates a random angle within a cicrle to determine the new direction
        float randomAngle = rng.Next(0, 360);
        randomAngle = randomAngle * math.PI / 180;

        // calculate the new direction based on the random angle and wander radius
        Vector2 wanderDirection = new Vector2(math.cos(randomAngle), math.sin(randomAngle));
        Vector2 wanderPosition = rb.position + wanderDirection * wanderRadius;

        // set the new direction and speed
        direction = (wanderPosition - rb.position).normalized;
        speed = 4f;
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
