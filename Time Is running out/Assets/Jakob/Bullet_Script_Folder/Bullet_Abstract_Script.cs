using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet_Abstract_Script : MonoBehaviour
{
    public float Damage { get; set; }
    public string DoesDamageTo { get; set; }
    public Vector2 Direction { get; set; }
    public float speed { get; set; }
    public float FlyTime { get; set; }
    public Rigidbody2D rb { get; set; }
    public void BulletMovement()
    {
        rb.velocity = Direction * speed;
        FlyTime -= Time.deltaTime;
        if(FlyTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
