using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Script : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] public int enemiesAlive = 0;
    [SerializeField] GameObject enemyObject;


    [Header("Timer")]
    [SerializeField] public float remainingTime;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(enemyObject, transform.localPosition, transform.rotation);
            enemiesAlive++;
            Debug.Log(enemiesAlive);
        }
    }
}
