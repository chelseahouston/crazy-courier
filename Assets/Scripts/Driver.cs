using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class Driver : MonoBehaviour
{
    [SerializeField] public float speed; // accelleration
    [SerializeField] public float steerSpeed; // speed of turning L or R
    public int health, maxHealth;
    public bool isDead;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 30;
        speed = 15f;
        steerSpeed = 270f;
        health = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime; // get user L + R / A + D input
        transform.Rotate(0, 0, -steerAmount);

        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime; // get user F + B / W + S input
        transform.Translate(0, moveSpeed, 0);

        if (health <= 0)
        {
            isDead = true;
        }

        healthSlider.value = health;
    }

    public void DecreaseHealth()
    {
        health--;
        Debug.Log("Health = " + health);
    }

}
