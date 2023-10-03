using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author: chelsea houston
// @date-last-update-dd-mm-yy: 03-10-23

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
