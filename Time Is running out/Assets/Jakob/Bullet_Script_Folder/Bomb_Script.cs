using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : Bullet_Abstract_Script
{
    private GameObject Player;
    [SerializeField] GameObject Explosion;
    void Start()
    {
        Speed = 15f;
        FlyTime = 1f;
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Direction = ((Vector2)Player.transform.position - rb.position).normalized;
    }
    void Update()
    {
        BulletMovement();
        if (FlyTime - Time.deltaTime <= 0)
            Instantiate(Explosion, transform.position, transform.rotation);
        Speed -= Time.deltaTime * (Speed / FlyTime);
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
}
