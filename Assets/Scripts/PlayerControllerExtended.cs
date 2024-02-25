using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PlayerControllerExtended : MonoBehaviour
{
    // Declaring a variable of type HealthBar
    public HealthBar healthBar;
    // Player max health value
    [SerializeField] private int maxHealth = 100;
    // Player current health
    public int playerHealth;
    private bool canTakeDamage = true;
    public float damageCooldown = 2.0f;

    // This ReduceHealth function updates playerHealth and the healthbar
    // damage can different depending on which enemy did damage
    void ReduceHealth(int damage)
    {
        // Deduct damage from playerHealth
        playerHealth -= damage;
        // Update health bar
        healthBar.SetHealth(playerHealth);
    }

    void Start()
    {
        // Health is initially set to max value
        playerHealth = maxHealth;
        // Update heath bar
        healthBar.SetMaxHealth(maxHealth);
    }

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
            // Health is decremented by 1 and bar is updated
            ReduceHealth(1);
            Debug.Log($"Health: {playerHealth}");
            
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}