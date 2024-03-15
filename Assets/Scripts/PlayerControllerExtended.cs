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

    private float maxMana = 100f;
    public float currMana;

    private bool canTakeDamage = true;
    public float damageCooldown = 3.0f;

    // Declaring a variable of type HealthBar
    public HealthBar healthBar;

    public ManaBar manaBar;

    public XPBar xpBar;

    public GameObject redFlash;

    
    // Start at level 1 and 0 XP
    private float level = 1;
    private float currXP = 0;

    void Start()
    {
        // Health is initially set to max value
        currHealth = maxHealth;
        // Update heath bar
        healthBar.SetMaxHealth(maxHealth);

        currMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    void Update()
    {
        // Debug.Log($"Health: {currHealth}, Mana: {currMana}, xp: {currXP}");
        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;

            if (damageCooldown <= 0.0f)
            {
                canTakeDamage = true;
                damageCooldown = 3.0f;
            }
        }
    }

    void levelUp()
    {
        if (currXP >= 100)
        {
            level++;
            currXP = currXP - 100;
            
            // Reset health to max when leveling up
            currHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            // Reset mana to max when leveling up
            currMana = maxMana;
            manaBar.SetMaxMana(maxMana);

            Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
            levelText.text = "level " + level.ToString();
            xpBar.SetXP(currXP);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") & canTakeDamage)
        {
            canTakeDamage = false;
            ReduceHealth(1);
        }
        if (collision.gameObject.CompareTag("EnemyProjectile") & canTakeDamage)
        {
            Debug.Log("Hit");
            canTakeDamage = false;
            ReduceHealth(2);
        }
    }

    void CheckDeath()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane") & canTakeDamage)
        {
            Debug.Log("Touching spikes");
            canTakeDamage = false;
            ReduceHealth(3);
        }

        if (other.CompareTag("Explosion"))
        {
            Debug.Log("You were caught in the barrel explosion!");
            ReduceHealth(4);
        }
    }


    // void OnTriggerEnter(Collider other)
    // {   
    //     Debug.Log("COLLIDED W XP");
    //     if (other.CompareTag("XPOrb"))
    //     {
    //         // Handle XP Orb collision logic here
    //         Destroy(other.gameObject);
    //         UpdateXP();
    //         Debug.Log($"Collected XP Orb. Current XP: {currXP}");
    //         levelUp();
    //     }
    // }

    public void setCanTakeDamage(bool damageTF)
    {
        canTakeDamage = damageTF;
    }

    // This ReduceHealth function updates playerHealth and the healthbar
    // damage can different depending on which enemy did damage
    void ReduceHealth(int damage)
    {
        FlashRed();
        // Deduct damage from playerHealth
        currHealth -= damage;
        // Update health bar
        healthBar.SetHealth(currHealth);

        CheckDeath();
    }

    public void UpdateXP(float xpValue)
    {
        currXP += xpValue;
        xpBar.SetXP(currXP);
        levelUp();
    }
    
    IEnumerator FlashRed()
    {
        redFlash.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        redFlash.SetActive(false);
    }
}