using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ddd.Application
{
    public class ExitGameSceneState : IFSMState
    {
        public ExitGameSceneState()
        {
        }

        public void Enter()
        {
            SceneManager.LoadScene(0);
            Debug.Log("The gameplay is paused");
        }

        public void Exit()
        {
        }
    }
}