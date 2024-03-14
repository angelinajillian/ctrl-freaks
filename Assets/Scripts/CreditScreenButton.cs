using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreenButton : MonoBehaviour
{
    // Varaible to store the next scene (in this case it's MainScene)
    public int nextScene;
    
    // Method to return to menu from credits screen
    public void ReturnToMenu()
    {
        // Load the next scene using SceneManager
        SceneManager.LoadScene(nextScene);
    }
}
