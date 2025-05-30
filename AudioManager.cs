using UnityEngine;

/// <summary>
/// Singleton manager for handling all game audio.
/// Persists between scenes to maintain audio continuity.
/// </summary>
public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager instance;
    
    // Audio sources
    public AudioSource musicSource;     // For background music
    public AudioSource sfxSource;       // For sound effects
    
    // Music audio clips
    [Header("Music Clips")]
    public AudioClip mainMenuMusic;     // Background music for main menu
    public AudioClip victoryMusic;      // Music for victory/end scene
    
    // Sound effect audio clips
    [Header("SFX Clips")]
    public AudioClip coinSound;         // Sound when collecting a coin
    public AudioClip dogBark;           // Dog enemy sound
    public AudioClip doorOpen;          // Door opening sound
    public AudioClip keyFound;          // Key pickup sound
    public AudioClip lizardWalk;        // Player walking sound
    public AudioClip mouseSqueak;       // Rat enemy sound
    public AudioClip punch;             // Attack/hit sound

    /// <summary>
    /// Initialize singleton instance on wake
    /// </summary>
    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            // This is the first instance
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            // Another instance exists, destroy this one
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play a sound effect once
    /// </summary>
    /// <param name="clip">Sound effect to play</param>
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Play music track with looping
    /// </summary>
    /// <param name="clip">Music track to play</param>
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    
    /// <summary>
    /// Start playing the lizard walking sound with looping
    /// </summary>
    public void PlayLizardWalk()
    {
        if (lizardWalk != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = lizardWalk;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    /// <summary>
    /// Stop the lizard walking sound if it's playing
    /// </summary>
    public void StopLizardWalk()
    {
        if (sfxSource.isPlaying && sfxSource.clip == lizardWalk)
        {
            sfxSource.Stop();
        }
    }
}