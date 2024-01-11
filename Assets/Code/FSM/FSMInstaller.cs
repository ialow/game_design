using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FSMInstaller : MonoInstaller
{
    [SerializeField] private InputManager userInput;
    [SerializeField] private List<GameObject> disenableUI;
    [SerializeField] private GameObject enablePauseUI;
    [SerializeField] private GameObject enableDeathUI;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().WithId("UserInput").FromInstance(userInput);
        Container.Bind<List<GameObject>>().FromInstance(disenableUI);
        Container.Bind<GameObject>().WithId("PauseUI").FromInstance(enablePauseUI);
        Container.Bind<GameObject>().WithId("DeathUI").FromInstance(enableDeathUI);
    }
}
