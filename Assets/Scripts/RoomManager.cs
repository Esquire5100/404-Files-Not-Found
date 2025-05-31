using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) //Checks if the entering collider is tagged as Player and isnt a trigger collider
        {
            virtualCam.SetActive(true); //activate the preset camera
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) //Checks if the entering collider is tagged as Player and isnt a trigger collider
        {
            virtualCam.SetActive(false); //deactivate the preset camera
        }
    }
}
