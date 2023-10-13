using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 03-10-23

public class CarDamaged : MonoBehaviour
{

    public GameObject carDestroyedPanel; // end of day/level panel 
    public TextMeshProUGUI jobs, money; // end of day jobs completed and earnings text
    public GameObject data; // for getting job info

    void Start()
    {
        carDestroyedPanel.SetActive(false); // not shown until end of day
        clearText(); // clears jobs/money text
        Time.timeScale = 1;
    }

    public void SetCarDestroyedActive()
    {
        updateUI();
        carDestroyedPanel.SetActive(true); // called when car too damaged
        Time.timeScale = 0;
    }

    public void updateUI()
    {
        jobs.text = "Jobs Completed: " + data.GetComponent<Job>().getTotalJobs();
        money.text = "Daily Earnings: $" + data.GetComponent<Job>().getTotalMoney();

    }

    public void clearText()
    {
        jobs.text = "";
        money.text = "";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
    }



}
