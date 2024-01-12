using Ddd.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Application
{
    public class PauseMenuState : IFSMState
    {
        private readonly InputManager userInput;
        private readonly List<GameObject> disenableUI;
        private readonly GameObject enableUI;

        public PauseMenuState(List<GameObject> disenableUI, GameObject enableUI, InputManager userInput)
        {
            this.userInput = userInput;
            this.disenableUI = disenableUI;
            this.enableUI = enableUI;
        }

        public void Enter()
        {
            userInput.OnUI();
            var countDisenable = disenableUI.Count;

            enableUI.SetActive(true);
            for (var i = 0; i < countDisenable; i++)
                disenableUI[i].SetActive(false);
            Time.timeScale = 0;

            Debug.Log("The gameplay is paused");
        }

        public void Exit()
        {
            var countDisenable = disenableUI.Count;

            enableUI.SetActive(false);
            for (var i = 0; i < countDisenable; i++)
                disenableUI[i].SetActive(true);

            userInput.OnGameplay();
            Time.timeScale = 1;
            Debug.Log("The gameplay has resumed");
        }
    }
}