using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Movement_Script : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sprRen;

    float frozenByMudCoolDown;
    public enum State { Normal, DodgeRollSliding, Död }
    public State state;


    [SerializeField] Player_Script player_Script;
    [SerializeField] GameController_Script gameController;


    [Header("Movement")]
    public float movementSpeed = 5.5f;
    Vector2 moveInput;

    [Header("Dodgeroll")]
    [SerializeField] float slidingSpeed;
    [SerializeField] Vector3 slideDir;
    bool canDodgeRoll = true;

    [Header("Knockback")]
    Vector2 knockback;

    [Header("Animations")]
    Animator ani;

    [Header("Mud Freeze")]
    public bool frozenByMud = false;
    public float unFreezeTimer;
    float unFreezeTimerValueHolder;
    public Sprite mudFreezeSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRen = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        state = State.Normal;
        unFreezeTimerValueHolder = unFreezeTimer;
    }

    void Update()
    {
        if (!player_Script.isAlive)
            state = State.Död;
        //State normal
        if (gameController.cardSelect || (frozenByMud && frozenByMudCoolDown > Time.time))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;

            if (player_Script.isAlive && state == State.Normal)
            {
                LookRightWay();
                Run();
                HandleAnimations();
                UnfreezeFromMud();
                slideDir = moveInput;
            }
            //State dodgerolling
            else if (state == State.DodgeRollSliding/* && !canDodgeRoll*/)
            {
                LookRightWay();
                HandleAnimations();
                HandleDodgeRollSliding();
                if (slidingSpeed <= 0.1f)
                {
                    canDodgeRoll = true;
                    state = State.Normal;
                    slidingSpeed = 10f;
                }
            }
            //State död/gameover
            else if (state == State.Död)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                rb.freezeRotation = true;
            }
        }


    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnDash()
    {
        if (!frozenByMud && canDodgeRoll)
        {
            canDodgeRoll = false;
            state = State.DodgeRollSliding;
        }

    }
    private void HandleDodgeRollSliding()
    {
        transform.position += slideDir * slidingSpeed * Time.deltaTime;
        slidingSpeed -= slidingSpeed * 10f * Time.deltaTime;
    }
    void Run()
    {
        rb.velocity = (Vector3)knockback;
        knockback = knockback * 0.93f;
        if (Vector2.Distance(Vector2.zero, knockback) <= 1)
            knockback = Vector2.zero;
        if (knockback == Vector2.zero)
        {
            rb.velocity = moveInput * movementSpeed;
            if (!frozenByMud)
                canDodgeRoll = true;
        }
    }
    private void LookRightWay()
    {
        if (moveInput.x < 0)
            sprRen.flipX = true;
        else if (moveInput.x > 0)
            sprRen.flipX = false;
    }
    private void UnfreezeFromMud()
    {
        if (frozenByMud)
            unFreezeTimer -= Time.deltaTime;
        if (frozenByMud && unFreezeTimer <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            frozenByMud = false;
            canDodgeRoll = true;
            ani.SetBool("isMudded", false);
            unFreezeTimer = unFreezeTimerValueHolder;
        }
    }
    private void HandleAnimations()
    {
        if (state == State.DodgeRollSliding)
        {
            ani.SetBool("isRunning", false);
            ani.SetBool("isDashing", true);
            ani.SetBool("isIdle", false);
        }
        else if ((moveInput.x != 0 || moveInput.y != 0) && !frozenByMud)
        {
            ani.SetBool("isIdle", false);
            ani.SetBool("isRunning", true);
            ani.SetBool("isDashing", false);
        }
        else if ((moveInput.x == 0 || moveInput.y == 0) && !frozenByMud)
        {
            ani.SetBool("isRunning", false);
            ani.SetBool("isDashing", false);
            ani.SetBool("isIdle", true);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mud_Bullet") && !frozenByMud)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            frozenByMud = true;
            frozenByMudCoolDown = Time.time + 1f;
            ani.SetBool("isIdle", false);
            ani.SetBool("isRunning", false);
            ani.SetBool("isMudded", true);
        }
        if (other.CompareTag("Explosion") && !frozenByMud)
        {
            canDodgeRoll = false;
            knockback = (transform.position - other.gameObject.transform.position).normalized * 30;
        }

    }


}
