using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVScript : MonoBehaviour
{
    public LayerMask WhoICanSee; //Can change the visible layers in unity
    public SecurityCamera SecCam; //Ref to the Security Camera script

    public GameObject guard; //Ref to the guard prefab that will be spawned
    public Transform[] spawnPoints; //Left spawn point for the guards (placed outside the camera view)
    public Transform player; //Ref to the player so guards know where to chase
    public List<GameObject> activeGuards = new List<GameObject>(); //Tracks currently spawned guards

    // Start is called before the first frame update
    void Start()
    {
        SecCam = GetComponentInParent<SecurityCamera>(); //Get access to the Security Camera script
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //Only if the obj detected is the player...
        {
            //Do a vector in the direction of the player to know if can directly see it
            Vector2 dir = other.transform.position - transform.position;
            float dist = Vector2.Distance(transform.position, other.transform.position); //Max dist to cast the ray
            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, dist, WhoICanSee); //Cast a ray toward the player within the defined layer mask (assigned in unity)

            //If the raycast result == the player (player has been directly seen)...
            if (result.collider != null && result.collider.CompareTag("Player")) //The && is a double check to make sure it is specifically the player that is detected and not the ground or bg
            {
                //...then sound an alarm and summon guards
                // Only spawn guards if none are active yet
                if (activeGuards.Count == 0)
                {
                    for (int i = 0; i < spawnPoints.Length; i++)
                    {
                        if (i >= 2) break; // Make sure we only try 2 spawn points max

                        GameObject newGuard = Instantiate(guard, spawnPoints[i].position, Quaternion.identity);

                        if (newGuard.GetComponent<ChasingGuard>() != null)
                        {
                            newGuard.GetComponent<ChasingGuard>().StartChase(player);
                        }
                        else
                        {
                            Debug.LogWarning("Guard prefab missing Guard_Controller script!");
                        }

                        activeGuards.Add(newGuard); // Add to list
                        Debug.Log("Guard spawned from spawn point " + i);
                    }
                }
            }
            else
            {
                // Despawn guards when player is no longer visible
                foreach (GameObject g in activeGuards)
                {
                    if (g != null)
                    {
                        Destroy(g);
                    }
                }

                activeGuards.Clear();
            }
        }

        if (other.CompareTag("Hidden"))
        {
            foreach (GameObject g in activeGuards)
            {
                if (g != null)
                {
                    Destroy(g);
                }
            }

            activeGuards.Clear();
        }
    }
}