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
    [SerializeField] Player_Movement_Script player_Movement_Script;

    public int numberOfBullets;
    int maxBullets = 24;

    public GameObject bullet1;

    public GameObject bombBullet1;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        if (Input.GetMouseButton(0) && canFire && !player_Movement_Script.frozenByMud)
        {
            canFire = false;
            if (rNG.Next(100) + 1 <= playerBombChance)
            {
                for (int i = 0; i < numberOfBullets; i++)
                {
                    if (weaponFlip)
                        Instantiate(bombBullet1, bulletTransformLeftSide.position, Quaternion.identity);
                    else
                        Instantiate(bombBullet1, bulletTransformRightSide.position, Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < numberOfBullets; i++)
                {
                    if (weaponFlip)
                        Instantiate(bullet1, bulletTransformLeftSide.position, Quaternion.identity);
                    else
                        Instantiate(bullet1, bulletTransformRightSide.position, Quaternion.identity);
                }
            }
        }
    }
}
