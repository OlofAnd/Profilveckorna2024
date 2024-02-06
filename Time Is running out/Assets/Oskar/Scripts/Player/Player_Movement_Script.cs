using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_Script : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D col;
    [SerializeField] Player_Script player_Script;

    [Header("Movement")]
    Vector2 moveInput;
    [SerializeField] float movementSpeed = 5.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(player_Script.isAlive)
        {
            Run();
        }
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        rb.velocity = moveInput * movementSpeed;
    }
}
