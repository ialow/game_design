using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private Dictionary<Type, IFSMState> states;

    public FSM(InputManager userInput, List<GameObject> disenableUI, GameObject enablePauseUI, 
        GameObject enableDeathUI)
    {
        states = new Dictionary<Type, IFSMState>()
        {
            [typeof(LoadingLevelState)] = new LoadingLevelState(this, userInput),
            [typeof(GameplayState)] = new GameplayState(this),
            [typeof(PauseMenuState)] = new PauseMenuState(this, disenableUI, enablePauseUI, userInput),
            [typeof(DeathMenuState)] = new DeathMenuState(this, disenableUI, enableDeathUI, userInput),
        };
    }

    public IFSMState currentState { get; private set; }

    public void EnterIn<TState>() where TState : IFSMState
    {
        if (states.TryGetValue(typeof(TState), out IFSMState state))
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
    }
}