using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuState : IFSMState
{
    private readonly FSM levelStateMachine;

    private readonly List<GameObject> disenableUI;
    private readonly GameObject enableUI;

    public DeathMenuState(FSM levelStateMachine, List<GameObject> disenableUI, GameObject enableUI)
    {
        this.levelStateMachine = levelStateMachine;

        this.disenableUI = disenableUI;
        this.enableUI = enableUI;
    }

    public void Enter()
    {
        enableUI?.SetActive(true);

        foreach (var ui in disenableUI)
            ui?.SetActive(false);

        Time.timeScale = 0;
        Debug.Log("The gameplay is paused");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
