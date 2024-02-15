using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;

public class mud_Enemy_Script : Enemy_Abstract_Script
{
    [SerializeField] GameObject bullet;
    [SerializeField] float teleportRadius = 5f;
    [SerializeField] float teleportCooldown = 1f;

    Rigidbody2D rb;
    GameObject target;
    Random rng = new Random();

    bool playerDetected = false;
    bool attacking = false;
    bool hurting = false;

    float attackTimer = 0;
    float hurtTimer = 0;

    void Start()
    {
        EnemySpawnCooldown = Time.time + 0.5f;
        EnemyHealthPoints = 10f;
        Damage = 1f;

        ScoreValue = 30;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (EnemySpawnCooldown < Time.time)
        {
            EnemyBehaviour();
        }
        rb.velocity = Vector2.zero;
    }

    public override void EnemyBehaviour()
    {
        if (hurting)
        {
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
            else
            {
                HandleTeleportation();
                if (!attacking)
                {
                    attacking = true;
                    attackTimer = Time.time + 1f;
                }
            }
        }

        UpdateCooldowns();

        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void HandleTeleportation()
    {
        if (ShouldTeleport())
        {
            Teleport();
        }
    }

    bool ShouldTeleport()
    {
        return (Vector2.Distance(rb.position, target.transform.position) >= 7 ||
                (Vector2.Distance(rb.position, target.transform.position) < 4 && teleportCooldown <= 0));
    }

    void Teleport()
    {
        transform.position = NextLocation();
        teleportCooldown = 2f;
    }

    void UpdateCooldowns()
    {
        if (teleportCooldown > 0)
        {
            teleportCooldown -= Time.deltaTime;
        }
    }

    void Idle()
    {
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

    void Hurt()
    {
        EnemyHealthPoints -= PlayerDamage;
        if (Time.time > hurtTimer)
        {
            hurting = false;
        }
    }

    Vector2 NextLocation()
    {
        Vector2 spawnPoint = Vector2.zero;
        do
        {
            float angle = rng.Next(0, 360);
            angle = angle * math.PI / 180;
            spawnPoint = target.transform.position + (Vector3)(new Vector2(math.cos(angle), math.sin(angle)) * teleportRadius);
        }
        while (spawnPoint.x <= -13 || spawnPoint.x >= 15 || spawnPoint.y >= 7 || spawnPoint.y <= -7);
        return spawnPoint;
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
