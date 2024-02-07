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
    SpriteRenderer sprRen;
    private State state;
    private enum State { Normal, DodgeRollSliding, Pause }

    [SerializeField] Player_Script player_Script;

    [Header("Movement")]
    Vector2 moveInput;
    [SerializeField] float movementSpeed = 5.5f;

    [Header("Dodgeroll")]
    [SerializeField] float slidingSpeed;
    bool canDodgeRoll = true;
    [SerializeField] Vector3 slideDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprRen = GetComponent<SpriteRenderer>();

        state = State.Normal;
    }

    void Update()
    {
        PlayerFacingDirection();
        if (player_Script.isAlive && state == State.Normal)
        {
            Run();
            slideDir = moveInput;
        }
        else if (state == State.DodgeRollSliding && !canDodgeRoll)
        {
            HandleDodgeRollSliding();
            if (slidingSpeed <= 0.1f)
            {
                canDodgeRoll = true;
                state = State.Normal;
                slidingSpeed = 10f;
            }
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnDash()
    {
        canDodgeRoll = false;
        state = State.DodgeRollSliding;
    }

    private void HandleDodgeRollSliding()
    {
        transform.position += slideDir * slidingSpeed * Time.deltaTime;
        slidingSpeed -= slidingSpeed * 10f * Time.deltaTime;
    }

    void Run()
    {
        rb.velocity = moveInput * movementSpeed;
    }
    private void PlayerFacingDirection()
    {
        if (slideDir.x < 0)
            sprRen.flipX = true;
        else if (slideDir.x > 0)
            sprRen.flipX = false;
    }
}
