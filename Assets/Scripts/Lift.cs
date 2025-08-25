using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite closed;
    public Sprite halfway;
    public Sprite open;

    public GameObject LiftTarget; // Where the elevator takes the player
    public float liftDelay = 3f;

    private GameObject playerInRange = null;
    private bool isUsingElevator = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }

    private void Update()
    {
        if (playerInRange != null && !isUsingElevator && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(UseElevator());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.gameObject;
            sr.sprite = open;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
            sr.sprite = closed;
        }
    }

    private IEnumerator UseElevator()
    {
        isUsingElevator = true;

        sr.sprite = open;

        // Optional: play elevator sound here
        SoundEffectManager.Play("ElevatorStart");

        yield return new WaitForSeconds(liftDelay);

        if (playerInRange != null)
            playerInRange.transform.position = LiftTarget.transform.position;

        yield return new WaitForSeconds(0.2f); // small delay before enabling movement again

        sr.sprite = closed;
        isUsingElevator = false;
    }

    private IEnumerator AnimateDoorOpening()
{
    sr.sprite = halfway;
    yield return new WaitForSeconds(0.2f);
    sr.sprite = open;
}
}

