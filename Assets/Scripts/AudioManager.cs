using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;
    public static AudioManager Instance;

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

        if (sound != null)
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
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);

    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    void LoadVolumePrefs()
    {
        SetSFXVolume(sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume"));
        SetMusicVolume(volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume"));
    }

    void OnApplicationQuit()
    {
        musicSource.Stop();
    }



}
