using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainOptionsMenu : MonoBehaviour
{

    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject optionsMenu;
    public Slider volumeSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
    }

    public void ToggleMusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(volumeSlider.value);
    }

    public void ToggleSFXvolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

}
