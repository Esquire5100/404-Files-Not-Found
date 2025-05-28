using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public SpriteRenderer sr;                           //Sprite render reference

    //Public Reference to Camera sprites
    public Sprite leftCamera;                           //left
    public Sprite centreCamera;                         //centre
    public Sprite rightCamera;                          //right


    // Start is called before the first frame update
    void Start()        
    {
        sr = GetComponent<SpriteRenderer>();            //Get and storing reference to component
        StartCoroutine(ChangeSpriteAfterDelay());       //State it exist i think
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeSpriteAfterDelay()                //Make sprite change after specified times        
    {
        for(; ; )
        {
            yield return new WaitForSeconds(3f);
            sr.sprite = leftCamera;
            yield return new WaitForSeconds(3f);
            sr.sprite = centreCamera;
            yield return new WaitForSeconds(3f);
            sr.sprite = rightCamera;
        }
        
    }
}
