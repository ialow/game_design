using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneSpawn : MonoBehaviour
{
    public GameObject[] platformPattern;
    public float platformSize;
    public int platformsToPreCreate;

    private List<GameObject> platforms = new List<GameObject>();
    private Vector3 lastSpawnPosition;

    private void Start()
    {
        lastSpawnPosition = transform.position;
        for (var i = 0; i < platformsToPreCreate; i++)
        {
            SpawnPlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        var platformPatterns = platformPattern[Random.Range(0, platformPattern.Length)];
        var spawnPosition = lastSpawnPosition + new Vector3(0f, 0f, platformSize);
        var newPlatform = Instantiate(platformPatterns, spawnPosition, Quaternion.identity);
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
        yield return new WaitForSeconds(1f);
        Destroy(platform);
    }
}
