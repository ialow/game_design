using Ddd.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Application
{
    public class DeathMenuState : IFSMState
    {
        private readonly InputManager userInput;
        private readonly List<GameObject> disenableUI;
        private readonly GameObject enableUI;
        private readonly GameScore score;

        public DeathMenuState(List<GameObject> disenableUI, GameObject enableUI, InputManager userInput, GameScore score)
        {
            this.userInput = userInput;
            this.disenableUI = disenableUI;
            this.enableUI = enableUI;
            this.score = score;
        }

        public void Enter()
        {
            userInput.OnUI();
            enableUI?.SetActive(true);

            foreach (var ui in disenableUI)
                ui?.SetActive(false);

            score.SaveScore();
            Time.timeScale = 0;
            Debug.Log("The gameplay is paused");
        }

        public void Exit()
        {
        }
    }
}
