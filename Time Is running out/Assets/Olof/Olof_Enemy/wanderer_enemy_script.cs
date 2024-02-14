using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = System.Random;

public class wanderer_enemy_script : Enemy_Abstract_Script
{
    private Rigidbody2D rb;
    private GameObject target;
    private Vector2 direction;

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
        EnemyHealthPoints = 10f;
        Damage = 0f;
    }

    void Update()
    {
        if (EnemySpawnCooldown < Time.time)
        {
            EnemyBehaviour();
        }
    }

    public override void EnemyBehaviour()
    {
        rb.velocity = direction * speed;

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
            Destroy(gameObject);
        }
    }

    bool IsPlayerDetected()
    {
        return target != null && Vector2.Distance(rb.position, target.transform.position) <= runAwayRadius;
    }

    void StopMovement()
    {
        direction = Vector2.zero;
        speed = 0f;
    }

    void Walk(Vector2 targetPosition)
    {
        direction = (targetPosition - rb.position).normalized;
        speed = 1.5f;
    }

    void Wander()
    {
        if (Time.time > wanderTimer)
        {
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

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            target = trig.gameObject;
        }

        if (trig.gameObject.CompareTag("Player_Bullet"))
        {
            EnemyHealthPoints--;
        }
    }
}
