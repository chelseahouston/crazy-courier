using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23

// generates jobs at random

public class JobGenerator : MonoBehaviour
{

    public List<string> restaurants = new List<string>(); // list of all restaurants
    public List<Customer> customers = new List<Customer>(); // list of all customers and addresses
    public string jobRestaurant; // name of current job restaurant pickup location
    public Customer jobCustomer; // current job customer
    public int orderNo; // current order number
    public Customer cust; // Customer script
    public bool complete; // is current job complete?
    public GameObject restaurantGameObject; // where to pick up current order
    public GameObject customerHouseGameObject; // where to deliver current order

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

        // list of customers
        cust = GetComponent<Customer>();
        PopulateCustomers(cust.GetCustomers());

        // order no starts at 1 -- first order
        orderNo = 1;

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

    public void PopulateCustomers(List<Customer> customers)
    {
        this.customers = customers;
    }

    public void GenerateJob()
    {
        complete = false; // job not completed yet
        // coroutine of animation whilst "searching" for a job
        int randomRest = Random.Range(0, restaurants.Count);
        jobRestaurant = restaurants[randomRest];
        int randomCust = Random.Range(0, customers.Count);
        jobCustomer = customers[randomCust];

        restaurantGameObject = GameObject.Find(jobRestaurant);
        customerHouseGameObject = GameObject.Find(jobCustomer.address);

        // update TMPro UI to show current order on phone

    }
}
