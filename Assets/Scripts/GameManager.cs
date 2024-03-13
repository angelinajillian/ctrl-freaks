using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject UICanvas;

    public GameObject player;
    // private StarterAssetsInputs starterAssetsInputs;
    private FirstPersonController firstPersonController;
    private PlayerControllerExtended playerControllerExtended;

    private bool isPaused = false;

    private void Start()
    {
        // starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
        firstPersonController = player.GetComponent<FirstPersonController>();
        playerControllerExtended = player.GetComponent<PlayerControllerExtended>();
        // Ensure the pause menu is initially inactive
        ResumeGame();
    }

    private void Update()
    {
        // Check if the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
    }

    public void PauseGame()
    {
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
        UICanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // starterAssetsInputs.enabled = true;
        firstPersonController.enabled = true;
        playerControllerExtended.enabled = true;
        // Resume the game by setting time scale to 1
        
    }
}