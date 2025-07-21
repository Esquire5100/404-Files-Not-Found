using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Level : MonoBehaviour
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
        textToSpeak = "Now then, let’s see if you were paying attention.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(2f);
        PhionaTalk.Stop();
        responses.SetActive(true);

        eventPos = 1;
    }

    public void HUH()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseHuh());
        }
        
    }

    private IEnumerator ResponseHuh()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);                                                 //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "This’ll give you a taste of what’s to come, Scottie. This way, you’ll know what to expect!";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }

    public void Cry()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseCry());
        }
    }

    private IEnumerator ResponseCry()
    {
        yield return new WaitForSeconds(0.03f);
        responses1.SetActive(false);                                                 //Disable the buttons so the player can't keep clicking them
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "Long hallways will lead to the next building. If there isn’t one, then take the stairs on the top floor. That will be your way out of the current building.";                        //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(8f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();                                                          //Play the sfx for Phiona talking

        textToSpeak = "Good Luck!~";                                                //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                 //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);          //Wait until the text has finished 

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();

        eventPos = 3;
    }
}