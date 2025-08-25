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
            var player = other.GetComponent<Scottie_Controller>();
            player.EnterHideZone(zoneIndex, this);


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Scottie_Controller>();
            player.ExitHideZone(zoneIndex, this);
        }
    }
}

