using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class UpgradeSystem : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerControllerExtended playerControllerExtended;
    
    private int manaUpgrades = 0;
    private int healthUpgrades = 0;
    private int punchUpgrades = 0;
    private int fistsUpgrades = 0;

    public GameObject upgradeMana;
    public GameObject upgradeHealth;
    public GameObject upgradePunch;
    public GameObject upgradeFist;

    private bool upgradeable;

    void Start()
    {
        upgradeable = true;
    }

    public void UpgradeMana()
    {  
        if (manaUpgrades < 3 & upgradeable)
        {  
            upgradeable = false;
            manaUpgrades++;

            if (manaUpgrades == 1)
            {
                upgradeMana.transform.GetChild(0).gameObject.SetActive(true);

                playerControllerExtended.maxMana = 125;
                Debug.Log($"maxMana: {playerControllerExtended.maxMana}");
                Debug.Log($"currMana: {playerControllerExtended.currMana}");
            } 
            else if (manaUpgrades == 2)
            {
                upgradeMana.transform.GetChild(1).gameObject.SetActive(true);

                playerControllerExtended.maxMana = 150;
                Debug.Log($"maxMana: {playerControllerExtended.maxMana}");
                Debug.Log($"currMana: {playerControllerExtended.currMana}");
            }
            else if (manaUpgrades == 3)
            {
                upgradeMana.transform.GetChild(2).gameObject.SetActive(true);

                playerControllerExtended.maxMana = 200;
                Debug.Log($"maxMana: {playerControllerExtended.maxMana}");
                Debug.Log($"currMana: {playerControllerExtended.currMana}");
            }

            StartCoroutine(timeBeforeClosing());  
        }
    }

    public void UpgradeHealth()
    {
        if (healthUpgrades < 3  & upgradeable)
        {
            healthUpgrades++;

            upgradeable = false;

            if (healthUpgrades == 1)
            {
                upgradeHealth.transform.GetChild(0).gameObject.SetActive(true);

                playerControllerExtended.maxHealth = 13;
                Debug.Log($"maxMana: {playerControllerExtended.maxHealth}");
                Debug.Log($"currHealth: {playerControllerExtended.currHealth}");
            } 
            else if (healthUpgrades == 2)
            {
                upgradeHealth.transform.GetChild(1).gameObject.SetActive(true);
            
                playerControllerExtended.maxHealth = 16;
                Debug.Log($"maxMana: {playerControllerExtended.maxHealth}");
                Debug.Log($"currHealth: {playerControllerExtended.currHealth}");
            }
            else if (healthUpgrades == 3)
            {
                upgradeHealth.transform.GetChild(2).gameObject.SetActive(true);
            
                playerControllerExtended.maxHealth = 20;
                Debug.Log($"maxMana: {playerControllerExtended.maxHealth}");
                Debug.Log($"currHealth: {playerControllerExtended.currHealth}");
            }

            StartCoroutine(timeBeforeClosing());
        }
    }

    public void UpgradePunch()
    {
        if (punchUpgrades < 3 & upgradeable)
        {
            punchUpgrades++;
            upgradeable = false;

            if (punchUpgrades == 1)
            {
                upgradePunch.transform.GetChild(0).gameObject.SetActive(true);

                playerControllerExtended.fistDamage = 3;
            } 
            else if (punchUpgrades == 2)
            {
                upgradePunch.transform.GetChild(1).gameObject.SetActive(true);
            
                playerControllerExtended.fistDamage = 4;
            }
            else if (punchUpgrades == 3)
            {
                upgradePunch.transform.GetChild(2).gameObject.SetActive(true);
            
                playerControllerExtended.fistDamage = 6;
            }

            StartCoroutine(timeBeforeClosing());
        }
    }

    public void UpgradeFists()
    {
        if (fistsUpgrades < 3 & upgradeable)
        {
            fistsUpgrades++;

            upgradeable = false;
        
            if (fistsUpgrades == 1)
            {
                upgradeFist.transform.GetChild(0).gameObject.SetActive(true);
            } 
            else if (fistsUpgrades == 2)
            {
                upgradeFist.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (fistsUpgrades == 3)
            {
                upgradeFist.transform.GetChild(2).gameObject.SetActive(true);
            }

            StartCoroutine(timeBeforeClosing());
        }
    }

    IEnumerator timeBeforeClosing()
    {
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            yield return null;
        }

        upgradeable = true;

        gameManager.ResumeGame();
    }

}

