using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scottie_Controller : MonoBehaviour
{
    public float MoveSpeed = 5f;                                                 //how fast the character moves

    private Rigidbody2D rb;                                                      //make a ref to the rigidbody2D component
    private SpriteRenderer sr;
    private float movement;

    private bool isHidden = false;                                               //is 'false' by default because we want the default tag to be "Player"

    private Animator myAnim;                                                     //Store a ref to animtions to access later

    AudioManager audioManager;                                                   //reference the Audio Manager Script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                        //Get easy access to the Rigidbody2D component
        sr = GetComponent<SpriteRenderer>();                                     //Get easy access to the SpriteRenderer component
        myAnim = GetComponent<Animator>();                                       //Get easy access to the Animator component
    }

    void Update()
    {
        /*Get horizontal input (-1 for left, 1 for right, 0 for none)
        movement = Input.GetAxisRaw("Horizontal");

        //For making sure the character faces the direction it's moving in
        if (movement < 0)
        {
            //moving left -> sprite flips to the left
            sr.flipX = true;
        }

        if (movement > 0)
        {
            //moving right -> sprite doesnt flip
            sr.flipX = false;
        }

        //Toggle between "Player" and "Hidden" Tags in unity (so that the enemies can't detect the player)
        if (Input.GetKeyDown("e"))
        {
            isHidden = !isHidden; //Changes 'isHidden' to true
            gameObject.tag = isHidden ? "Hidden" : "Player";                    //The "?" means "value if true" : "value if false". It's cleaner than a lot of if-else statements
        }*/

        //Setting up Parameters in the Animator
        myAnim.SetFloat("MoveSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    void FixedUpdate()
    {
        // Move the character left or right
        //rb.velocity = new Vector2(movement * MoveSpeed, rb.velocity.y);
    }

    //Audio Scripts
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //Add "audioManager.PlaySFX(audioManager.XX);" to movements/hacking etc
    }

    public void Move(float dir)
    {
        //Get horizontal input (-1 for left, 1 for right, 0 for none)
        //movement = Input.GetAxisRaw("Horizontal");

        //For making sure the character faces the direction it's moving in
        if (dir > 0)
        {
            //moving left -> sprite flips to the left
            //sr.flipX = true;
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir < 0)
        {
            rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        /*if (dir > 0)
        {
            //moving right -> sprite doesnt flip
            sr.flipX = false;
        }*/
    }
    
}
