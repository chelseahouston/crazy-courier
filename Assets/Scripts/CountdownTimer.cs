using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // timer text on screen
    public float countdownDuration = 43200; // 12 hours in seconds
    public float countdownSpeed = 1.0f; // countdown speed (1x speed)
    public Color textColorWhite = Color.white; // usual color
    public Color textColorRed = Color.red; // when low on time change to red
    public float colorChangeThreshold = 10800.0f; // 3 hours in seconds - when to change to red
    public Job job; // for setting end-of-day jobs and money

    private float currentTime;
    private bool isCountingDown = true; // for EOD reached
    private bool hasEnded = false; // to ensure it stays at 0:00

    void Start()
    {
        // Initialize the timer to 12:00 (43200 seconds)
        currentTime = countdownDuration;
    }

    void Update()
    {
        if (isCountingDown && currentTime > 0)
        {
            // Decrease timer by countdownSpeed
            currentTime = Mathf.Max(currentTime - (countdownSpeed * Time.deltaTime), 0);

            if (currentTime <= 0 && !hasEnded)
            {
                // Timer has reached zero for the first time, handle your event here
                currentTime = 0;
                job.EndOfDay();
                isCountingDown = false; // Stop counting down
                hasEnded = true; // Set the flag to ensure it stays at 0:00
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Calculate hours and minutes from currentTime
        int totalMinutes = Mathf.FloorToInt(currentTime / 60);
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        // Convert to 12-hour format
        hours = hours % 12;
        if (hours == 0)
        {
            hours = 12; // 12:00 AM or 12:00 PM
        }

        // Update the UI text to display the countdown
        countdownText.text = string.Format("{0:00}:{1:00}", hours, minutes);

        // Change text color to red when there's 3 hours or less remaining
        if (currentTime <= colorChangeThreshold && !hasEnded)
        {
            countdownText.color = textColorRed;
        }
        else
        {
            countdownText.color = textColorWhite;
        }
    }
}
