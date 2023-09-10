using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 11-09-23
public class CustomerData : MonoBehaviour
{

    public static List<Customer> customers = new List<Customer>();

    // Start is called before the first frame update
    void Awake()
    {
        // create list of all customers and their address
        // multiple people living in same address
        customers.Add(new Customer("Sarah Thompson", "3 Maple Street"));
        customers.Add(new Customer("James Thompson", "2 Maple Street"));
        customers.Add(new Customer("Mason White", "2 Oak Drive"));
        customers.Add(new Customer("Josh White", "2 Oak Drive"));
        customers.Add(new Customer("Ava Taylor", "5 Maple Street"));
        customers.Add(new Customer("Olivia Martinez", "10 Walnut Street"));
        customers.Add(new Customer("Sandra Martinez", "10 Walnut Street"));
        customers.Add(new Customer("Sofia Wilson", "1 Apple Lane"));
        customers.Add(new Customer("Jack Wilson", "1 Apple Lane"));
        customers.Add(new Customer("Chloe Wilson", "1 Apple Lane"));
        customers.Add(new Customer("Amelia Smith", "4 Oak Drive"));
        customers.Add(new Customer("Emma Johnson", "3 Forest Way"));
        customers.Add(new Customer("Mei Chen", "3 Forest Way"));
        customers.Add(new Customer("Jamal Ali", "7 Pine Drive"));
        customers.Add(new Customer("Priya Gupta", "7 Pine Drive"));
        customers.Add(new Customer("Mohammad Hassan", "9 Pine Drive\""));
        customers.Add(new Customer("Julie Marsh", "11 Pine Drive"));
        customers.Add(new Customer("Bernard Marsh", "11 Pine Drive"));
        customers.Add(new Customer("Craig Jones", "12 Orange Grove"));
        customers.Add(new Customer("Mark Brown", "9 Hickory Road"));
        customers.Add(new Customer("Cassie Brown", "9 Hickory Road"));
        customers.Add(new Customer("Claire Parker", "3 Willow Way"));
        customers.Add(new Customer("Sam Parker", "3 Willow Way"));
        customers.Add(new Customer("Lucy Bennet", "7 Willow Way"));
        customers.Add(new Customer("Matthew Jackson", "7 Willow Way"));
    }

    public List<Customer> GetCustomers()
    {
        return customers;
    }

}
