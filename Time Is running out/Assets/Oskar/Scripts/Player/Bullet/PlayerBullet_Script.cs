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
    public float bulletDamage;

    [SerializeField] int degDiff;

    public Vector2 testDir;

    [SerializeField] Shooting_Script shootScript;

    void Start()
    {
        Damage = bulletDamage;

        FlyTime = timeAlive;
        Speed = force * Time.deltaTime;
        Target = "Enemy";
        rb = GetComponent<Rigidbody2D>();
        shootScript = FindObjectOfType<Shooting_Script>();

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
