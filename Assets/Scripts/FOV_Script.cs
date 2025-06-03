using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_Script : MonoBehaviour
{
    public SpriteRenderer sr; //Made a public ref to the sprite renderer component (of the guard/enemy script to change enemy colour when player in range) so that i can manually assign it in unity
    public LayerMask WhoICanSee; //Can change the visible layers in unity
    public Guard_Controller guard; //Ref to the Guard Controller script

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        guard = GetComponentInParent<Guard_Controller>(); //Get access to the guard script
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
                //...then change colour to a light red and start chasing the player
                sr.color = new Color32(255, 150, 150, 255);
                guard.StartChase(other.transform);
            }

            //If player is not detected (blocked/not visible) then...
            else
            {
                sr.color = new Color32(255, 255, 255, 255); //Reset the colours back to normal
                guard.StopChase(); //Stop chasing the player
            }
        }

        /*if (other.CompareTag("Hidden"))
        {
            sr.color = new Color32(255, 255, 255, 255); //Reset the colours back to normal
            guard.StopChase(); //Stop chasing the player
        }*/
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Ensure that the leaving object is the player (otherwise any other objs will affect the line of sight)
        if (other.tag == "Player")
        {
            //Revert to normal colours and stop chasing the player
            sr.color = new Color32(255, 255, 255, 255);
            guard.StopChase();
        }
    }
}
