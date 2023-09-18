using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOptionsMenu : MonoBehaviour
{

    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
    }
}
