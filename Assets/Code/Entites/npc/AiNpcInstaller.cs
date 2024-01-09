using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AiNpcInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<NavMeshAgent>().FromComponentInChildren().AsSingle();
        Container.Bind<GameObject>().WithId("Player").FromComponentInHierarchy().AsSingle();
        Container.Bind<AiNpc>().FromComponentInHierarchy().AsSingle();
    }
}