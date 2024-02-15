using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class EnemyShooting_Script : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] SpriteRenderer sprRen;
    float rotZ;
    public Transform bulletTransformRightSide;
    public Transform bulletTransformLeftSide;
    [SerializeField] GameObject Enemy;
    Player_Script Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = FindObjectOfType<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotZ > 90f && rotZ < 180f || rotZ < -90f && rotZ > -180f)
        {
            sprRen.flipY = true;
            transform.position = new Vector3(0.12f, 0.2f, 0) + Enemy.transform.position;
        }
        else if (rotZ < 0f && rotZ > -90f || rotZ > 0f && rotZ < 90f)
        {
            sprRen.flipY = false;
            transform.position = new Vector3(-0.12f, 0.2f, 0) + Enemy.transform.position;
        }

        Vector3 rotation = (Target.transform.position - transform.position) / Vector2.Distance(Vector2.zero, Target.transform.position - transform.position);

        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }
}
