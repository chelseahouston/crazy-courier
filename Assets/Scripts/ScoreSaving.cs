using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaving
{
    public List<int> jobScores = new List<int>();
    public List<int> moneyScores = new List<int>();
    public Job currentJob;
    public int J1, J2, J3; // top 3 jobs
    public int M1, M2, M3; // top 3 money


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
        
    void Start()
    {
        // load playerpref daily job top 3 high scores
        // load playerpref daily money top 3 high scores
        LoadScores();
    }

    public void LoadScores()
    {
        PlayerPrefs.SetInt(J1, "JobScores" + 0);
        PlayerPrefs.SetInt(J2, "JobScores" + 1);
        PlayerPrefs.SetInt(J3, "JobScores" + 2);
        PlayerPrefs.SetInt(M1, "MoneyScores" + 0);
        PlayerPrefs.SetInt(M1, "MoneyScores" + 1);
        PlayerPrefs.SetInt(M1, "MoneyScores" + 2);
    }
    
    public void ResetScores()
    {
        jobScores.Clear();
        moneyScores.Clear();
        jobScores.AddRange(0, 0, 0);
        moneyScores.AddRange(0, 0, 0);
    }

    public void AddToJobScores(int score) { }
    public void AddToMoneyScores(int score) { }

    public void SaveScores()
    {
        for (int i = 0; i < jobScores.Count; i++)
        {
            PlayerPrefs.SetInt("JobScores" + i, jobScores[i]);
        }
        for (int i = 0; i < moneyScores.Count; i++)
        {
            PlayerPrefs.SetInt("MoneyScores" + i, moneyScores[i]);
        }
    }
    
    public List<int> GetJobScores()
    {
        return jobScores;
    }
    public List<int> GetMoneyScores()
    {
        return moneyScores;
    }
    
    void OnApplicationQuit()
    {
        SaveScores();
        PlayerPrefs.Save();
    }
}
