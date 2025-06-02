using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{

    public Color flashColor = Color.white;                                                //colour sprite will be
    public float flashDuration = 2.0f;                                                      //
    public float flashSpeed = 15f;                                                          //

    private SpriteRenderer spriteRenderer;
    private Material material;
    private float flashTime = 0f;
    private bool isFlashing = false;


    // Start is called before the first frame update    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
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
        }
    }

    public void Flash()
    {
        isFlashing = true;
        flashTime = 0f;
    }
}
