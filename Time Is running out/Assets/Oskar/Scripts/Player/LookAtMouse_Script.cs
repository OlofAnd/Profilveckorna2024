using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse_Script : MonoBehaviour
{
    [SerializeField] private Transform m_transform;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    float angle;
    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        LAMouse();
        if (angle > 90f && angle < 180f || angle < -90f && angle > -180f)
            m_spriteRenderer.flipY = true;
        else if (angle < 0f && angle > -90f || angle > 0f && angle < 90f)
            m_spriteRenderer.flipY = false;
    }
    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_transform.rotation = rotation;
        Debug.Log(angle);
    }
}
