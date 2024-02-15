using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAura_Script : MonoBehaviour
{
    public bool isFreezingAuraActive;

    CircleCollider2D freezingAreaCollider;
    Rigidbody2D otherRb;
    SpriteRenderer sprRen;
    [SerializeField] GameObject ParticleSystem;

    void Start()
    {
        freezingAreaCollider = GetComponent<CircleCollider2D>();
        sprRen = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        freezingAreaCollider.enabled = isFreezingAuraActive;
        sprRen.enabled = isFreezingAuraActive;
        ParticleSystem.SetActive(isFreezingAuraActive);
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
