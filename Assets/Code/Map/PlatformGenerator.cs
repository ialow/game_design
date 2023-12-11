using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public interface IPatternGenerator
    {
        void SpawnPlatform(int directionZ = 1, int directionX = 0);
        bool CheckPatternConditions(GameObject platform);
    }

    public class PlatformGenerator : MonoBehaviour, IPatternGenerator
    {
        public Transform player;
        public GameObject[] platformPatterns;
        public float detectionRange;
        public float platformSize;
        public int platformsToPreCreate;

        private List<GameObject> platforms = new List<GameObject>();
        private Vector3 lastSpawnPosition;
        private Dictionary<Infrastructure.PlatformTag, List<Infrastructure.PlatformTag>> invalidTagCombinations = new Dictionary<Infrastructure.PlatformTag, List<Infrastructure.PlatformTag>>()
        {
            { Infrastructure.PlatformTag.Red, new List<Infrastructure.PlatformTag>{ Infrastructure.PlatformTag.Blue } },
            { Infrastructure.PlatformTag.Blue, new List<Infrastructure.PlatformTag>{ Infrastructure.PlatformTag.Red } },
            { Infrastructure.PlatformTag.Green, new List<Infrastructure.PlatformTag>{ Infrastructure.PlatformTag.Black } },
            { Infrastructure.PlatformTag.Black, new List<Infrastructure.PlatformTag>{ Infrastructure.PlatformTag.Green } }
        };

        private void Start()
        {
            lastSpawnPosition = player.position;
            for (var i = 0; i < platformsToPreCreate; i++)
            {
                SpawnPlatform();
            }
        }

        private void Update()
        {
            var directionZ = GetPlayerDirectionZ();
            var directionX = GetPlayerDirectionX();

            if (directionZ == Direction.Down)
            {
                SpawnPlatform(-1, 0);
            }
            else if (directionZ == Direction.Up)
            {
                SpawnPlatform(1, 0);
            }

            if (directionX == Direction.Left)
            {
                SpawnPlatform(0, -1);
            }
            else if (directionX == Direction.Right)
            {
                SpawnPlatform(0, 1);
            }
        }

        private Direction GetPlayerDirectionZ()
        {
            var playerDirectionZ = player.position.z - lastSpawnPosition.z;

            if (playerDirectionZ < -detectionRange)
            {
                return Direction.Down;
            }
            else if (playerDirectionZ > detectionRange)
            {
                return Direction.Up;
            }

            return Direction.None;
        }

        private Direction GetPlayerDirectionX()
        {
            var playerDirectionX = player.position.x - lastSpawnPosition.x;

            if (playerDirectionX < -detectionRange)
            {
                return Direction.Left;
            }
            else if (playerDirectionX > detectionRange)
            {
                return Direction.Right;
            }

            return Direction.None;
        }

        public void SpawnPlatform(int directionZ = 1, int directionX = 0)
        {
            if (CheckPlayerOnPlatform())
            {
                return;
            }

            var platformPattern = platformPatterns[Random.Range(0, platformPatterns.Length)];
            Vector3 spawnPosition;
            if (directionZ > 0)
            {
                spawnPosition = lastSpawnPosition + new Vector3(0f, 0f, platformSize);
            }
            else if (directionZ < 0)
            {
                spawnPosition = lastSpawnPosition - new Vector3(0f, 0f, platformSize);
            }
            else if (directionX > 0)
            {
                spawnPosition = lastSpawnPosition + new Vector3(platformSize, 0f, 0f);
            }
            else
            {
                spawnPosition = lastSpawnPosition - new Vector3(platformSize, 0f, 0f);
            }

            var newPlatform = Instantiate(platformPattern, spawnPosition, Quaternion.identity);
            lastSpawnPosition = newPlatform.transform.position;

            platforms.Add(newPlatform);
            if (platforms.Count > platformsToPreCreate)
            {
                StartCoroutine(DestroyPlatformAfterDelay(platforms[0]));
                platforms.RemoveAt(0);
            }
        }

        private IEnumerator DestroyPlatformAfterDelay(GameObject platform)
        {
            yield return new WaitForSeconds(1.7f);
            Destroy(platform);
        }

        private bool CheckPlayerOnPlatform()
        {
            foreach (var platform in platforms)
            {
                var position = platform.transform.position;
                var halfSize = platformSize / 2;

                if (player.position.z > position.z - halfSize && player.position.z < position.z + halfSize 
                    && player.position.x > position.x - halfSize && player.position.x < position.x + halfSize)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckPatternConditions(GameObject platform)
        {
            var colliders = platform.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                if (invalidTagCombinations.ContainsKey((Infrastructure.PlatformTag)System.Enum.Parse(typeof(Infrastructure.PlatformTag), collider.tag))
                    && invalidTagCombinations[(Infrastructure.PlatformTag)System.Enum.Parse(typeof(Infrastructure.PlatformTag), collider.tag)]
                    .Contains((Infrastructure.PlatformTag)System.Enum.Parse(typeof(Infrastructure.PlatformTag), platform.tag)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}