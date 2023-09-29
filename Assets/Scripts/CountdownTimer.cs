using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // timer text on screen
    public float countdownDuration = 43200; // 12 hours in seconds
    public float countdownSpeed = 120.0f; // countdown speed (in seconds)
    public Color textColorWhite = Color.white; // usual color
    public Color textColorRed = Color.red; // when low on time change to red
    public float colorChangeThreshold = 12000.0f; // 3 hour in seconds - when to change to red
    public Job job; // for setting end of day jobs and money

    private float currentTime;
    private bool isCountingDown = true; // for EOD reached
    private float realTimeElapsed;

    void Start()
    {
        // Initialize the timer
        currentTime = countdownDuration;
        realTimeElapsed = 0f;
    }

    void Update()
    {
        if (isCountingDown && currentTime > 0)
        {
            // Increase real-time elapsed
            realTimeElapsed += Time.deltaTime;

            // Calculate the adjusted countdown time
            float adjustedCountdownTime = countdownDuration - (realTimeElapsed / 90) * countdownDuration;

            // Decrease timer by countdownSpeed
            currentTime = adjustedCountdownTime - (countdownSpeed * Time.deltaTime);

            if (currentTime <= 0)
            {
                // Timer has reached zero, handle your event here
                currentTime = 0;
                job.EndOfDay();
                isCountingDown = false; // Stop counting down
            }

            UpdateUI();
        }
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

        // Change text color to red when there's 1 hour or less remaining
        if (currentTime <= colorChangeThreshold)
        {
            countdownText.color = textColorRed;
        }
        else
        {
            countdownText.color = textColorWhite;
        }
    }
}
