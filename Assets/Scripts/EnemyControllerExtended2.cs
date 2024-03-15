using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerExtended2 : MonoBehaviour
{
    [SerializeField] private GameObject enemyMinion;
    public GameObject enemySpawnOne;
    public GameObject enemySpawnTwo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fireCooldown());
    }

    IEnumerator fireCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(7.0f);
            MakeMinions();
        }
    }

    private void MakeMinions()
    {
        GameObject EnemyMinion1 = Instantiate(enemyMinion, enemySpawnOne.transform.position, Quaternion.identity);
        GameObject EnemyMinion2 = Instantiate(enemyMinion, enemySpawnTwo.transform.position, Quaternion.identity);
    }
}
