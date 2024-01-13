using Ddd.Domain;
using UnityEngine;

namespace Ddd.Application
{
    public class LoadingLevelState : IFSMState
    {
        private readonly FSM levelStateMachine;
        private readonly InputManager userInput;

        public LoadingLevelState(FSM levelStateMachine, InputManager userInput)
        {
            this.levelStateMachine = levelStateMachine;
            this.userInput = userInput;
        }

        public void Enter()
        {
            Debug.Log("Loading the scene resources");
            Time.timeScale = 1;
            userInput.OnGameplay();
            levelStateMachine.EnterIn<GameplayState>();
        }

        public void Exit()
        {
            Debug.Log("Completing the loading of the scene resources");
        }
    }
}