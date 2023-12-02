using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] platformPatterns;
    public float detectionRange;
    public float platformSize;
    public float spawnOffset;
    public int platformsToPreCreate;

    private List<GameObject> platforms = new List<GameObject>();
    private Vector3 lastSpawnPosition;

    private Dictionary<string, List<string>> invalidTagCombinations = new Dictionary<string, List<string>>()
    {
        { "Red", new List<string>{ "Blue" } },
        { "Blue", new List<string>{ "Red" } },
        { "Green", new List<string>{ "Black" } },
        { "Black", new List<string>{ "Green" } }
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
        var playerDirectionZ = player.position.z - lastSpawnPosition.z;
        var playerDirectionX = player.position.x - lastSpawnPosition.x;

        if (playerDirectionZ < -detectionRange)
        {
            SpawnPlatform(-1, 0);
        }
        else if (playerDirectionZ > detectionRange)
        {
            SpawnPlatform(1, 0);
        }

        if (playerDirectionX < -detectionRange)
        {
            SpawnPlatform(0, -1);
        }
        else if (playerDirectionX > detectionRange)
        {
            SpawnPlatform(0, 1);
        }
    }

    private void SpawnPlatform(int directionZ = 1, int directionX = 0)
    {
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

        if (CheckPatternConditions(newPlatform))
        {
            Destroy(newPlatform);
            SpawnPlatform(directionZ, directionX);
        }
        else
        {
            platforms.Add(newPlatform);

            if (platforms.Count > platformsToPreCreate)
            {
                Destroy(platforms[0]);
                platforms.RemoveAt(0);
            }
        }
    }

    private bool CheckPatternConditions(GameObject platform)
    {
        var colliders = platform.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            if (invalidTagCombinations.ContainsKey(collider.tag) && invalidTagCombinations[collider.tag].Contains(platform.tag))
            {
                return true;
            }
        }

        return false;
    }
}