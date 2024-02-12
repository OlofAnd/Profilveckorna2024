using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting_Script : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] SpriteRenderer sprRen;
    float rotZ;
    public bool canFire;
    public Transform bulletTransform;
    private float timer;
    public float timeBetweenFiring;
    public GameObject bullet;
    [SerializeField] Player_Script player_Script;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (rotZ > 90f && rotZ < 180f || rotZ < -90f && rotZ > -180f)
            sprRen.flipY = true;
        else if (rotZ < 0f && rotZ > -90f || rotZ > 0f && rotZ < 90f)
            sprRen.flipY = false;
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
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
