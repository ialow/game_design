using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance;

    public FSM Fsm { get; private set; }

    [Inject] private List<GameObject> disenableUI;
    [Inject(Id = "UserInput")] private InputManager userInput;
    [Inject(Id = "PauseUI")] private GameObject enablePauseUI;
    [Inject(Id = "DeathUI")] private GameObject enableDeathUI;

    private void Awake()
    {
        userInput.PauseEvent += HandlerPause;
        userInput.ResumetEvent += HandlerResume;

        if (Instance == null) Instance = this;

        var score = new GameScore();
        Fsm = new FSM(userInput, score, disenableUI, enablePauseUI, enableDeathUI);
        Fsm.EnterIn<LoadingLevelState>();
    }

    private void HandlerPause()
    {
        Fsm.EnterIn<PauseMenuState>();
    }

    private void HandlerResume()
    {
        Fsm.EnterIn<GameplayState>();
    }
}