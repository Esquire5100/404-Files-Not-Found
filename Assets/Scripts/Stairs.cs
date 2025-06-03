using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite closed;
    public Sprite open;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); //Get easy access to the SpriteRenderer component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnterCollider2D(Collider2D other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            sr.sprite = open;
        }
    }

    private void OnTriggerExitCollider2D(Collider2D other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            sr.sprite = closed;
        }
    }
}
