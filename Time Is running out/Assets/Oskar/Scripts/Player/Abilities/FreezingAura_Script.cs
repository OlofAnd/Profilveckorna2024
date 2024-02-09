using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAura_Script : MonoBehaviour
{
    public bool isFreezingAuraActive;

    [SerializeField] Card_Information_Script CardInfo_Script;
    CircleCollider2D freezingAreaCollider;
    Rigidbody2D otherRb;
    SpriteRenderer sprRen;


    void Start()
    {
        freezingAreaCollider = GetComponent<CircleCollider2D>();
        sprRen = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //isFreezingAuraActive = CardInfo_Script.freezingAura;
        if (isFreezingAuraActive)
        {
            freezingAreaCollider.enabled = true;
            sprRen.enabled = true;

        }
        else
        {
            freezingAreaCollider.enabled = false;
            sprRen.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFreezingAuraActive)
        {
            if (other.CompareTag("Enemy"))
            {
                otherRb = other.GetComponent<Rigidbody2D>();
                otherRb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isFreezingAuraActive)
        {
            if (other.CompareTag("Enemy"))
            {
                otherRb = other.GetComponent<Rigidbody2D>();
                otherRb.constraints = RigidbodyConstraints2D.None;
                otherRb.freezeRotation = true;
            }
        }
    }
}
