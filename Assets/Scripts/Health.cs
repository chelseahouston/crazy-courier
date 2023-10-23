using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class Health : MonoBehaviour
{
    public List<GameObject> healthObjs; // list of health game objects in scene
    public int duration; // duration to wait before making a health object active
    public int location; // position in list for health location
    public bool counting; // if counting down dont change duration or spawn health icon
    public int currentIndex; // current index of health obj

    void Start()
    {
        // always start with empty list
        healthObjs.Clear();
        // add all Health Circles to list and set each to inactive
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Health"))
        {
            healthObjs.Add(gameObj);
            gameObj.SetActive(false);
        }

        counting = false;

    }

    void Update()
    {
        // if not already counting down, start counting down
        if (!counting)
        {
            StartCoroutine(SpawnHealth());
        }
    }

    // how long to countdown until spawn
    public int GetDuration()
    {
        duration = Random.Range(10, 40);
        return duration;
    }

    // which health icon/location to spawn
    public int GetHealthLocation()
    {
        location = Random.Range(0, healthObjs.Count);
        return location;
    }

    // coroutine for spawning health throughout the game
    IEnumerator SpawnHealth()
    {
        int time = GetDuration();
        counting = true;
        Debug.Log("Time until health spawn: " + time + " seconds!");
        yield return new WaitForSeconds(duration);

        currentIndex = GetHealthLocation();
        if (currentIndex >= 0 && currentIndex < healthObjs.Count)
        {
            healthObjs[currentIndex].SetActive(true);
            Debug.Log("Health No: " + currentIndex + "active!");
        }
    }

    public void HealthCollected()
    {
        healthObjs[currentIndex].SetActive(false); 
        healthObjs.RemoveAt(currentIndex);
        counting = false;
    }

}

