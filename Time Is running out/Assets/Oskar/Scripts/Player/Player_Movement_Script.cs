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
    BoxCollider2D col;
    SpriteRenderer sprRen;
    Transform m_transform;


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


    bool frozenByMud = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprRen = GetComponent<SpriteRenderer>();
        m_transform = GetComponent<Transform>();

        state = State.Normal;
    }

    void Update()
    {
        if (!player_Script.isAlive)
            state = State.Död;
        //State normal
        if (player_Script.isAlive && state == State.Normal)
        {
            LAMouse();
            Run();
            slideDir = moveInput;
        }
        //State dodgerolling
        else if (state == State.DodgeRollSliding && !canDodgeRoll)
        {
            LAMouse();
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
    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle > 90f && angle < 180f || angle < -90f && angle > -180f)
            sprRen.flipX = true;
        else if (angle < 0f && angle > -90f || angle > 0f && angle < 90f)
            sprRen.flipX = false;
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mud_Bullet") && !frozenByMud)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            frozenByMud = true;
        }
    }
    private void UnfreezeFromMud()
    {
        if (frozenByMud)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            frozenByMud = false;
        }
    }

}
