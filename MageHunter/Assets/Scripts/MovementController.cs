using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
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

    private Rigidbody2D rigidbody2D;
    private Vector3 currentVelocity = Vector3.zero;
    private bool facingRight = true;
    private bool isGrounded;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void FixedUpdate()
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
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float direction)
    {
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(direction * movementSpeed * Time.deltaTime * 5, rigidbody2D.velocity.y);
            if (movementSmoothing > 0.0f)
            {
                targetVelocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
            }
            
            rigidbody2D.velocity = targetVelocity;

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
            rigidbody2D.AddForce(new Vector2(0.0f, jumpForce * 10));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}