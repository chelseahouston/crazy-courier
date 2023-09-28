using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaving : MonoBehaviour
{
    public List<int> jobScores = new List<int>();
    public List<int> moneyScores = new List<int>();
    public Job currentJob;
    private static ScoreSaving m_instance;
    private const int LeaderboardLength = 3;

    public static ScoreSaving _instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("ScoreSaving").AddComponent<ScoreSaving>();
            }
            return m_instance;
        }
    }

    void Start()
    {
        // load playerpref daily job top 3 high scores
        // load playerpref daily money top 3 high scores

        LoadJobScores();
        LoadMoneyScores();

    }

    public void LoadMoneyScores(){

    }

    public void LoadJobScores()
    {

    }

    public void ResetScores()
    {
        jobScores.Clear();
        moneyScores.Clear();
        jobScores.AddRange(0, 0, 0);
        moneyScores.AddRange(0, 0, 0);
    }

    public void SaveScores()
    {
        jobScores.Add()
    }

        void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else if (m_instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void SaveHighScore(string name, int score)
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
            {
                Scores temp = new Scores();
                temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                HighScores.Add(temp);
                i++;
            }
            if (HighScores.Count == 0)
            {
                Scores _temp = new Scores();
                _temp.name = name;
                _temp.score = score;
                HighScores.Add(_temp);
            }
            else
            {
                for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
                {
                    if (score > HighScores[i - 1].score)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.score = score;
                        HighScores.Insert(i - 1, _temp);
                        break;
                    }
                    if (i == HighScores.Count && i < LeaderboardLength)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.score = score;
                        HighScores.Add(_temp);
                        break;
                    }
                }
            }

            i = 1;
            while (i <= LeaderboardLength && i <= HighScores.Count)
            {
                PlayerPrefs.SetString("HighScore" + i + "name", HighScores[i - 1].name);
                PlayerPrefs.SetInt("HighScore" + i + "score", HighScores[i - 1].score);
                i++;
            }

        }

        public List<Scores> GetHighScore()
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
            {
                Scores temp = new Scores();
                temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                HighScores.Add(temp);
                i++;
            }

            return HighScores;
        }

        public void ClearLeaderBoard()
        {
            //for(int i=0;i<HighScores.
            List<Scores> HighScores = GetHighScore();

            for (int i = 1; i <= HighScores.Count; i++)
            {
                PlayerPrefs.DeleteKey("HighScore");
                PlayerPrefs.DeleteKey("HighScore");
            }
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }


}
