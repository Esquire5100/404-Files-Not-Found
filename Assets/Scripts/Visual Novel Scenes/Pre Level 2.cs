using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLevel2 : MonoBehaviour
{
    public GameObject This;

    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut;

    public GameObject charPhiona;
    public Sprite PhionaNeutral;
    public Sprite PhionaAnnoyed;
    public Sprite PhionaAngry;

    public GameObject Dialogue;

    [SerializeField] AudioSource PhionaTalk;
    [SerializeField] AudioSource PhionaSqueal;
    [SerializeField] AudioSource PhionaShout;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;  //Ref the parent gameobj of the text(box)s

    [SerializeField] GameObject responses;
    [SerializeField] GameObject responses1;

    [SerializeField] int eventPos = 0;

    // Update is called once per frame
    void Update()
    {
        textLength = TextCreator.charCount; //Ref to the charCount in TextCreator script

        if (eventPos == 3)
        {
            //Wait for a bit before fading out and loading next scene
            new WaitForSeconds(2f);
            fadeScreenOut.SetActive(true);

            This.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        eventPos = 0;

        //Set everyth but the fade screen to false so its easier to edit the scene in unity
        fadeScreenIn.SetActive(true);
        charPhiona.SetActive(false);
        mainTextObject.SetActive(false);

        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        //Event 0

        //After 1.5s in the scene, disable the fadescreen and enable phiona gameobj
        yield return new WaitForSeconds(1f);
        fadeScreenIn.SetActive(false);
        charPhiona.SetActive(true);

        //Enable and control the text(box) functions
        yield return new WaitForSeconds(1f);
        PhionaTalk.Play(); //Play the sfx for Phiona talking

        //Activate the textbox, but deactivate the dialogue text so that it will print properly
        mainTextObject.SetActive(true);

        //Set a small delay between when the text box appears and when the text starts printing
        yield return new WaitForSeconds(1f);
        textToSpeak = "Good job on successfully stealing their files! But my sche- ahem- PLAN is far from over.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(3.5f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This next building won’t be as easy as the first time. You'd better be careful. I don’t want him finding out…";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);  //Wait until the text has finished 

        yield return new WaitForSeconds(5f);
        PhionaTalk.Stop();
        responses.SetActive(true);

        eventPos = 1;
    }

    //Event 1
    public void Who()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseOne());
        }
    }

    private IEnumerator ResponseOne()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                 //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite

        textToSpeak = "You’ll know soon enough.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "Now, hurry along and get me the files. Remember, your job is on the line~";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(5f);
        PhionaTalk.Stop();

        eventPos = 3;
    }

    public void ShouldWe()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseTwo());
        }
    }
    private IEnumerator ResponseTwo()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                           //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();
        charPhiona.GetComponent<Image>().sprite = PhionaAngry;                    //Change Phiona's sprite

        textToSpeak = "Would you like an office on the side of the road?";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);                    //Wait until the text has finished 

        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();

        responses1.SetActive(true);

        eventPos = 2;
    }

    // Event 2
    public void uh()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseOneuh());
        }
    }

    private IEnumerator ResponseOneuh()
    {
        yield return new WaitForSeconds(0.03f);
        responses1.SetActive(false);                                                //Disable the buttons so the player can't keep clicking them
        PhionaShout.Play();                                                          //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                    //Change Phiona's sprite

        textToSpeak = "That's what I thought.";                       //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(2f);
        PhionaShout.Stop();

        yield return new WaitForSeconds(1.5f);
        charPhiona.GetComponent<Image>().sprite = PhionaNeutral;                    //Change Phiona's sprite
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "Now, hurry along and get me the files. Remember, your job is on the line~";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(5f);
        PhionaTalk.Stop();

        eventPos = 3;
    }
}
