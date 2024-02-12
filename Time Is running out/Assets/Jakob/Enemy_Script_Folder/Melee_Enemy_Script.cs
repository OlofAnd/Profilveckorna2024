using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;

class Melee_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    [SerializeField] GameObject Bullet;
    [SerializeField] SpriteRenderer sprRen;

    Vector2 Direction;
    float Speed;
    Vector2 DashTo;
    Animator ani;

    Vector2 KnockBack;

    Random RNG = new Random();

    bool Dashing = false;
    bool PlayerDetected = false;
    bool walking;
    bool Attacking = false;
    bool Hurting = false;

    float IdleingTimer = 0;
    float AttackTimer = 0;
    float HurtTimer = 0;
    void Start()
    {
        EnemyHealthPoints = 10f;
        Damage = 1f;
        ScoreValue = 10;
        DifficultyValue = 1;

        rb = GetComponent<Rigidbody2D>();
        sprRen = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyBehaviour();
        if (!PlayerDetected)
        {
            if (Direction.x > 0) sprRen.flipX = true;
            else sprRen.flipX = false;
        }
        else
        {
            if (Direction.x > 0) sprRen.flipX = false;
            else sprRen.flipX = true;
        }

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
            hurt();
        }
        else if (Dashing)
        {
            Dash();
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
            else if (Vector2.Distance(rb.position, Target.transform.position) <= 7 && Vector2.Distance(rb.position, Target.transform.position) >= 6.5)
            {
                Dashing = true;
                DashTo = (Vector2)(Target.transform.position);
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) > 2)
            {
                Walk();
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) <= 2)
            {
                Attacking = true;
                AttackTimer = Time.time + 0.7f;
            }
        }
        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        if (PlayerDetected)
        {
            ani.SetBool("isIdle", false);
            ani.SetBool("isRunning", true);
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
            Speed = 1;
        else
            Speed = 0;
    }
    void Dash()
    {
        Direction = (DashTo - rb.position) / Vector2.Distance(Vector2.zero, DashTo - rb.position);
        Speed = 9f;

        if (Vector2.Distance(transform.position, DashTo) <= 2)
        {
            Dashing = false;
            Speed = 0f;
            AttackTimer = 0f;
            Attack();
        }
    }
    void Attack()
    {
        Speed = 0f;
        if (Time.time > AttackTimer)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            Attacking = false;
        }
    }
    void Walk()
    {
        Direction = ((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
        Speed = 4f;

    }
    void hurt()
    {
        Speed = 0;
        if (Time.time > HurtTimer)
        {
            EnemyHealthPoints--;
            Hurting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {

            PlayerDetected = true;
            Target = trig.gameObject;
            DashTo = (Vector2)(Target.transform.position);
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

