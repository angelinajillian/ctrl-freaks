using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainSceneName = "MainScene";
    public string creditSceneName = "CreditScene";

    void Start()
    {
        if (FindObjectOfType<SoundManager>().GetComponents<AudioSource>()[1].isPlaying)
            return;

        FindObjectOfType<SoundManager>().PlayMusic();
    }

    public void StartGame()
    {
        FindObjectOfType<SoundManager>().StopMusic();
        SceneManager.LoadScene(mainSceneName);
        FindObjectOfType<SoundManager>().PlayAmbience();
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditSceneName);
    }
}