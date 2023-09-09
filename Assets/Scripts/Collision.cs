using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23
public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger!");
    }



}
