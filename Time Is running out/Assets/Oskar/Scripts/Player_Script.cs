using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Debug.Log(transform.localPosition);
    }
}
