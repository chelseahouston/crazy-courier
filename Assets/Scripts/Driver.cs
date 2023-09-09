using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23

public class Driver : MonoBehaviour
{
    [SerializeField] public float speed; // accelleration
    [SerializeField] public float steerSpeed; // speed of turning L or R

    // Start is called before the first frame update
    void Start()
    {
        speed = 15f;
        steerSpeed = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime; // get user L + R / A + D input
        transform.Rotate(0, 0, -steerAmount);

        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime; // get user F + B / W + S input
        transform.Translate(0, moveSpeed, 0);
    }

}
