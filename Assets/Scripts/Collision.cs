using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 03-10-23

public class Collision : MonoBehaviour
{
    public bool pickedup = false; // order not picked up at start
    public Job job; // job script
    public GameObject data;

    private void Start()
    {
        data = GameObject.Find("Data");
        job = data.GetComponent<Job>();
        
    }


    private void OnCollisionEnter2D(Collision2D thing)
    {
        AudioManager.Instance.PlaySFX("Crash");
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
        else
        {
            // else do nothing
        }

        // if the customer house is correct and pickedup is true
        if (thing.tag == "Current Customer" && pickedup)
        {
            Debug.Log("Delivered to Customer!");
            AudioManager.Instance.PlaySFX("Dropoff");
            // deliver order
            job.CompleteJob();
        
        }
        else
        {
            // else do nothing
        }
    }



}
