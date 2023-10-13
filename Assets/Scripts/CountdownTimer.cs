using UnityEngine;
using TMPro;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 90.0f; // Total countdown time in real-time seconds
    public Color warningColor = Color.red;

    private float currentTime;
    private bool isRunning = false;

    public GameObject Data;
    public Job job;
    public Driver driver;

    public Collision collection;

    void Start()
    {
        job = Data.GetComponent<Job>();
        // Set the initial time to 720.0f (12 minutes) to count down to 0:00 in 60 seconds.
        currentTime = 720.0f; // 12 real-time minutes
        UpdateTimerText();
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime * (720.0f / 120.0f); // Adjust the countdown speed

            // Ensure the timer doesn't go into negative values
            if (currentTime < 0.0f)
            {
                currentTime = 0.0f;
            }

            // Check if the timer has reached 3:00 (180 seconds) and change text color to red
            if (currentTime <= 180.0f)
            {
                timerText.color = warningColor;
            }

            UpdateTimerText();

            if (currentTime <= 0.0f)
            {
                currentTime = 0.0f;
                
                // Timer has reached 0:00
                if (!collection.pickedup) // if order has been collected dont end until delivered to customer!
                {
                    isRunning = false;
                    job.EndOfDay();
                }
                
            }

            if (driver.isDead)
            {
                isRunning = false;
                job.CarDestroyed();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Format the timer text as "mm:ss"
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}

