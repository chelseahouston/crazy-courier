using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.Windows;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

// generates jobs at random and deals with money and navigation circles for each job

public class Job : MonoBehaviour
{
    public List<string> restaurants; // list of all restaurants
    public List<Customer> customers;  // list of all customers and addresses
    public string jobRestaurant; // name of current job restaurant pickup location
    public Customer jobCustomer; // current job customer
    public int orderNo; // current order number
    private float pay; // how much money the job pays
    public string jobPay; // how much money the job pays to string
    public CustomerData customerData; // customer data script
    public bool complete; // is current job complete?
    public GameObject restaurantGameObject; // where to pick up current order
    public GameObject restaurantLocationCircle; // restaurant circle target
    public GameObject customerHouseGameObject; // where to deliver current order
    public GameObject customerLocationCircle; // customer circle target
    private bool jobOfferOnScreen = false; // if the potential job is shown on phone, false to start
    private bool jobAccepted = false; // if a job has been accepted
    public GameObject generatingJobScreen, jobScreen, acceptedJobScreen, declinedJobScreen, completedJobScreen; // phone screens
    public TextMeshProUGUI  orderNoDisplay, restaurantDisplay, customerDisplay, payDisplay; // for job found
    public TextMeshProUGUI acceptedOrderNo, acceptedRestaurant, acceptedCustomer, acceptedPay, cancelOption; // for job accepted
    public GameObject cancelX; // for enable/disable cancel icon
    public TextMeshProUGUI completedPayment; // pay from job completed
    public TextMeshProUGUI money; // total pay text
    private float currentMoney; // total pay so far
    public TextMeshProUGUI totalJobsText; // total jobs text
    private int totalJobs; // total jobs completed
    public ScoreSaving saveScores; // for saving daily scores
    public Pause pause; // pause script
    public EndDay endDayPanel; // end day panel script
    public CarDamaged carDestroyedPanel; // end day panel script for car too damaged
    public Collision collision; // collision script

    // Start is called before the first frame update
    void Start()
    {
        currentMoney = 0.00f;
        money.text = "$ " + currentMoney + "";
        totalJobs = 0;
        totalJobsText.text = "Jobs Completed: " + totalJobs;

        GameObject scoreSavingObject = GameObject.Find("ScoreSaving");
        if (scoreSavingObject != null)
        {
            saveScores = scoreSavingObject.GetComponent<ScoreSaving>();
        }
        else
        {
            Debug.LogError("ScoreSaving GameObject not found!");
        }

        // Set all Target Circles to inactive
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("TargetCircle"))
        {
            if (gameObj.name == "TargetLocationCircle")
{
                gameObj.SetActive(false);
            }
        }


