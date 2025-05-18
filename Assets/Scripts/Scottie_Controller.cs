using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scottie_Controller : MonoBehaviour
{
    public float MoveSpeed = 5f; //how fast the character moves

    private Rigidbody2D rb; //make a ref to the rigidbody2D component
    private float movement;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Checks the current obj for a component called "Rigidbody2D". In this case, the current obj is the player because we are currently in the player script.
        //(GetComponent can only be used within the same script/current game obj)
    }

    void Update()
    {
        //Get horizontal input (-1 for left, 1 for right, 0 for none)
        movement = Input.GetAxisRaw("Horizontal");

        //For making sure the character faces the direction it's moving in
        if (movement < 0)
        {
            //moving left -> sprite flips to the left
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
            facingRight = false;
        }

        if (movement > 0)
        {
            //moving right -> sprite doesnt flip
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = false;
            facingRight = true;
        }
    }

    void FixedUpdate()
    {
        // Move the character left or right
        rb.velocity = new Vector2(movement * MoveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //SceneManager.LoadScene("Game Over");
        }
    }
}
