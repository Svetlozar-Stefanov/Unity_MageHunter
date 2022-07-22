using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/IO/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IInGameActions
{
    //Gameplay
    public event UnityAction jumpEvent;
    public event UnityAction<float> moveEvent;

	public event UnityAction lightCastEvent;
	public event UnityAction heavyCastEvent;

	public event UnityAction<float> lightSpellScroll;
	public event UnityAction<float> heavySpellScroll;

	public event UnityAction<Vector2> moveMouseEvent;

    private GameInput gameInput;

	public bool isChangingSpellSelector = false;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.InGame.SetCallbacks(this);
		}
		EnableInGameInput();
	}

    private void OnDisable()
    {
		DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context)
	{
		if (moveEvent != null)
		{
			moveEvent.Invoke(context.ReadValue<float>());
		}
	}

    public void OnJump(InputAction.CallbackContext context)
    {
		if (jumpEvent != null
			&& context.phase == InputActionPhase.Performed)
			jumpEvent.Invoke();
	}

	public void OnMousePos(InputAction.CallbackContext context)
	{
        if (moveMouseEvent != null)
        {
			moveMouseEvent.Invoke(context.ReadValue<Vector2>());
        }
	}

	public void OnLightSpell(InputAction.CallbackContext context)
	{
		if (lightCastEvent != null && context.phase == InputActionPhase.Performed)
		{
			lightCastEvent.Invoke();
		}
	}

	public void OnHeavySpell(InputAction.CallbackContext context)
	{
		if (heavyCastEvent != null && context.phase == InputActionPhase.Performed)
		{
			heavyCastEvent.Invoke();
		}
	}

	public void OnLightSpellScroll(InputAction.CallbackContext context)
	{
        if (lightSpellScroll != null && context.phase == InputActionPhase.Performed && !isChangingSpellSelector)
        {
			lightSpellScroll.Invoke(context.ReadValue<float>());
        }
	}

	public void OnHeavySpellScroll(InputAction.CallbackContext context)
	{
		if (heavySpellScroll != null && context.phase == InputActionPhase.Performed && isChangingSpellSelector)
		{
			heavySpellScroll.Invoke(context.ReadValue<float>());
		}
	}

	public void OnQPressed(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			isChangingSpellSelector = true;
		}
        if (context.phase == InputActionPhase.Canceled)
        {
			isChangingSpellSelector = false;
        }
	}

	public void EnableInGameInput()
	{
		gameInput.InGame.Enable();
	}

	public void DisableAllInput()
	{
		gameInput.InGame.Disable();
	}
}
