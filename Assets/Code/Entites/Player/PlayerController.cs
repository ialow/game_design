using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private InputManager userInput;

    private static Action StartUseItem;
    private static Action EndUseItem;

    private void Awake()
    {
        userInput.WalkEvent += HandlerWalk;
        userInput.ToolbarEvent += HandlerChangeSelectedItem;
        userInput.ThrowItemEvent += HandlerThrowItem;

        userInput.StartUseItemEvent += HandlerPerformedUseItem;
        userInput.EndUseItemEvent += HandlerCanceledUseItem;
    }

    private void Update()
    {
        HandlerTurn(Mouse.current.position.ReadValue());
        HandlerPosition();
    }

    private void OnDisable()
    {
        userInput.WalkEvent -= HandlerWalk;
        userInput.ToolbarEvent -= HandlerChangeSelectedItem;
        userInput.ThrowItemEvent -= HandlerThrowItem;

        userInput.StartUseItemEvent -= HandlerPerformedUseItem;
        userInput.EndUseItemEvent -= HandlerCanceledUseItem;
    }

    private void HandlerWalk(Vector2 diresction)
    {
        InputParametrs.ControllerDirectionXZ = diresction;
    }

    private void HandlerChangeSelectedItem(int newSlot)
    {
        InputParametrs.Toolbar = newSlot;
        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.Toolbar);
    }

    private void HandlerThrowItem()
    {
        InventoryManager.Instance.ThrowItem();
    }

    private void HandlerTurn(Vector3 turn)
    {
        var mousePositionVector3 = orthographicCamera.ScreenToWorldPoint(turn);
        InputParametrs.MousePositionXZ = new Vector2(mousePositionVector3.x, mousePositionVector3.z);
    }

    private void HandlerPosition()
    {
        Player.position = transform.position;
    }

    private void HandlerPerformedUseItem()
    {
        StartUseItem?.Invoke();
    }

    private void HandlerCanceledUseItem()
    {
        EndUseItem?.Invoke();
    }

    public static void SetActionUsingItem(Action startUseItem, Action endUseItem)
    {
        StartUseItem = startUseItem;
        EndUseItem = endUseItem;
    }
}