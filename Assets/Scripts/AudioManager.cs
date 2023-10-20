using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 20-10-23

public class AudioManager : MonoBehaviour
{
    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;
    public Slider volumeSlider;
    public Slider sfxSlider;
    public static AudioManager Instance;

    public AudioSource getMusicSource
    { get { return musicSource; } }

    public AudioSource getSfxSource
    { get { return sfxSource; } }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadVolumePrefs();
        PlayMusic("MainMusic");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(music, x => x.name == name);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }

        else
        {
            Debug.Log(name + " sound not found!");
        }
    }


    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfx, x => x.name == name);

        if (sound != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = sound.clip;
            sfxSource.Play();
        }

        else
        {
            Debug.Log(name + " sfx not found!");
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.getMusicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.getSfxSource.volume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }


    public void LoadVolumePrefs()
    {
        // Load the PlayerPrefs values
        float savedSfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume");

        // Update the sliders with the loaded values
        sfxSlider.value = savedSfxVolume;
        volumeSlider.value = savedMusicVolume;

        // Set the audio volumes with the loaded values
        SetSFXVolume(savedSfxVolume);
        SetMusicVolume(savedMusicVolume);
    }


    void OnApplicationQuit()
    {
        musicSource.Stop();
    }



}
