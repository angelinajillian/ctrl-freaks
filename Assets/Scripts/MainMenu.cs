using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainSceneName = "MainScene";
    public string creditSceneName = "CreditScene";

    public void StartGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditSceneName);
    }
}