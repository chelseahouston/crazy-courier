using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScreen : MonoBehaviour
{

    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject highScoreScreen;

    void Start()
    {
        // get high scores from high score player prefs string
        // set as text on screen
        
    }

    public void Back()
    {
        highScoreScreen.SetActive(false);
    }

}
