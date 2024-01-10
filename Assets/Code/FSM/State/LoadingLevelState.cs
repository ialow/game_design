using Unity.VisualScripting;
using UnityEngine;

public class LoadingLevelState : IFSMState
{
    private readonly FSM levelStateMachine;
    public LoadingLevelState(FSM levelStateMachine)
    {
        this.levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
        Debug.Log("Loading the scene resources");
        levelStateMachine.EnterIn<GameplayState>();
    }

    public void Exit()
    {
        Debug.Log("Completing the loading of the scene resources");
    }
}