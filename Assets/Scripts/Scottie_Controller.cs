using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Scottie_Controller : MonoBehaviour
{
    public float MoveSpeed;                                                      //Control Speed that the player is moving around the world
    private Rigidbody2D rb;                                                      //make a ref to the rigidbody2D component
    private SpriteRenderer sr;
    private float movement;
    private float moveDirection = 0;
    public bool canMove = true;

    private bool isHidden = false;                                               //is 'false' by default because we want the default tag to be "Player"

    private Animator myAnim;                                                     //Store a ref to animtions to access later

    AudioManager audioManager;                                                   //reference the Audio Manager Script

    LvlManager thelvlManager;                                                    //Reference Level Manager

    private bool canHide = false;                                                //Define if player is able to hde or not
    private bool hiding = false;                                                 //Define if player is hiding to avoid enemy
    public bool IsHiding {  get; private set; }                                  //Allows guards FOV's scipt to determine if player is hding and adjust behaviour
    public Sprite normalSprite;                                                  //Allow to assign normal sprite directly from inspector
    public Sprite crouchingSprite;                                                  //Allow to assign hiding sprite directly from inspector

    public GameObject stairsTarget;                                              //Space in scene where the player will end up after "using the stairs"
    private bool hasTeleported = false;                                          //Define if player has already teleported ONCE (used the stairs)

    public HackableObject hackableObject;

    private bool playerInTrigger = false;
    private bool uiButtonPressed = false;

    //FlashLight Varibles
    public GameObject flashlight;
    /*public Color flashColor = Color.white;
    public float flashDuration = 0.5f;
    public float flashSpeed = 10f;

    private Material material;
    private float flashTime = 0f;
    private bool isFlashing = false;*/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                        //Get easy access to the Rigidbody2D component
        sr = GetComponent<SpriteRenderer>();                                     //Get easy access to the SpriteRenderer component
        myAnim = GetComponent<Animator>();                                       //Get easy access to the Animator component
        thelvlManager = FindObjectOfType<LvlManager>();                          //Get reference for the level manager
        sr.sprite = normalSprite;
    }

    void Update()
    {
        if (!thelvlManager.showMobileUI)
        {
            Move(Input.GetAxisRaw("Horizontal"));                                    //Code for left right movement   
        }
        else
        {
            Move(moveDirection);
        }

        /*Toggle between "Player" and "Hidden" Tags in unity (so that the enemies can't detect the player)
        if (Input.GetKeyDown("e"))
        {
            isHidden = !isHidden;                                               //Changes 'isHidden' to true
            gameObject.tag = isHidden ? "Hidden" : "Player";                    //The "?" means "value if true" : "value if false". It's cleaner than a lot of if-else statements
        }*/

        //Setting up Parameters in the Animator
        myAnim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));                     //"Mathf.abs()" returns the value of velocity of rigidbody along the x axis

        /*if (isFlashing)
        {
            flashTime += Time.deltaTime * flashSpeed;
            float flashAmount = Mathf.PingPong(flashTime, flashDuration) / flashDuration;
            material.SetColor("_FlashColor", flashColor);
            material.SetFloat("_FlashAmount", flashAmount);

            if (flashTime >= flashDuration)
            {
                isFlashing = false;
                material.SetFloat("_FlashAmount", 0);
            }
        }*/

    }

    //Audio Scripts
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //Add "audioManager.PlaySFX(audioManager.XX);" to movements/hacking etc
    }

    //Movement Script
    public void Move(float dir)
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        if (dir > 0)
        {   
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);                 //Move right
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);                //When move right, face right
        }
        else if (dir < 0)
        {
            rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);                //Move Left
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);               //When move left, face left
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);                         //Stand still when input detects nothing
        }
        moveDirection = dir;
    }


    //Hiding Script
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.gameObject.name.Equals("Table"))                                //If name of gameObject is detected, player is able to hide in said object
        {
            canHide = true;
        }
        if (other.gameObject.CompareTag("Hackable"))
        {
            playerInTrigger = true;
        }

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Stairs") && !hasTeleported) //If the tag is stairs and the player has not yet teleported
        {
            StartCoroutine(UseStairs());
        }
    }

    private void OnTriggerExit2D(Collider2D other)                               //If name of gameObject is exited, player will not be hide anymore
    {
        if(other.gameObject.name.Equals("Table"))
        {
            canHide = false;
        }

        if (other.CompareTag("Stairs"))
        {
            hasTeleported = false; //Reset when leaving the stairs
        }

        if (other.gameObject.CompareTag("Hackable"))
        {
            playerInTrigger = false;
        }
    }

    private IEnumerator UseStairs()
    {
        yield return new WaitForSeconds(2);
        transform.position = new Vector2(stairsTarget.transform.position.x, stairsTarget.transform.position.y);
        hasTeleported = true;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.Comparetag("Hackable"))
        {
            playerInTrigger = true;
        }
    }*/

    /*private void OnTriggerExit2D(Collider other)
    {
        if (other.CompareTag("Hackable"))
        {
            playerInTrigger = false;
        }
    }*/

    //Mobile Control for hiding
    public void Hide()
    {
        if(!hiding && canHide)
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Physics2D.IgnoreLayerCollision(6, 11, true);
            sr.sortingOrder = 0;
            sr.sprite = crouchingSprite;
            hiding = true;
            IsHiding = true;
            canMove = false;
            
        }
        else
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
            Physics2D.IgnoreLayerCollision(6, 11, false);
            sr.sortingOrder = 2;
            sr.sprite = normalSprite;
            hiding = false;
            IsHiding = false;
            canMove = true;
           
        }
    }

    public void Flashlight()
    {
        /*isFlashing = true;
        flashTime = 0;*/

        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay() //Activate preset flashlight, wait for 2s, then deactivate
    {
        flashlight.SetActive(flashlight); 
        yield return new WaitForSeconds(1.5f);
        flashlight.SetActive(false);
    }

    public void OnHackButtonPressed()
    {
        uiButtonPressed = true;
        Hack();
    }
    public void Hack()
    {
        if (hackableObject != null && playerInTrigger && uiButtonPressed)
        {
            hackableObject.Hack();
            StartCoroutine(LoadCaptchaSceneAsync());
        }
    }
    private IEnumerator LoadCaptchaSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Captcha");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}