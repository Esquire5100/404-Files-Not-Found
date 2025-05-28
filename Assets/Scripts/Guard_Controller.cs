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
    private bool isWaiting = false; //Checks if the enemy is waiting
    private bool isChasing = false; //Checks if the enemy is chasing (chasing is a state that overrides the "patrolling" behaviour)

    public SpriteRenderer sr; //Ref to the sprite renderer component  (to change enemy colour when player in range)
    public Transform player; //Ref to the player (to be assigned in Unity)
    public Transform FOVTransform; //Ref to the FOV GameObj (child)
    public Transform lightTransform; //Make ref to the light GameObj (child)

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

        if (isChasing)
        {
            //Move to player pos
            Vector2 direction = (player.position - transform.position).normalized;
            enemyRb.velocity = new Vector2(direction.x * moveSpeed * 3.5f, enemyRb.velocity.y);

            //Flip sprite and FOV to face player
            if (direction.x > 0.1f) //Face right
            {
                sr.flipX = true;
                FOVTransform.localScale = new Vector3(-1, 1, 1); //Moves the FOV following the parents' (guard)
                lightTransform.localEulerAngles = new Vector3(0, 0, -107.853f); //Moves the light following the parents' (guard)
            }
            else if (direction.x < -0.1f) //Face left
            {
                sr.flipX = false;
                FOVTransform.localScale = new Vector3(1, 1, 1); //Moves the FOV with an offset, otherwise the FOV will be inaccurate to where we want it to be
                lightTransform.localEulerAngles = new Vector3(0, 0, 107.853f); //Moves the light with an offset, otherwise the light will be inaccurate to where we want it to be
            }
        }

        //Only move if NOT in "isWaiting" state
        else if (!isWaiting) //! = not; !isWaiting'= isWaiting is false (can also be written as "isWaiting = false", but it looks messier)
        {
            if (movingRight)
            {
                enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
                sr.flipX = true; //Flips the sprite so it faces the direction it's moving
                FOVTransform.localScale = new Vector3(-1, 1, 1);
                FOVTransform.GetComponent<EdgeCollider2D>().offset = new Vector3(3.7f, 0, 0); //Offset to make the collider be in the desired position: in front of the guards face
                lightTransform.localEulerAngles = new Vector3(0, 0, -107.853f);

                //If enemy is moving right and has gone past the right point, it should pause for a bit, turn around, then move left
                if (transform.position.x >= rightPoint.position.x)
                {
                    StartCoroutine(WaitAndTurn(false)); //'false' means the enemy should turn left
                }
            }

            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
                sr.flipX = false; //Doesn't flip the sprite so it faces the direction it's moving
                FOVTransform.localScale = new Vector3(1, 1, 1);
                FOVTransform.GetComponent<EdgeCollider2D>().offset = new Vector3(0, 0, 0); //Reset the offset to make the collider be in the desired position: in front of the guards face
                lightTransform.localEulerAngles = new Vector3(0, 0, 107.853f);

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

    //Coroutine for pausing movement, then turning around
    IEnumerator WaitAndTurn(bool turnRight)
    {
        isWaiting = true;
        enemyRb.velocity = Vector2.zero; //Stop movement
        yield return new WaitForSeconds(waitTime); //Wait for however many seconds put in unity
        movingRight = turnRight; //Change directions (Turning around)
        isWaiting = false; //End "isWaiting" state and resume movement
    }

    public void StartChase(Transform target) //Locks onto pos of target (in this case the player)
    {
        player = target;
        isChasing = true;
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
            SceneManager.LoadScene("Game Over"); //...then bring player to the Game Over scene
        }
    }
}