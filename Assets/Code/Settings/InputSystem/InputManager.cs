using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable object/InputManager")]
public class InputManager : ScriptableObject, UserInput.IGameplayActions, UserInput.IUIActions
{
    private UserInput userInput;

    public event Action<Vector2> WalkEvent;
    public event Action<int> ToolbarEvent;
    public event Action ThrowItemEvent;

    private void OnEnable()
    {
        if (userInput is null)
        {
            userInput = new UserInput();
            userInput.Gameplay.SetCallbacks(this);
            userInput.UI.SetCallbacks(this);

            OnGameplay();
        }
    }

    public void OnGameplay()
    {
        userInput.Gameplay.Enable();
        userInput.UI.Disable();
    }

    public void OnUI()
    {
        userInput.Gameplay.Disable();
        userInput.UI.Enable();
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        WalkEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnToolbar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            var key = Convert.ToUInt16(context.control.name);
            ToolbarEvent?.Invoke(key);
        }
    }

    public void OnThrowItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            ThrowItemEvent?.Invoke();
        }
    }

    public void OnNewaction(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}