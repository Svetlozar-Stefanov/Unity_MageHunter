using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private FightingComponent fightingComponent;
    [SerializeField] private AnimatorComponent animatorComponent;

    private float direction = 0.0f;
    private bool startJump = false;

    public FightingComponent FightingComponent { get => fightingComponent; }

    private void OnEnable()
    {
        inputReader.moveEvent += OnMoveInitiated;
        inputReader.jumpEvent += OnJumpInitiated;
        inputReader.lightCastEvent += OnLightCastInitiated;
        inputReader.heavyCastEvent += OnHeavyCastInitiated;
        inputReader.moveMouseEvent += OnMouseMoveInitiated;
        inputReader.lightSpellScroll += OnLightSpellScrollInitiated;
        inputReader.heavySpellScroll += OnHeavySpellScrollInitiated;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMoveInitiated;
        inputReader.jumpEvent -= OnJumpInitiated;
        inputReader.lightCastEvent -= OnLightCastInitiated;
        inputReader.heavyCastEvent -= OnHeavyCastInitiated;
        inputReader.moveMouseEvent -= OnMouseMoveInitiated;
        inputReader.lightSpellScroll -= OnLightSpellScrollInitiated;
        inputReader.heavySpellScroll -= OnHeavySpellScrollInitiated;
    }

    private void FixedUpdate()
    {
        movementComponent.Move(direction);
        if (direction != 0)
        {
            animatorComponent.IsMoving(true);
        }
        else
        {
            animatorComponent.IsMoving(false);
        }

        if (startJump)
        {
            movementComponent.Jump();
            startJump = false;
        }

        animatorComponent.IsFalling(movementComponent.IsFalling);
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

    public void OnLightCastInitiated()
    {
        fightingComponent.CastLightSpell();
    }

    public void OnHeavyCastInitiated()
    {
        fightingComponent.CastHeavySpell();
    }

    public void OnMouseMoveInitiated(Vector2 mousePos)
    {
        fightingComponent.SetMousePos(mousePos);
    }

    public bool IsChangingSpellSelector()
    {
        return inputReader.isChangingSpellSelector;
    }

    public void OnLightSpellScrollInitiated(float sVal)
    {
        int offset = GetScrollOffset(sVal);

        fightingComponent.LightSpellShift(offset);
    }

    public void OnHeavySpellScrollInitiated(float sVal)
    {
        int offset = GetScrollOffset(sVal);

        fightingComponent.HeavySpellShift(offset);
    }

    private int GetScrollOffset(float sVal)
    {
        int offset = 0;
        if (sVal > 0)
        {
            offset = 1;
        }
        else if (sVal < 0)
        {
            offset = -1;
        }

        return offset;
    }
}
