using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 20-10-23

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

    public GameObject globalLightObject;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public float maxIntensity;
    public float minIntensity;
    public float elapsedTime;

    public List<GameObject> carLights;
    public List<GameObject> restaurantLights;
    public List<GameObject> homeLights;
    public List<GameObject> streetLights;

    void Start()
    {
        job = Data.GetComponent<Job>();
        // Set the initial time to 720.0f (12 minutes) to count down to 0:00 in 90 seconds.
        currentTime = 720.0f; // 12 real-time minutes

        elapsedTime = 0.0f;
        maxIntensity = 1.0f;
        minIntensity = 0.4f;
        globalLight = globalLightObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        globalLight.intensity = maxIntensity;

        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Lights"))
        {
            if (gameObj.name == "Car Light")
            {
                carLights.Add(gameObj);
                gameObj.SetActive(false);
            }
            if (gameObj.name == "Light 2D")
            {
                streetLights.Add(gameObj);
                gameObj.SetActive(false);
            }
            if (gameObj.name == "Restaurant Light")
            {
                restaurantLights.Add(gameObj);
                gameObj.SetActive(false);
            }
            if (gameObj.name == "House Light")
            {
                homeLights.Add(gameObj);
                gameObj.SetActive(false);
            }
        }


        UpdateTimerText();
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime * (720.0f / 120.0f); // Adjust the countdown speed

            // Check if the timer has reached 3:00 (180 seconds) and change text color to red
            if (currentTime <= 180.0f)
            {
                timerText.color = warningColor;
            }

            UpdateTimerText();

            if (currentTime <= 0.0f)
            {
                // Timer has reached 0:00
                if (!collection.pickedup) // if order has been collected don't end until delivered to the customer!
                {
                    isRunning = false;
                    currentTime = 0.0f;
                    job.EndOfDay();
                }
                else
                {
                    
                    timerText.text = "OVERTIME";
                    currentTime = 0.0f;
                }
            }

            if (driver.isDead)
            {
                isRunning = false;
                job.CarDestroyed();
            }

            // Interpolate the light intensity over time
            if (globalLight.intensity > minIntensity)
            {
                float currentIntensity = Mathf.Lerp(maxIntensity, minIntensity, elapsedTime / 120);
                globalLight.intensity = currentIntensity;

                elapsedTime += Time.deltaTime;

            }

            if (!isRunning)
            {
                currentTime = 0.0f;
                timerText.text = "00:00";
            }

            // turn on street, car, house, and restaurant light when dark enough
            if (globalLight.intensity < 0.75f)
            {
                foreach (GameObject gameObj in carLights)
                {
                    gameObj.SetActive(true);
                }
                StartCoroutine(ActivateLightsOverTime(0.03f));
            }

        }
    }

    IEnumerator ActivateLightsOverTime(float delayBetweenActivations)
    {
        foreach (GameObject gameObj in homeLights)
        {
            gameObj.SetActive(true);
            yield return new WaitForSeconds(delayBetweenActivations); 
        }
        foreach (GameObject gameObj in restaurantLights)
        {
            gameObj.SetActive(true);
            yield return new WaitForSeconds(delayBetweenActivations);
        }
        foreach (GameObject gameObj in streetLights)
        {
            gameObj.SetActive(true);
            yield return new WaitForSeconds(delayBetweenActivations);
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

