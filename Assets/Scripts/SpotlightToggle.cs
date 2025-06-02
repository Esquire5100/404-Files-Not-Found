using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject flashlightPrefab; // Reference to the flashlight prefab
    public float interval = 5f;
    public Button toggleButton; // Reference to the UI Button
    private GameObject flashlightInstance; // Instance of the flashlight

    void Start()
    {
        // Add listener to the button to toggle the flashlight
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleFlashlight);
        }
    }

    void ToggleFlashlight()
    {
        if (flashlightInstance == null)
        {
            // Instantiate the flashlight and make it a child of the player
            flashlightInstance = Instantiate(flashlightPrefab, transform.position, Quaternion.identity);
            flashlightInstance.transform.SetParent(transform);
            flashlightInstance.transform.localPosition = new Vector3(0, 0, 1); // Adjust position as needed

            // Set the rotation to match the player's facing direction
            flashlightInstance.transform.right = transform.right;
        }
        else
        {
            // Toggle the flashlight on or off
            Light2D lightComponent = flashlightInstance.GetComponent<Light2D>();
            if (lightComponent != null)
            {
                lightComponent.enabled = !lightComponent.enabled;
            }
        }
    }
}



