using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBomb_Script : Bullet_Abstract_Script
{
    private GameObject Player;
    [SerializeField] GameObject Explosion;
    Vector3 mousePos;
    Camera cam;

    void Start()
    {
        Speed = 15f;
        FlyTime = 1f;
        Player = GameObject.FindGameObjectWithTag("Player");
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
        if (FlyTime - Time.deltaTime <= 0)
            Instantiate(Explosion, transform.position, transform.rotation);
        Speed -= Time.deltaTime * (Speed / FlyTime);
    }
}
