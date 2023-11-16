using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private InputManager userInput;

    private void Awake()
    {
        userInput.TurnEvent += HandlerTurn;
        userInput.WalkEvent += HandlerWalk;
    }

    private void OnDisable()
    {
        userInput.TurnEvent -= HandlerTurn;
        userInput.WalkEvent -= HandlerWalk;
    }

    private void HandlerTurn(Vector3 turn)
    {
        var mousePositionVector3 = orthographicCamera.ScreenToWorldPoint(turn);
        InputParametrs.MousePositionXZ = new Vector2(mousePositionVector3.x, mousePositionVector3.z);
    }

    private void HandlerWalk(Vector2 diresction)
    {
        InputParametrs.ControllerPosition = diresction;
    }
}