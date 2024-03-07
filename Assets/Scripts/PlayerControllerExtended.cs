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
        if (currHealth <= 0)
        {
            currXP = 0;
            level = 1;
            Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
            levelText.text = "level: " + level.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
            Debug.Log($"Health: {currHealth}");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Touched killplane");
            currXP = 0;
            level = 1;
            Text levelText = GameObject.Find("LevelTextBox").GetComponent<Text>();
            levelText.text = "level: " + level.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.CompareTag("Explosion"))
        {
            Debug.Log("You were caught in the barrel explosion!");
            ReduceHealth(4);
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

    public void UpdateXP(float xpValue)
    {
        currXP += xpValue;
        xpBar.SetXP(currXP);
        levelUp();
    }
}