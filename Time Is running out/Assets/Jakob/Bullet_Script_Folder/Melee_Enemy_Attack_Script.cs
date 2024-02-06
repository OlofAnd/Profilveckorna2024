using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Attack_Script : Bullet_Abstract_Script
{
    void Start()
    {
        Direction = new Vector2(1, 0);
        speed = 1;
        FlyTime = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BulletMovement();
    }
}
