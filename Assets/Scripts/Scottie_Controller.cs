using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovementscript : MonoBehaviour
{
    public float speed = 5; //public -> can see in unity
    private float movement; //private -> can only be seen/changed/referenced to in the script

    new public Rigidbody2D rigidbody; //make reference to the Rigidbody2D component

    //Ground detection variables
    public Transform groundCheck; //A pt in space to check where the ground is
    public float groundCheckRadius;
    public LayerMask realGround;
    public bool isGrounded;
    private float horizontalInput;

    private Animator myAnim; //Store a ref to animtions to access later

    public Vector3 respawnPosition; //Store a respawn pos the player will go to whenever she dies

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
