using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);  //Attaches player to platform by making it a child of platform
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);  //Detaches player from platform by removing it as a child of platform
        }
    }

    private IEnumerator DelayedUnparent(Transform child)
    {
        yield return null;
        child.SetParent(null);
    }
}
