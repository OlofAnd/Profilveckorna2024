using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class PlayerBullet_Script : Bullet_Abstract_Script
{
    Vector3 mousePos;
    Camera cam;
    public float force;
    public float timeAlive;
    public float bulletDamage;


    int degDiff;

    public Vector2 testDir;

    Random RNG = new Random();
    Shooting_Script shootScript;

    void Start()
    {
        Damage = bulletDamage;
        shootScript = FindObjectOfType<Shooting_Script>();
        timeAlive = shootScript.TimeAlive;
        force = shootScript.Force;

        FlyTime = timeAlive;
        Speed = force * Time.deltaTime;
        Target = "Enemy";
        rb = GetComponent<Rigidbody2D>();
        shootScript = FindObjectOfType<Shooting_Script>();
        degDiff = RNG.Next(-shootScript.numberOfBullets * 7, shootScript.numberOfBullets * 7);

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Direction = mousePos - transform.position;
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = Mathf.Rad2Deg * angleRad;
        float angleRadWithOffset = Mathf.Deg2Rad * (angleDeg + degDiff);
        Direction = new Vector2(Mathf.Cos(angleRadWithOffset), Mathf.Sin(angleRadWithOffset));
        transform.rotation = Quaternion.Euler(0, 0, angleDeg + 180 + degDiff);
    }
    void Update()
    {
        BulletMovement();
    }
}
