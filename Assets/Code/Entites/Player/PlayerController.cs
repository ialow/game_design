using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private InputManager userInput;

    private void Awake()
    {
        userInput.WalkEvent += HandlerWalk;
        userInput.ToolbarEvent += HandlerChangeSelectedItem;
        userInput.ThrowItemEvent += HandlerThrowItem;
    }

    private void Update()
    {
        HandlerTurn(Mouse.current.position.ReadValue());
    }

    private void OnDisable()
    {
        userInput.WalkEvent -= HandlerWalk;
        userInput.ToolbarEvent -= HandlerChangeSelectedItem;
        userInput.ThrowItemEvent -= HandlerThrowItem;
    }

    private void HandlerTurn(Vector3 turn)
    {
        var mousePositionVector3 = orthographicCamera.ScreenToWorldPoint(turn);
        InputParametrs.MousePositionXZ = new Vector2(mousePositionVector3.x, mousePositionVector3.z);
    }

    private void HandlerWalk(Vector2 diresction)
    {
        InputParametrs.ControllerPositionXZ = diresction;
    }

    private void HandlerChangeSelectedItem(int newSlot)
    {
        InputParametrs.InventorySlot = newSlot;
        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.InventorySlot);
    }

    private void HandlerThrowItem()
    {
        InventoryManager.Instance.ThrowItem();
    }
}