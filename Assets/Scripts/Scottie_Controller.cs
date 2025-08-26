using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Scottie_Controller : MonoBehaviour
{
    public GameObject Minigame;

    private static Scottie_Controller instance;                                  //Singleton reference to ensure only one player exists

    void Awake()
    {
        // Single instance logic to prevent duplicates on scene reloads
        if (instance == null)
        {
            instance = this;
            // Only mark as persistent if this GameObject is a root object
            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("Scottie_Controller must be on a root GameObject for DontDestroyOnLoad to work.");
            }
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate players
            return;
        }

        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //Add "audioManager.PlaySFX(audioManager.XX);" to movements/hacking etc
    }

    public float MoveSpeed;                                                      //Control Speed that the player is moving around the world
    private Rigidbody2D rb;                                                      //make a ref to the rigidbody2D component
    private SpriteRenderer sr;
    private float movement;
    private float moveDirection = 0;
    public bool canMove = true;

    //private bool isHidden = false;                                               //is 'false' by default because we want the default tag to be "Player"

    private Animator myAnim;                                                     //Store a ref to animtions to access later

    //AudioManager audioManager;                                                   //reference the Audio Manager Script

    LvlManager thelvlManager;                                                    //Reference Level Manager
    public FlashlightStun flashlightStun;

    private bool canHide = false;                                                //Define if player is able to hde or not
    private bool hiding = false;                                                 //Define if player is hiding to avoid enemy
    public bool IsHiding { get; private set; }                                  //Allows guards FOV's scipt to determine if player is hding and adjust behaviour
    public Sprite normalSprite;                                                  //Allow to assign normal sprite directly from inspector
    public Sprite crouchingSprite;                                                  //Allow to assign hiding sprite directly from inspector

    public HackableObject hackableObject;

    private bool playerInTrigger = false;
    private bool uiButtonPressed = false;

    //FlashLight Varibles
    public GameObject flashlight;
    private float nextAvailableTime = 0f;
    private float cooldownDuration = 15f;
    public Image cooldownOverlay;
    /*public Color flashColor = Color.white;
    public float flashDuration = 0.5f;
    public float flashSpeed = 10f;

    private Material material;
    private float flashTime = 0f;
    private bool isFlashing = false;*/

    public Button actionButton;
    public Image buttonIcon;
    public Sprite defaultIcon;
    public Sprite hackIcon;
    public Sprite hideIcon;
    public Sprite UseStairsIcon;
    public Sprite UseLiftIcon;
    private enum ActionMode { None, Hide, Hack, UseStairs, UseLift }
    private ActionMode currentMode = ActionMode.None;

    //Sound Effect
    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;

    public Sprite[] hideSprites;
    public int currentZoneIndex = -1;
    //public int currentZoneIndex;
    public HidingZone currentHidingZone;

    [SerializeField] private float footstepInterval = 0.5f;

    private GameObject stairsTarget;
    private bool nearStairs = false;
    private Lift currentLift;
    private Transform liftTarget;
    private bool nearLift = false;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                        //Get easy access to the Rigidbody2D component
        sr = GetComponent<SpriteRenderer>();                                     //Get easy access to the SpriteRenderer component
        myAnim = GetComponent<Animator>();                                       //Get easy access to the Animator component
        thelvlManager = FindObjectOfType<LvlManager>();                          //Get reference for the level manager
        sr.sprite = normalSprite;
        SoundEffectManager.Play("Background Music");

        /*//Restore position if saved
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            transform.position = new Vector2(x, y);
        }*/

        //If there's a saved position from before entering Captcha, teleport back to it
        if (GameData.SavedPlayerPosition != Vector2.zero)
        {
            transform.position = GameData.SavedPlayerPosition;
        }

        UpdateActionButtonUI();
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

        // Null check for Animator and Rigidbody
        if (myAnim != null && rb != null)
        {
            myAnim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        else
        {
            if (myAnim == null)
                Debug.LogError("Animator component is not assigned!");

            if (rb == null)
                Debug.LogError("Rigidbody component is not assigned!");
        }

        float remaining = (nextAvailableTime - Time.time);
        cooldownOverlay.fillAmount = Mathf.Clamp01(remaining / cooldownDuration);
        actionButton.interactable = remaining <= 0f;

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

        /*Toggle between "Player" and "Hidden" Tags in unity (so that the enemies can't detect the player)
        if (Input.GetKeyDown("e"))
        {
            isHidden = !isHidden;                                               //Changes 'isHidden' to true
            gameObject.tag = isHidden ? "Hidden" : "Player";                    //The "?" means "value if true" : "value if false". It's cleaner than a lot of if-else statements
        }*/
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
     //     SoundEffectManager.Play("Walking");
        }
        else if (dir < 0)
        {
            rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);                //Move Left
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);               //When move left, face left
      //    SoundEffectManager.Play("Walking");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);                         //Stand still when input detects nothing
        }
        moveDirection = dir;
    }

    private Coroutine footstepRoutine;

    private IEnumerator FootstepLoop()
    {
        while (true)
        {
            Debug.Log("SoundEffectisplaying");
            yield return new WaitForSeconds(footstepInterval);
            SoundEffectManager.Play("Walking");
        }
    }

    public void StartWalking()
    {
        if (footstepRoutine == null)
            footstepRoutine = StartCoroutine(FootstepLoop());
    }

    public void StopWalking()
    {
        if (footstepRoutine != null)
        {
            StopCoroutine(footstepRoutine);
            footstepRoutine = null;
        }
    }
    /*void StartFootsteps()
      {
          playingFootsteps = true;
          InvokeRepeating(nameof(PlayFootsteps), 0f, footstepSpeed);
      }

      void StopFootsteps()
      {
          playingFootsteps = false;
          CancelInvoke(nameof(PlayFootsteps));
      }

      void PlayFootsteps()
      {
          SoundEffectManager.Play("Footstep");
      }*/

    //Hiding Script
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.gameObject.name.Equals("Table"))                                //If name of gameObject is detected, player is able to hide in said object
        {
            canHide = true;
            currentMode = ActionMode.Hide;
            UpdateActionButtonUI();
        }

        if (other.CompareTag("Hackable"))
        {
            hackableObject = other.GetComponent<HackableObject>();
            playerInTrigger = true;
            currentMode = ActionMode.Hack;
            UpdateActionButtonUI();
        }

        if (other.CompareTag("Stairs"))
        {
            stairsTarget = other.GetComponent<Stairs>()?.stairsTarget;
            nearStairs = stairsTarget != null;
            if (nearStairs)
            {
                currentMode = ActionMode.UseStairs;
                UpdateActionButtonUI();
            }
        }

        if (other.CompareTag("Lift"))
        {
            currentLift = other.GetComponent<Lift>();
            nearLift = currentLift != null && currentLift.IsPlayerInRange();

            if (nearLift)
            {
                currentMode = ActionMode.UseLift;
                UpdateActionButtonUI();
            }
        }

    }
    
    private void OnTriggerExit2D(Collider2D other)                               //If name of gameObject is exited, player will not be hide anymore
    {
        if(other.gameObject.name.Equals("Table"))
        {
            canHide = false;
            if (currentMode == ActionMode.Hide)
            {
                currentMode = ActionMode.None;
                UpdateActionButtonUI();
            }
        }

        if (other.gameObject.CompareTag("Hackable"))
        {
            hackableObject = null;
            playerInTrigger = false;
            if (currentMode == ActionMode.Hack)
            {
                currentMode = ActionMode.None;
                UpdateActionButtonUI();
            }
        }

        if (other.CompareTag("Stairs"))
        {
            stairsTarget = null;
            nearStairs = false;
            if (currentMode == ActionMode.UseStairs)
            {
                currentMode = ActionMode.None;
                UpdateActionButtonUI();
            }
        }

        if (other.CompareTag("Lift"))
        {
            currentLift = null;
            nearLift = false;

            if (currentMode == ActionMode.UseLift)
            {
                currentMode = ActionMode.None;
                UpdateActionButtonUI();
            }
        }
    }

    public void EnterHideZone(int index, HidingZone zone)
    {
        currentZoneIndex = index;
        currentHidingZone = zone;
    }

    public void ExitHideZone(int index, HidingZone zone)
    {
        if (currentHidingZone == zone)
        {
            currentZoneIndex = -1;
            currentHidingZone = null;
        }
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
        if (!hiding && canHide && currentZoneIndex >= 0 && currentZoneIndex < hideSprites.Length)
        {
            Debug.Log($"Hiding at zone {currentZoneIndex}, sprite: {hideSprites[currentZoneIndex].name}");

            myAnim.enabled = false;
            sr.sprite = hideSprites[currentZoneIndex];
            myAnim.enabled = true;

            Physics2D.IgnoreLayerCollision(6, 7, true);
            Physics2D.IgnoreLayerCollision(6, 11, true);
            sr.sortingOrder = 0;

            
            if (currentHidingZone != null && currentHidingZone.targetRenderer != null)
            {
                currentHidingZone.targetRenderer.enabled = false;
            }

            hiding = true;
            IsHiding = true;
            canMove = false;

            SoundEffectManager.Play("Hiding");
        }
        else if (hiding)
        {
            sr.sprite = normalSprite;
            myAnim.enabled = true;

            Physics2D.IgnoreLayerCollision(6, 7, false);
            Physics2D.IgnoreLayerCollision(6, 11, false);
            sr.sortingOrder = 2;

            
            if (currentHidingZone != null && currentHidingZone.targetRenderer != null)
            {
                currentHidingZone.targetRenderer.enabled = true;
            }

            hiding = false;
            IsHiding = false;
            canMove = true;

            SoundEffectManager.Play("Hiding");
        }
    }


    private void LateUpdate()
    {
        if (hiding && currentZoneIndex >= 0)
        sr.sprite = hideSprites[currentZoneIndex];
    }
    public void EnterHideZone(int index)
    {
        currentZoneIndex = index;
        canHide = true;
        currentMode = ActionMode.Hide;
        UpdateActionButtonUI();
    }

    public void ExitHideZone(int index)
    {
        if (index == currentZoneIndex)
        {
            currentZoneIndex = -1;
            canHide = false;
            if (currentMode == ActionMode.Hide)
            {
                currentMode = ActionMode.None;
                UpdateActionButtonUI();
            }
        }
    }

    public void Flashlight()
    {
        if (Time.time < nextAvailableTime)
            return;

        nextAvailableTime = Time.time + cooldownDuration;
        StartCoroutine(DeactivateAfterTime());
    }

    private IEnumerator DeactivateAfterTime() //Activate preset flashlight, wait for 2s, then deactivate
    {
        flashlight.SetActive(true); 
        yield return new WaitForSeconds(1.5f);
        flashlight.SetActive(false);
    }

    public void OnHackButtonPressed()
    {
        Debug.Log("Hack button pressed");
        uiButtonPressed = true;
        Hack();
    }

    public void Hack()
    {
        
        if (hackableObject != null && playerInTrigger && uiButtonPressed)
        {
            Debug.Log("can hack");
            hackableObject.Hack();
            Debug.Log("Captcha");
            Minigame.SetActive(true);
            SoundEffectManager.Play("Hacking");
            /*//Save player position
           PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
           PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);

           //Save file count
           PlayerPrefs.SetInt("FileCount", thelvlManager.FileCount);
           PlayerPrefs.Save(); //actually saves the stuff to disk

           Debug.Log("Captcha");
           StartCoroutine(LoadCaptchaSceneAsync());*/

            //GameData.SavedPlayerPosition = transform.position; //Save current position before going into Captcha
        }
        else
        {
            Debug.Log("Hack conditions not met.");
        }
        /*if (hackableObject != null && playerInTrigger && uiButtonPressed)
        {
            Debug.Log("can hack");
            hackableObject.Hack();
            Debug.Log("e");
            StartCoroutine(LoadCaptchaSceneAsync());
        }

        Debug.Log("Hack() called");
        Debug.Log($"hackableObject != null: {hackableObject != null}");
        Debug.Log($"playerInTrigger: {playerInTrigger}");
        Debug.Log($"uiButtonPressed: {uiButtonPressed}"); 
        */
    }

    public void UseStairs()
    {
        if (stairsTarget != null && nearStairs)
        {
            Debug.Log("Using stairs...");
            SoundEffectManager.Play("Stairs"); // optional
            transform.position = stairsTarget.transform.position;
        }
    }
    public void UseLift()
    {
        if (currentLift == null)
        {
            Debug.LogWarning("UseLift: currentLift is null.");
            return;
        }

        if (!currentLift.IsPlayerInRange())
        {
            Debug.LogWarning("UseLift: Player is not in range of the lift.");
            return;
        }

        Debug.Log("Using lift...");
        currentLift.UseLift(gameObject, sr);
        SoundEffectManager.Play("Lift");
    }

    private IEnumerator LiftSequence()
    {
        // Animate lift doors and teleport player
        yield return StartCoroutine(currentLift.AnimateLiftAndTeleport(gameObject, sr));

        // Restore Scottie's sorting order after teleport and animation
        sr.sortingOrder = 10; // or whatever is the normal front sorting order

        // Enable player movement again
        canMove = true;
    }


    /*private IEnumerator UseLiftRoutine()
    {
        SoundEffectManager.Play("Lift");

        // Door animation is handled in Lift script — optional
        yield return new WaitForSeconds(3f); // Wait before teleport

        transform.position = liftTarget.transform.position;

        Debug.Log("Lift teleport complete.");
    }
    */
    private void UpdateActionButtonUI()
    {
        switch (currentMode)
        {
            case ActionMode.Hide:
                if (buttonIcon != null && hideIcon != null)
                {
                    buttonIcon.sprite = hideIcon;
                    buttonIcon.gameObject.SetActive(true); // re-enable if hidden
                }
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(() => 
                {
                    Hide();
                    UpdateActionButtonUI(); // refresh after action
                });
                break;

            case ActionMode.Hack:
                if (buttonIcon != null && hackIcon != null)
                {
                    buttonIcon.sprite = hackIcon;
                    buttonIcon.gameObject.SetActive(true); // re-enable if hidden
                }
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(OnHackButtonPressed);
                break;

            default:
                if (buttonIcon != null)
                {
                    // OPTION 1: Set default icon
                    if (defaultIcon != null)
                    {
                        buttonIcon.sprite = defaultIcon;
                        buttonIcon.gameObject.SetActive(true);
                    }
                }
                actionButton.onClick.RemoveAllListeners();
                break;

            case ActionMode.UseStairs:
                if (buttonIcon != null && defaultIcon != null) // You can assign a stairs icon instead if you have one
                {
                    buttonIcon.sprite = UseStairsIcon;
                    buttonIcon.gameObject.SetActive(true);
                }
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(() =>
                {
                    UseStairs();
                    UpdateActionButtonUI(); // optional, if stairs usage disables anything
                });
                break;

            case ActionMode.UseLift:
                if (buttonIcon != null && UseLiftIcon != null)
                {
                    buttonIcon.sprite = UseLiftIcon;
                    buttonIcon.gameObject.SetActive(true);
                }

                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(() =>
                {
                    UseLift();
                    UpdateActionButtonUI();
                });
                break;
        }
    }
}