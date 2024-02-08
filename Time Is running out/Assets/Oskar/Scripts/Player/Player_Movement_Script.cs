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



    private enum State { Normal, DodgeRollSliding, Död }
    private State state;


    [SerializeField] Player_Script player_Script;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 5.5f;
    Vector2 moveInput;

    [Header("Dodgeroll")]
    [SerializeField] float slidingSpeed;
    [SerializeField] Vector3 slideDir;
    bool canDodgeRoll = true;

    [Header("Animations")]
    [SerializeField] Animator ani;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprRen = GetComponent<SpriteRenderer>();

        state = State.Normal;
    }

    void Update()
    {
        if (!player_Script.isAlive)
            state = State.Död;
        //State normal
        if (player_Script.isAlive && state == State.Normal)
        {
            PlayerFacingDirection();
            Run();
            slideDir = moveInput;
        }
        //State dodgerolling
        else if (state == State.DodgeRollSliding && !canDodgeRoll)
        {
            PlayerFacingDirection();
            HandleDodgeRollSliding();
            if (slidingSpeed <= 0.1f)
            {
                canDodgeRoll = true;
                state = State.Normal;
                slidingSpeed = 10f;
            }
        }
        //State död/gameover
        else if(state == State.Död)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.freezeRotation = true;
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
