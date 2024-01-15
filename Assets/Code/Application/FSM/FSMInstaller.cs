using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ddd.Application
{
    public class FSMInstaller : MonoInstaller
    {
        [SerializeField] private InputManager userInput;
        [SerializeField] private List<GameObject> disenableUI;
        [SerializeField] private GameObject enablePauseUI;
        [SerializeField] private GameObject enableDeathUI;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject NPC;
        [SerializeField] private Transform targetPositionPlayer;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject runNPC;
        [SerializeField] private GameObject gearPrefab;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().WithId("UserInput").FromInstance(userInput);
            Container.Bind<List<GameObject>>().FromInstance(disenableUI);
            Container.Bind<GameObject>().WithId("PauseUI").FromInstance(enablePauseUI);
            Container.Bind<GameObject>().WithId("DeathUI").FromInstance(enableDeathUI);
            Container.Bind<Transform[]>().WithId("SpawnPoints").FromInstance(spawnPoints);
            Container.Bind<GameObject>().WithId("NPCGameobject").FromInstance(NPC);
            Container.Bind<Transform>().WithId("TargetPosition").FromInstance(targetPositionPlayer);
            Container.Bind<GameObject>().WithId("PlayerGameobject").FromInstance(player);
            Container.Bind<GameObject>().WithId("ExplosionGameobject").FromInstance(explosion);
            Container.Bind<GameObject>().WithId("RunNPCGameobject").FromInstance(runNPC);
            Container.Bind<GameObject>().WithId("GearGameobject").FromInstance(gearPrefab);
        }
    }
}
