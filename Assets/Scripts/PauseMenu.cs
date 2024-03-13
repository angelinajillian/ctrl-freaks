using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This method will be called when the "Quit Game" button is clicked
    public void QuitGame()
    {
        Application.Quit(); // This will quit the application
    }
}