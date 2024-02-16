using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;
using UnityEngine.Device;

public class mud_Enemy_Script : Enemy_Abstract_Script
{
    [SerializeField] GameObject bullet;
    [SerializeField] float teleportRadius = 5f;
    [SerializeField] float teleportCooldown = 1f;

    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sprRen;

    Vector2 Direction;

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
        EnemyHealthPoints = 1f + (Wave * 2);
        Damage = 1f;

        ScoreValue = 30;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprRen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (EnemySpawnCooldown < Time.time)
        {
            EnemyBehaviour();
        }
        rb.velocity = Vector2.zero;

        Direction = ((Vector2)(target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(target.transform.position) - rb.position);
        if (Direction.x > 0) sprRen.flipX = false;
        else sprRen.flipX = true;
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
                    attackTimer = Time.time + 0.667f;
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
        else if ((Vector2.Distance(rb.position, target.transform.position) >= 7 || (Vector2.Distance(rb.position, target.transform.position) < 4)))
        {
            resetAni();
            ani.SetBool("Up", true);
        }

    }

    bool ShouldTeleport()
    {
        return (Vector2.Distance(rb.position, target.transform.position) >= 7 || (Vector2.Distance(rb.position, target.transform.position) < 4 && teleportCooldown <= 0));
    }

    void Teleport()
    {
        transform.position = NextLocation();
        teleportCooldown = 2f;
        resetAni();
        ani.SetBool("Up", true);
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
        resetAni();
        ani.SetBool("Idle", true);
        playerDetected = false;
    }

    void Attack()
    {
        resetAni();
        ani.SetBool("Shoot", true);
        if (Time.time > attackTimer)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            attacking = false;
        }
    }

    void Hurt()
    {
        resetAni();
        ani.SetBool("Hurt", true);

        if (Time.time > hurtTimer)
        {
            EnemyHealthPoints -= PlayerDamage;
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
        while (spawnPoint.x <= -22 || spawnPoint.x >= 24 || spawnPoint.y >= 12 || spawnPoint.y <= -12);
        return spawnPoint;
    }
    void resetAni()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Down", false);
        ani.SetBool("Up", false);
        ani.SetBool("Shoot", false);
        ani.SetBool("Hurt", false);
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            playerDetected = true;
            target = trig.gameObject;
        }

        if (trig.gameObject.CompareTag("Player_Bullet"))
        {
            if (!hurting)
            {
                hurtTimer = Time.time + 0.5f;
                hurting = true;
            }
        }
        if (trig.gameObject.tag == "Explosion")
        {
            if (!hurting)
            {
                hurtTimer = Time.time + 0.5f;
                hurting = true;
            }
        }
    }
}
