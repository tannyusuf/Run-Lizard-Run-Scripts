using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the main menu functionality and scene transitions
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Start playing background music when main menu loads
    /// </summary>
    void Start()
    {
        // Play main menu music when the scene loads
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
        }
    }

    /// <summary>
    /// Start the game when play button is pressed
    /// </summary>
    public void StartGame()
    {
        // Load the gameplay scene
        // Replace "SampleScene" with your actual gameplay scene name
        SceneManager.LoadScene("SampleScene");
    }
}