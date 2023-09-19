using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject howToPlayButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject optionsMenu;


    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
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
