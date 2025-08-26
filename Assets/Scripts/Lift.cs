using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public SpriteRenderer doorRenderer;

    public Sprite closedSprite;
    public Sprite halfOpenSprite;
    public Sprite openSprite;

    public Transform liftTarget;
    private GameObject playerInRange = null;

    private bool isAnimating  = false;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.gameObject;

            if (!isAnimating && doorRenderer != null)
            {
                doorRenderer.sprite = openSprite;
                doorRenderer.sortingOrder = 0; // background
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;

            if (!isAnimating && doorRenderer != null)
            {
                doorRenderer.sprite = closedSprite;
                doorRenderer.sortingOrder = 0;
            }
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange != null;
    }

    public void UseLift(GameObject player, SpriteRenderer playerSR)
    {
        if (player != null)
        {
            StartCoroutine(AnimateLiftAndTeleport(player, playerSR));
        }
    }

    public IEnumerator AnimateLiftAndTeleport(GameObject player, SpriteRenderer playerSR)
    {
        // Step 1: Lift doors start to close, so put player behind
        if (playerSR != null)
        {
            playerSR.sortingOrder = 0;  // move player to background
        }

        // Step 2: Change lift door sprite and set foreground sorting
        doorRenderer.sprite = halfOpenSprite;
        doorRenderer.sortingOrder = 10;  // doors in foreground

        yield return new WaitForSeconds(1f);

        doorRenderer.sprite = closedSprite;
        doorRenderer.sortingOrder = 10;

        yield return new WaitForSeconds(3f);

        // Step 3: Teleport player
        if (player != null && liftTarget != null)
        {
            player.transform.position = liftTarget.position;
        }

        yield return new WaitForSeconds(0.5f);

        // Step 4: Restore sprites and sorting
        doorRenderer.sprite = closedSprite;
        doorRenderer.sortingOrder = 0;

        if (playerSR != null)
        {
            playerSR.sortingOrder = 10; // back to foreground
        }
    }

}

