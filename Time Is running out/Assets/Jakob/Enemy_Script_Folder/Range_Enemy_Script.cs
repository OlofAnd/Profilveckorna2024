using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;
using UnityEngine.Device;

public class Range_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Bomb;

    Animator ani;
    SpriteRenderer sprRen;
    Vector2 Direction;
    float Speed;
    Vector2 KnockBack;

    Random RNG = new Random();

    bool PlayerDetected = false;
    bool Attacking = false;
    bool Hurting = false;

    float IdleingTimer = 0;
    float AttackTimer = 0;
    float HurtTimer = 0;
    Vector2 NewLocation;
    void Start()
    {
        EnemySpawnCooldown = Time.time + 0.5f;
        EnemyHealthPoints = 10f + (Wave * 2);
        Damage = 15f + (Wave * 2);

        ScoreValue = 20;
        sprRen = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (EnemySpawnCooldown < Time.time)
        {
            EnemyBehaviour();
        }
        if (Direction.x > 0) sprRen.flipX = false;
        else sprRen.flipX = true;
    }
    public override void EnemyBehaviour()
    {
        rb.velocity = (Vector3)KnockBack;
        KnockBack = KnockBack * 0.99f;
        if (Vector2.Distance(Vector2.zero, KnockBack) <= 1)
        {
            KnockBack = Vector2.zero;
        }
        if (KnockBack == Vector2.zero)
        {
            rb.velocity = Direction * Speed;
        }

        if (Hurting)
        {
            Hurt();
        }
        else if (Attacking)
        {
            Attack();
        }
        else
        {
            if (!PlayerDetected)
            {
                Idle();
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) >= 7 || Vector2.Distance(rb.position, Target.transform.position) < 5)
            {
                Walk();
            }
            else
            {
                Speed = 0f;
                Attacking = true;
                AttackTimer = Time.time + 1f;
                NewLocation = (Vector2)transform.position;
            }
        }

        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    float angle;
    void Idle()
    {
        if (IdleingTimer < Time.time)
        {
            angle = RNG.Next(0, 360);
            angle = angle * math.PI / 180;
            IdleingTimer = Time.time + RNG.Next(2, 3);
        }

        Direction = new Vector2(math.cos(angle), math.sin(angle));
        if (IdleingTimer > Time.time + 1)
        {
            Speed = 1;

            ani.SetBool("Idle", false);
            ani.SetBool("Walk", true);
            ani.SetBool("hurt", false);
        }
        else
        {
            Speed = 0;
            ani.SetBool("Idle", true);
            ani.SetBool("Walk", false);
            ani.SetBool("hurt", false);
        }
    }
    void Attack()
    {
        if (Time.time > AttackTimer && NewLocation == (Vector2)transform.position)
        {

            ani.SetBool("Idle", true);
            ani.SetBool("Walk", false);
            ani.SetBool("hurt", false);
            if (RNG.Next(0, 5) == 0)
                Instantiate(Bomb, transform.position, transform.rotation);
            else
                Instantiate(Bullet, transform.position, transform.rotation);
            NewLocation = NextLocation();
        }
        else if (Vector2.Distance(NewLocation, transform.position) >= 1f)
        {
            Direction = (NewLocation - rb.position).normalized;
            Speed = 2f;

            ani.SetBool("Idle", false);
            ani.SetBool("Walk", true);
            ani.SetBool("hurt", false);
        }
        else if (Time.time > AttackTimer)
            Attacking = false;
        else
        {
            ani.SetBool("Idle", true);
            ani.SetBool("Walk", false);
            ani.SetBool("hurt", false);
        }
    }
    Vector2 NextLocation()
    {
        Vector2 newLocation = transform.position;
        do
        {
            angle = RNG.Next(0, 360);
            angle = angle * math.PI / 180;
            newLocation = rb.position + (new Vector2(math.cos(angle), math.sin(angle)) * 3);
        }
        while (Vector2.Distance(newLocation, Target.transform.position) <= 7 && Vector2.Distance(newLocation, Target.transform.position) >= 5 && (newLocation.x <= -8 || newLocation.x >= 27 || newLocation.y >= 14 || newLocation.y <= -4));

        ;
        return newLocation;
    }
    void Walk()
    {

        ani.SetBool("Idle", false);
        ani.SetBool("Walk", true);
        ani.SetBool("hurt", false);
        if (Vector2.Distance(rb.position, Target.transform.position) < 5)
        {
            Direction = -((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
            Speed = 3f;
        }
        if (Vector2.Distance(rb.position, Target.transform.position) >= 7)
        {
            Direction = ((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
            Speed = 3f;
        }
    }
    void Hurt()
    {
        ani.SetBool("hurt", true);
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        Speed = 0;
        if (Time.time > HurtTimer)
        {
            EnemyHealthPoints -= PlayerDamage;
            Hurting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            PlayerDetected = true;
            Target = trig.gameObject;
        }
        if (trig.gameObject.tag == "Player_Bullet")
        {
            if (!Hurting)
            {
                HurtTimer = Time.time + 0.5f;
                Hurting = true;
            }
        }
        if (trig.gameObject.tag == "Explosion")
        {
            KnockBack = (transform.position - trig.gameObject.transform.position).normalized * 20;
            if (!Hurting)
            {
                HurtTimer = Time.time + 0.5f;
                Hurting = true;
            }
        }

    }
}
