using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23

// generates jobs at random

public class Job : MonoBehaviour
{

    public List<string> restaurants; // list of all restaurants
    public List<Customer> customers;  // list of all customers and addresses
    public string jobRestaurant; // name of current job restaurant pickup location
    public Customer jobCustomer; // current job customer
    public int orderNo; // current order number
    public string jobPay; // how much money the job pays
    public CustomerData customerData; // customer data script
    public bool complete; // is current job complete?
    public GameObject restaurantGameObject; // where to pick up current order
    public GameObject customerHouseGameObject; // where to deliver current order
    public bool jobOfferOnScreen = false; // if the potential job is shown on phone, false to start

    // Start is called before the first frame update
    void Start()
    {

        // list of restaurants
        restaurants.AddRange(new List<string> {
            "Sugar Rush!",
            "Slice of Peace",
            "Ramen to the Rescue!",
            "Burger Bites",
            "Pattys Patties",
            "Dough Me a Flavor",
            "Flippin' Off!",
            "Pizza Palace",
            "Baked",
            "Ramen Rhapsody"});

        
        PopulateCustomers();

        // order no starts at 0 -- no orders yet!
        orderNo = 0;
        
        // generate first job!
        GenerateJob();

    }

    // Update is called once per frame
    void Update()
    {
        // if the user completes the job, generate another
        if (complete)
        {
            GenerateJob();
        }
    }

    public void PopulateCustomers()
    {
        customerData = GetComponent<CustomerData>();
        customers.AddRange(customerData.GetCustomers());
    }

    public void GenerateJob()
    {
        complete = false; // job not completed yet
        // coroutine of animation whilst "searching" for a job
        jobOfferOnScreen = true;

        // get random restaurant
        int randomRest = Random.Range(0, restaurants.Count);
        jobRestaurant = restaurants[randomRest];

        // get random customer
        int randomCust = Random.Range(0, customers.Count);
        jobCustomer = customers[randomCust];

        // increase order number and display
        orderNo++;

        // create random number for job pay
        float pay = Random.Range(3.5f, 15.0f);
        jobPay = pay.ToString("#.##");

        // update TMPro UI to show current order on phone
        // remember to add # in front of orderNumber and $ in front of jobPay
        Debug.Log("Order #" + orderNo + " Pick up from: " + jobRestaurant + " Deliver to: " + jobCustomer.GetName() + " at " + jobCustomer.GetAddress() + " Payment: $" + jobPay);
        
        AcceptJob(); // temp 
    }

    public void AcceptJob()
    {
        Debug.Log("Job Accepted :)");
        jobOfferOnScreen = false;
        // set restaurant and customer home game objects for current job
        restaurantGameObject = GameObject.Find(jobRestaurant);
        restaurantGameObject.tag = "Current Restaurant";
        customerHouseGameObject = GameObject.Find(jobCustomer.address);
        customerHouseGameObject.tag = "Current Customer";

        // change UI on phone to say Current Job and details
    }

    public void DeclineJob()
    {
        jobOfferOnScreen = false;
        Debug.Log("Job Declined :(");
        GenerateJob(); // generate new job
    }

    public void CompleteJob()
    {
        // UI update good work etc
        // update money wallet
        // update jobs completed
        complete = true;
    }
}
