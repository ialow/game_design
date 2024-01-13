using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ddd.Application
{
    public class ExitGameSceneState : IFSMState
    {
        private readonly InputManager userInput;

        public ExitGameSceneState(InputManager userInput)
        {
            this.userInput = userInput;
        }

        public void Enter()
        {
            userInput.OnCastomDisable();
            SceneManager.LoadScene(0);
            Debug.Log("The gameplay is paused");
        }

        public void Exit()
        {
        }
    }
}