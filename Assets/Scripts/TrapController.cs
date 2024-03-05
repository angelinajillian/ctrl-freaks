using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> inactiveCovers = new List<GameObject>();
    private int curWave = 0;
    [SerializeField] private GameObject enemySpawner;
    void Start()
    {
        var spawnScript = enemySpawner.GetComponent<EnemySpawner>();
        curWave = spawnScript.GetWave();
    }

    // Update is called once per frame
    void Update()
    {
        var spawnScript = enemySpawner.GetComponent<EnemySpawner>();
        List<GameObject> activeCovers = new List<GameObject>();
        int wave = spawnScript.GetWave();

        activeCovers = GameObject.FindGameObjectsWithTag("TrapCover").ToList();

        // Reveal floor pits every 2 waves
        if (((wave % 2 == 0) && wave > 0 && (curWave != wave)) || Input.GetKeyDown(KeyCode.T))
        {
            if (activeCovers.Count > 0)
            {
                RevealTrap(activeCovers);
            }
        }

        // Shoot barrels into arena every 3 waves
        if ((wave % 3 == 0) && wave > 0 && (curWave != wave))
        {
            var cannonList = GameObject.FindGameObjectsWithTag("Cannon").ToList();

            foreach (var cannon in cannonList)
            {
                var control = cannon.GetComponent<CannonController>();
                control.SpawnBarrel();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inactiveCovers.Count > 0)
            {
                HideTrap();
            }
        }

        curWave = wave;
    }

    void RevealTrap(List<GameObject> actives)
    {
        Debug.Log("Trap revealed!");
        var randomIndex = Random.Range(0, actives.Count);
        var selectedCover = actives[randomIndex];
        selectedCover.SetActive(false);
        inactiveCovers.Add(selectedCover);
    }

    void HideTrap()
    {
        var randomIndex = Random.Range(0, inactiveCovers.Count);
        var selectedCover = inactiveCovers[randomIndex];
        selectedCover.SetActive(true);
        inactiveCovers.Remove(selectedCover);
    }
}
