using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class SlowDown : MonoBehaviour
{
    public List<GameObject> slowdownObjs; // list of slowdown game objects in scene
    public int duration; // duration to wait before making a slowdown object active
    public int location; // position in list for slowdown location
    public bool counting; // if counting down dont change duration or spawn slowdown icon
    public int currentIndex; // current index of obj

    void Start()
    {
        // always start with empty list
        slowdownObjs.Clear();
        // add all Health Circles to list and set each to inactive
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("SlowDown"))
        {
            slowdownObjs.Add(gameObj);
            gameObj.SetActive(false);
        }

        counting = false;

    }

    void Update()
    {
        // if not already counting down, start counting down
        if (!counting)
        {
            StartCoroutine(SpawnSlowDown());
        }
    }

    // how long to countdown until spawn
    public int GetDuration()
    {
        duration = Random.Range(20, 40);
        return duration;
    }

    // which slowdown icon/location to spawn
    public int GetLocation()
    {
        location = Random.Range(0, slowdownObjs.Count);
        return location;
    }

    // coroutine for spawning slowdown throughout the game
    IEnumerator SpawnSlowDown()
    {
        int time = GetDuration();
        counting = true;
        Debug.Log("Time until health spawn: " + time + " seconds!");
        yield return new WaitForSeconds(duration);

        currentIndex = GetLocation();
        if (currentIndex >= 0 && currentIndex < slowdownObjs.Count)
        {
            slowdownObjs[currentIndex].SetActive(true);
            Debug.Log("SlowDown No: " + currentIndex + " active!");
        }
    }

    public void SlowDownCollected()
    {
        slowdownObjs[currentIndex].SetActive(false);
        slowdownObjs.RemoveAt(currentIndex);
        counting = false;
    }
}

