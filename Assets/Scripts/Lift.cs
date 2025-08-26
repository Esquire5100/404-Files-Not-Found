using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite closedSprite;
    public Sprite halfOpenSprite;
    public Sprite openSprite;
    public Transform liftTarget;

    private GameObject playerInRange = null;

    void Start()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.gameObject;
            sr.sprite = halfOpenSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
            sr.sprite = closedSprite;
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange != null;
    }

    public IEnumerator TeleportPlayer(GameObject player)
    {
        if (sr != null)
            sr.sprite = halfOpenSprite;

        SoundEffectManager.Play("Lift");

        yield return new WaitForSeconds(1f);

        if (sr != null)
            sr.sprite = openSprite;

        yield return new WaitForSeconds(1f);

        if (player != null && liftTarget != null)
            player.transform.position = liftTarget.position;

        yield return new WaitForSeconds(0.5f);

        if (sr != null)
            sr.sprite = closedSprite;
    }
}

