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
        if (other.CompareTag("Player"))
        {
            // Check if the player is hiding
            bool isPlayerHiding = other.GetComponent<Scottie_Controller>().IsHiding;

            // If the player is hiding, ignore detection
            if (isPlayerHiding)
            {
                sr.color = new Color32(255, 255, 255, 255); // Reset color to normal
                guard.StopChase(); // Stop chasing the player
                return;
            }

            // Perform raycast to detect player
            Vector2 dir = other.transform.position - transform.position;
            float dist = Vector2.Distance(transform.position, other.transform.position);
            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, dist, WhoICanSee);

            if (result.collider != null && result.collider.CompareTag("Player"))
            {
                sr.color = new Color32(255, 150, 150, 255); // Change color to indicate detection
                guard.StartChase(other.transform); // Start chasing the player
            }
            else
            {
                sr.color = new Color32(255, 255, 255, 255); // Reset color to normal
                guard.StopChase(); // Stop chasing the player
            }
        }
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
