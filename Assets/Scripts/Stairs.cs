using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite closed;
    public Sprite open;

    public GameObject player; 
    public GameObject stairsTarget;                                              //Space in scene where the player will end up after "using the stairs"
    //private bool hasTeleported = false;                                          //Define if player has already teleported ONCE (used the stairs)

    private GameObject playerInRange = null;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); //Get easy access to the SpriteRenderer component
        sr.sprite = closed; // Initial state
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(UseStairs());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered stairs range");
            playerInRange = other.gameObject;
            sr.sprite = open; // Optional: visually indicate it's usable
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left stairs range");
            playerInRange = null;
            sr.sprite = closed;
        }
    }

    private IEnumerator UseStairs()
    {
        sr.sprite = open; // Show open before teleport

        yield return new WaitForSeconds(1.5f);
        //player.transform.position = new Vector2(stairsTarget.transform.position.x, stairsTarget.transform.position.y);
        if (playerInRange !=null)
        {
            playerInRange.transform.position = stairsTarget.transform.position;
        }

        sr.sprite = closed; // Switch back to closed after teleport

        //hasTeleported = true;
    }
}
