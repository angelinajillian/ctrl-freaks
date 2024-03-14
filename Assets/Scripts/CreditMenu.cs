using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public void BackButton()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}