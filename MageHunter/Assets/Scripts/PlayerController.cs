using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MovementController movementController;
    [SerializeField] private Weapon currentWeapon;

    [SerializeField] private Animator animator;

    private float direction = 0.0f;
    private bool startJump = false;

    private void OnEnable()
    {
        inputReader.moveEvent += OnMoveInitiated;
        inputReader.jumpEvent += OnJumpInstatiated;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMoveInitiated;
        inputReader.jumpEvent -= OnJumpInstatiated;
    }

    private void FixedUpdate()
    {
        movementController.Move(direction);
        if (startJump)
        {
            movementController.Jump();
            startJump = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", startJump);
    }

    public void OnMoveInitiated(float dir)
    {
        direction = dir;
    }

    public void OnJumpInstatiated()
    {
        startJump = true;
    }
}
