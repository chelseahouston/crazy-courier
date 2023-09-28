using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton, howToPlayButton, optionsButton, creditsButton, exitButton, optionsMenu, highScores;

    void Start()
    {
        optionsMenu.SetActive(false);
        highScores.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void ShowHighScores()
    {
        optionsMenu.SetActive(false);
        highScores.SetActive(true);
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
