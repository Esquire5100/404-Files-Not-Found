using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftFloorSensor : MonoBehaviour
{
    public Collider2D floorGapCollider;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lift"))
        {
            // Lift is at 2nd floor, hide the floor collider
            if (floorGapCollider != null)
                floorGapCollider.enabled = false;
            // Debug.Log("Lift at 2nd floor — gap hidden");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Lift"))
        {
            // Lift left 2nd floor, show the floor collider
            if (floorGapCollider != null)
                floorGapCollider.enabled = true;
            // Debug.Log("Lift left 2nd floor — gap revealed");
        }
    }
}
