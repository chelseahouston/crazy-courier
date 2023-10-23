using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 23-10-23

public class Driver : MonoBehaviour
{
    [SerializeField] public float speed; // acceleration
    [SerializeField] public float steerSpeed; // speed of turning L or R
    public float slowSpeed, regularSpeed, boostSpeed; // power up/down speeds
    public int health, maxHealth;
    public bool isDead;
    [SerializeField] private Slider healthSlider;
    public TMP_Text powerUI;

    private Coroutine activePowerUp; // store the active power-up coroutine

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 30;
        speed = 15f;
        slowSpeed = 9f;
        boostSpeed = 30f;
        regularSpeed = 15f;
        steerSpeed = 270f;
        health = maxHealth;
        isDead = false;
        powerUI.text = "";
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

    // when collecting health powerup
    public void IncreaseHealth()
    {
        if (activePowerUp != null)
        {
            // Cancel the previous power-up
            StopCoroutine(activePowerUp);
        }
        activePowerUp = StartCoroutine(AddHealthCoroutine());
    }

    IEnumerator AddHealthCoroutine() {
        health = health + 5;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        powerUI.color = Color.green;
        powerUI.text = "+Health";

        yield return new WaitForSeconds(1.5f);

        powerUI.text = "";

    }

    // when collected slowdown powerdown :(
    public void SlowDown()
    {
        if (activePowerUp != null)
        {
            // Cancel the previous power-up
            StopCoroutine(activePowerUp);
        }
        activePowerUp = StartCoroutine(SlowerCoroutine());
    }

    IEnumerator SlowerCoroutine()
    {
        speed = slowSpeed;
        powerUI.color = Color.red;
        powerUI.text = "Slower Speed";

        yield return new WaitForSeconds(8);

        powerUI.text = "";
        speed = regularSpeed;
    }

    // when collected boost :D
    public void Boost()
    {
        if (activePowerUp != null)
        {
            // Cancel the previous power-up
            StopCoroutine(activePowerUp);
        }
        activePowerUp = StartCoroutine(BoostCoroutine());
    }

    IEnumerator BoostCoroutine()
    {
        speed = boostSpeed;

        powerUI.color = Color.blue;
        powerUI.text = "BOOOOOOST!";

        yield return new WaitForSeconds(2);

        powerUI.text = "";
        speed = regularSpeed;
    }

    // when collected beer
    public void Drink()
    {
        if (activePowerUp != null)
        {
            // Cancel the previous power-up
            StopCoroutine(activePowerUp);
        }
        activePowerUp = StartCoroutine(BeerCoroutine());
    }

    IEnumerator BeerCoroutine()
    {
        // original values
        float originalSteerSpeed = steerSpeed;
        float originalSpeed = regularSpeed;

        // invert the controls for 8 secs
        steerSpeed = -originalSteerSpeed;
        speed = -originalSpeed;

        powerUI.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        powerUI.text = "Drunnkkedd... hiccup!";

        yield return new WaitForSeconds(8);

        // restore the original values
        steerSpeed = originalSteerSpeed;
        speed = originalSpeed;
        powerUI.text = "";
    }


}
