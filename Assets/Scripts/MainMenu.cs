using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton, howToPlayButton, optionsButton, creditsButton, exitButton, optionsMenu, highScores, howToPlayScreen;

    void Start()
    {
        optionsMenu.SetActive(false);
        highScores.SetActive(false);
        howToPlayScreen.SetActive(false);
<<<<<<< Updated upstream
=======
        howToPlayScreen2.SetActive(false);
        howToPlayScreen3.SetActive(false);
        
>>>>>>> Stashed changes
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

    public void ExitGame()
    {
        Debug.Log("Exiting App!");
        Application.Quit();
    }

}
