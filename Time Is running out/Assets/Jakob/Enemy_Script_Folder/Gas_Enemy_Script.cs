using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Device;

public class Gas_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    Animator ani;
    SpriteRenderer sprRen;


    Vector2 Direction;
    float Speed;
    Vector2 DashTo;

    Vector2 KnockBack;

    Random RNG = new Random();

    bool PlayerDetected = false;
    bool walking;
    bool Hurting = false;

    float IdleingTimer = 0;
    float HurtTimer = 0;
    void Start()
    {
        EnemySpawnCooldown = Time.time + 0.5f;
        EnemyHealthPoints = 10f;
        Damage = 0;

        ScoreValue = 50;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Speed == 0)
        {
            ani.SetBool("isIdle", true);
            ani.SetBool("isRunning", false);
        }
        else
        {
            ani.SetBool("isIdle", false);
            ani.SetBool("isRunning", true);
        }
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
            hurt();
        }
        else
        {
            if (!PlayerDetected)
            {
                Idle();
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) > 2)
            {
                Walk();
            }
            else
            {
                Speed = 0;
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
            Speed = 0.5f;
        else
            Speed = 0;
    }
    void Walk()
    {
        Direction = ((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
        Speed = 0.5f;

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
