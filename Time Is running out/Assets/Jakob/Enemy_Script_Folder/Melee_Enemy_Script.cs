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

    bool startDashing = false;
    bool PlayerDetected = false;
    bool Attacking;

    float IdleingTimer = 3;
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
        Debug.Log(IdleingTimer);
    }
    public override void EnemyBehaviour()
    {
        rb.velocity = Direction * Speed;
        if (!startDashing)
        {
            if (!PlayerDetected)
            {
                Idle();
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) <= 7 && Vector2.Distance(rb.position, Target.transform.position) >= 5)
            {
                startDashing = true;
                DashTo = (Vector2)(Target.transform.position);
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) <= 5 && Vector2.Distance(rb.position, Target.transform.position) > 2)
            {
                Walk();
            }
            else if (Vector2.Distance(rb.position, Target.transform.position) <= 2)
            {
                Attack();
            }
        }
        else if (startDashing)
        {
            Dash();
        }
    }
    float angle;
    void Idle()
    {
        IdleingTimer -= Time.deltaTime;
        if (IdleingTimer < 0)
        {
            angle = RNG.Next(0, 360);
            angle = angle * math.PI / 180;
            IdleingTimer = 3;
        }

        Direction = new Vector2(math.cos(angle), math.sin(angle));
        if (IdleingTimer > 1)
            Speed = 1;
        else
            Speed = 0;
    }
    void Dash()
    {
        Direction = (DashTo - rb.position) / Vector2.Distance(Vector2.zero, DashTo - rb.position);
        Speed = 5f;

        if (Vector2.Distance(transform.position, DashTo) <= 1)
        {
            startDashing = false;
            Speed = 0f;
        }
    }
    void Attack()
    {
        Speed = 0f;
        Instantiate(Bullet, transform.position,new Quaternion(transform.rotation.x,transform.rotation.y, Vector2.Angle(transform.position, Target.transform.position), transform.rotation.w));
    }
    void Walk()
    {
        Direction = ((Vector2)(Target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(Target.transform.position) - rb.position);
        Speed = 2f;
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            PlayerDetected = true;
            Target = trig.gameObject;
            DashTo = (Vector2)(Target.transform.position);
        }
    }
}
