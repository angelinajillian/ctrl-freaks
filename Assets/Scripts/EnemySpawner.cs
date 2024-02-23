using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints;
    public Transform gameStart;
    public float spawnInterval = 20.0f;
    private bool initialSpawn;

    void Start()
    {
        initialSpawn = true;
        // InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (initialSpawn)
            {
                List<Transform> selectedSpawnPoints = new List<Transform>();
                List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

                for (int i = 0; i < 3; i++)
                {
                    int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                    selectedSpawnPoints.Add(availableSpawnPoints[randomIndex]);
                    availableSpawnPoints.RemoveAt(randomIndex);
                }

                foreach (Transform spawnPoint in selectedSpawnPoints)
                {
                    int randomIndex = Random.Range(0, enemyPrefabs.Length);
                    Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
                }

                initialSpawn = false;
                InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
            }
        }
    }

    void SpawnEnemies()
    {
        List<Transform> selectedSpawnPoints = new List<Transform>();
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            selectedSpawnPoints.Add(availableSpawnPoints[randomIndex]);
            availableSpawnPoints.RemoveAt(randomIndex);
        }

        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}