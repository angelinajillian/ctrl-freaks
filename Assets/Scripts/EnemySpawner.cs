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
        // Check if the player entered the trigger zone
        if (other.CompareTag("Player"))
        {
            if (initialSpawn)
            {
                foreach (Transform spawnPoint in spawnPoints)
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
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}