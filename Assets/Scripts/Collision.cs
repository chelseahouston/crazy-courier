using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 11-09-23
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

    }

    private void OnTriggerEnter2D(Collider2D thing)
    {
        
        // if the restaurant is correct
        if (thing.tag == "Current Restaurant")
        {
            // pickup order and set pickedup bool to true
            pickedup = true;
            Debug.Log("Picked up Order");
            // and remove waypoint from restaurant to customer house
            // set restaurantGameObject tag to default untagged
            thing.tag = "Untagged";

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
            // deliver order
            job.CompleteJob();

            // set customerGameObject tag to default untagged
            thing.tag = "Untagged";

        }
        else
        {
            // else do nothing
        }
    }



}
