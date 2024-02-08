using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

class Melee_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    [SerializeField] GameObject Bullet;

    Vector2 Direction;
    float Speed;
    Vector2 DashTo;

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
        EnemyDamage = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnemyBehaviour();
        // if (Target != null)
        // Debug.Log(IdleingTimer);
    }
    public override void EnemyBehaviour()
    {
        rb.velocity = Direction * Speed;
        if (Hurting)
        {
            HurtTimer = Time.time + 0.5f;
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
        EnemyHealthPoints--;
        if (Time.time > HurtTimer)
        {
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
            Hurting = true;
        }
    }
}

