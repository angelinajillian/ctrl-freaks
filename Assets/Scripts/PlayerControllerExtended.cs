using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PlayerControllerExtended : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    public int currHealth;

    private bool canTakeDamage = true;
    public float damageCooldown = 2.0f;

    // Declaring a variable of type HealthBar
    public HealthBar healthBar;

    
    // Start at level 1 and 0 XP
    private float level = 1;
    private float currXP = 0;

    void Start()
    {
        // Health is initially set to max value
        currHealth = maxHealth;
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

    void levelUp()
    {
        if (currXP >= 100)
        {
            level++;
            currXP = currXP - 100;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") & canTakeDamage)
        {
            canTakeDamage = false;
            ReduceHealth(1);
            Debug.Log($"Health: {currHealth}");
            
            if (currHealth <= 0)
            {
                currXP = 0;
                level = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        Debug.Log("COLLIDED W XP");
        if (other.CompareTag("XPOrb"))
        {
            // Handle XP Orb collision logic here
            Destroy(other.gameObject);
            currXP += 10;
            Debug.Log($"Collected XP Orb. Current XP: {currXP}");
            levelUp();
        }
    }

    public void setCanTakeDamage(bool damageTF)
    {
        canTakeDamage = damageTF;
    }

    // This ReduceHealth function updates playerHealth and the healthbar
    // damage can different depending on which enemy did damage
    void ReduceHealth(int damage)
    {
        // Deduct damage from playerHealth
        currHealth -= damage;
        // Update health bar
        healthBar.SetHealth(currHealth);
    }

}