using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Attack_Script : Bullet_Abstract_Script
{
    [SerializeField] GameObject Player;
    void Start()
    {
        Direction = new Vector2(0, 0);
        speed = 1;
        FlyTime = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BulletMovement();
    }
    void SlashBehaviour()
    {

    }
}
