using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject UICanvas;
    public GameObject redFlash;
    public GameObject upgradeSystemUI;

    public GameObject player;
    // private StarterAssetsInputs starterAssetsInputs;
    private FirstPersonController firstPersonController;
    private PlayerControllerExtended playerControllerExtended;
    private SpellFactory spellFactory;

    private bool isPaused = false;

    private void Start()
    {
        // starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
        firstPersonController = player.GetComponent<FirstPersonController>();
        playerControllerExtended = player.GetComponent<PlayerControllerExtended>();
        spellFactory = player.GetComponent<SpellFactory>();
        // Ensure the pause menu is initially inactive
        ResumeGame();
        redFlash.SetActive(false);
    }

    private void Update()
    {
        // Check if the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (upgradeSystemUI.activeSelf)
            {
                return;
            }

            // Toggle pause state
            isPaused = !isPaused;

            // Update game state based on pause state
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     UpgradeMenu();
        // }
    }

    public void PauseGame()
    {
        spellFactory.enabled = false;
        Time.timeScale = 0f;
        // Show pause menu
        pauseMenuUI.SetActive(true);
        UICanvas.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // starterAssetsInputs.enabled = false;
        firstPersonController.enabled = false;
        playerControllerExtended.enabled = false;
        // Pause the game by setting time scale to 0
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        // Hide pause menu
        pauseMenuUI.SetActive(false);
        upgradeSystemUI.SetActive(false);
        UICanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // starterAssetsInputs.enabled = true;
        firstPersonController.enabled = true;
        playerControllerExtended.enabled = true;
        spellFactory.enabled = true;
        // Resume the game by setting time scale to 1
        
    }

    // // Resume from Upgrading
    // public void ResumeGame()
    // {
    //     Time.timeScale = 1f;
    //     // Hide pause menu
    //     upgradeSystemUI.SetActive(false);
    //     UICanvas.SetActive(true);
    //     Cursor.visible = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    //     // starterAssetsInputs.enabled = true;
    //     firstPersonController.enabled = true;
    //     playerControllerExtended.enabled = true;
    //     spellFactory.enabled = true;
    //     // Resume the game by setting time scale to 1
        
    // }

    public void UpgradeMenu()
    {
        spellFactory.enabled = false;
        Time.timeScale = 0f;
        // Show pause menu
        upgradeSystemUI.SetActive(true);
        UICanvas.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // starterAssetsInputs.enabled = false;
        firstPersonController.enabled = false;
        playerControllerExtended.enabled = false;
        // Pause the game by setting time scale to 0
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRedEnum(.3f));
    }

    IEnumerator FlashRedEnum(float flashLength)
    {
        Debug.Log("FLASH RED");
        redFlash.SetActive(true);

        yield return new WaitForSeconds(flashLength);

        redFlash.SetActive(false);
    }
}