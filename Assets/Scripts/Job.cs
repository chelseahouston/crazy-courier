using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

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
    public bool jobAccepted = false; // if a job has been accepted
    public GameObject generatingJobScreen, jobScreen, acceptedJobScreen, declinedJobScreen; // phone screens
    public TextMeshProUGUI  orderNoDisplay, restaurantDisplay, customerDisplay, payDisplay; // for job found
    public TextMeshProUGUI acceptedOrderNo, acceptedRestaurant, acceptedCustomer, acceptedPay; // for job accepted

    // Start is called before the first frame update
    void Start()
    {
        generatingJobScreen.SetActive(false);
        jobScreen.SetActive(false);
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(false);

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
        DefaultPhoneText();

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
        if (jobOfferOnScreen)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(DeclineJobCoroutine());
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AcceptJob();
            }
        }
        if (jobAccepted)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                DeclineJobCoroutine();
            }
        }
    }

    public void PopulateCustomers()
    {
        customerData = GetComponent<CustomerData>();
        customers.AddRange(customerData.GetCustomers());
    }

    public IEnumerator GeneratingJobCoroutine()
    {
        complete = false; // job not completed yet
        jobAccepted = false; // job not accepted yet
        generatingJobScreen.SetActive(true);
        yield return new WaitForSeconds(Random.Range(1, 5));
        generatingJobScreen.SetActive(false);
        jobScreen.SetActive(true);
        jobOfferOnScreen = true;

    }
    public void GenerateJob()
    {
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(false);
        // coroutine of animation whilst "searching" for a job
        StartCoroutine(GeneratingJobCoroutine());

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
        orderNoDisplay.text += orderNo;
        restaurantDisplay.text += jobRestaurant;
        customerDisplay.text += jobCustomer.GetName() + "<br>" + jobCustomer.GetAddress();
        payDisplay.text += jobPay;

        Debug.Log("Order #" + orderNo + " Pick up from: " + jobRestaurant + " Deliver to: " + jobCustomer.GetName() + " at " + jobCustomer.GetAddress() + " Payment: $" + jobPay);
       
    }

    public void AcceptJob()
    {
        jobAccepted = true;
        Debug.Log("Job Accepted :)");
        jobOfferOnScreen = false;
        jobScreen.SetActive(false);
        acceptedJobScreen.SetActive(true);

        // update TMPro UI to show current order on phone
        acceptedOrderNo.text += orderNo;
        acceptedRestaurant.text += jobRestaurant;
        acceptedCustomer.text += jobCustomer.GetName() + "<br>" + jobCustomer.GetAddress();
        acceptedPay.text += jobPay;

        // set restaurant and customer home game objects for current job
        restaurantGameObject = GameObject.Find(jobRestaurant);
        restaurantGameObject.tag = "Current Restaurant";
        customerHouseGameObject = GameObject.Find(jobCustomer.address);
        customerHouseGameObject.tag = "Current Customer";

        // change UI on phone to say Current Job and details
    }

    public IEnumerator DeclineJobCoroutine()
    {
        jobOfferOnScreen = false;
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(true);
        Debug.Log("Job Declined :(");
        restaurantGameObject = GameObject.Find(jobRestaurant);
        restaurantGameObject.tag = "Untagged";
        customerHouseGameObject = GameObject.Find(jobCustomer.address);
        customerHouseGameObject.tag = "Untagged";
        yield return new WaitForSeconds(1.5f);
        DefaultPhoneText();
        GenerateJob(); // generate new job
    }

    public void CompleteJob()
    {
        // update TMPro text to default
        DefaultPhoneText();
        // update money wallet
        // update jobs completed
        complete = true;



    }

    public void DefaultPhoneText()
    {
        // update TMPro text to default
        orderNoDisplay.text = "Order #";
        restaurantDisplay.text = "Pickup: <br>";
        customerDisplay.text = "Delivery: <br>";
        payDisplay.text = "$";
        acceptedOrderNo.text = "Order #";
        acceptedRestaurant.text = "Pickup: <br>";
        acceptedCustomer.text = "Delivery: <br>";
        acceptedPay.text = "$";
    }
}
