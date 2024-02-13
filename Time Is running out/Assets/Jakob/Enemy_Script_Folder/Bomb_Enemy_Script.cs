using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class Bomb_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    SpriteRenderer sprRen;
    [SerializeField] GameObject Explosion;

    Vector2 Direction;
    Vector2 KnockBack;
    Vector2 JumpTo;
    float Speed;

    float JumpTImer;
    float JumpDelay;
    float HurtTimer;

    bool Attacking = false;
    bool Walking = false;
    bool Hurting = false;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        sprRen = GetComponent<SpriteRenderer>();
        EnemyHealthPoints = 10f;
        Damage = 0f;

        ScoreValue = 10;
        DifficultyValue = 5;

        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        EnemyBehaviour();
        if (Direction.x > 0) sprRen.flipX = true;
        else sprRen.flipX = false;
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
        else if (Walking)
        {
            Walk();
        }
        else if (Attacking)
        {
            Attack();
        }
        else
        {
            if (Vector2.Distance(transform.position, Target.transform.position) >= 4)
            {
                Walking = true;
            }
            else
            {
                Attacking = true;
                JumpTo = (Vector2)(Target.transform.position);
                JumpDelay = Time.time + 0.1f;
                JumpTImer = JumpDelay + 0.5f;
            }
        }

        if (EnemyHealthPoints <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Walk()
    {
        Direction = ((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
        Speed = 6f;
        Walking = false;
    }
    void Attack()
    {
        Speed = 0;
        if (Time.time > JumpDelay)
        {
            Direction = (JumpTo - rb.position) / Vector2.Distance(Vector2.zero, JumpTo - rb.position);
            Speed = 30f * (JumpTImer - Time.time);
        }
        if (Time.time > JumpTImer)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
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
