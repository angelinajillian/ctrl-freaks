using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UpgradeSystem : MonoBehaviour, IPointerEnterHandler
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

    public Text upgradeManaText;
    public Text upgradeHealthText;
    public Text upgradePunchText;
    public Text upgradeFistText;

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
            
                playerControllerExtended.maxHealth = 22;
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
                playerControllerExtended.punchDamage = 2;
            } 
            else if (punchUpgrades == 2)
            {
                upgradePunch.transform.GetChild(1).gameObject.SetActive(true);
                playerControllerExtended.punchDamage = 3;
            }
            else if (punchUpgrades == 3)
            {
                upgradePunch.transform.GetChild(2).gameObject.SetActive(true);
                playerControllerExtended.punchDamage = 5;
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
                playerControllerExtended.fistDamage = 3;
            } 
            else if (fistsUpgrades == 2)
            {
                upgradeFist.transform.GetChild(1).gameObject.SetActive(true);
                playerControllerExtended.fistDamage = 4;
            }
            else if (fistsUpgrades == 3)
            {
                upgradeFist.transform.GetChild(2).gameObject.SetActive(true);
                playerControllerExtended.fistDamage = 6;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == upgradeMana)
        {
            // Ensure all the rest are off
            upgradeHealthText.gameObject.SetActive(false);
            upgradePunchText.gameObject.SetActive(false);
            upgradeFistText.gameObject.SetActive(false);

            if(manaUpgrades < 3)
            {
                if (manaUpgrades == 0)
                {
                    upgradeManaText.text = "Mana + 25%";
                }
                else if (manaUpgrades == 1)
                {
                    upgradeManaText.text = "Mana + 25%";
                }
                else if (manaUpgrades == 2)
                {
                    upgradeManaText.text = "Mana + 50%";
                }
                upgradeManaText.gameObject.SetActive(true);
            }
        }
        else if (eventData.pointerEnter == upgradeHealth)
        {
            // Ensure all the rest are off
            upgradeManaText.gameObject.SetActive(false);
            upgradePunchText.gameObject.SetActive(false);
            upgradeFistText.gameObject.SetActive(false);

            if(healthUpgrades < 3)
            {
                if (healthUpgrades == 0)
                {
                    upgradeHealthText.text = "Health + 33%";
                }
                else if (healthUpgrades == 1)
                {
                    upgradeHealthText.text =  "Health + 33%";
                }
                else if (healthUpgrades == 2)
                {
                    upgradeHealthText.text =  "Health + 50%";
                }
                upgradeHealthText.gameObject.SetActive(true);
            }
        }
        else if (eventData.pointerEnter == upgradePunch)
        {
            // Ensure all the rest are off
            upgradeManaText.gameObject.SetActive(false);
            upgradeHealthText.gameObject.SetActive(false);
            upgradeFistText.gameObject.SetActive(false);

            if(punchUpgrades < 3)
            {
                if (punchUpgrades == 0)
                {
                    upgradePunchText.text = "Punch Distance + 25%";
                }
                else if (punchUpgrades == 1)
                {
                    upgradePunchText.text = "Punch Distance + 25%";
                }
                else if (punchUpgrades == 2)
                {
                    upgradePunchText.text = "Punch Distance + 50%";
                }
                upgradePunchText.gameObject.SetActive(true);
            }
        }
        else if (eventData.pointerEnter == upgradeFist)
        {
            // Ensure all the rest are off
            upgradeManaText.gameObject.SetActive(false);
            upgradePunchText.gameObject.SetActive(false);
            upgradeHealthText.gameObject.SetActive(false);
            
            if (fistsUpgrades < 3)
            {
                if (fistsUpgrades == 0)
                {
                    upgradeFistText.text = "Fist Damage + 50%";
                }
                else if (punchUpgrades == 1)
                {
                    upgradeFistText.text = "Fist Damage + 25%";
                }
                else if (punchUpgrades == 2)
                {
                    upgradeFistText.text = "Fist Damage + 50%";
                }
                upgradeFistText.gameObject.SetActive(true);
            }
        }
    }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if (eventData.pointerEnter == upgradeMana)
    //     {
    //         upgradeManaText.gameObject.SetActive(false);
    //     }
    //     else if (eventData.pointerEnter == upgradeHealth)
    //     {
    //         upgradeHealthText.gameObject.SetActive(false);
    //     }
    //     else if (eventData.pointerEnter == upgradePunch)
    //     {
    //         upgradePunchText.gameObject.SetActive(false);
    //     }
    //     else if (eventData.pointerEnter == upgradeFist)
    //     {
    //         upgradeFistText.gameObject.SetActive(false);
    //     }
    // }
}

