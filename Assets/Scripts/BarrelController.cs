using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private float health = 3f;
    [SerializeField] private GameObject explosion;
    private float fuseTimer = 3f;
    private bool fuseActive = false;
    private EnemySpawner enemySpawner;

    void Start()
    {
        // Randomly activate the fuse of a new barrel
        float fuseProb = Random.Range(0f, 1f);
        if (fuseProb <= 0.05f)
        {
            fuseActive = true;
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 || fuseTimer <= 0)
        {
            Explode();
        }

        if (fuseActive)
        {
            var barrelRend = gameObject.GetComponent<Renderer>();
            var barrelMat = barrelRend.material;

            barrelMat.color = Color.red;
            fuseTimer -= Time.deltaTime;
            Debug.Log("Time left: " + Mathf.Round(fuseTimer));
        }

        if (enemySpawner.gameWon & Input.GetKeyDown(KeyCode.V))
        {
            ExplodeAll();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 1f;
            fuseActive = true;
        }

        if (collision.gameObject.CompareTag("AOEProjectile"))
        {
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 12.5f, ForceMode.Impulse);
            health -= 2f;
            fuseActive = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Barrel fell out of the map");
            health = 0;
        }

        if (other.CompareTag("Explosion"))
        {
            health = 0;
        }
    }

    void Explode()
    {
        FindObjectOfType<SoundManager>().PlayExplosionSound(this.transform.position);

        var position = gameObject.transform.position;
        var rotation = gameObject.transform.rotation;
        var newExplosion = Instantiate(explosion, position, rotation);
        var particleSystem = newExplosion.GetComponent<ParticleSystem>();
        var collider = newExplosion.transform.GetChild(2).gameObject;
        Destroy(collider, 0.15f);
        Destroy(newExplosion, particleSystem.main.duration);
        Destroy(gameObject);
    }

    void ExplodeAll()
    {
        var barrelsList = GameObject.FindGameObjectsWithTag("Barrel").ToList();
        if (barrelsList.Count > 0)
        {
            foreach (var barrel in barrelsList)
            {
                var barrelControl = barrel.GetComponent<BarrelController>();
                barrelControl.Explode();
            }
        }
    }
}
