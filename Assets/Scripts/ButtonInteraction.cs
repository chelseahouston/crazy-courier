using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public void OnHover()
    {
        // hand cursor

        // change color of text
    }

    public void OnMouseExit()
    {
        // arrow cursor

        // revert color of text
    }
    
    public void OnPress() 
    {
        // selected sound
        AudioManager.Instance.PlaySFX("NewJob");
    }


}
