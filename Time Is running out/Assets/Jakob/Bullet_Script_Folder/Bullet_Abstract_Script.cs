using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet_Abstract_Script : MonoBehaviour
{
    public float Damage { get; set; }
    public string Target { get; set; }
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public float FlyTime { get; set; }
    public Rigidbody2D rb { get; set; }
    public void BulletMovement()
    {
        rb.velocity = new Vector2(Direction.x, Direction.y).normalized * Speed;
        FlyTime -= Time.deltaTime;
        if(FlyTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == Target)
        {
            Destroy(gameObject);
        }
    }
}
