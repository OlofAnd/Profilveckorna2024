using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Enemy_Bullet_script : Bullet_Abstract_Script
{

    private GameObject Player;
    void Start()
    {
        Damage = 5f;
        Speed = 6f;
        FlyTime = 5f;
        Target = "Player";
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Direction = ((Vector2)Player.transform.position - rb.position).normalized;

        //float rotation = Mathf.Atan2(-Direction.x, Direction.y) * Mathf.Rad2Deg;
        //transform.position += (Vector3)Direction;
        //transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    void Update()
    {
        BulletMovement();
    }
}
