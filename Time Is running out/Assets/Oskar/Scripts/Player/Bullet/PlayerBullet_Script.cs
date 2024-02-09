using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerBullet_Script : Bullet_Abstract_Script
{
    Vector3 mousePos;
    Camera cam;
    public float force;
    public float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        FlyTime = timeAlive;
        Speed = force;
        Target = "Enemy";
        rb = GetComponent<Rigidbody2D>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Direction = mousePos - transform.position;

        Vector3 rotation = transform.position - mousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    void Update()
    {
        BulletMovement();
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (!other.CompareTag("Player"))
    //        Destroy(gameObject);
    //}
}