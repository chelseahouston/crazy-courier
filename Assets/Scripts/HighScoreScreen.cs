using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
