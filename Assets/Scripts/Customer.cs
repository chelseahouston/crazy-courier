using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23

[Serializable]
public class Customer
{
    public string customerName;
    public string address;
 


    public Customer(string name, string address)
    {
        customerName = name;
        this.address = address;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public string GetName()
    {
        return customerName;
    }

    public string GetAddress()
    {
        return address;
    }

}

