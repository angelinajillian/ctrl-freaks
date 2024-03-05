using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private GameObject explosion;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Explode();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ExplodeAll();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Barrel fell out of the map");
            Explode();
        }
    }

    void Explode()
    {
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
