using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MovementController movementController;
    [SerializeField] private Weapon currentWeapon;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private float direction = 0.0f;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(direction) > 0.0f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.Shoot();
        }
    }

    private void FixedUpdate()
    {
        movementController.Move(direction);
        if (jump)
        {
            movementController.Jump();
            jump = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}
