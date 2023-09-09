using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23
public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the restaurant is correct
        // pickup order and set pickedup bool to true
        // and remove waypoint from restaurant to customer house
        // set restaurantGameObject to null?
        // else do nothing

        // if the customer house is correct and pickedup is true
        // deliver order
        // set customerGameObject to null?
        // else do nothing
    }



}
