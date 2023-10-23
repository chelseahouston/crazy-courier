using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    public Animator animator;
    public TMP_Text returnText;
    public bool animationEnded, textActive;

    void Start()
    {
        ResetAnims();
        StartCoroutine(Animate());
    }

    public void ResetAnims()
    {
        Time.timeScale = 1;
        returnText.gameObject.SetActive(false);
        textActive = false;
        animationEnded = false;
        
    }

    void Update()
    {
        if (animationEnded)
        {
            ShowReturnText();
        }

        if (textActive && Input.anyKeyDown)
        {
            ResetAnims();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ShowReturnText()
    {
        returnText.gameObject.SetActive(true);
        textActive = true;
    }

    public void AnimationEnded()
    {
        animationEnded = true;
        Debug.Log("Animation Ended");
    }

    IEnumerator Animate()
    {
        if (animator != null)
        {
            animator.Play("credits", -1, 0.0f);
        }
        else
        {
            Debug.Log("Can't find animator!");
        }

        Debug.Log("Animation starting");
        yield return new WaitForSeconds(4);

        AnimationEnded();
    }
}
