using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerExtended1 : MonoBehaviour
{
    [SerializeField] private GameObject fistProjectile1;

    private EnemyController enemyController;

    public GameObject fistProjSpawn;

    private GameObject player;

    // private float delay = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(fireCooldown());
        enemyController = GetComponent<EnemyController>();
    }

    IEnumerator fireCooldown()
    {
        while(true)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            yield return new WaitForSeconds(2.5f);

            if (!enemyController.isStunned & !enemyController.isPunched & distanceToPlayer <= 10f)
            {
                ShootFist();
            }
        }
    }

    private void ShootFist()
    {
        Vector3 directionToPlayer = new Vector3 (player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.x);
        directionToPlayer.Normalize();

        FindObjectOfType<SoundManager>().PlayEnemyShootSpellSound(this.transform.position);
        // Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);

        GameObject fistProjectile = Instantiate(fistProjectile1, fistProjSpawn.transform.position, Quaternion.identity);
        FistProjectileEnemyController projectileController = fistProjectile.GetComponent<FistProjectileEnemyController>();

        fistProjectile.transform.Rotate(0f, 180f, 0f);

        projectileController.SetTarget(player);
    }
}
