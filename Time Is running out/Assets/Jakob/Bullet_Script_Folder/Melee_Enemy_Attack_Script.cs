using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Melee_Enemy_Attack_Script : Bullet_Abstract_Script
{
    private GameObject Player;
    void Start()
    {
        Damage = 20f;
        Speed = 0f;
        FlyTime = 0.3f;
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Direction = ((Vector2)Player.transform.position - rb.position).normalized;

        float rotation = Mathf.Atan2(-Direction.x, Direction.y) * Mathf.Rad2Deg + 90;
        transform.position += (Vector3)Direction;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    void Update()
    {
        BulletMovement();
    }
}
