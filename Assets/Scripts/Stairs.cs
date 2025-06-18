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
    private bool hasTeleported = false;                                          //Define if player has already teleported ONCE (used the stairs)

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); //Get easy access to the SpriteRenderer component
        sr.sprite = closed; // Initial state
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(UseStairs());
        }
    }

    private void OnTriggerExitCollider2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasTeleported = false; //Reset when leaving the stairs

            sr.sprite = closed;
        }
    }

    private IEnumerator UseStairs()
    {
        sr.sprite = open; // Show open before teleport

        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector2(stairsTarget.transform.position.x, stairsTarget.transform.position.y);

        sr.sprite = closed; // Switch back to closed after teleport

        hasTeleported = true;
    }
}
