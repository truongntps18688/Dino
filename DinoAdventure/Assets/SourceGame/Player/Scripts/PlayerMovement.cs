using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveDirection;
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 0.0f;
        rb.gravityScale = 0.0f;
    }
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
      rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));
    }
}
