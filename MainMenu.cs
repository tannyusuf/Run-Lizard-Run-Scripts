using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Play main menu music when the scene loads
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
        }
    }

    public void StartGame()
    {
        // Replace "SampleScene" with your actual gameplay scene name
        SceneManager.LoadScene("SampleScene");
    }
}