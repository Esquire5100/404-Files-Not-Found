using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public SpriteRenderer sr;                           //Sprite render reference

    //Public reference to Camera sprites
    public Sprite leftCamera;                           //left
    public Sprite centreCamera;                         //centre
    public Sprite rightCamera;                          //right

    public Transform FOVTransform;                      //Ref to the FOV GameObj (child)
    public Transform lightTransform;                    //Make ref to the light GameObj (child)

    //Public references to each light GameObject
    public GameObject leftLight;
    public GameObject centreLight;
    public GameObject rightLight;

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

    //Make sprite change after specified times (the sprites make it look like it's oscillating)
    IEnumerator ChangeSpriteAfterDelay()
    {
        for (; ; )                                      //Infinite loop to keep oscillating
        {
            yield return new WaitForSeconds(3f);        //Wait 3 seconds before switching
            sr.sprite = leftCamera;                     //Switch sprite to left-facing camera
            SetActiveLight(leftLight);                  //Enable only the left light

            yield return new WaitForSeconds(3f);
            sr.sprite = centreCamera;
            SetActiveLight(centreLight);

            yield return new WaitForSeconds(3f);
            sr.sprite = rightCamera;
            SetActiveLight(rightLight);

            yield return new WaitForSeconds(3f);
            sr.sprite = centreCamera;
            SetActiveLight(centreLight);
        }
    }

    void SetActiveLight(GameObject activeLight)         //Activate only one light and deactivate others
    {
        //Only the matching direction's light stays active, the others are deactivated from the scene
        leftLight.SetActive(activeLight == leftLight);
        centreLight.SetActive(activeLight == centreLight);
        rightLight.SetActive(activeLight == rightLight);
    }
}