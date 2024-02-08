using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Script : Bullet_Abstract_Script
{
    void Start()
    {
        Speed = 0f;
        FlyTime = 0.3f;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        BulletMovement();
    }
}
