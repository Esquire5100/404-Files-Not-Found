using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingZone : MonoBehaviour
{
    public int zoneIndex;
    public SpriteRenderer targetRenderer;  // Assign in Inspector: the object that should disappear

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Scottie_Controller>().EnterHideZone(zoneIndex);          // Tell player it's in this zone
            if (targetRenderer != null)                                                 // Make the object visually disappear
                targetRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Scottie_Controller>().ExitHideZone(zoneIndex);            // Notify player that it's exiting the zone
            if (targetRenderer != null)                                                  // Restore the object's visibility
                targetRenderer.enabled = true;
        }
    }
}

