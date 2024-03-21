using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    
    public void RestartGame()
    {
        //FindObjectOfType<SoundManager>().PlayAmbience
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This method will be called when the "Quit Game" button is clicked
    public void QuitGame()
    {
        FindObjectOfType<SoundManager>().StopAmbience();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}