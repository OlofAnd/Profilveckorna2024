using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mud_Enemy_Bullet_Script : Bullet_Abstract_Script
{
    private GameObject Player;

    void Start()
    {
        Damage = 5f;
        Speed = 6f;
        FlyTime = 2f;
        Target = "Player";
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Direction = ((Vector2)Player.transform.position - rb.position).normalized;
    }

    void Update()
    {
        BulletMovement();    
    }
}
