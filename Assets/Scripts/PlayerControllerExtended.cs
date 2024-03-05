using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerExtended : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    public int currHealth;

    private bool canTakeDamage = true;
    public float damageCooldown = 2.0f;

    // Declaring a variable of type HealthBar
    public HealthBar healthBar;

    public XPBar xpBar;


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
            Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
            levelText.text = "level: " + level.ToString();
            xpBar.SetXP(currXP);
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
                Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
                levelText.text = "level: " + level.ToString();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("XPOrb"))
        {
            // Handle XP Orb collision logic here
            Debug.Log("COLLIDED W XP");
            Destroy(other.gameObject);
            UpdateXP();
            Debug.Log($"Collected XP Orb. Current XP: {currXP}");
            levelUp();
        }

        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Touched killplane");
            currXP = 0;
            level = 1;
            Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
            levelText.text = "level: " + level.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    void UpdateXP()
    {
        currXP += 20.0f;
        xpBar.SetXP(currXP);
    }
}