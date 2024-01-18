using System.ComponentModel;
using UnityEngine;
using Zenject;
using static UnityEngine.Rendering.DebugUI;

namespace Ddd.Domain
{
    public class NpcSpawner : MonoBehaviour
    {
        [Inject(Id = "NPCGameobject")] private GameObject NPC;
        [Inject(Id = "SpawnPoints")] private Transform[] spawnPoints;
        [Inject] private DiContainer container;

        private int maxNPCs = 4;
        private int previousNpcCount = 0;
        private int currentSpawnPointIndex = 0;

        private bool isFirstSpawn = true;

        private void Awake()
        {
            isFirstSpawn = true;
        }

        private void Update()
        {
            var npcCount = CountNPCs();
            if (isFirstSpawn && npcCount == 0)
                SpawnInitialNPCs();

            if (npcCount < maxNPCs && npcCount != previousNpcCount)
                SpawnNPC();

            previousNpcCount = npcCount;
        }

        private void SpawnInitialNPCs()
        {
            for (var i = 0; i < maxNPCs; i++)
            {
                var spawnPoint = spawnPoints[i % spawnPoints.Length];
                container.InstantiatePrefab(NPC, spawnPoint.position, Quaternion.identity, null);
                //Instantiate(NPC, spawnPoint.position, Quaternion.identity);
            }
        }

        private void SpawnNPC()
        {
            var spawnPoint = spawnPoints[currentSpawnPointIndex];
            container.InstantiatePrefab(NPC, spawnPoint.position, Quaternion.identity, null);
            //Instantiate(NPC, spawnPoint.position, Quaternion.identity);
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
            isFirstSpawn = false;
        }

        private int CountNPCs() => FindObjectsOfType<AiNpc>().Length;
    }
}