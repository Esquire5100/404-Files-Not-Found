using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard_Controller : MonoBehaviour
{
    //Reference point positions
    public Transform leftPoint; //Can make a ref to the leftPoint Game Obj in unity
    public Transform rightPoint; //Can make a ref to the rightPoint Game Obj in unity

    public float moveSpeed; //How fast the enemy can move
    public float waitTime;  //The time to wait at each end point

    private Rigidbody2D enemyRb; //Ref to the rigidbody2D component of the enemy

    public bool movingRight; //Checks if enemy is moving right
    private bool isWaiting = false;  //Checks if the enemy is waiting

    private SpriteRenderer sr; //Ref to the sprite renderer component of the enemy (to change enemy colour when player in range)
    public LayerMask WhoICanSee; //Can change the visible layers in unity

    //Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>(); //Get access to the enemy Rigidbody2D component
        sr = GetComponent<SpriteRenderer>(); //Get access to the Sprite Renderer component
    }

    //Update is called once per frame
    void Update()
    {
        //if enemy is moving right and has gone past the right point, it should turn and move left
        //if (movingRight && (transform.position.x > rightPoint.position.x))
        //{
        //movingRight = false;
        //}

        //if enemy is moving left and has gone past the left point, it should turn and move right
        //if (!movingRight && (transform.position.x < leftPoint.position.x))
        //{
        //movingRight = true;
        //}

        //if (movingRight)
        //{
        //enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.flipX = true;
        //}

        //else
        //{
        //enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.flipX = false;
        //}
        //The code above made it such that the enemy was constantly moving. I want the guard to stop for a while at the end point, then turn and continue moving. The code below makes it as such.

        //Only move if NOT in "isWaiting" state
        if (!isWaiting) //! = not; !isWaiting'= isWaiting is false (can also be written as "isWaiting = false", but it looks messier)
        {
            if (movingRight)
            {
                enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
                GetComponent<SpriteRenderer>().flipX = true; //Flips the sprite so it faces the direction it's moving

                //If enemy is moving right and has gone past the right point, it should pause for a bit, turn around, then move left
                if (transform.position.x >= rightPoint.position.x)
                {
                    StartCoroutine(WaitAndTurn(false)); //'false' means the enemy should turn left
                }
            }

            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
                GetComponent<SpriteRenderer>().flipX = false; //Doesn't flip the sprite so it faces the direction it's moving

                //If enemy is moving left and has gone past the left point, it should pause for a bit, turn around, then move right
                if (transform.position.x <= leftPoint.position.x)
                {
                    StartCoroutine(WaitAndTurn(true)); //'true' means the enemy should turn right
                }
            }
        }

        else
        {
            //Stop the enemy's movement while waiting
            enemyRb.velocity = Vector2.zero;
        }
    }

    //Coroutine for pausing movement, then turning
    IEnumerator WaitAndTurn(bool turnRight)
    {
        isWaiting = true;             //Set "isWaiting" state to true
        yield return new WaitForSeconds(waitTime); //Wait for however many seconds put in unity

        movingRight = turnRight;      // Change direction
        isWaiting = false;            //End "isWaiting" state and resume movement
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