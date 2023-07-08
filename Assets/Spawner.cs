using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float minSpawnDelay = 1f;  // Minimum delay between spawns in seconds
    public float maxSpawnDelay = 5f;  // Maximum delay between spawns in seconds

    void Start()
    {
        StartCoroutine(SpawnEnemies());  // Start the spawning coroutine
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            // Wait for a random amount of time between minSpawnDelay and maxSpawnDelay
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;  // Get a random position within a sphere
        spawnPosition.y = 0;  // Make sure the enemy spawns on the ground (assuming ground is at y = 0)
        spawnPosition += transform.position;  // Move the spawn position relative to the spawner

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);  // Spawn the enemy
    }
}
