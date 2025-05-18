using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Controller : MonoBehaviour
{
    //Reference point positions
    public Transform leftPoint;
    public Transform rightPoint;

    public float moveSpeed; //How fast the enemy can move

    private Rigidbody2D enemyRb; //Ref to the rigidbody2D component of the enemy

    public bool movingRight; //Checks if enemy is moving right
    private bool isWaiting = false;  //Checks if the enemy is waiting

    //Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>(); //Get access to the enemy rigidbody2D component
    }

    //Update is called once per frame
    void Update()
    {
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

    // Coroutine to handle waiting and turning
    IEnumerator WaitAndTurn(bool turnRight)
    {
        isWaiting = true;             //Set "isWaiting" state to true
        yield return new WaitForSeconds(2f); //Wait for 2 seconds (you can change the duration)

        movingRight = turnRight;      // Change direction
        isWaiting = false;            //End "isWaiting" state and resume movement
    }
    
        //if enemy is moving right and has gone past the right point, it should stop for a bit, turn around, and move left
        //if (movingRight && (transform.position.x > rightPoint.position.x))
        //{
            //movingRight = false;
        //}

        //if enemy is moving left and has gone past the left point, it should stop for a bit, turn around, and move right
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
}