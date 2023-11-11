using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager userInput;

    private void Awake()
    {
        //userInput.TurnEvent += 
        userInput.WalkEvent += HandlerWalk;
    }

    private void OnDisable()
    {
        //userInput.TurnEvent += 
        userInput.WalkEvent -= HandlerWalk;
    }

    //private void HandlerTurn(Vector2 turn) => 
    private void HandlerWalk(Vector2 diresction) => InputParametrs.DiresctionWalk = diresction;
}