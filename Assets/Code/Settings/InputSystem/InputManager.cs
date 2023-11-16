using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable object/InputManager")]
public class InputManager : ScriptableObject, UserInput.IGameplayActions, UserInput.IUIActions
{
    private UserInput userInput;

    public event Action<Vector3> TurnEvent;
    public event Action<Vector2> WalkEvent;

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

    public void OnTurn(InputAction.CallbackContext context)
    {
        TurnEvent?.Invoke(Mouse.current.position.ReadValue());
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        WalkEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnNewaction(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}