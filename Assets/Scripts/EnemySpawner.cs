using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints;
    public float spawnInterval = 20.0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
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