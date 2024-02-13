using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;
using Unity.Mathematics;


public class Shooting_Script : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] SpriteRenderer sprRen;
    float rotZ;
    public bool canFire;
    public Transform bulletTransformRightSide;
    public Transform bulletTransformLeftSide;
    bool weaponFlip;
    private float timer;
    public float timeBetweenFiring;
    public GameObject bombBullet;
    [SerializeField] Player_Script player_Script;
    Random rNG = new Random();
    public int playerBombChance;


    public int numberOfBullets;
    int maxBullets = 24;

    List<GameObject> bulletList;
    List<GameObject> bombBulletList;

    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject bullet6;
    public GameObject bullet7;
    public GameObject bullet8;
    public GameObject bullet9;
    public GameObject bullet10;
    public GameObject bullet11;
    public GameObject bullet12;
    public GameObject bullet13;
    public GameObject bullet14;
    public GameObject bullet15;
    public GameObject bullet16;
    public GameObject bullet17;
    public GameObject bullet18;
    public GameObject bullet19;
    public GameObject bullet20;
    public GameObject bullet21;
    public GameObject bullet22;
    public GameObject bullet23;
    public GameObject bullet24;

    public GameObject bombBullet1;
    public GameObject bombBullet2;
    public GameObject bombBullet3;
    public GameObject bombBullet4;
    public GameObject bombBullet5;
    public GameObject bombBullet6;
    public GameObject bombBullet7;
    public GameObject bombBullet8;
    public GameObject bombBullet9;
    public GameObject bombBullet10;
    public GameObject bombBullet11;
    public GameObject bombBullet12;
    public GameObject bombBullet13;
    public GameObject bombBullet14;
    public GameObject bombBullet15;
    public GameObject bombBullet16;
    public GameObject bombBullet17;
    public GameObject bombBullet18;
    public GameObject bombBullet19;
    public GameObject bombBullet20;
    public GameObject bombBullet21;
    public GameObject bombBullet22;
    public GameObject bombBullet23;
    public GameObject bombBullet24;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletList = new List<GameObject>();
        bombBulletList = new List<GameObject>();

        bulletList.Add(bullet1);
        bulletList.Add(bullet2);
        bulletList.Add(bullet3);
        bulletList.Add(bullet4);
        bulletList.Add(bullet5);
        bulletList.Add(bullet6);
        bulletList.Add(bullet7);
        bulletList.Add(bullet8);
        bulletList.Add(bullet9);
        bulletList.Add(bullet10);
        bulletList.Add(bullet11);
        bulletList.Add(bullet12);
        bulletList.Add(bullet13);
        bulletList.Add(bullet14);
        bulletList.Add(bullet15);
        bulletList.Add(bullet16);
        bulletList.Add(bullet17);
        bulletList.Add(bullet18);
        bulletList.Add(bullet19);
        bulletList.Add(bullet20);
        bulletList.Add(bullet21);
        bulletList.Add(bullet22);
        bulletList.Add(bullet23);
        bulletList.Add(bullet24);

        bombBulletList.Add(bombBullet1);
        bombBulletList.Add(bombBullet2);
        bombBulletList.Add(bombBullet3);
        bombBulletList.Add(bombBullet4);
        bombBulletList.Add(bombBullet5);
        bombBulletList.Add(bombBullet6);
        bombBulletList.Add(bombBullet7);
        bombBulletList.Add(bombBullet8);
        bombBulletList.Add(bombBullet9);
        bombBulletList.Add(bombBullet10);
        bombBulletList.Add(bombBullet11);
        bombBulletList.Add(bombBullet12);
        bombBulletList.Add(bombBullet13);
        bombBulletList.Add(bombBullet14);
        bombBulletList.Add(bombBullet15);
        bombBulletList.Add(bombBullet16);
        bombBulletList.Add(bombBullet17);
        bombBulletList.Add(bombBullet18);
        bombBulletList.Add(bombBullet19);
        bombBulletList.Add(bombBullet20);
        bombBulletList.Add(bombBullet21);
        bombBulletList.Add(bombBullet22);
        bombBulletList.Add(bombBullet23);
        bombBulletList.Add(bombBullet24);
    }

    void Update()
    {
        if (numberOfBullets >= maxBullets)
            numberOfBullets = maxBullets;
        if (rotZ > 90f && rotZ < 180f || rotZ < -90f && rotZ > -180f)
        {
            sprRen.flipY = true;
            weaponFlip = true;
            transform.position = new Vector3(0.172f, -0.019f, 0) + player_Script.transform.position;
        }
        else if (rotZ < 0f && rotZ > -90f || rotZ > 0f && rotZ < 90f)
        {
            sprRen.flipY = false;
            weaponFlip = false;
            transform.position = new Vector3(-0.17f, 0, 0) + player_Script.transform.position;
        }
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire && player_Script.isAlive)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            if (rNG.Next(100) + 1 <= playerBombChance)
            {
                for (int i = 0; i < numberOfBullets; i++)
                {
                    if (weaponFlip)
                        Instantiate(bombBulletList[i], bulletTransformLeftSide.position, Quaternion.identity);
                    else
                        Instantiate(bombBulletList[i], bulletTransformRightSide.position, Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < numberOfBullets; i++)
                {
                    if (weaponFlip)
                        Instantiate(bulletList[i], bulletTransformLeftSide.position, Quaternion.identity);
                    else
                        Instantiate(bulletList[i], bulletTransformRightSide.position, Quaternion.identity);
                }
            }
        }
    }
}
