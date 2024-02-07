using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = System.Random;

public class Range_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject Target;
    [SerializeField] GameObject Bullet;

    Vector2 Direction;
    float Speed;

    Random RNG = new Random();

    bool PlayerDetected = false;
    bool walking;
    bool Attacking = false;
    bool Hurting = false;

    float IdleingTimer = 0;
    float AttackTimer = 0;
    float HurtTimer = 0;
    Vector2 NewLocation;
    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(Vector2.Distance(rb.position, NewLocation));
        EnemyBehaviour();
    }
    public override void EnemyBehaviour()
    {
        rb.velocity = Direction * Speed;
        if (Hurting)
        {
            HurtTimer = Time.time + 0.5f;
            hurt();
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
                AttackTimer = Time.time + 0.7f;
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
            Speed = 1;
        else
            Speed = 0;
    }
    void Attack()
    {
        if (Time.time > AttackTimer && NewLocation == (Vector2)transform.position)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            NewLocation = NextLocation();
        }
        else if (Vector2.Distance(NewLocation, transform.position) >= 1f)
        {
            Direction = (NewLocation - rb.position).normalized;
            Speed = 2f;
        }
        else if (Time.time > AttackTimer)
            Attacking = false;
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
        while (Vector2.Distance(newLocation, Target.transform.position) <= 7 && Vector2.Distance(newLocation, Target.transform.position) >= 5);

        return newLocation;
    }
    void Walk()
    {
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
        }
        if (trig.gameObject.tag == "Player_Bullet")
        {
            Hurting = true;
        }
    }
}
