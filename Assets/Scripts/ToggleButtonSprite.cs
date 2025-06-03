using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonSprite : MonoBehaviour
{

    public Sprite normalSprite;
    public Sprite changedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    public void ToggleSprite()
    {
        isChanged = !isChanged;
        spriteRenderer.sprite = isChanged ? changedSprite : normalSprite;
    }
}
