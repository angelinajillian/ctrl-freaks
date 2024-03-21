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
    private bool isSpawningWave;
    public bool gameWon;
    public int waveNumber = 1;

    [SerializeField] private GameObject entranceBlock;
    [SerializeField] private GameObject circle;

    void Start()
    {
        initialSpawn = true;
        gameWon = false;
    }

    void Update()
    {
        bool nextWave = AreAllEnemiesKilled();
        if (!initialSpawn & nextWave & !isSpawningWave & !gameWon)
        {
            
            StartCoroutine(NextWaveDelay());
            Debug.Log("SPAWNING NEXT WAVE");
            // SpawnWave();
        }
    }

    IEnumerator NextWaveDelay()
    {
        isSpawningWave = true; // Set flag to indicate that a wave is being spawned
        
        Text waveText = GameObject.Find("WaveNumber").GetComponent<Text>();
        waveText.text = "3..."; // Display initial countdown

        yield return new WaitForSeconds(1f); // Wait for 1 second
        waveText.text = "2..."; // Update countdown text

        yield return new WaitForSeconds(1f); // Wait for 1 second
        waveText.text = "1..."; // Update countdown text

        yield return new WaitForSeconds(1f); // Wait for 1 second

        isSpawningWave = false; // Reset flag after delay

        waveNumber++;
        displayWave();

        SpawnWave();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (initialSpawn)
            {
                displayWave();
                SpawnWave();
                initialSpawn = false;
                var circleAnim = circle.GetComponent<Animator>();
                circleAnim.enabled = true;
                entranceBlock.SetActive(true);
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
    // First commented out spawns test xp, scales with level
    // Second commented out spawns test waves

    void SpawnWave()
    {
        Debug.Log($"Spawning enemies for Wave {waveNumber}");

        List<Transform> selectedSpawnPoints = new List<Transform>();
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        if (waveNumber == 1)
        {
            SpawnEnemies(0, 0, 3, 0);
        } 
        else if (waveNumber == 2)
        {
            SpawnEnemies(0, 1, 1, 2);
        }
        else if (waveNumber == 3)
        {
            SpawnEnemies(0, 1, 4, 2);
        }
        else if (waveNumber == 4)
        {
            SpawnEnemies(0, 2, 3, 4);
        }
        else if (waveNumber == 5)
        {
            SpawnEnemies(0, 2, 4, 2);
        }
        else if (waveNumber == 6)
        {
            SpawnEnemies(0, 3, 4, 2);
        }
        else if (waveNumber == 7)
        {
            SpawnEnemies(1, 3, 4, 3);
        }
        else if (waveNumber == 8)
        {
            SpawnEnemies(1, 3, 5, 3);
        }
        else if (waveNumber == 9)
        {
            SpawnEnemies(2, 3, 3, 3);
        }
        else if (waveNumber == 10)
        {
            SpawnEnemies(2, 4, 3, 3);
        }
        else if (waveNumber == 11)
        {
            SpawnEnemies(4, 4, 4, 4);
        }
        else if (waveNumber == 12)
        {
            SpawnEnemies(8, 5, 4, 4);
        }
        else if (waveNumber > 12)
        {  
            gameWon = true;
            return;
        }
    }

    bool AreAllEnemiesKilled()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    void displayWave()
    {
        if (waveNumber < 12)
        {
            Text waveText = GameObject.Find("WaveNumber").GetComponent<Text>();
            waveText.text = "Wave " + waveNumber.ToString();
        }
        else if (waveNumber == 12)
        {
            Text waveText = GameObject.Find("WaveNumber").GetComponent<Text>();
            waveText.text = "Final Wave!";
        }
        else if (waveNumber > 12)
        {
            Text waveText = GameObject.Find("WaveNumber").GetComponent<Text>();
            waveText.text = "You Won!!";
        }
    }

    public int GetWave()
    {
        return waveNumber;
    }
}