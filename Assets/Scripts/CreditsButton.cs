using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour
{
    // Varaible to store the next scene (in this case it's MainScene)
    public int nextScene;
    
    // Method to open credits
    public void OpenCredits()
    {
        // Load the next scene using SceneManager
        SceneManager.LoadScene(nextScene);
    }
}
