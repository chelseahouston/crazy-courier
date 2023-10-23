using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class Credits : MonoBehaviour
{
    public Animator animator;
    public TMP_Text returnText;
    public bool animationEnded, textActive;

    void Start()
    {
        returnText.gameObject.SetActive(false);
        textActive = false;

        StartCoroutine(Animate());
    }

    void Update()
    {
        if (animationEnded)
        {
            ShowReturnText();
        }

        if (textActive && Input.anyKeyDown)
        {
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
    }

    IEnumerator Animate()
    {
        if (animator != null)
        {
            animator.Play("credits");
            animationEnded = false;
        }
        yield return new WaitForSeconds(4);

        AnimationEnded();
    }


}
