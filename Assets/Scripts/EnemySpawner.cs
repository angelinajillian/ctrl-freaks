using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public Transform gameStart;
    public float spawnInterval = 5.0f;
    private bool initialSpawn;
    public int waveNumber = 1;


    void Start()
    {
        initialSpawn = true;
    }

    void Update()
    {
        bool nextWave = AreAllEnemiesKilled();
        if (!initialSpawn & nextWave)
        {
            waveNumber++;
            Text waveText = GameObject.Find("WaveNumber").GetComponent<Text>();
            waveText.text = "Wave " + waveNumber.ToString();

            NextWaveDelay();
            Debug.Log("SPAWNING NEXT WAVE");
            SpawnWave();
        }
    }

    IEnumerator NextWaveDelay()
    {
        yield return new WaitForSeconds(spawnInterval);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (initialSpawn)
            {
                SpawnWave();
                initialSpawn = false;
            }
        }
    }

    void SpawnEnemies(int gray, int bald, int red, int purple)
    {
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        List<Transform> graySpawnPoints = new List<Transform>();

        for (int i = 0; i < gray; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            graySpawnPoints.Add(availableSpawnPoints[randomIndex]);
            availableSpawnPoints.RemoveAt(randomIndex);
        }

        foreach (Transform spawnPoint in graySpawnPoints)
        {
            Instantiate(enemyPrefabs[0], spawnPoint.position, spawnPoint.rotation);
        }

        List<Transform> baldSpawnPoints = new List<Transform>();

        for (int i = 0; i < bald; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            baldSpawnPoints.Add(availableSpawnPoints[randomIndex]);
            availableSpawnPoints.RemoveAt(randomIndex);
        }

        foreach (Transform spawnPoint in baldSpawnPoints)
        {
            Instantiate(enemyPrefabs[1], spawnPoint.position, spawnPoint.rotation);
        }

        List<Transform> redSpawnPoints = new List<Transform>();

        for (int i = 0; i < red; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            redSpawnPoints.Add(availableSpawnPoints[randomIndex]);
            availableSpawnPoints.RemoveAt(randomIndex);
        }

        foreach (Transform spawnPoint in redSpawnPoints)
        {
            Instantiate(enemyPrefabs[2], spawnPoint.position, spawnPoint.rotation);
        }

        List<Transform> purpleSpawnPoints = new List<Transform>();

        for (int i = 0; i < purple; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            purpleSpawnPoints.Add(availableSpawnPoints[randomIndex]);
            availableSpawnPoints.RemoveAt(randomIndex);
        }

        foreach (Transform spawnPoint in purpleSpawnPoints)
        {
            Instantiate(enemyPrefabs[3], spawnPoint.position, spawnPoint.rotation);
        }
    }

    // 0 = grayskull, 1 = bald, 2 = red, 3 = purple

    void SpawnWave()
    {
        Debug.Log($"Spawning enemies for Wave {waveNumber}");

        List<Transform> selectedSpawnPoints = new List<Transform>();
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        if (waveNumber == 1)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 2)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 3)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 4)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 5)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 6)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 7)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 8)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 9)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
        else if (waveNumber == 10)
        {
            SpawnEnemies(0, 0, 1, 0);
        }
    }

    bool AreAllEnemiesKilled()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    public int GetWave()
    {
        return waveNumber;
    }
}