using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveDirection;
    public float moveSpeed;


    private Vector3 mousePos;
    private Vector3 weaponPos;
    private float angle;
    [SerializeField] private Transform weaponTransform;

    public static Action shootInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 0.0f;
        rb.gravityScale = 0.0f;
    }
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shootInput?.Invoke();
        }
        
    }
    void FixedUpdate()
    {
        CalculateMovement();
        WeaponRotation();
    }
    void CalculateMovement()
    {
        rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));
        
    }

    public void WeaponRotation()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f; //The distance between the camera and object
        weaponPos = Camera.main.WorldToScreenPoint(weaponTransform.position);
        mousePos.x = mousePos.x - weaponPos.x;
        mousePos.y = mousePos.y - weaponPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle < -90 || angle > 90)
        {
            weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, -1, weaponTransform.localScale.z);
        }
        else
        {
            weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, 1, weaponTransform.localScale.z);
        }
    }
}
