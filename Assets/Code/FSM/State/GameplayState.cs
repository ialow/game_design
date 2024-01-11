using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IFSMState
{
    public GameplayState()
    {
    }

    public void Enter()
    {
        Debug.Log("Starting the gameplay");
    }

    public void Exit()
    {
        Debug.Log("End/pause of the gameplay");
    }
}