using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance;

    [SerializeField] private InputManager userInput;

    public FSM Fsm {  get; private set; }

    [Header("Menu UI")]
    [SerializeField] private List<GameObject> disenableUI;
    [SerializeField] private GameObject enablePauseUI;
    [SerializeField] private GameObject enableDeathUI;

    private void Awake()
    {
        userInput.PauseEvent += HandlerPause;
        userInput.ResetEvent += HandlerResume;

        if (Instance == null) Instance = this;

        Fsm = new FSM(userInput, disenableUI, enablePauseUI, enableDeathUI);
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