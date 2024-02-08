using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : Bullet_Abstract_Script
{
    private GameObject Player;
    void Start()
    {
        Speed = 10f;
        FlyTime = 1f;
        Target = "Player";
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Direction = ((Vector2)Player.transform.position - rb.position).normalized;
    }
    void Update()
    {
        BulletMovement();
        Speed -= Time.deltaTime * (Speed / FlyTime);
    }
}
