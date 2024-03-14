using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    public string mainSceneName = "MainScene";

    public void BackButton()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}