        generatingJobScreen.SetActive(false);
        jobScreen.SetActive(false);
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(false);
        completedJobScreen.SetActive(false);

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
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                pause.PauseGame();
            }
            else
            {
                pause.Resume();
            }
        }

        totalJobsText.text = "Jobs Completed: " + totalJobs;
        // if the user completes the job, generate another
        if (complete)
        {
            GenerateJob();
        }

        if (jobOfferOnScreen && !pause.isPaused)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(DeclineJobCoroutine());
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
            {
                AcceptJob();
            }
        }

        if (jobAccepted && !pause.isPaused && !collision.pickedup) // cancel job if already accepted, game isn't paused, and order not already picked up from restaurant
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(DeclineJobCoroutine());
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
        

        yield return new WaitForSeconds(Random.Range(1f, 5f));

        // get random restaurant
        int randomRest = Random.Range(0, restaurants.Count);
        jobRestaurant = restaurants[randomRest];

        // get random customer
        int randomCust = Random.Range(0, customers.Count);
        jobCustomer = customers[randomCust];

        // increase order number and display
        orderNo++;

        // create random number for job pay
        pay = Random.Range(3.5f, 15.0f);
        jobPay = pay.ToString("#.##");

        // update TMPro UI to show current order on phone
        orderNoDisplay.text += orderNo;
        restaurantDisplay.text += jobRestaurant;
        customerDisplay.text += jobCustomer.GetName() + "<br>" + jobCustomer.GetAddress();
        payDisplay.text += jobPay;

        generatingJobScreen.SetActive(false);
        Debug.Log("Order #" + orderNo + " Pick up from: " + jobRestaurant + " Deliver to: " + jobCustomer.GetName() + " at " + jobCustomer.GetAddress() + " Payment: $" + jobPay);
        
        jobScreen.SetActive(true);
        jobOfferOnScreen = true;
        AudioManager.Instance.PlaySFX("NewJob");

        Debug.Log("JobAccepted = " + jobAccepted);
    }

    public void GenerateJob()
    {
        DefaultPhoneText();
        jobScreen.SetActive(false);
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(false);
        completedJobScreen.SetActive(false);

        // coroutine of animation whilst "searching" for a job
        StartCoroutine(GeneratingJobCoroutine());

    }

    public void AcceptJob()
    {
        jobAccepted = true;
        acceptedRestaurant.enabled = true;
        cancelOption.enabled = true;
        cancelX.SetActive(true);
        Debug.Log("Job Accepted :)");
        AudioManager.Instance.PlaySFX("NewJob");
        jobOfferOnScreen = false;
        jobScreen.SetActive(false);
        acceptedJobScreen.SetActive(true);

        // update TMPro UI to show current order on phone
        acceptedOrderNo.text += orderNo;
        Debug.Log("restaurant = " + jobRestaurant);
        acceptedRestaurant = acceptedRestaurant.GetComponent<TextMeshProUGUI>();
        acceptedRestaurant.text = "Pickup: <br>"+jobRestaurant;
        restaurantDisplay.text = "Pickup: <br>" + jobRestaurant;
        acceptedCustomer.text += jobCustomer.GetName() + "<br>" + jobCustomer.GetAddress();
        acceptedPay.text += jobPay;
        completedPayment.text += jobPay;
        acceptedCustomer.enabled = false; ; // just show pickup address first

        // set restaurant and customer home game objects for current job

        restaurantGameObject = GameObject.Find(jobRestaurant); // get restaurant location
        restaurantLocationCircle = restaurantGameObject.transform.Find("TargetLocationCircle").gameObject; // set location marker circle
        restaurantLocationCircle.tag = "Current Restaurant"; // set circle tag as trigger
        restaurantLocationCircle.SetActive(true); // target location set

        Debug.Log("JobAccepted = " + jobAccepted);
    }

    public void ShowCustomerAddress()
    {
        acceptedRestaurant = acceptedRestaurant.GetComponent<TextMeshProUGUI>();
        acceptedCustomer = acceptedCustomer.GetComponent<TextMeshProUGUI>();
        acceptedRestaurant.enabled = false; // when order picked up, dont show pickup address
        acceptedCustomer.enabled = true;  // show customer address
        cancelOption.enabled = false; // don't show option to cancel when order picked up
        cancelX.SetActive(false); // don't show option to cancel when order picked up

        restaurantGameObject = GameObject.Find(jobRestaurant); // find restaurant address to get circle marker
        restaurantLocationCircle = restaurantGameObject.transform.Find("TargetLocationCircle").gameObject; // get marker
        restaurantLocationCircle.tag = "TargetCircle"; // reset tag
        restaurantLocationCircle.SetActive(false); // set marker to inactive

        customerHouseGameObject = GameObject.Find(jobCustomer.address); // get customer address
        Debug.Log("Customer Address: " +jobCustomer.address);
        customerLocationCircle = customerHouseGameObject.transform.Find("TargetLocationCircle").gameObject; // get address location marker circle
        customerLocationCircle.tag = "Current Customer"; // set circle tag as trigger
        customerLocationCircle.SetActive(true); // target location set

    }

    public IEnumerator DeclineJobCoroutine()
    {
        jobOfferOnScreen = false;
        acceptedJobScreen.SetActive(false);
        declinedJobScreen.SetActive(true);
        Debug.Log("Job Declined :(");
        restaurantGameObject = GameObject.Find(jobRestaurant);
        restaurantLocationCircle = restaurantGameObject.transform.Find("TargetLocationCircle").gameObject;
        restaurantLocationCircle.tag = "TargetCircle"; // reset in case job already in progress
        restaurantLocationCircle.SetActive(false);

        // add option/screen to only allow to decline job before picking up order - otherwise driver gets free food! lol
        customerHouseGameObject = GameObject.Find(jobCustomer.address);
        Debug.Log("Customer Location: " + jobCustomer.address);
        customerLocationCircle = customerHouseGameObject.transform.Find("TargetLocationCircle").gameObject;
        customerLocationCircle.tag = "TargetCircle";
        customerLocationCircle.SetActive(false);

        DefaultPhoneText();

        yield return new WaitForSeconds(1.5f);


        GenerateJob(); // generate new job
    }

    public void CompleteJob()
    {
        StartCoroutine(CompleteJobCoroutine());
    }

    public IEnumerator CompleteJobCoroutine()
    {
        acceptedJobScreen.SetActive(false);
        completedJobScreen.SetActive(true);
        currentMoney += pay;
        string moneyString = currentMoney.ToString("#.##");
        money.text = "$ " + moneyString + "";
        AudioManager.Instance.PlaySFX("DropOff");
        customerLocationCircle.SetActive(false);
        totalJobs = totalJobs + 1;
        yield return new WaitForSeconds(1.0f);
        // update TMPro text to default
        DefaultPhoneText();
        // update money wallet
        // update jobs completed
        complete = true;
        jobAccepted = false;

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
        completedPayment.text = "$";
    }

    public void EndOfDay()
    {
        if (totalJobs == 0)
        {
            currentMoney = 0.00f;
        }

        saveScores.UpdateDailyScores(currentMoney, totalJobs);
        endDayPanel.SetEodActive();
    }

    public void CarDestroyed()
    {
        if (totalJobs == 0)
        {
            currentMoney = 0.00f;
        }

        saveScores.UpdateDailyScores(currentMoney, totalJobs);
        carDestroyedPanel.SetCarDestroyedActive();
    }

    public string getTotalMoney()
    {
        string money;
        if (currentMoney == 0.00f)
        { 
            money = "0.00"; 
        }
        else {
            money = currentMoney.ToString("#.##");
        }
        return money;
    }

    public int getTotalJobs()
    {
        return totalJobs;
    }

    public void OnApplicationQuit()
    {
        // reset tags etc...

        restaurantGameObject = GameObject.Find(jobRestaurant);
        if (restaurantGameObject != null)
        {
            restaurantLocationCircle = restaurantGameObject.transform.Find("TargetLocationCircle").gameObject;
            if (restaurantLocationCircle != null)
            {
                restaurantLocationCircle.tag = "TargetCircle";
            }
        }

        customerHouseGameObject = GameObject.Find(jobCustomer.address);
        if (customerHouseGameObject != null)
        {
            customerLocationCircle = customerHouseGameObject.transform.Find("TargetLocationCircle").gameObject;
            if (customerLocationCircle != null)
            {
                customerLocationCircle.tag = "TargetCircle";
            }
        }

    }
}
