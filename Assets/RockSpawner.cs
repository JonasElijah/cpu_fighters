using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject squarePrefab; // The square prefab to spawn
    public float spawnInterval = 2.0f; // Time between spawns
    public float xRange = 10.0f; // Range of x coordinates for spawning
    public float yPosition = 5.0f; // Fixed y coordinate for all squares

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSquare();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnSquare()
    {
        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, yPosition, 0);
        Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
    }
}
