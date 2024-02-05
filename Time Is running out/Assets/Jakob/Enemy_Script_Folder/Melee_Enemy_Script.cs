using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class Melee_Enemy_Script : Enemy_Abstract_Script
{
    Rigidbody2D rb;
    [SerializeField] Enemy_Game_Info_Script GI;
    Vector2 Direction;
    float Speed;
    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnemyBehaviour();
        Debug.Log(Vector2.Distance(rb.position, GI.PlayerPosition));
    }
    public override void EnemyBehaviour()
    {
        if (Vector2.Distance(rb.position, GI.PlayerPosition) >= 5)
        {
            Idle();
        }
        else if (Vector2.Distance(rb.position, GI.PlayerPosition) <= 5 && Vector2.Distance(rb.position, GI.PlayerPosition) >= 3)
        {
            Dash();
        }
    }
    void Idle()
    {
        Direction = (GI.PlayerPosition - rb.position) / Vector2.Distance(Vector2.zero, GI.PlayerPosition - rb.position);
        Speed = 1f;
        rb.velocity = Direction * Speed;
    }
    void Dash()
    {
        Direction = (GI.PlayerPosition - rb.position) / Vector2.Distance(Vector2.zero, GI.PlayerPosition - rb.position);
        Speed = 2f;
        rb.velocity = Direction * Speed;
    }
    void Attack()
    {

    }
    void Walk()
    {

    }
}
