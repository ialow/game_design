using Ddd.Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Application
{
    public class FSM
    {
        private Dictionary<Type, IFSMState> states;

        public FSM(InputManager userInput, GameScore score, List<GameObject> disenableUI, GameObject enablePauseUI,
            GameObject enableDeathUI)
        {
            states = new Dictionary<Type, IFSMState>()
            {
                [typeof(LoadingLevelState)] = new LoadingLevelState(this, userInput),
                [typeof(GameplayState)] = new GameplayState(),
                [typeof(PauseMenuState)] = new PauseMenuState(disenableUI, enablePauseUI, userInput),
                [typeof(DeathMenuState)] = new DeathMenuState(disenableUI, enableDeathUI, userInput, score),
                [typeof(ExitGameSceneState)] = new ExitGameSceneState(userInput)
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
}