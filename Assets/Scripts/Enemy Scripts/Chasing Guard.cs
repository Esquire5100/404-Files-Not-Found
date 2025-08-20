using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingGuard : MonoBehaviour
{
    public float moveSpeed; //How fast the enemy can move

    private Rigidbody2D enemyRb; //Ref to the rigidbody2D component of the enemy

    public bool movingRight; //Checks if enemy is moving right
    private bool isChasing = false; //Checks if the enemy is chasing (chasing is a state that overrides the "patrolling" behaviour)

    public SpriteRenderer sr; //Ref to the sprite renderer component  (to change enemy colour when player in range)
    private Transform player; //Ref to the player
    public Transform lightTransform; //Make ref to the light GameObj (child)

    AudioManager audioManager;

    private VideoTransition caught;

    //Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>(); //Get access to the enemy Rigidbody2D component
        sr = GetComponent<SpriteRenderer>(); //Get access to the Sprite Renderer component
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing && player != null) //Only chase if chasing is true AND player is assigned
        {
            //Move to player pos
            Vector2 direction = (player.position - transform.position).normalized;
            enemyRb.velocity = new Vector2(direction.x * moveSpeed * 3.5f, enemyRb.velocity.y); //Move faster when chasing

            //Flip sprite and FOV to face player
            if (direction.x > 0.1f) //Face right
            {
                sr.flipX = false;
                lightTransform.localEulerAngles = new Vector3(0, 0, -107.853f); //Moves the light following the parents' (guard)
            }
            else if (direction.x < -0.1f) //Face left
            {
                sr.flipX = true;
                lightTransform.localEulerAngles = new Vector3(0, 0, 107.853f); //Moves the light with an offset, otherwise the light will be inaccurate to where we want it to be
            }
        }
        else //Only patrol when not chasing
        {
            if (movingRight)
            {
                enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
                sr.flipX = true; //Flips the sprite so it faces the direction it's moving
                lightTransform.localEulerAngles = new Vector3(0, 0, -107.853f);
            }

            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
                sr.flipX = false; //Doesn't flip the sprite so it faces the direction it's moving
                lightTransform.localEulerAngles = new Vector3(0, 0, 107.853f);
            }
        }
    }

    public void StartChase(Transform target) //Locks onto pos of target (in this case the player)
    {
        player = target; //Assign the player reference
        isChasing = true; //Enable chasing behaviour
        audioManager.PlaySFX(audioManager.AlertMusic);
    }

    public void StopChase()
    {
        isChasing = false;
        player = null;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) //If the guard directly hits the Player hitbox...
        {
            caught = FindFirstObjectByType <VideoTransition> ();
            caught.PlayVideo();
            //SceneManager.LoadScene("Game Over"); //...then bring player to the Game Over scene
        }
    }
}