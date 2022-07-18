using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MovementComponent movementController;
    [SerializeField] private FightingComponent fightingComponent;
    [SerializeField] private AnimatorComponent animatorComponent;

    private float direction = 0.0f;
    private bool startJump = false;

    private void OnEnable()
    {
        inputReader.moveEvent += OnMoveInitiated;
        inputReader.jumpEvent += OnJumpInitiated;
        inputReader.shootEvent += OnShootInitiated;
        inputReader.moveMouseEvent += OnMouseMoveInitiated;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMoveInitiated;
        inputReader.jumpEvent -= OnJumpInitiated;
        inputReader.shootEvent -= OnShootInitiated;
        inputReader.moveMouseEvent -= OnMouseMoveInitiated;
    }

    private void FixedUpdate()
    {
        movementController.Move(direction);
        if (direction != 0)
        {
            animatorComponent.IsMoving(false);
        }
        else
        {
            animatorComponent.IsMoving(true);
        }

        if (startJump)
        {
            movementController.Jump();
            startJump = false;
        }
    }

    public void OnLanding()
    {
        animatorComponent.IsJumping(false);
    }

    public void OnMoveInitiated(float dir)
    {
        direction = dir;
    }

    public void OnJumpInitiated()
    {
        startJump = true;
        animatorComponent.IsJumping(true);
    }

    public void OnShootInitiated()
    {
        fightingComponent.SpawnProjectile();
    }

    public void OnMouseMoveInitiated(Vector2 mousePos)
    {
        fightingComponent.SetMousePos(mousePos);
    }
}
