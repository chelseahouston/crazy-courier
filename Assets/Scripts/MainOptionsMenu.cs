using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class MainOptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject optionsMenu;
    public Slider volumeSlider;
    public Slider sfxSlider;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }
        else
        {
            // Set the initial values of the sliders based on PlayerPrefs
            volumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicVolumeKey, 1.0f);
            sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFXVolumeKey, 1.0f);
        }
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
    }

    public void ToggleMusicVolume()
    {
        if (audioManager != null)
        {
            audioManager.SetMusicVolume(volumeSlider.value);
            Debug.Log("Music Volume Slider Value: " + volumeSlider.value);
        }
    }

    public void ToggleSFXVolume()
    {
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(sfxSlider.value);
            Debug.Log("SFX Volume Slider Value: " + sfxSlider.value);
        }
    }
}
