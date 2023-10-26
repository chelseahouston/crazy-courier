using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 26-10-23

public class Driver : MonoBehaviour
{
    [SerializeField] public float speed; // acceleration
    [SerializeField] public float steerSpeed, originalSteerSpeed; // speed of turning L or R
    public float slowSpeed, originalSpeed, boostSpeed; // power up/down speeds
    public int health, maxHealth;
    public bool isDead;
    [SerializeField] private Slider healthSlider;
    public TMP_Text powerUI;
    public TMP_Text healthText;

    private Coroutine activePowerUp; // store the active power-up coroutine

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 30;
        speed = 15f;
        slowSpeed = 9f;
        boostSpeed = 30f;
        originalSpeed = 15f;
        steerSpeed = 270f;
        originalSteerSpeed = 270f;
        health = maxHealth;
        isDead = false;
        powerUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime; // get user left + right / A key + D key inputs
        transform.Rotate(0, 0, -steerAmount);

        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime; // get user forward + backward / W key + S key inputs
        transform.Translate(0, moveSpeed, 0);

        healthSlider.value = health;

        healthText.text = health + "/" + maxHealth;

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    public void DecreaseHealth()
    {
        health--;
        Debug.Log("Health = " + health);
    }

    // when collecting health powerup
    public void IncreaseHealth()
    {
        StartCoroutine(AddHealthCoroutine());
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
        steerSpeed = originalSteerSpeed;
        speed = slowSpeed;
        powerUI.color = Color.red;
        powerUI.text = "Slower Speed";

        yield return new WaitForSeconds(8);

        powerUI.text = "";
        speed = originalSpeed;
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
        steerSpeed = originalSteerSpeed;

        powerUI.color = new Color(0.03f, 0.9f, 1.0f, 1.0f);
        powerUI.text = "<i>BOOOOOOST!</i>";

        yield return new WaitForSeconds(2);

        powerUI.text = "";
        speed = originalSpeed;
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
