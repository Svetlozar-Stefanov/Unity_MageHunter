using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementComponent : MonoBehaviour
{
    const float GROUNDED_RADIUS = .2f;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10.0f;
    [Range(0.0f, 0.5f)][SerializeField] float movementSmoothing = 0.1f;
    
    [Header("Jumping")]
    [SerializeField] private bool airControl = false;
    [SerializeField] private float jumpForce = 40.0f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    [Header("Events")]
    [SerializeField] private UnityEvent onLandEvent;

    private Rigidbody2D rb2d;
    private Vector3 currentVelocity = Vector3.zero;
    private bool facingRight = true;
    private bool isGrounded = false;
    private bool isFalling = false;

    [HideInInspector]
    public bool IsFalling
    {
        get { return isFalling; }
    }


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GroundCheckCalc();

        IsFallingCalc();
    }

    public void Move(float direction)
    {
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(direction * movementSpeed * Time.deltaTime * 5, rb2d.velocity.y);
            if (movementSmoothing > 0.0f)
            {
                targetVelocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
            }
            
            rb2d.velocity = targetVelocity;

            if (direction > 0.0f && !facingRight)
            {
                Flip();
            }
            else if (direction < 0.0f && facingRight)
            {
                Flip();
            }
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb2d.AddForce(new Vector2(0.0f, jumpForce * 10));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void GroundCheckCalc()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GROUNDED_RADIUS, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    onLandEvent.Invoke();
                }
            }
        }
    }

    private void IsFallingCalc()
    {
        if (rb2d.velocity.y < -0.1)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }
}