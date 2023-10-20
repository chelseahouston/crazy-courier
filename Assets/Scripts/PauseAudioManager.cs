using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 20-10-23

public class PauseAudioManager : MonoBehaviour
{

    [SerializeField] private Slider pauseVolumeSlider;
    [SerializeField] private Slider pauseSfxSlider;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = AudioManager.Instance;
        audioManager.LoadVolumePrefs();
        pauseVolumeSlider.value = audioManager.volumeSlider.value;
        pauseSfxSlider.value = audioManager.sfxSlider.value;
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
        AudioManager.Instance.SetSFXVolume(volume);
    }

    public void ToggleMusicVolume()
    {
        SetMusicVolume(pauseVolumeSlider.value);
        audioManager.volumeSlider.value = pauseVolumeSlider.value;
    }

    public void ToggleSFXvolume()
    {
        SetSFXVolume(pauseSfxSlider.value);
        audioManager.sfxSlider.value = pauseSfxSlider.value;
    }
}

