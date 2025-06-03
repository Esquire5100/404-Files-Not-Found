using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite ogSprite;

    void Start()
    {
        // Get the sprite renderer component
        sr = GetComponent<SpriteRenderer>();

        // Store the original sprite so we can restore it later
        if (sr != null)
        {
            ogSprite = sr.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lift"))
        {
            StartCoroutine(HideAndRestoreSprite());
        }
    }

    private IEnumerator HideAndRestoreSprite()
    {
        if (sr != null)
        {
            sr.sprite = null; // Hide the sprite
        }

        yield return new WaitForSeconds(5);

        if (sr != null)
        {
            sr.sprite = ogSprite; // Restore the sprite
        }
    }
}
