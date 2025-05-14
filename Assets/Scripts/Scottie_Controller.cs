using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scottie_Controller : MonoBehaviour
{
    public float speed = 5; //public -> can see in unity
    private float movement; //private -> can only be seen/changed/referenced to in the script
    private float horizontalInput;

    new public Rigidbody2D rigidbody; //make reference to the Rigidbody2D component in unity

    private Animator myAnim; //Store a ref to animtions to access later

    public Vector3 respawnPosition; //Store a respawn pos the player will go to whenever she dies

    //public LevelManager LvlManager; //Make a ref to lvlmanager script

    public bool canMove = true; //When game is paused, player cannot move

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //Checks the obj "rigidbody" is tagged to for a component called "Rigidbody2D". In this case, it's the player because we are currently in the player script.
        //i.e. GetComponent can only be used within the same script/current game obj
        myAnim = GetComponent<Animator>();

        respawnPosition = transform.position; //When game starts, respawn pos = current player pos

        //LvlManager = FindAnyObjectByType<LvlManager>();
        //Can't use GetComponent to get LevelManager script component bc this script is attatched to the "Player" game object. They are diff gam objs, so we have to use FindObjectOfType<>.
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(movement, rigidbody.velocity.y);

        //Setting up Parameters in the Animator
        myAnim.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
    }

    void OnTriggerEnter2D(Collider2D other) //when player collider comes in contact with another collider
    {

    }

    float moveDirection = 0;
    //Code for left and right movement
    public void Move(float dir)
    {
        if (dir > 0)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir < 0)
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }

        moveDirection = dir;
    }
}