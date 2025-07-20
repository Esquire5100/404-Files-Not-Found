using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityButton = UnityEngine.UI.Button;

public class HackableObject : MonoBehaviour
{
    public UIButtonPress uiButtonPress;

    private bool isHacked = false;

    public void Hack()
    {
        if (isHacked) return; 

        if (uiButtonPress != null && uiButtonPress.uiButtonPressed)
        {
            // Perform hack logic
            isHacked = true;
            Debug.Log($"{gameObject.name} has been hacked!");


            this.enabled = false;
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

        }
    }


}


