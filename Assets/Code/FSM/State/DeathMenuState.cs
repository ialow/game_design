using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuState : IFSMState
{
    private readonly FSM levelStateMachine;

    private readonly InputManager userInput;
    private readonly List<GameObject> disenableUI;
    private readonly GameObject enableUI;

    public DeathMenuState(FSM levelStateMachine, List<GameObject> disenableUI, GameObject enableUI, InputManager userInput)
    {
        this.levelStateMachine = levelStateMachine;

        this.userInput = userInput;
        this.disenableUI = disenableUI;
        this.enableUI = enableUI;
    }

    public void Enter()
    {
        userInput.OnUI();
        var countDisenable = disenableUI.Count;

        enableUI.SetActive(true);
        for (var i = 0; i < countDisenable; i++)
            disenableUI[i].SetActive(false);
        Time.timeScale = 0;

        Debug.Log("The gameplay is paused");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
