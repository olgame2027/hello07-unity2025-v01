using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player03Move : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f;
    public float turnSpeed = 360f;
    
    private Vector3 moveDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }
    
    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
