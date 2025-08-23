using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Dialer_Window : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;
    private CutsceneTransition escape;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("spriteRenderer not found on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            escape = FindFirstObjectByType <CutsceneTransition> ();
            escape.PlayVideo();
            //SceneManager.LoadScene("Victory"); //...then bring player to the Victory scene
        }
    }
}
