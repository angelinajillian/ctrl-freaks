using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PlayerControllerExtended : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private bool canTakeDamage = true;
    public float damageCooldown = 2.0f;

    void Update()
    {
        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;

            if (damageCooldown <= 0.0f)
            {
                canTakeDamage = true;
                damageCooldown = 2.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") & canTakeDamage)
        {
            canTakeDamage = false;
            health -= 1;
            Debug.Log($"Health: {health}");
            
            if (health <= 0)
            {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}