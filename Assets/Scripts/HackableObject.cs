using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityButton = UnityEngine.UI.Button;

public class HackableObject : MonoBehaviour
{
    public UIButtonPress uiButtonPress;

    public void Hack()
    {
        if (uiButtonPress != null && uiButtonPress.uiButtonPressed)
        {
            // Perform hack logic
        }
    }
}


