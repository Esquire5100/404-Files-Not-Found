using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonPress : MonoBehaviour
{
    public Button button;
    public bool uiButtonPressed = false;

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonPressed);
        }
    }

    void OnButtonPressed()
    {
        uiButtonPressed = true;
        // Additional logic
    }
}