using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IInGameActions
{
    //Gameplay
    public event UnityAction jumpEvent;
    public event UnityAction<float> moveEvent;

	public event UnityAction shootEvent;

    private GameInput gameInput;

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

	public void OnShoot(InputAction.CallbackContext context)
	{
        if (shootEvent != null && context.phase == InputActionPhase.Performed)
        {
			shootEvent.Invoke();
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
