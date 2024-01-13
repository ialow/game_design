using Ddd.Domain;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ddd.Application
{
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

            var score = new GameScore();
            Fsm = new FSM(userInput, score, disenableUI, enablePauseUI, enableDeathUI);
            Fsm.EnterIn<LoadingLevelState>();
        }

        public void HandlerPause()
        {
            Fsm.EnterIn<PauseMenuState>();
        }

        public void HandlerResume()
        {
            Fsm.EnterIn<GameplayState>();
        }
    }
}