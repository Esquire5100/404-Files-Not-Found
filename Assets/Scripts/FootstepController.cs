using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public float minInterval = 0.3f;
    private float timer = 0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            timer += Time.deltaTime;
            if (timer >= minInterval)
            {
                timer = 0f;
                SoundEffectManager.Play("Walking");
                Debug.Log("SoundEffectisplaying");
            }
        }
        else
        {
            timer = minInterval; // reset when player stops
        }
    }
}