using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (Instance == null) Instance = this;

        Fsm = new FSM(userInput, disenableUI, enablePauseUI, enableDeathUI);
        Fsm.EnterIn<LoadingLevelState>();
    }
}