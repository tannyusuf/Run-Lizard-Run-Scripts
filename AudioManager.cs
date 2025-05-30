using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip victoryMusic;

    [Header("SFX Clips")]
    public AudioClip coinSound;
    public AudioClip dogBark;
    public AudioClip doorOpen;
    public AudioClip keyFound;
    public AudioClip lizardWalk;
    public AudioClip mouseSqueak;
    public AudioClip punch;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it active in all scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayLizardWalk()
    {
        if (lizardWalk != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = lizardWalk;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopLizardWalk()
    {
        if (sfxSource.isPlaying && sfxSource.clip == lizardWalk)
        {
            sfxSource.Stop();
        }
    }
}