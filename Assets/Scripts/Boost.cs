using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class Boost : MonoBehaviour
{
    public List<GameObject> boostObjs; // list of game objects in scene
    public int duration; // duration to wait before making an object active
    public int location; // position in list for location
    public bool counting; // if counting down dont change duration or spawn icon
    public int currentIndex; // current index of obj

    void Start()
    {
        // always start with empty list
        boostObjs.Clear();
        // add all Health Circles to list and set each to inactive
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Boost"))
        {
            boostObjs.Add(gameObj);
            gameObj.SetActive(false);
        }

        counting = false;

    }

    void Update()
    {
        // if not already counting down, start counting down
        if (!counting)
        {
            StartCoroutine(Spawn());
        }
    }

    // how long to countdown until spawn
    public int GetDuration()
    {
        duration = Random.Range(5, 20);
        return duration;
    }

    // which slowdown icon/location to spawn
    public int GetLocation()
    {
        location = Random.Range(0, boostObjs.Count);
        return location;
    }

    // coroutine for spawning throughout the game
    IEnumerator Spawn()
    {
        int time = GetDuration();
        counting = true;
        Debug.Log("Time until beer spawn: " + time + " seconds!");
        yield return new WaitForSeconds(duration);

        currentIndex = GetLocation();
        if (currentIndex >= 0 && currentIndex < boostObjs.Count)
        {
            boostObjs[currentIndex].SetActive(true);
            Debug.Log("Beer No: " + currentIndex + " active!");
        }
    }

    public void Collected()
    {
        boostObjs[currentIndex].SetActive(false);
        boostObjs.RemoveAt(currentIndex);
        counting = false;
    }
}

