using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogue : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut;

    public GameObject ProjectorScreen;
    public GameObject PhionaHand;
    public GameObject PC;
    public GameObject File;
    public GameObject Security;
    public GameObject foreshadowing;

    public GameObject charPhiona;
    public Sprite PhionaNeutral;
    public Sprite PhionaAnnoyed;
    public Sprite PhionaAngry;
    public Sprite PhionaTutorial;

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
        yield return new WaitForSeconds(1.5f);
        fadeScreenIn.SetActive(false);
        charPhiona.SetActive(true);

        //Activate the textbox, but deactivate the dialogue text so that it will print properly
        mainTextObject.SetActive(true);

        //Set a small delay between when the text box appears and when the text starts printing
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.03f);
        ProjectorScreen.SetActive(true);
        PhionaHand.SetActive(true);

        PhionaTalk.Play();                                                                  //Play the sfx for Phiona talking
        charPhiona.GetComponent<Image>().sprite = PhionaTutorial;                             //Change Phiona's sprite

        textToSpeak = "First thing's first, these are the computers that you need to target. They'll hold all the necessary files I need.";
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(1f);
        PC.SetActive(true);

        yield return new WaitForSeconds(5f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Hack into them and find the right file. It'll look like this.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(2f);
        PC.SetActive(false);
        File.SetActive(true);

        yield return new WaitForSeconds(3f);
        PhionaTalk.Stop();

        yield return new WaitForSeconds(1.5f);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking
        File.SetActive(false);
        Security.SetActive(true);

        textToSpeak = "Next, remember to watch out for MGD's security system. Unfortunately, Dialer has significantly enhanced his security since the last time.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();
        responses.SetActive(true);

        eventPos = 1;
    }

    public void LastTime()
    {
        if (eventPos == 1)
        {
            StartCoroutine(ResponseLT());
        }
    }

    private IEnumerator ResponseLT()
    {
        yield return new WaitForSeconds(0.03f);
        responses.SetActive(false);
        PhionaHand.SetActive(false);
        charPhiona.GetComponent<Image>().sprite = PhionaAnnoyed;                             //Change Phiona's sprite
        PhionaShout.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Problem, Scottie?";                                                    //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(1.5f);
        PhionaHand.SetActive(true);
        charPhiona.GetComponent<Image>().sprite = PhionaTutorial;                             //Change Phiona's sprite
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking

        textToSpeak = "Now then, don't get caught in their light. The camera will alert all the guards in the vicinity, and if they catch you...";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(6f);
        PhionaTalk.Stop();
        responses1.SetActive(true);

        eventPos = 2;
    }

    public void IfTheyCatch()
    {
        if (eventPos == 2)
        {
            StartCoroutine(ResponseITC());
        }
    }

    private IEnumerator ResponseITC()
    {
        yield return new WaitForSeconds(0.03f);
        responses1.SetActive(false);
        PhionaTalk.Play();                                                                    //Play the sfx for Phiona talking
        foreshadowing.SetActive(true);


        textToSpeak = "Well, let's just say you'll be escorted out of the building.";   //Define the text that needs to be printed
        Dialogue.GetComponent<TMPro.TMP_Text>().text = textToSpeak;                           //Easily ref the TMPro component
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitUntil(() => textLength == currentTextLength);

        yield return new WaitForSeconds(4f);
        PhionaTalk.Stop();
        foreshadowing.SetActive(false);

        //Wait for a bit before fading out and loading next scene
        yield return new WaitForSeconds(0.03f);
        fadeScreenOut.SetActive(true);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tutorial Level");

        eventPos = 3;
    }
}
