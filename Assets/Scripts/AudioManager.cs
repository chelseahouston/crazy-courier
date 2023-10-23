using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public List<AudioSource> sfxSources; // Use a List of AudioSources for SFX

    public List<AudioClip> musicClips; // List of available music clips
    public List<AudioClip> sfxClips;   // List of available SFX clips

    private Dictionary<string, AudioClip> musicClipDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClipDict = new Dictionary<string, AudioClip>();

    private float musicVolume = 1.0f;
    private float sfxVolume = 1.0f;

    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Populate the clip dictionaries for faster lookup
        foreach (var clip in musicClips)
        {
            musicClipDict[clip.name] = clip;
        }

        foreach (var clip in sfxClips)
        {
            sfxClipDict[clip.name] = clip;
        }

        // Load volume prefs
        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);

        // Initialize SFX AudioSources
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxClips.Count; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(source);
        }
    }

    void Start()
    {
        PlayMusic("MainMusic");
    }

    public void PlayMusic(string musicClipName)
    {
        if (musicClipDict.ContainsKey(musicClipName))
        {
            musicSource.clip = musicClipDict[musicClipName];
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Music clip not found: " + musicClipName);
        }
    }

    public void PlaySFX(string sfxClipName)
    {
        if (sfxClipDict.ContainsKey(sfxClipName))
        {
            AudioSource source = GetAvailableSFXSource();
            if (source != null)
            {
                source.clip = sfxClipDict[sfxClipName];
                source.Play();
            }
        }
        else
        {
            Debug.LogError("SFX clip not found: " + sfxClipName);
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var source in sfxSources)
        {
            source.volume = volume;
        }
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
