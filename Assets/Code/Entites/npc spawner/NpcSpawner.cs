using UnityEngine;

namespace Ddd.Domain
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject NPC;
        [SerializeField] private Transform[] spawnPoints;
        private int maxNPCs = 2;

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
                Instantiate(NPC, spawnPoint.position, Quaternion.identity);
            }
        }

        private void SpawnNPC()
        {
            var spawnPoint = spawnPoints[currentSpawnPointIndex];
            Instantiate(NPC, spawnPoint.position, Quaternion.identity);
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
            isFirstSpawn = false;
        }

        private int CountNPCs() => FindObjectsOfType<AiNpc>().Length;
    }
}