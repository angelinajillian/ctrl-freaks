using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{    
    // Varaible to store the next scene (in this case it's MainScene)
    public int nextScene;
    
    // Method to start playing the game
    public void PlayGame()
    {
        // Load the next scene using SceneManager
        SceneManager.LoadScene(nextScene);
    }
}
