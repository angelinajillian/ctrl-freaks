using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerExtended1 : MonoBehaviour
{
    [SerializeField] private GameObject fistProjectile1;

    public GameObject fistProjSpawn;

    private GameObject player;

    private float delay = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(fireCooldown());

    }

    IEnumerator fireCooldown()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.5f);
            ShootFist();
        }
    }

    private void ShootFist()
    {
        Vector3 directionToPlayer = player.transform.position;
        directionToPlayer.Normalize();
        
        GameObject fistProjectile = Instantiate(fistProjectile1, fistProjSpawn.transform.position, Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(0, 180f, 0));
        FistProjectile1Controller projectileController = fistProjectile.GetComponent<FistProjectile1Controller>();

        // Set the target for the projectile
        projectileController.SetTarget(player);
    }
}
