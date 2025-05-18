using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_Script : MonoBehaviour
{
    public SpriteRenderer sr; //Made a public ref to the sprite renderer component (of the guard/enemy script to change enemy colour when player in range) so that i can manually assign it in unity
    public LayerMask WhoICanSee; //Can change the visible layers in unity

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Do a raycast to know if can directly see the player
            Vector2 dir = other.transform.position - transform.position; //The vector representing the direction of the ray
            float dist = Vector2.Distance(transform.position, other.transform.position); //Max dist to cast the ray
            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, dist, WhoICanSee); //Pt in 2D space where the ray originates

            //If the raycast result == the one entering the cone, it means the player has been seen
            if (result.collider == other)
            {
                sr.color = new Color32(255, 150, 150, 255);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Ensure that the leaving object is the player, otherwise other objs firing this function will affect the line of sight
        if (other.tag == "Player")
        {
            sr.color = new Color32(255, 255, 255, 255);
        }
    }
}
