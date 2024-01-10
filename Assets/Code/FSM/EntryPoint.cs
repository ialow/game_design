using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance;

    public FSM Fsm {  get; private set; }

    [Header("Menu UI")]
    [SerializeField] private List<GameObject> disenableUI;
    [SerializeField] private GameObject enablePauseUI;
    [SerializeField] private GameObject enableDeathUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        Fsm = new FSM(disenableUI, enablePauseUI, enableDeathUI);
        Fsm.EnterIn<LoadingLevelState>();
    }
}