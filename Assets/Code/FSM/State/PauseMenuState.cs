using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : IFSMState
{
    private readonly FSM levelStateMachine;

    private readonly List<GameObject> disenableUI;
    private readonly GameObject enableUI;

    public PauseMenuState(FSM levelStateMachine, List<GameObject> disenableUI, GameObject enableUI)
    {
        this.levelStateMachine = levelStateMachine;

        this.disenableUI = disenableUI;
        this.enableUI = enableUI;
    }

    public void Enter()
    {
        var countDisenable = disenableUI.Count;

        enableUI.SetActive(true);
        for (var i = 0; i < countDisenable; i++)
            disenableUI[i].SetActive(false);
        Time.timeScale = 0;

        Debug.Log("The gameplay is paused");
    }

    public void Exit()
    {
        var countDisenable = disenableUI.Count;

        enableUI.SetActive(false);
        for (var i = 0; i < countDisenable; i++)
            disenableUI[i].SetActive(true);
        Time.timeScale = 1;

        Debug.Log("The gameplay has resumed");
    }
}