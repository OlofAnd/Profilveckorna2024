using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = System.Random;

public class wanderer_enemy_script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    GameObject target;

    Vector2 direction;
    float speed;
    float angle;

    Random rng = new Random();

    bool playerDetected = false;
    bool hurting = false;

    float idleTimer = 0;
    float hurtTimer = 0;

    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 0f;

        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        EnemyBehaviour();
    }
    public override void EnemyBehaviour()
    {
        // alla behaviour här a.k.a hur enemy ska hantera dess olika behaviour

        rb.velocity = direction * speed;
    }

    
    void Idle()
    {
        if (idleTimer < Time.time)
        {
            
        }
    }
    void Walk()
    {
        direction = ((Vector2)(target.transform.position) - rb.position) / Vector2.Distance(Vector2.zero, (Vector2)(target.transform.position) - rb.position);
        speed = 3f;
    }
    void hurt()
    {
        EnemyHealthPoints--;
        if(Time.time>hurtTimer)
        {
            hurting = false;
        }
    }
}
