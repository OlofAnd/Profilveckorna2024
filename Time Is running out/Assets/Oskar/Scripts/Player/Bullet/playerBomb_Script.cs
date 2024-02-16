using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class playerBomb_Script : Bullet_Abstract_Script
{
    private GameObject Player;
    [SerializeField] GameObject Explosion;
    Vector3 mousePos;
    Camera cam;

    int degDiff;

    Random RNG = new Random();
    Shooting_Script shootScript;

    void Start()
    {
        shootScript = FindObjectOfType<Shooting_Script>();
        Speed = 2f;
        FlyTime = 1f;
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        degDiff = RNG.Next(-shootScript.numberOfBullets * 7, shootScript.numberOfBullets * 7);

        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = Mathf.Rad2Deg * angleRad;
        float angleRadWithOffset = Mathf.Deg2Rad * (angleDeg + degDiff);
        Direction = new Vector2(Mathf.Cos(angleRadWithOffset), Mathf.Sin(angleRadWithOffset));


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
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
}
