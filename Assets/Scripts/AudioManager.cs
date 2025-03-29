using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Auto create empty game object and add this script to it if it doesn't exist
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                _instance = obj.AddComponent<AudioManager>();
                DontDestroyOnLoad(obj);
                _instance.Initialize();
            }
            return _instance;
        }
    }

    // Split into music and sfx for modularity
    private AudioSource musicSource;
    private AudioSource sfxSource;

    // Initialize audio clip objects
    private AudioClip _gameMusic;
    private AudioClip _menuMusic;
    private AudioClip _gameWin;
    private AudioClip _ghostEaten;
    private AudioClip _speedup;
    private AudioClip _consumePellet;
    private AudioClip _loseLife;

    // Getters connecting clip with logic, executes only if not already defined (loaded)
    private AudioClip GameMusic => _gameMusic ??= LoadClip("GameMusic");
    private AudioClip MenuMusic => _menuMusic ??= LoadClip("MenuMusic");
    private AudioClip GameWin => _gameWin ??= LoadClip("GameWin");
    private AudioClip GhostEaten => _ghostEaten ??= LoadClip("GhostEaten");
    private AudioClip Speedup => _speedup ??= LoadClip("Speedup");
    private AudioClip ConsumePellet => _consumePellet ??= LoadClip("ConsumePellet");
    private AudioClip LoseLife => _loseLife ??= LoadClip("LoseLife");

    // Increase pitch of game music as levels go higher
    private int currentLevel = 1;
    private const float PITCH_INCREASE_PER_LEVEL = 0.1f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        // Initalize; adjust volume here
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f; // volume (music louder than sfx in source files)

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 0.8f; //volume
    }

    private AudioClip LoadClip(string clipName)
    {
        // Concatenation shenanagins and loading
        AudioClip clip = Resources.Load<AudioClip>($"Audio/{clipName}");
        if (clip == null)
        {
            clip = Resources.Load<AudioClip>(clipName);
            if (clip == null)
                Debug.LogWarning($"AudioManager: Could not find clip '{clipName}'");
        }
        return clip;
    }

    // Call the following methods from other files based on game state
    public void PlayMenuMusic(bool withFade = false)
    {
        if (withFade)
        {
            FadeToMusic(MenuMusic);
            return;
        }

        musicSource.clip = MenuMusic;
        musicSource.pitch = 1.0f;
        musicSource.Play();
    }

    public void PlayGameMusic(int level = 1, bool withFade = false)
    {
        currentLevel = Mathf.Clamp(level, 1, 3);
        float pitch = 1.0f + ((currentLevel - 1) * PITCH_INCREASE_PER_LEVEL);

        if (withFade)
        {
            FadeToMusic(GameMusic, pitch);
            return;
        }

        musicSource.clip = GameMusic;
        musicSource.pitch = pitch;
        musicSource.Play();
    }
    // Helper methods for fading music 
    private void FadeToMusic(AudioClip newClip, float targetPitch = 1.0f)
    {
        StartCoroutine(FadeMusicCoroutine(newClip, targetPitch));
    }

    private IEnumerator FadeMusicCoroutine(AudioClip newClip, float targetPitch)
    {
        // Fade out
        float startVolume = musicSource.volume;
        float duration = 1.0f;

        float time = 0;
        while (time < duration / 2)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }

        // Switch clip
        musicSource.clip = newClip;
        musicSource.pitch = targetPitch;
        musicSource.Play();

        // Fade in
        time = 0;
        while (time < duration / 2)
        {
            musicSource.volume = Mathf.Lerp(0, startVolume, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = startVolume;
    }

    // Method that changes the pitch based on variable defined earlier
    public void IncreaseLevel()
    {
        currentLevel = Mathf.Min(currentLevel + 1, 3);
        if (musicSource.clip == GameMusic)
        {
            musicSource.pitch = 1.0f + ((currentLevel - 1) * PITCH_INCREASE_PER_LEVEL);
        }
    }

    // SFX Methods
    public void PlayWinSound() => PlaySound(GameWin);
    public void PlayGhostEatenSound() => PlaySound(GhostEaten);
    public void PlaySpeedupSound() => PlaySound(Speedup);

    public void PlayPelletSound()
    {
        // Prevent fatigue from same sound (slightly different pitch on each collection)
        sfxSource.pitch = Random.Range(0.98f, 1.02f);
        PlaySound(ConsumePellet);
    }

    public void PlayLoseLifeSound() => PlaySound(LoseLife);

    // Prevents playing a clip if it's already playing - can change if we decide on overlapping sound
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }
}