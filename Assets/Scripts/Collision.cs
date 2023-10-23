using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class Collision : MonoBehaviour
{
    public bool pickedup = false; // order not picked up at start
    public Job job; // job script
    public GameObject data;
    public Driver driver;
    public Health health;
    public SlowDown slowdown;
    public Boost boost;
    public Beer beer;

    private void Start()
    {
        data = GameObject.Find("Data");
        job = data.GetComponent<Job>();
        
    }


    private void OnCollisionEnter2D(Collision2D thing)
    {
        AudioManager.Instance.PlaySFX("Crash");
        driver.DecreaseHealth();
    }

    private void OnTriggerEnter2D(Collider2D thing)
    {
        
        // if the restaurant is correct
        if (thing.tag == "Current Restaurant")
        {
            // pickup order and set pickedup bool to true
            pickedup = true;
            AudioManager.Instance.PlaySFX("Collect");
            Debug.Log("Picked up Order");
            // and remove waypoint from restaurant to customer house
            job.ShowCustomerAddress();
        }

        // if the customer house is correct and pickedup is true
        if (thing.tag == "Current Customer" && pickedup)
        {
            Debug.Log("Delivered to Customer!");
            AudioManager.Instance.PlaySFX("DropOff");
            // deliver order
            pickedup = false;
            job.CompleteJob();
        
        }

        // if the driver collects a health powerup
        if (thing.tag == "Health")
        {
            driver.IncreaseHealth();
            Debug.Log("Yay more health! Health = " + driver.health + "!") ;
            AudioManager.Instance.PlaySFX("Collect");
            health.HealthCollected();
        }

        // if the driver collects a slowdown powerdown :(
        if (thing.tag == "SlowDown")
        {
            Debug.Log("Oh No, less speed for 8 seconds!");
            driver.SlowDown();
            AudioManager.Instance.PlaySFX("Collect");
            slowdown.SlowDownCollected();
        }

        // if the driver collects a boost! :D
        if (thing.tag == "Boost")
        {
            Debug.Log("BOOOOOOOST");
            driver.Boost();
            AudioManager.Instance.PlaySFX("Collect");
            boost.Collected();
        }


        // if the driver collects a beer D:
        if (thing.tag == "Beer")
        {
            Debug.Log("hic-cup!");
            driver.Drink();
            AudioManager.Instance.PlaySFX("Collect");
            beer.Collected();
        }

    }



}
