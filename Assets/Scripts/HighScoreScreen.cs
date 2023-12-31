using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class HighScoreScreen : MonoBehaviour
{

    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject highScoreScreen;
    public ScoreSaving scores;

    void Start()
    {
        scores.LoadDailyScores();

    }

    public void Back()
    {
        highScoreScreen.SetActive(false);
    }

}
