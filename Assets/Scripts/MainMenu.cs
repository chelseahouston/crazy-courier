using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton, howToPlayButton, optionsButton, creditsButton, exitButton, optionsMenu, highScores, howToPlayScreen;

    void Start()
    {
        optionsMenu.SetActive(false);
        highScores.SetActive(false);
        howToPlayScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadHowToPlay()
    {
        howToPlayScreen.SetActive(true);
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
