using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 10-09-23

public class FollowCam : MonoBehaviour
{
    // Camera to follow player's car...

    [SerializeField] GameObject driver;

    // Update is called once per frame
    void Update()
    {
        transform.position = driver.transform.position + new Vector3 (0, 0, -40);
    }
}
