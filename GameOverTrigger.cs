using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the end game trigger zone
/// When player enters this zone, the game over scene is loaded
/// </summary>
public class GameOverTrigger : MonoBehaviour
{
    // Name of scene to load when triggered
    public string gameOverSceneName = "GameOver"; // Set your Game Over scene name here
    
    /// <summary>
    /// Start playing victory music when this object initializes
    /// </summary>
    void Start()
    {
        // Play victory music when the scene loads
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.victoryMusic);
        }
    }
    
    /// <summary>
    /// Detect when player enters the trigger zone
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if player entered the trigger
        if (other.CompareTag("Player"))
        {
            // Load the game over scene
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
