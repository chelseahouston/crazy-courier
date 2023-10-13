using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 13-10-23

public class HowToPlayScreen : MonoBehaviour
{
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject howToPlayScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Back()
    {
        howToPlayScreen.SetActive(false);
       
    }
}
