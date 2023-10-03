using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause, options, timer;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        pause.SetActive(true);
        timer.GetComponent<CountdownTimer>().PauseTimer();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        pause.SetActive(false);
        timer.GetComponent<CountdownTimer>().ResumeTimer();
        Time.timeScale = 1;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void ShowOptions()
    {
        options.SetActive(true);
    }

}
