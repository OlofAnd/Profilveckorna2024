using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = System.Random;
using UnityEngine.Device;

public class wanderer_enemy_script : Enemy_Abstract_Script
{
    private Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sprRen;

    private GameObject target;
    private Vector2 direction;
    private Vector2 Knockback;

    bool Hurting = false;
    float HurtTimer;
    [SerializeField] private float speed;
    private Random rng = new Random();

    // Wander parameters
    [SerializeField] private float wanderRadius = 5f;
    [SerializeField] private float wanderCooldown = 3f;
    private float wanderTimer = 0f;

    // RunAway parameters
    [SerializeField] private float runDuration = 2f;
    [SerializeField] private float runAwayRadius = 10f;
    private float runEndTime = 0f;

    void Start()
    {
        EnemySpawnCooldown = Time.time + 0.5f;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
        EnemyHealthPoints = 10f + (Wave * 2);
        Damage = 0f;
    }

    void Update()
    {
        if (EnemySpawnCooldown < Time.time)
        {
            EnemyBehaviour();
        }
        if (direction.x > 0) sprRen.flipX = false;
        else sprRen.flipX = true;
    }

    public override void EnemyBehaviour()
    {
        rb.velocity = (Vector3)Knockback;
        Knockback = Knockback * 0.99f;
        if (Vector2.Distance(Vector2.zero, Knockback) <= 1)
        {
            Knockback = Vector2.zero;
        }
        if (Knockback == Vector2.zero)
        {
            rb.velocity = direction * speed;
        }

        if (Hurting)
        {
            hurt();
        }
        if (IsPlayerDetected())
        {
            RunAway();
        }
        else
        {
            if (Time.time > runEndTime)
            {
                Wander();
                runEndTime = Time.time + runDuration;
            }
        }

        if (EnemyHealthPoints <= 0)
        {
            ani.SetTrigger("Dead");
            Destroy(gameObject);
        }
    }

    void hurt()
    {
        speed = 0;
        if (Time.time > HurtTimer)
        {
            EnemyHealthPoints -= PlayerDamage;
            Hurting = false;
        }
    }
    bool IsPlayerDetected()
    {
        return target != null && Vector2.Distance(rb.position, target.transform.position) <= runAwayRadius;
    }

    void StopMovement()
    {
        resetAni();
        ani.SetBool("Idle", true);
        direction = Vector2.zero;
        speed = 0f;
    }

    void Walk(Vector2 targetPosition)
    {
        resetAni();
        ani.SetBool("Run", true);
        direction = (targetPosition - rb.position).normalized;
        speed = 1.5f;
    }

    void Wander()
    {
        if (Time.time > wanderTimer)
        {
            resetAni();
            ani.SetBool("Run", true);
            float randomAngle = rng.Next(0, 360) * Mathf.PI / 180;
            Vector2 wanderDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            Vector2 wanderPosition = rb.position + wanderDirection * wanderRadius;

            Walk(wanderPosition);
            wanderTimer = Time.time + wanderCooldown;
        }
        else
        {
            StopMovement();
        }
    }

    void RunAway()
    {
        if (target != null)
        {
            resetAni();
            ani.SetBool("Run", true);
            Vector2 runAwayDirection = (rb.position - (Vector2)target.transform.position).normalized;
            Vector2 runAwayPosition = rb.position + runAwayDirection * runAwayRadius;

            direction = (runAwayPosition - rb.position).normalized;
            speed = 3f;
        }
        else
        {
            StopMovement();
        }
    }
    void resetAni()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", false);
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            target = trig.gameObject;
        }

        if (trig.gameObject.CompareTag("Player_Bullet"))
        {
            if (!Hurting)
            {
                HurtTimer = Time.time + 0.5f;
                Hurting = true;
            }
        }
        if (trig.gameObject.tag == "Explosion")
        {
            Knockback = (transform.position - trig.gameObject.transform.position).normalized * 20;
            if (!Hurting)
            {
                HurtTimer = Time.time + 0.5f;
                Hurting = true;
            }
        }
    }
}
