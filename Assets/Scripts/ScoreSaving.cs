using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreSaving: MonoBehaviour
{
    public DailyScore dailyScores;
    public TextMeshProUGUI J1, J2, J3; // top 3 job high scores
    public TextMeshProUGUI M1, M2, M3; // top 3 money high scores

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // load saved daily top scores
        LoadDailyScores();
    }

    public void UpdateDailyScores(float newEarnings, int newJobsCompleted)
    {
        // update the daily earnings list
        for (int i = 0; i < dailyScores.top3Earnings.Length; i++)
        {
            if (newEarnings > dailyScores.top3Earnings[i])
            {
                for (int j = dailyScores.top3Earnings.Length - 1; j > i; j--)
                {
                    dailyScores.top3Earnings[j] = dailyScores.top3Earnings[j - 1];
                }
                dailyScores.top3Earnings[i] = newEarnings;
                break;
            }
        }

        // update the daily jobs completed list
        for (int i = 0; i < dailyScores.top3JobsCompleted.Length; i++)
        {
            if (newJobsCompleted > dailyScores.top3JobsCompleted[i])
            {
                for (int j = dailyScores.top3JobsCompleted.Length - 1; j > i; j--)
                {
                    dailyScores.top3JobsCompleted[j] = dailyScores.top3JobsCompleted[j - 1];
                }
                dailyScores.top3JobsCompleted[i] = newJobsCompleted;
                break;
            }
        }

        // save the updated daily scores
        SaveDailyScores();
    }

    public void ClearDailyScores()
    {
        // clear both daily earnings and jobs completed lists
        dailyScores.top3Earnings = new float[3];
        dailyScores.top3JobsCompleted = new int[3];

        // Save the cleared scores
        SaveDailyScores();
        UpdateUIScores();
    }

    private void SaveDailyScores()
    {
        // convert dailyScores to JSON and save it in PlayerPrefs
        string json = JsonUtility.ToJson(dailyScores);
        PlayerPrefs.SetString("DailyScores", json);
        UpdateUIScores();
    }

    public void LoadDailyScores()
    {
        // load the saved daily scores from PlayerPrefs
        string json = PlayerPrefs.GetString("DailyScores");
        if (!string.IsNullOrEmpty(json))
        {
            dailyScores = JsonUtility.FromJson<DailyScore>(json);
        }
        else
        {
            // initialize dailyScores if it's not found in PlayerPrefs
            dailyScores = new DailyScore();
        }

        UpdateUIScores();
    }

    public void UpdateUIScores()
    {
        // Update job completed scores
        J1.text = dailyScores.top3JobsCompleted[0].ToString() + " jobs";
        J2.text = dailyScores.top3JobsCompleted[1].ToString() + " jobs";
        J3.text = dailyScores.top3JobsCompleted[2].ToString() + " jobs";

        // Update money earnings scores (assuming you want to format them as floats)
        M1.text = "$" + dailyScores.top3Earnings[0].ToString("F2") + ""; // "F2" for two decimal places
        M2.text = "$" + dailyScores.top3Earnings[1].ToString("F2") + ""; // "F2" for two decimal places
        M3.text = "$" + dailyScores.top3Earnings[2].ToString("F2") + ""; // "F2" for two decimal places

    }

}
