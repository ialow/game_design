using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public FSM Fsm {  get; private set; }

    [Header("Pause UI")]
    [SerializeField] private List<GameObject> disenableUI;
    [SerializeField] private GameObject enableUI;

    private void Awake()
    {
        Fsm = new FSM(disenableUI, enableUI);
        Fsm.EnterIn<LoadingLevelState>();
    }
}