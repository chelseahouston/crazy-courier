using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class HowToPlayScreen : MonoBehaviour
{
    [SerializeField] private GameObject backButton, nextButton1, nextButton2;
    [SerializeField] private GameObject howToPlayScreen, howToPlayScreen2, howToPlayScreen3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadHTP2()
    {
        howToPlayScreen.SetActive(false);
        howToPlayScreen2.SetActive(true);
    }

    public void LoadHTP3()
    {
        howToPlayScreen2.SetActive(false);
        howToPlayScreen3.SetActive(true);
    }

    public void MainMenu()
    {
        howToPlayScreen3.SetActive(false);
    }
}
